namespace Allors.Core.Database.Engines.Tests;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Allors.Core.Database.Engines.Tests.Meta;
using Allors.Core.Database.Meta;
using FluentAssertions;
using MoreLinq;
using Xunit;

public abstract class ManyToManyTests : Tests
{
    private readonly Func<(
        ManyToManyAssociationType Association,
        ManyToManyRoleType Role,
        Func<ITransaction, IObject>[] Builders,
        Func<ITransaction, IObject> FromBuilder,
        Func<ITransaction, IObject> FromAnotherBuilder,
        Func<ITransaction, IObject> From3Builder,
        Func<ITransaction, IObject> From4Builder,
        Func<ITransaction, IObject> ToBuilder,
        Func<ITransaction, IObject> ToAnotherBuilder,
        Func<ITransaction, IObject> To3Builder,
        Func<ITransaction, IObject> To4Builder)>[] fixtures;

    private readonly (string, Action<ITransaction>)[] preActs;

    protected ManyToManyTests()
    {
        this.fixtures =
        [
            () =>
            {
                // C1 <-> C1
                Debugger.Log(0, null, $"C1 <-> C1\n");
                var association = this.Meta.C1sWhereC1ManyToMany();
                var role = this.Meta.C1C1ManyToMany();

                return (association, role, [C1Builder], C1Builder, C1Builder, C1Builder, C1Builder, C1Builder, C1Builder, C1Builder, C1Builder);

                IObject C1Builder(ITransaction transaction) => transaction.Build(this.Meta.C1);
            },
        ];

        this.preActs =
        [
            ("Nothing", _ => { }),
            ("Checkpoint", v => v.Checkpoint()),
            ("Checkpoint Checkpoint", v =>
            {
                v.Checkpoint();
                v.Checkpoint();
            }),
            ("Commit", v => v.Commit()),
            ("Commit Commit", v =>
            {
                v.Commit();
                v.Commit();
            }),
            ("Checkpoint Commit", v =>
            {
                v.Checkpoint();
                v.Commit();
            }),
            ("Commit Checkpoint", v =>
            {
                v.Commit();
                v.Checkpoint();
            }),
        ];
    }

    [Fact]
    public void FromToInitial()
    {
        this.FromTo(
            () =>
            [
            ],
            () =>
            [
                (association, _, _, to) => to[association].Should().BeEmpty(),
                (_, role, from, _) => from[role].Should().BeEmpty()
            ]);
    }

    [Fact]
    public void FromToAdd()
    {
        this.FromTo(
            () =>
            [
                (_, role, from, to) => from.Add(role, to)
            ],
            () =>
            [
                (association, _, from, to) => to[association].Should().BeEquivalentTo([from]),
                (_, role, from, to) => from[role].Should().BeEquivalentTo([to])
            ]);
    }

    [Fact]
    public void FromToSet()
    {
        this.FromTo(
            () =>
            [
                (_, role, from, to) => from[role] = [to]
            ],
            () =>
            [
                (association, _, from, to) => to[association].Should().BeEquivalentTo([from]),
                (_, role, from, to) => from[role].Should().BeEquivalentTo([to])
            ]);
    }

    [Fact]
    public void FromToSetReset()
    {
        this.FromTo(
            () =>
            [
                (_, role, from, to) => from[role] = [to],
                (_, role, from, to) => from[role] = []
            ],
            () =>
            [
                (association, _, _, to) => to[association].Should().BeEmpty(),
                (_, role, from, _) => from[role].Should().BeEmpty()
            ]);
    }

    [Fact]
    public void FromFromAnotherToSet()
    {
        this.FromFromAnotherTo(
            () =>
            [
                (_, role, from, _, to) => from[role] = [to],
                (_, role, _, fromAnother, to) => fromAnother[role] = [to]
            ],
            () =>
            [
                (association, _, from, fromAnother, to) => to[association].Should().BeEquivalentTo([from, fromAnother]),
                (_, role, from, _, to) => from[role].Should().BeEquivalentTo([to]),
                (_, role, _, fromAnother, to) => fromAnother[role].Should().BeEquivalentTo([to])
            ]);
    }

    [Fact]
    public void FromToToAnotherSet()
    {
        this.FromToToAnother(
            () =>
            [
                (_, role, from, to, toAnother) => from[role] = [to, toAnother]
            ],
            () =>
            [
                (association, _, from, to, toAnother) => to[association].Should().BeEquivalentTo([from]),
                (association, _, from, to, toAnother) => toAnother[association].Should().BeEquivalentTo([from]),
                (_, role, from, to, toAnother) => from[role].Should().BeEquivalentTo([to, toAnother]),
            ]);
    }

