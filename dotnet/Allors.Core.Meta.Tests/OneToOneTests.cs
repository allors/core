﻿namespace Allors.Core.Meta.Tests;

using System;
using System.Collections.Generic;
using System.Linq;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;
using FluentAssertions;
using MoreLinq;
using Xunit;

public class OneToOneTests
{
    private readonly Func<(
        MetaOneToOneAssociationType Association,
        MetaOneToOneRoleType Role,
        Func<Meta, IMetaObject>[] Builders,
        Func<Meta, IMetaObject> FromBuilder,
        Func<Meta, IMetaObject> FromAnotherBuilder,
        Func<Meta, IMetaObject> ToBuilder,
        Func<Meta, IMetaObject> ToAnotherBuilder)>[] fixtures;

    private readonly Action<Meta>[] preActs;

    public OneToOneTests()
    {
        this.Meta = new Domain.TestsMeta();

        this.fixtures =
        [
            () =>
            {
                // C1 <-> C1
                var association = this.Meta.C1WhereC1OneToOne;
                var role = this.Meta.C1C1OneToOne;

                return (association, role, [C1Builder], C1Builder, C1Builder, C1Builder, C1Builder);

                IMetaObject C1Builder(Meta transaction) => transaction.Build(this.Meta.C1);
            },
            () =>
            {
                // C1 <-> I1
                var association = this.Meta.C1WhereI1OneToOne;
                var role = this.Meta.C1I1OneToOne;

                return (association, role, [C1Builder], C1Builder, C1Builder, C1Builder, C1Builder);

                IMetaObject C1Builder(Meta transaction) => transaction.Build(this.Meta.C1);
            },
            () =>
            {
                // C1 <-> C2
                var association = this.Meta.C1WhereC2OneToOne;
                var role = this.Meta.C1C2OneToOne;

                return (association, role, [C1Builder, C2Builder], C1Builder, C1Builder, C2Builder, C2Builder);

                IMetaObject C1Builder(Meta transaction) => transaction.Build(this.Meta.C1);
                IMetaObject C2Builder(Meta transaction) => transaction.Build(this.Meta.C2);
            },
            () =>
            {
                // C1 <-> I2
                var association = this.Meta.C1WhereI2OneToOne;
                var role = this.Meta.C1I2OneToOne;

                return (association, role, [C1Builder, C2Builder], C1Builder, C1Builder, C2Builder, C2Builder);

                IMetaObject C1Builder(Meta transaction) => transaction.Build(this.Meta.C1);
                IMetaObject C2Builder(Meta transaction) => transaction.Build(this.Meta.C2);
            },
        ];

        this.preActs =
        [
            _ => { },
            v => v.Checkpoint(),
            v =>
            {
                v.Checkpoint();
                v.Checkpoint();
            }
        ];
    }

    public Domain.TestsMeta Meta { get; }

    [Fact]
    public void FromToInitial()
    {
        this.FromTo(
            () =>
            [
            ],
            () =>
            [
                (association, _, _, to) => to[association].Should().BeNull(),
                (_, role, from, _) => from[role].Should().BeNull()
            ]);
    }

    [Fact]
    public void FromToSet()
    {
        this.FromTo(
            () =>
            [
                (_, role, from, to) => from[role] = to
            ],
            () =>
            [
                (association, _, from, to) => to[association].Should().Be(from),
                (_, role, from, to) => from[role].Should().Be(to)
            ]);
    }

    [Fact]
    public void FromToSetReset()
    {
        this.FromTo(
            () =>
            [
                (_, role, from, to) => from[role] = to,
                (_, role, from, _) => from[role] = null
            ],
            () =>
            [
                (association, _, _, to) => to[association].Should().BeNull(),
                (_, role, from, _) => from[role].Should().BeNull()
            ]);
    }

