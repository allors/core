namespace Allors.Core.Database.Adapters.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Allors.Core.Database.Meta.Handles;
    using MoreLinq;
    using Xunit;

    public abstract class OneToOneTests : Tests
    {
        private readonly Func<(
            OneToOneAssociationTypeHandle Association,
            OneToOneRoleTypeHandle Role,
            Func<ITransaction, IObject>[] Builders,
            Func<ITransaction, IObject> FromBuilder,
            Func<ITransaction, IObject> FromAnotherBuilder,
            Func<ITransaction, IObject> ToBuilder,
            Func<ITransaction, IObject> ToAnotherBuilder)>[] fixtures;

        protected OneToOneTests()
        {
            this.fixtures =
            [
                () =>
                {
                    var association = this.Meta.C1WhereC1OneToOne;
                    var role = this.Meta.C1C1OneToOne;

                    return (association, role, [Builder], Builder, Builder, Builder, Builder);

                    IObject Builder(ITransaction transaction) => transaction.Build(this.Meta.C1);
                }
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
                    (association, _, _, to) => Assert.Null(to[association]),
                    (_, role, from, _) => Assert.Null(from[role])
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
                    (association, _, from, to) => Assert.Equal(from, to[association]),
                    (_, role, from, to) => Assert.Equal(to, from[role])
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
                    (association, _, _, to) => Assert.Null(to[association]),
                    (_, role, from, _) => Assert.Null(from[role])
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
                    (association, _, _, to) => Assert.Null(to[association]),
                    (_, role, from, _) => Assert.Null(from[role])
                ]);
        }

        [Fact]
        public void FromToInitialWithExist()
        {
            this.FromTo(
                () =>
                [
                ],
                () =>
                [
                    (association, _, _, to) => Assert.False(to.Exist(association)),
                    (_, role, from, _) => Assert.False(from.Exist(role))
                ]);
        }

        [Fact]
        public void FromToSetWithExist()
        {
            this.FromTo(
                () =>
                [
                    (_, role, from, to) => from[role] = to
                ],
                () =>
                [
                    (association, _, _, to) => Assert.True(to.Exist(association)),
                    (_, role, from, _) => Assert.True(from.Exist(role))
                ]);
        }

        [Fact]
        public void FromToSetResetWithExist()
        {
            this.FromTo(
                () =>
                [
                    (_, role, from, to) => from[role] = to,
                    (_, role, from, _) => from[role] = null
                ],
                () =>
                [
                    (association, _, _, to) => Assert.False(to.Exist(association)),
                    (_, role, from, _) => Assert.False(from.Exist(role))
                ]);
        }

        [Fact]
        public void FromToSetAndResetWithExist()
        {
            this.FromTo(
                () =>
                [
                    (_, role, from, to) =>
                    {
                        from[role] = to;
                        from[role] = null;
                    }
                ],
                () =>
                [
                    (association, _, _, to) => Assert.False(to.Exist(association)),
                    (_, role, from, _) => Assert.False(from.Exist(role))
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
                    (association, _, _, _, to) => Assert.Null(to[association]),
                    (_, role, from, _, _) => Assert.Null(from[role]),
                    (_, role, _, fromAnother, _) => Assert.Null(fromAnother[role])
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
                    (association, _, _, to, _) => Assert.Null(to[association]),
                    (association, _, _, _, toAnother) => Assert.Null(toAnother[association]),
                    (_, role, from, _, _) => Assert.Null(from[role]),
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
                    (association, _, _, _, to, _) => Assert.Null(to[association]),
                    (association, _, _, _, _, toAnother) => Assert.Null(toAnother[association]),
                    (_, role, from, _, _, _) => Assert.Null(from[role]),
                    (_, role, _, fromAnother, _, _) => Assert.Null(fromAnother[role]),
                ]);
        }

        [Fact]
        public void C1_C1OneToOne()
        {
            foreach (var fixture in this.fixtures)
            {
                var database = this.CreateDatabase();
                var transaction = database.CreateTransaction();

                var (association, role, builders, fromBuilder, fromAnotherBuilder, toBuilder, toAnotherBuilder) =
                    fixture();

                var from = fromBuilder(transaction);
                var fromAnother = fromAnotherBuilder(transaction);
                var to = toBuilder(transaction);
                var toAnother = toAnotherBuilder(transaction);

                // Same New / Different To
                // Get
                Assert.Null(to[association]);
                Assert.Null(to[association]);
                Assert.Null(from[role]);
                Assert.Null(from[role]);
                Assert.Null(toAnother[association]);
                Assert.Null(toAnother[association]);

                from[role] = to;
                from[role] = to;

                Assert.Equal(from, to[association]);
                Assert.Equal(from, to[association]);
                Assert.Equal(to, from[role]);
                Assert.Equal(to, from[role]);
                Assert.Null(toAnother[association]);
                Assert.Null(toAnother[association]);

                from[role] = toAnother;
                from[role] = toAnother;

                Assert.Null(to[association]);
                Assert.Null(to[association]);
                Assert.Equal(toAnother, from[role]);
                Assert.Equal(toAnother, from[role]);
                Assert.Equal(from, toAnother[association]);
                Assert.Equal(from, toAnother[association]);

                from[role] = null;
                from[role] = null;

                Assert.Null(to[association]);
                Assert.Null(to[association]);
                Assert.Null(from[role]);
                Assert.Null(from[role]);
                Assert.Null(toAnother[association]);
                Assert.Null(toAnother[association]);

                // Exist
                Assert.False(to.Exist(association));
                Assert.False(to.Exist(association));
                Assert.False(from.Exist(role));
                Assert.False(from.Exist(role));
                Assert.False(toAnother.Exist(association));
                Assert.False(toAnother.Exist(association));

                from[role] = to;
                from[role] = to;

                Assert.True(to.Exist(association));
                Assert.True(to.Exist(association));
                Assert.True(from.Exist(role));
                Assert.True(from.Exist(role));
                Assert.False(toAnother.Exist(association));
                Assert.False(toAnother.Exist(association));

                from[role] = toAnother;
                from[role] = toAnother;

                Assert.False(to.Exist(association));
                Assert.False(to.Exist(association));
                Assert.True(from.Exist(role));
                Assert.True(from.Exist(role));
                Assert.True(toAnother.Exist(association));
                Assert.True(toAnother.Exist(association));

                from[role] = null;
                from[role] = null;

                Assert.False(to.Exist(association));
                Assert.False(to.Exist(association));
                Assert.False(from.Exist(role));
                Assert.False(from.Exist(role));
                Assert.False(toAnother.Exist(association));
                Assert.False(toAnother.Exist(association));

                // Different New / Different To
                // Get
                Assert.Null(from[role]);
                Assert.Null(from[role]);
                Assert.Null(to[association]);
                Assert.Null(to[association]);
                Assert.Null(fromAnother[role]);
                Assert.Null(fromAnother[role]);
                Assert.Null(toAnother[association]);
                Assert.Null(toAnother[association]);

                from[role] = to;
                from[role] = to;

                Assert.Equal(to, from[role]);
                Assert.Equal(to, from[role]);
                Assert.Equal(from, to[association]);
                Assert.Equal(from, to[association]);
                Assert.Null(fromAnother[role]);
                Assert.Null(fromAnother[role]);
                Assert.Null(toAnother[association]);
                Assert.Null(toAnother[association]);

                fromAnother[role] = toAnother;

                Assert.Equal(to, from[role]);
                Assert.Equal(to, from[role]);
                Assert.Equal(from, to[association]);
                Assert.Equal(from, to[association]);
                Assert.Equal(toAnother, fromAnother[role]);
                Assert.Equal(toAnother, fromAnother[role]);
                Assert.Equal(fromAnother, toAnother[association]);
                Assert.Equal(fromAnother, toAnother[association]);

                from[role] = null;
                from[role] = null;

                Assert.Null(from[role]);
                Assert.Null(from[role]);
                Assert.Null(to[association]);
                Assert.Null(to[association]);
                Assert.Equal(toAnother, fromAnother[role]);
                Assert.Equal(toAnother, fromAnother[role]);
                Assert.Equal(fromAnother, toAnother[association]);
                Assert.Equal(fromAnother, toAnother[association]);

                fromAnother[role] = null;
                fromAnother[role] = null;

                Assert.Null(from[role]);
                Assert.Null(from[role]);
                Assert.Null(to[association]);
                Assert.Null(to[association]);
                Assert.Null(fromAnother[role]);
                Assert.Null(fromAnother[role]);
                Assert.Null(toAnother[association]);
                Assert.Null(toAnother[association]);

                // Exist
                Assert.False(from.Exist(role));
                Assert.False(from.Exist(role));
                Assert.False(to.Exist(association));
                Assert.False(to.Exist(association));
                Assert.False(fromAnother.Exist(role));
                Assert.False(fromAnother.Exist(role));
                Assert.False(toAnother.Exist(association));
                Assert.False(toAnother.Exist(association));

                from[role] = to;
                from[role] = to;

                Assert.True(from.Exist(role));
                Assert.True(from.Exist(role));
                Assert.True(to.Exist(association));
                Assert.True(to.Exist(association));
                Assert.False(fromAnother.Exist(role));
                Assert.False(fromAnother.Exist(role));
                Assert.False(toAnother.Exist(association));
                Assert.False(toAnother.Exist(association));

                fromAnother[role] = toAnother;
                fromAnother[role] = toAnother;

                Assert.True(from.Exist(role));
                Assert.True(from.Exist(role));
                Assert.True(to.Exist(association));
                Assert.True(to.Exist(association));
                Assert.True(fromAnother.Exist(role));
                Assert.True(fromAnother.Exist(role));
                Assert.True(toAnother.Exist(association));
                Assert.True(toAnother.Exist(association));

                from[role] = null;
                from[role] = null;

                Assert.False(from.Exist(role));
                Assert.False(from.Exist(role));
                Assert.False(to.Exist(association));
                Assert.False(to.Exist(association));
                Assert.True(fromAnother.Exist(role));
                Assert.True(fromAnother.Exist(role));
                Assert.True(toAnother.Exist(association));
                Assert.True(toAnother.Exist(association));

                fromAnother[role] = null;
                fromAnother[role] = null;

                Assert.False(from.Exist(role));
                Assert.False(from.Exist(role));
                Assert.False(to.Exist(association));
                Assert.False(to.Exist(association));
                Assert.False(fromAnother.Exist(role));
                Assert.False(fromAnother.Exist(role));
                Assert.False(toAnother.Exist(association));
                Assert.False(toAnother.Exist(association));

                // Different New / Same To
                // Get
                Assert.Null(from[role]);
                Assert.Null(from[role]);
                Assert.Null(fromAnother[role]);
                Assert.Null(fromAnother[role]);
                Assert.Null(to[association]);
                Assert.Null(to[association]);

                from[role] = to;
                from[role] = to;

                Assert.Equal(to, from[role]);
                Assert.Equal(to, from[role]);
                Assert.Null(fromAnother[role]);
                Assert.Null(fromAnother[role]);
                Assert.Equal(from, to[association]);
                Assert.Equal(from, to[association]);

                fromAnother[role] = to;
                fromAnother[role] = to;

                Assert.Null(from[role]);
                Assert.Null(from[role]);
                Assert.Equal(to, fromAnother[role]);
                Assert.Equal(to, fromAnother[role]);
                Assert.Equal(fromAnother, to[association]);
                Assert.Equal(fromAnother, to[association]);

                fromAnother[role] = null;
                fromAnother[role] = null;

                Assert.Null(from[role]);
                Assert.Null(from[role]);
                Assert.Null(fromAnother[role]);
                Assert.Null(fromAnother[role]);
                Assert.Null(to[association]);
                Assert.Null(to[association]);

                from[role] = to;

                Assert.Equal(to, from[role]);
                fromAnother[role] = to;

                Assert.Null(from[role]);
                Assert.Equal(to, fromAnother[role]);
                fromAnother[role] = null;

                // Exist
                Assert.False(from.Exist(role));
                Assert.False(from.Exist(role));
                Assert.False(fromAnother.Exist(role));
                Assert.False(fromAnother.Exist(role));
                Assert.False(to.Exist(association));
                Assert.False(to.Exist(association));

                from[role] = to;
                from[role] = to;

                Assert.True(from.Exist(role));
                Assert.True(from.Exist(role));
                Assert.False(fromAnother.Exist(role));
                Assert.False(fromAnother.Exist(role));
                Assert.True(to.Exist(association));
                Assert.True(to.Exist(association));

                fromAnother[role] = to;
                fromAnother[role] = to;

                Assert.False(from.Exist(role));
                Assert.False(from.Exist(role));
                Assert.True(fromAnother.Exist(role));
                Assert.True(fromAnother.Exist(role));
                Assert.True(to.Exist(association));
                Assert.True(to.Exist(association));

                fromAnother[role] = null;
                fromAnother[role] = null;

                Assert.False(from.Exist(role));
                Assert.False(from.Exist(role));
                Assert.False(fromAnother.Exist(role));
                Assert.False(fromAnother.Exist(role));
                Assert.False(to.Exist(association));
                Assert.False(to.Exist(association));

                // Null
                // Set Null
                // Get
                Assert.Null(from[role]);
                Assert.Null(from[role]);

                from[role] = null;
                from[role] = null;

                Assert.Null(from[role]);
                Assert.Null(from[role]);

                from[role] = to;
                from[role] = to;

                Assert.Equal(to, from[role]);
                Assert.Equal(to, from[role]);

                from[role] = null;
                from[role] = null;

                Assert.Null(from[role]);
                Assert.Null(from[role]);

                // Get
                Assert.Null(from[role]);
                Assert.Null(from[role]);

                from[role] = null;
                from[role] = null;

                Assert.Null(from[role]);
                Assert.Null(from[role]);

                from[role] = to;
                from[role] = to;

                Assert.Equal(to, from[role]);
                Assert.Equal(to, from[role]);

                from[role] = null;
                from[role] = null;

                Assert.Null(from[role]);
                Assert.Null(from[role]);

                from = fromBuilder(transaction);
                to = toBuilder(transaction);

                Assert.Null(from[role]);
                Assert.Null(from[role]);
                Assert.Null(to[association]);
                Assert.Null(to[association]);

                // 1-1
                from[role] = to;
                from[role] = to;

                Assert.Equal(to, from[role]);
                Assert.Equal(to, from[role]);
                Assert.Equal(from, to[association]);
                Assert.Equal(from, to[association]);

                // 0-0
                from[role] = null;
                from[role] = null;

                Assert.Null(from[role]);
                Assert.Null(from[role]);
                Assert.Null(to[association]);
                Assert.Null(to[association]);

                if (builders.Length == 1)
                {
                    var builder = builders[0];

                    // New - Middle - To
                    var begin = builder(transaction);
                    var middle = builder(transaction);
                    var end = builder(transaction);

                    begin[role] = middle;
                    middle[role] = end;
                    begin[role] = end;

                    Assert.Null(middle[association]);
                    Assert.Null(middle[role]);
                }
            }
        }

        protected abstract IDatabase CreateDatabase();

        private void Test(Action<
            Func<(
                OneToOneAssociationTypeHandle Association,
                OneToOneRoleTypeHandle Role,
                Func<ITransaction, IObject>[] Builders,
                Func<ITransaction, IObject> FromBuilder,
                Func<ITransaction, IObject> FromAnotherBuilder,
                Func<ITransaction, IObject> ToBuilder,
                Func<ITransaction, IObject> ToAnotherBuilder)>,
            Action<Action>,
            Action<Action>> test)
        {
            foreach (var actRepeat in new[] { 1, 2 })
            {
                foreach (var assertRepeat in new[] { 1, 2 })
                {
                    foreach (var fixture in this.fixtures)
                    {
                        test(
                            fixture,
                            act =>
                            {
                                for (var i1 = 0; i1 < actRepeat; i1++)
                                {
                                    act();
                                }
                            },
                            assert =>
                            {
                                for (var i1 = 0; i1 < assertRepeat; i1++)
                                {
                                    assert();
                                }
                            });
                    }
                }
            }
        }

        private void FromTo(
           Func<IEnumerable<Action<OneToOneAssociationTypeHandle, OneToOneRoleTypeHandle, IObject, IObject>>> acts,
           Func<IEnumerable<Action<OneToOneAssociationTypeHandle, OneToOneRoleTypeHandle, IObject, IObject>>> asserts)
        {
            var assertPermutations = asserts().Permutations().ToArray();

            foreach (var actRepeats in new[] { 1, 2 })
            {
                foreach (var assertPermutation in assertPermutations)
                {
                    foreach (var assertRepeats in new[] { 1, 2 })
                    {
                        foreach (var fixture in this.fixtures)
                        {
                            var (association, role, _, fromBuilder, _, toBuilder, _) = fixture();

                            var database = this.CreateDatabase();
                            var transaction = database.CreateTransaction();

                            var from = fromBuilder(transaction);
                            var to = toBuilder(transaction);

                            foreach (var act in acts())
                            {
                                for (var actRepeat = 0; actRepeat < actRepeats; actRepeat++)
                                {
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

        private void FromFromAnotherTo(
           Func<IEnumerable<Action<OneToOneAssociationTypeHandle, OneToOneRoleTypeHandle, IObject, IObject, IObject>>> acts,
           Func<IEnumerable<Action<OneToOneAssociationTypeHandle, OneToOneRoleTypeHandle, IObject, IObject, IObject>>> asserts)
        {
            var assertPermutations = asserts().Permutations().ToArray();

            foreach (var actRepeats in new[] { 1, 2 })
            {
                foreach (var assertPermutation in assertPermutations)
                {
                    foreach (var assertRepeats in new[] { 1, 2 })
                    {
                        foreach (var fixture in this.fixtures)
                        {
                            var (association, role, _, fromBuilder, fromAnotherBuilder, toBuilder, _) = fixture();

                            var database = this.CreateDatabase();
                            var transaction = database.CreateTransaction();

                            var from = fromBuilder(transaction);
                            var fromAnother = fromAnotherBuilder(transaction);
                            var to = toBuilder(transaction);

                            foreach (var act in acts())
                            {
                                for (var actRepeat = 0; actRepeat < actRepeats; actRepeat++)
                                {
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

        private void FromToToAnother(
           Func<IEnumerable<Action<OneToOneAssociationTypeHandle, OneToOneRoleTypeHandle, IObject, IObject, IObject>>> acts,
           Func<IEnumerable<Action<OneToOneAssociationTypeHandle, OneToOneRoleTypeHandle, IObject, IObject, IObject>>> asserts)
        {
            var assertPermutations = asserts().Permutations().ToArray();

            foreach (var actRepeats in new[] { 1, 2 })
            {
                foreach (var assertPermutation in assertPermutations)
                {
                    foreach (var assertRepeats in new[] { 1, 2 })
                    {
                        foreach (var fixture in this.fixtures)
                        {
                            var (association, role, _, fromBuilder, _, toBuilder, toAnotherBuilder) = fixture();

                            var database = this.CreateDatabase();
                            var transaction = database.CreateTransaction();

                            var from = fromBuilder(transaction);
                            var to = toBuilder(transaction);
                            var toAnother = toAnotherBuilder(transaction);

                            foreach (var act in acts())
                            {
                                for (var actRepeat = 0; actRepeat < actRepeats; actRepeat++)
                                {
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

        private void FromFromAnotherToToAnother(
           Func<IEnumerable<Action<OneToOneAssociationTypeHandle, OneToOneRoleTypeHandle, IObject, IObject, IObject, IObject>>> acts,
           Func<IEnumerable<Action<OneToOneAssociationTypeHandle, OneToOneRoleTypeHandle, IObject, IObject, IObject, IObject>>> asserts)
        {
            var assertPermutations = asserts().Permutations().ToArray();

            foreach (var actRepeats in new[] { 1, 2 })
            {
                foreach (var assertPermutation in assertPermutations)
                {
                    foreach (var assertRepeats in new[] { 1, 2 })
                    {
                        foreach (var fixture in this.fixtures)
                        {
                            var (association, role, _, fromBuilder, fromAnotherBuilder, toBuilder, toAnotherBuilder) = fixture();

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