    [Fact]
    public void CheckRoleIsAssignable()
    {
        foreach (var (_, preact) in this.preActs)
        {
            var database = this.CreateDatabase();
            var transaction = database.CreateTransaction();

            var m = this.Meta;

            var c1a = transaction.Build(m.C1);
            var c1b = transaction.Build(m.C1);

            var c2a = transaction.Build(m.C2);

            // Illegal Role
            preact(transaction);

            c1a.Invoking(v => v.Add(m.C1C2ManyToMany, c1b))
                .Should().Throw<ArgumentException>();

            preact(transaction);

            c1a.Invoking(v => v.Remove(m.C1C2ManyToMany, c1b))
                .Should().Throw<ArgumentException>();

            preact(transaction);

            c1a.Invoking(v => v[m.C1C2ManyToMany] = [c1b])
                .Should().Throw<ArgumentException>();

            preact(transaction);

            c1a.Invoking(v => v.Add(m.C1I2ManyToMany, c1b))
                .Should().Throw<ArgumentException>();

            preact(transaction);

            c1a.Invoking(v => v.Remove(m.C1I2ManyToMany, c1b))
                .Should().Throw<ArgumentException>();

            preact(transaction);

            c1a.Invoking(v => v[m.C1I2ManyToMany] = [c1b])
                .Should().Throw<ArgumentException>();

            preact(transaction);

            c1a.Invoking(v => v.Add(m.C2C1ManyToMany, c2a))
                .Should().Throw<ArgumentException>();

            preact(transaction);

            c1a.Invoking(v => v.Remove(m.C2C1ManyToMany, c2a))
                .Should().Throw<ArgumentException>();

            preact(transaction);

            c1a.Invoking(v => v[m.C2C1ManyToMany] = [c2a])
                .Should().Throw<ArgumentException>();
        }
    }

    protected abstract IDatabase CreateDatabase();