    [Fact]
    public void FromToSetAndReset()
    {
        this.FromTo(
            () =>
            [
                (_, role, from, to) =>
                {
                    from[role] = to;
                    from[role] = null;
                },
            ],
            () =>
            [
                (association, _, _, to) => to[association].Should().BeNull(),
                (_, role, from, _) => from[role].Should().BeNull()
            ]);
    }

    [Fact]
    public void FromFromAnotherToInitial()
    {
        this.FromFromAnotherTo(
            () =>
            [
            ],
            () =>
            [
                (association, _, _, _, to) => to[association].Should().BeNull(),
                (_, role, from, _, _) => from[role].Should().BeNull(),
                (_, role, _, fromAnother, _) => fromAnother[role].Should().BeNull()
            ]);
    }

    [Fact]
    public void FromFromAnotherToSetSet()
    {
        this.FromFromAnotherTo(
            () =>
            [
                (_, role, from, _, to) => from[role] = to,
                (_, role, _, fromAnother, to) => fromAnother[role] = to
            ],
            () =>
            [
                (association, _, _, fromAnother, to) => fromAnother.Should().Be(to[association]),
                (_, role, from, _, _) => from[role].Should().BeNull(),
                (_, role, _, fromAnother, to) => fromAnother[role].Should().Be(to)
            ]);
    }

    [Fact]
    public void FromFromAnotherToSetAndSet()
    {
        this.FromFromAnotherTo(
            () =>
            [
                (_, role, from, fromAnother, to) =>
                {
                    from[role] = to;
                    fromAnother[role] = to;
                },
            ],
            () =>
            [
                (association, _, _, fromAnother, to) => fromAnother.Should().Be(to[association]),
                (_, role, from, _, _) => from[role].Should().BeNull(),
                (_, role, _, fromAnother, to) => to.Should().Be(fromAnother[role])
            ]);
    }

    [Fact]
    public void FromFromAnotherToSetSetReset()
    {
        this.FromFromAnotherTo(
            () =>
            [
                (_, role, from, _, to) => from[role] = to,
                (_, role, _, fromAnother, to) => fromAnother[role] = to,
                (_, role, _, fromAnother, _) => fromAnother[role] = null
            ],
            () =>
            [
                (association, _, _, _, to) => to[association].Should().BeNull(),
                (_, role, from, _, _) => from[role].Should().BeNull(),
                (_, role, _, fromAnother, _) => fromAnother[role].Should().BeNull()
            ]);
    }

    [Fact]
    public void FromFromAnotherToSetSetAndReset()
    {
        this.FromFromAnotherTo(
            () =>
            [
                (_, role, from, fromAnother, to) =>
                {
                    from[role] = to;
                    fromAnother[role] = to;
                    fromAnother[role] = null;
                }
            ],
            () =>
            [
                (association, _, _, _, to) => to[association].Should().BeNull(),
                (_, role, from, _, _) => from[role].Should().BeNull(),
                (_, role, _, fromAnother, _) => fromAnother[role].Should().BeNull()
            ]);
    }

    [Fact]
    public void FromToToAnotherInitial()
    {
        this.FromToToAnother(
            () =>
            [
            ],
            () =>
            [
                (association, _, _, to, _) => to[association].Should().BeNull(),
                (association, _, _, _, toAnother) => toAnother[association].Should().BeNull(),
                (_, role, from, _, _) => from[role].Should().BeNull(),
            ]);
    }

    [Fact]
    public void FromToToAnotherSetSet()
    {
        this.FromToToAnother(
            () =>
            [
                (_, role, from, to, _) => from[role] = to,
                (_, role, from, _, toAnother) => from[role] = toAnother
            ],
            () =>
            [
                (association, _, _, to, _) => to[association].Should().BeNull(),
                (association, _, from, _, toAnother) => from.Should().Be(toAnother[association]),
                (_, role, from, _, toAnother) => toAnother.Should().Be(from[role])
            ]);
    }

    [Fact]
    public void FromToToAnotherSetAndSet()
    {
        this.FromToToAnother(
            () =>
            [
                (_, role, from, to, toAnother) =>
                {
                    from[role] = to;
                    from[role] = toAnother;
                },
            ],
            () =>
            [
                (association, _, _, to, _) => to[association].Should().BeNull(),
                (association, _, from, _, toAnother) => from.Should().Be(toAnother[association]),
                (_, role, from, _, toAnother) => toAnother.Should().Be(from[role])
            ]);
    }

    [Fact]
    public void FromToToAnotherSetSetReset()
    {
        this.FromToToAnother(
            () =>
            [
                (_, role, from, to, _) => from[role] = to,
                (_, role, from, _, toAnother) => from[role] = toAnother,
                (_, role, from, _, _) => from[role] = null
            ],
            () =>
            [
                (association, _, _, to, _) => to[association].Should().BeNull(),
                (association, _, _, _, toAnother) => toAnother[association].Should().BeNull(),
                (_, role, from, _, _) => from[role].Should().BeNull(),
            ]);
    }

    [Fact]
    public void FromToToAnotherSetSetAndReset()
    {
        this.FromToToAnother(
            () =>
            [
                (_, role, from, to, toAnother) =>
                {
                    from[role] = to;
                    from[role] = toAnother;
                    from[role] = null;
                }
            ],
            () =>
            [
                (association, _, _, to, _) => to[association].Should().BeNull(),
                (association, _, _, _, toAnother) => toAnother[association].Should().BeNull(),
                (_, role, from, _, _) => from[role].Should().BeNull(),
            ]);
    }

    [Fact]
    public void FromFromAnotherToToAnotherInitial()
    {
        this.FromFromAnotherToToAnother(
            () =>
            [
            ],
            () =>
            [
                (association, _, _, _, to, _) => to[association].Should().BeNull(),
                (association, _, _, _, _, toAnother) => toAnother[association].Should().BeNull(),
                (_, role, from, _, _, _) => from[role].Should().BeNull(),
                (_, role, _, fromAnother, _, _) => fromAnother[role].Should().BeNull(),
            ]);
    }

    [Fact]
    public void FromFromAnotherToToAnotherSetSetSet()
    {
        this.FromFromAnotherToToAnother(
            () =>
            [
                (_, role, from, _, to, _) => from[role] = to,
                (_, role, _, fromAnother, _, toAnother) => fromAnother[role] = toAnother,
                (_, role, from, _, _, toAnother) => from[role] = toAnother,
            ],
            () =>
            [
                (association, _, _, _, to, _) => to[association].Should().BeNull(),
                (association, _, from, _, _, toAnother) => from.Should().Be(toAnother[association]),
                (_, role, from, _, _, toAnother) => toAnother.Should().Be(from[role]),
                (_, role, _, fromAnother, _, _) => fromAnother[role].Should().BeNull(),
            ]);
    }

    [Fact]
    public void FromFromAnotherToToAnotherSetAndSetAndSet()
    {
        this.FromFromAnotherToToAnother(
            () =>
            [
                (_, role, from, fromAnother, to, toAnother) =>
                {
                    from[role] = to;
                    fromAnother[role] = toAnother;
                    from[role] = toAnother;
                },
            ],
            () =>
            [
                (association, _, _, _, to, _) => to[association].Should().BeNull(),
                (association, _, from, _, _, toAnother) => from.Should().Be(toAnother[association]),
                (_, role, from, _, _, toAnother) => toAnother.Should().Be(from[role]),
                (_, role, _, fromAnother, _, _) => fromAnother[role].Should().BeNull(),
            ]);
    }