    private void FromTo(
        Func<IEnumerable<Action<ManyToManyAssociationType, ManyToManyRoleType, IObject, IObject>>> acts,
        Func<IEnumerable<Action<ManyToManyAssociationType, ManyToManyRoleType, IObject, IObject>>> asserts)
    {
        var assertPermutations = asserts().Permutations().ToArray();

        foreach (var (preactName, preact) in this.preActs)
        {
            Debugger.Log(0, null, $"Preact {preactName}\n");
            foreach (var actRepeats in new[] { 1, 2 })
            {
                Debugger.Log(0, null, $"Act Repeats {actRepeats}\n");
                foreach (var assertPermutation in assertPermutations)
                {
                    foreach (var assertRepeats in new[] { 1, 2 })
                    {
                        Debugger.Log(0, null, $"Assert Repeats {assertRepeats}\n");
                        foreach (var fixture in this.fixtures)
                        {
                            var (association, role, _, fromBuilder, _, _, _, toBuilder, _, _, _) = fixture();

                            var database = this.CreateDatabase();
                            var transaction = database.CreateTransaction();

                            var from = fromBuilder(transaction);
                            var to = toBuilder(transaction);

                            foreach (var act in acts())
                            {
                                for (var actRepeat = 0; actRepeat < actRepeats; actRepeat++)
                                {
                                    preact(transaction);
                                    act(association, role, from, to);
                                }
                            }

                            foreach (var assert in assertPermutation)
                            {
                                for (var assertRepeat = 0; assertRepeat < assertRepeats; assertRepeat++)
                                {
                                    assert(association, role, from, to);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void FromFromAnotherTo(
        Func<IEnumerable<Action<ManyToManyAssociationType, ManyToManyRoleType, IObject, IObject, IObject>>> acts,
        Func<IEnumerable<Action<ManyToManyAssociationType, ManyToManyRoleType, IObject, IObject, IObject>>> asserts)
    {
        var assertPermutations = asserts().Permutations().ToArray();

        foreach (var (preactName, preact) in this.preActs)
        {
            Debugger.Log(0, null, $"Preact {preactName}\n");
            foreach (var actRepeats in new[] { 1, 2 })
            {
                Debugger.Log(0, null, $"Act Repeats {actRepeats}\n");

                foreach (var assertPermutation in assertPermutations)
                {
                    foreach (var assertRepeats in new[] { 1, 2 })
                    {
                        Debugger.Log(0, null, $"Assert Repeats {assertRepeats}\n");
                        foreach (var fixture in this.fixtures)
                        {
                            var (association, role, _, fromBuilder, fromAnotherBuilder, _, _, toBuilder, _, _, _) = fixture();

                            var database = this.CreateDatabase();
                            var transaction = database.CreateTransaction();

                            var from = fromBuilder(transaction);
                            var fromAnother = fromAnotherBuilder(transaction);
                            var to = toBuilder(transaction);

                            foreach (var act in acts())
                            {
                                for (var actRepeat = 0; actRepeat < actRepeats; actRepeat++)
                                {
                                    preact(transaction);
                                    act(association, role, from, fromAnother, to);
                                }
                            }

                            foreach (var assert in assertPermutation)
                            {
                                for (var assertRepeat = 0; assertRepeat < assertRepeats; assertRepeat++)
                                {
                                    assert(association, role, from, fromAnother, to);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void FromToToAnother(
        Func<IEnumerable<Action<ManyToManyAssociationType, ManyToManyRoleType, IObject, IObject, IObject>>> acts,
        Func<IEnumerable<Action<ManyToManyAssociationType, ManyToManyRoleType, IObject, IObject, IObject>>> asserts)
    {
        var assertPermutations = asserts().Permutations().ToArray();

        foreach (var (preactName, preact) in this.preActs)
        {
            Debugger.Log(0, null, $"Preact {preactName}\n");
            foreach (var actRepeats in new[] { 1, 2 })
            {
                Debugger.Log(0, null, $"Act Repeats {actRepeats}\n");
                foreach (var assertPermutation in assertPermutations)
                {
                    foreach (var assertRepeats in new[] { 1, 2 })
                    {
                        Debugger.Log(0, null, $"Assert Repeats {assertRepeats}\n");
                        foreach (var fixture in this.fixtures)
                        {
                            var (association, role, _, fromBuilder, _, _, _, toBuilder, toAnotherBuilder, _, _) = fixture();

                            var database = this.CreateDatabase();
                            var transaction = database.CreateTransaction();

                            var from = fromBuilder(transaction);
                            var to = toBuilder(transaction);
                            var toAnother = toAnotherBuilder(transaction);

                            foreach (var act in acts())
                            {
                                for (var actRepeat = 0; actRepeat < actRepeats; actRepeat++)
                                {
                                    preact(transaction);
                                    act(association, role, from, to, toAnother);
                                }
                            }

                            foreach (var assert in assertPermutation)
                            {
                                for (var assertRepeat = 0; assertRepeat < assertRepeats; assertRepeat++)
                                {
                                    assert(association, role, from, to, toAnother);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

#pragma warning disable S1144 // Unused private types or members should be removed
    private void FromFromAnotherToToAnother(
        Func<IEnumerable<Action<ManyToManyAssociationType, ManyToManyRoleType, IObject, IObject, IObject, IObject>>> acts,
        Func<IEnumerable<Action<ManyToManyAssociationType, ManyToManyRoleType, IObject, IObject, IObject, IObject>>> asserts)
    {
        var assertPermutations = asserts().Permutations().ToArray();

        foreach (var (preactName, preact) in this.preActs)
        {
            Debugger.Log(0, null, $"Preact {preactName}\n");
            foreach (var actRepeats in new[] { 1, 2 })
            {
                Debugger.Log(0, null, $"Act Repeats {actRepeats}\n");

                foreach (var assertPermutation in assertPermutations)
                {
                    foreach (var assertRepeats in new[] { 1, 2 })
                    {
                        Debugger.Log(0, null, $"Assert Repeats {assertRepeats}\n");
                        foreach (var fixture in this.fixtures)
                        {
                            var (association, role, _, fromBuilder, fromAnotherBuilder, _, _, toBuilder, toAnotherBuilder, _, _) = fixture();

                            var database = this.CreateDatabase();
                            var transaction = database.CreateTransaction();

                            var from = fromBuilder(transaction);
                            var fromAnother = fromAnotherBuilder(transaction);
                            var to = toBuilder(transaction);
                            var toAnother = toAnotherBuilder(transaction);

                            foreach (var act in acts())
                            {
                                for (var actRepeat = 0; actRepeat < actRepeats; actRepeat++)
                                {
                                    preact(transaction);
                                    act(association, role, from, fromAnother, to, toAnother);
                                }
                            }

                            foreach (var assert in assertPermutation)
                            {
                                for (var assertRepeat = 0; assertRepeat < assertRepeats; assertRepeat++)
                                {
                                    assert(association, role, from, fromAnother, to, toAnother);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
#pragma warning restore S1144 // Unused private types or members should be removed
}