    [Fact]
    public void FromFromAnotherToToAnotherSetSetSetReset()
    {
        this.FromFromAnotherToToAnother(
            () =>
            [
                (_, role, from, _, to, _) => from[role] = to,
                (_, role, _, fromAnother, _, toAnother) => fromAnother[role] = toAnother,
                (_, role, from, _, _, toAnother) => from[role] = toAnother,
                (_, role, from, _, _, _) => from[role] = null
            ],
            () =>
            [
                (association, _, _, _, to, _) => to[association].Should().BeNull(),
                (association, _, _, _, _, toAnother) => toAnother[association].Should().BeNull(),
                (_, role, from, _, _, _) => from[role].Should().BeNull(),
                (_, role, _, fromAnother, _, _) => fromAnother[role].Should().BeNull(),
            ]);
    }

    [Fact]
    public void FromFromAnotherToToAnotherSetAndSetAndSetAndReset()
    {
        this.FromFromAnotherToToAnother(
            () =>
            [
                (_, role, from, fromAnother, to, toAnother) =>
                {
                    from[role] = to;
                    fromAnother[role] = toAnother;
                    from[role] = toAnother;
                    from[role] = null;
                }
            ],
            () =>
            [
                (association, _, _, _, to, _) => to[association].Should().BeNull(),
                (association, _, _, _, _, toAnother) => toAnother[association].Should().BeNull(),
                (_, role, from, _, _, _) => from[role].Should().BeNull(),
                (_, role, _, fromAnother, _, _) => fromAnother[role].Should().BeNull(),
            ]);
    }

    [Fact]
    public void BeginMiddleEndSet()
    {
        foreach (var fixture in this.fixtures)
        {
            var meta = this.CreateMeta();

            var (association, role, builders, _, _, _, _) =
                fixture();

            if (builders.Length == 1)
            {
                var builder = builders[0];

                // Begin - Middle - End
                var begin = builder(meta);
                var middle = builder(meta);
                var end = builder(meta);

                begin[role] = middle;
                middle[role] = end;
                begin[role] = end;

                middle[association].Should().BeNull();
                end[association].Should().Be(begin);
                begin[association].Should().BeNull();

                begin[role].Should().Be(end);
                middle[role].Should().BeNull();
                end[role].Should().BeNull();
            }
        }
    }

    [Fact]
    public void BeginMiddleEndRingSet()
    {
        foreach (var fixture in this.fixtures)
        {
            var meta = this.CreateMeta();

            var (association, role, builders, _, _, _, _) = fixture();

            if (builders.Length == 1)
            {
                var builder = builders[0];

                // Begin - Middle - End
                var begin = builder(meta);
                var middle = builder(meta);
                var end = builder(meta);

                begin[role] = middle;
                middle[role] = end;
                end[role] = begin;

                middle[association].Should().Be(begin);
                end[association].Should().Be(middle);
                begin[association].Should().Be(end);

                begin[role].Should().Be(middle);
                middle[role].Should().Be(end);
                end[role].Should().Be(begin);
            }
        }
    }

    private Meta CreateMeta()
    {
        return new Meta(this.Meta.MetaMeta);
    }

    private void FromTo(
        Func<IEnumerable<Action<MetaOneToOneAssociationType, MetaOneToOneRoleType, IMetaObject, IMetaObject>>> acts,
        Func<IEnumerable<Action<MetaOneToOneAssociationType, MetaOneToOneRoleType, IMetaObject, IMetaObject>>> asserts)
    {
        var assertPermutations = asserts().Permutations().ToArray();

        foreach (var preact in this.preActs)
        {
            foreach (var actRepeats in new[] { 1, 2 })
            {
                foreach (var assertPermutation in assertPermutations)
                {
                    foreach (var assertRepeats in new[] { 1, 2 })
                    {
                        foreach (var fixture in this.fixtures)
                        {
                            var (association, role, _, fromBuilder, _, toBuilder, _) = fixture();

                            var meta = this.CreateMeta();

                            var from = fromBuilder(meta);
                            var to = toBuilder(meta);

                            foreach (var act in acts())
                            {
                                for (var actRepeat = 0; actRepeat < actRepeats; actRepeat++)
                                {
                                    preact(meta);
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
        Func<IEnumerable<Action<MetaOneToOneAssociationType, MetaOneToOneRoleType, IMetaObject, IMetaObject, IMetaObject>>> acts,
        Func<IEnumerable<Action<MetaOneToOneAssociationType, MetaOneToOneRoleType, IMetaObject, IMetaObject, IMetaObject>>> asserts)
    {
        var assertPermutations = asserts().Permutations().ToArray();

        foreach (var preact in this.preActs)
        {
            foreach (var snapshot in new bool[] { false, true })
            {
                foreach (var actRepeats in new[] { 1, 2 })
                {
                    foreach (var assertPermutation in assertPermutations)
                    {
                        foreach (var assertRepeats in new[] { 1, 2 })
                        {
                            foreach (var fixture in this.fixtures)
                            {
                                var (association, role, _, fromBuilder, fromAnotherBuilder, toBuilder, _) =
                                    fixture();

                                var meta = this.CreateMeta();

                                var from = fromBuilder(meta);
                                var fromAnother = fromAnotherBuilder(meta);
                                var to = toBuilder(meta);

                                foreach (var act in acts())
                                {
                                    for (var actRepeat = 0; actRepeat < actRepeats; actRepeat++)
                                    {
                                        preact(meta);
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
    }

    private void FromToToAnother(
        Func<IEnumerable<Action<MetaOneToOneAssociationType, MetaOneToOneRoleType, IMetaObject, IMetaObject, IMetaObject>>> acts,
        Func<IEnumerable<Action<MetaOneToOneAssociationType, MetaOneToOneRoleType, IMetaObject, IMetaObject, IMetaObject>>> asserts)
    {
        var assertPermutations = asserts().Permutations().ToArray();

        foreach (var preact in this.preActs)
        {
            foreach (var snapshot in new bool[] { false, true })
            {
                foreach (var actRepeats in new[] { 1, 2 })
                {
                    foreach (var assertPermutation in assertPermutations)
                    {
                        foreach (var assertRepeats in new[] { 1, 2 })
                        {
                            foreach (var fixture in this.fixtures)
                            {
                                var (association, role, _, fromBuilder, _, toBuilder, toAnotherBuilder) = fixture();

                                var meta = this.CreateMeta();

                                var from = fromBuilder(meta);
                                var to = toBuilder(meta);
                                var toAnother = toAnotherBuilder(meta);

                                foreach (var act in acts())
                                {
                                    for (var actRepeat = 0; actRepeat < actRepeats; actRepeat++)
                                    {
                                        preact(meta);
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
    }

    private void FromFromAnotherToToAnother(
        Func<IEnumerable<Action<MetaOneToOneAssociationType, MetaOneToOneRoleType, IMetaObject, IMetaObject, IMetaObject, IMetaObject>>> acts,
        Func<IEnumerable<Action<MetaOneToOneAssociationType, MetaOneToOneRoleType, IMetaObject, IMetaObject, IMetaObject, IMetaObject>>> asserts)
    {
        var assertPermutations = asserts().Permutations().ToArray();

        foreach (var preact in this.preActs)
        {
            foreach (var snapshot in new bool[] { false, true })
            {
                foreach (var actRepeats in new[] { 1, 2 })
                {
                    foreach (var assertPermutation in assertPermutations)
                    {
                        foreach (var assertRepeats in new[] { 1, 2 })
                        {
                            foreach (var fixture in this.fixtures)
                            {
                                var (association, role, _, fromBuilder, fromAnotherBuilder, toBuilder,
                                    toAnotherBuilder) = fixture();

                                var meta = this.CreateMeta();

                                var from = fromBuilder(meta);
                                var fromAnother = fromAnotherBuilder(meta);
                                var to = toBuilder(meta);
                                var toAnother = toAnotherBuilder(meta);

                                foreach (var act in acts())
                                {
                                    for (var actRepeat = 0; actRepeat < actRepeats; actRepeat++)
                                    {
                                        preact(meta);
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
    }
}
