namespace Allors.Core.Database.Adapters.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
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

        private readonly Action<ITransaction>[] preActs;

        protected OneToOneTests()
        {
            this.fixtures =
            [
                () =>
                {
                    // C1 <-> C1
                    Debugger.Log(0, null, $"C1 <-> C1\n");
                    var association = this.Meta.C1WhereC1OneToOne;
                    var role = this.Meta.C1C1OneToOne;

                    return (association, role, [C1Builder], C1Builder, C1Builder, C1Builder, C1Builder);

                    IObject C1Builder(ITransaction transaction) => transaction.Build(this.Meta.C1);
                },
                () =>
                {
                    // C1 <-> I1
                    Debugger.Log(0, null, $"C1 <-> I1\n");
                    var association = this.Meta.C1WhereI1OneToOne;
                    var role = this.Meta.C1I1OneToOne;

                    return (association, role, [C1Builder], C1Builder, C1Builder, C1Builder, C1Builder);

                    IObject C1Builder(ITransaction transaction) => transaction.Build(this.Meta.C1);
                },
                () =>
                {
                    // C1 <-> C2
                    Debugger.Log(0, null, $"C1 <-> C2\n");
                    var association = this.Meta.C1WhereC2OneToOne;
                    var role = this.Meta.C1C2OneToOne;

                    return (association, role, [C1Builder, C2Builder],  C1Builder, C1Builder, C2Builder, C2Builder);

                    IObject C1Builder(ITransaction transaction) => transaction.Build(this.Meta.C1);
                    IObject C2Builder(ITransaction transaction) => transaction.Build(this.Meta.C2);
                },
                () =>
                {
                    // C1 <-> I2
                    Debugger.Log(0, null, $"C1 <-> I2\n");
                    var association = this.Meta.C1WhereI2OneToOne;
                    var role = this.Meta.C1I2OneToOne;

                    return (association, role, [C1Builder, C2Builder],  C1Builder, C1Builder, C2Builder, C2Builder);

                    IObject C1Builder(ITransaction transaction) => transaction.Build(this.Meta.C1);
                    IObject C2Builder(ITransaction transaction) => transaction.Build(this.Meta.C2);
                },
            ];

            this.preActs =
            [
                _ =>
                {
                    Debugger.Log(0, null, "Nothing\n");
                },
                v =>
                {
                    Debugger.Log(0, null, "Checkpoint\n");
                    v.Checkpoint();
                },
                v =>
                {
                    Debugger.Log(0, null, "Checkpoint Checkpoint\n");
                    v.Checkpoint();
                    v.Checkpoint();
                },
                v =>
                {
                    Debugger.Log(0, null, "Commit\n");
                    v.Commit();
                },
                v =>
                {
                    Debugger.Log(0, null, "Commit Commit\n");
                    v.Commit();
                    v.Commit();
                },
                v =>
                {
                    Debugger.Log(0, null, "Checkpoint Commit\n");
                    v.Checkpoint();
                    v.Commit();
                },
                v =>
                {
                    Debugger.Log(0, null, "Commit Checkpoint\n");
                    v.Commit();
                    v.Checkpoint();
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
                    (association, _, _, fromAnother, to) => Assert.Equal(fromAnother, to[association]),
                    (_, role, from, _, _) => Assert.Null(from[role]),
                    (_, role, _, fromAnother, to) => Assert.Equal(to, fromAnother[role])
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
                    (association, _, _, fromAnother, to) => Assert.Equal(fromAnother, to[association]),
                    (_, role, from, _, _) => Assert.Null(from[role]),
                    (_, role, _, fromAnother, to) => Assert.Equal(to, fromAnother[role])
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
                    (association, _, _, _, to) => Assert.Null(to[association]),
                    (_, role, from, _, _) => Assert.Null(from[role]),
                    (_, role, _, fromAnother, _) => Assert.Null(fromAnother[role])
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
                    (association, _, _, _, to) => Assert.Null(to[association]),
                    (_, role, from, _, _) => Assert.Null(from[role]),
                    (_, role, _, fromAnother, _) => Assert.Null(fromAnother[role])
                ]);
        }

        [Fact]
        public void FromFromAnotherToInitialWithExist()
        {
            this.FromFromAnotherTo(
                () =>
                [
                ],
                () =>
                [
                    (association, _, _, _, to) => Assert.False(to.Exist(association)),
                    (_, role, from, _, _) => Assert.False(from.Exist(role)),
                    (_, role, _, fromAnother, _) => Assert.False(fromAnother.Exist(role))
                ]);
        }

        [Fact]
        public void FromFromAnotherToSetSetWithExist()
        {
            this.FromFromAnotherTo(
                () =>
                [
                    (_, role, from, _, to) => from[role] = to,
                    (_, role, _, fromAnother, to) => fromAnother[role] = to
                ],
                () =>
                [
                    (association, _, _, fromAnother, to) => Assert.Equal(fromAnother, to[association]),
                    (_, role, from, _, _) => Assert.False(from.Exist(role)),
                    (_, role, _, fromAnother, to) => Assert.Equal(to, fromAnother[role])
                ]);
        }

        [Fact]
        public void FromFromAnotherToSetAndSetWithExist()
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
                    (association, _, _, fromAnother, to) => Assert.Equal(fromAnother, to[association]),
                    (_, role, from, _, _) => Assert.False(from.Exist(role)),
                    (_, role, _, fromAnother, to) => Assert.Equal(to, fromAnother[role])
                ]);
        }

        [Fact]
        public void FromFromAnotherToSetSetResetWithExist()
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
                    (association, _, _, _, to) => Assert.False(to.Exist(association)),
                    (_, role, from, _, _) => Assert.False(from.Exist(role)),
                    (_, role, _, fromAnother, _) => Assert.False(fromAnother.Exist(role))
                ]);
        }

        [Fact]
        public void FromFromAnotherToSetSetAndResetWithExist()
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
                    (association, _, _, _, to) => Assert.False(to.Exist(association)),
                    (_, role, from, _, _) => Assert.False(from.Exist(role)),
                    (_, role, _, fromAnother, _) => Assert.False(fromAnother.Exist(role))
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
                    (association, _, _, to, _) => Assert.Null(to[association]),
                    (association, _, from, _, toAnother) => Assert.Equal(from, toAnother[association]),
                    (_, role, from, _, toAnother) => Assert.Equal(toAnother, from[role])
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
                    (association, _, _, to, _) => Assert.Null(to[association]),
                    (association, _, from, _, toAnother) => Assert.Equal(from, toAnother[association]),
                    (_, role, from, _, toAnother) => Assert.Equal(toAnother, from[role])
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
                    (association, _, _, to, _) => Assert.Null(to[association]),
                    (association, _, _, _, toAnother) => Assert.Null(toAnother[association]),
                    (_, role, from, _, _) => Assert.Null(from[role]),
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
                    (association, _, _, to, _) => Assert.Null(to[association]),
                    (association, _, _, _, toAnother) => Assert.Null(toAnother[association]),
                    (_, role, from, _, _) => Assert.Null(from[role]),
                ]);
        }

        [Fact]
        public void FromToToAnotherInitialWithExist()
        {
            this.FromToToAnother(
                () =>
                [
                ],
                () =>
                [
                    (association, _, _, to, _) => Assert.False(to.Exist(association)),
                    (association, _, _, _, toAnother) => Assert.False(toAnother.Exist(association)),
                    (_, role, from, _, _) => Assert.False(from.Exist(role)),
                ]);
        }

        [Fact]
        public void FromToToAnotherSetSetWithExist()
        {
            this.FromToToAnother(
                () =>
                [
                    (_, role, from, to, _) => from[role] = to,
                    (_, role, from, _, toAnother) => from[role] = toAnother
                ],
                () =>
                [
                    (association, _, _, to, _) => Assert.False(to.Exist(association)),
                    (association, _, from, _, toAnother) => Assert.Equal(from, toAnother[association]),
                    (_, role, from, _, toAnother) => Assert.Equal(toAnother, from[role])
                ]);
        }

        [Fact]
        public void FromToToAnotherSetAndSetWithExist()
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
                    (association, _, _, to, _) => Assert.False(to.Exist(association)),
                    (association, _, from, _, toAnother) => Assert.Equal(from, toAnother[association]),
                    (_, role, from, _, toAnother) => Assert.Equal(toAnother, from[role])
                ]);
        }

        [Fact]
        public void FromToToAnotherSetSetResetWithExist()
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
                    (association, _, _, to, _) => Assert.False(to.Exist(association)),
                    (association, _, _, _, toAnother) => Assert.False(toAnother.Exist(association)),
                    (_, role, from, _, _) => Assert.False(from.Exist(role)),
                ]);
        }

        [Fact]
        public void FromToToAnotherSetSetAndResetWithExist()
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
                    (association, _, _, to, _) => Assert.False(to.Exist(association)),
                    (association, _, _, _, toAnother) => Assert.False(toAnother.Exist(association)),
                    (_, role, from, _, _) => Assert.False(from.Exist(role)),
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
                    (association, _, _, _, to, _) => Assert.Null(to[association]),
                    (association, _, from, _, _, toAnother) => Assert.Equal(from, toAnother[association]),
                    (_, role, from, _, _, toAnother) => Assert.Equal(toAnother, from[role]),
                    (_, role, _, fromAnother, _, _) => Assert.Null(fromAnother[role]),
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
                    (association, _, _, _, to, _) => Assert.Null(to[association]),
                    (association, _, from, _, _, toAnother) => Assert.Equal(from, toAnother[association]),
                    (_, role, from, _, _, toAnother) => Assert.Equal(toAnother, from[role]),
                    (_, role, _, fromAnother, _, _) => Assert.Null(fromAnother[role]),
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
                    (association, _, _, _, to, _) => Assert.Null(to[association]),
                    (association, _, _, _, _, toAnother) => Assert.Null(toAnother[association]),
                    (_, role, from, _, _, _) => Assert.Null(from[role]),
                    (_, role, _, fromAnother, _, _) => Assert.Null(fromAnother[role]),
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
                    (association, _, _, _, to, _) => Assert.Null(to[association]),
                    (association, _, _, _, _, toAnother) => Assert.Null(toAnother[association]),
                    (_, role, from, _, _, _) => Assert.Null(from[role]),
                    (_, role, _, fromAnother, _, _) => Assert.Null(fromAnother[role]),
                ]);
        }

        [Fact]
        public void FromFromAnotherToToAnotherInitialWithExist()
        {
            this.FromFromAnotherToToAnother(
                () =>
                [
                ],
                () =>
                [
                    (association, _, _, _, to, _) => Assert.False(to.Exist(association)),
                    (association, _, _, _, _, toAnother) => Assert.False(toAnother.Exist(association)),
                    (_, role, from, _, _, _) => Assert.False(from.Exist(role)),
                    (_, role, _, fromAnother, _, _) => Assert.False(fromAnother.Exist(role)),
                ]);
        }

        [Fact]
        public void FromFromAnotherToToAnotherSetSetSetWithExist()
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
                    (association, _, _, _, to, _) => Assert.False(to.Exist(association)),
                    (association, _, from, _, _, toAnother) => Assert.Equal(from, toAnother[association]),
                    (_, role, from, _, _, toAnother) => Assert.Equal(toAnother, from[role]),
                    (_, role, _, fromAnother, _, _) => Assert.False(fromAnother.Exist(role)),
                ]);
        }

        [Fact]
        public void FromFromAnotherToToAnotherSetAndSetAndSetWithExist()
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
                    (association, _, _, _, to, _) => Assert.False(to.Exist(association)),
                    (association, _, from, _, _, toAnother) => Assert.Equal(from, toAnother[association]),
                    (_, role, from, _, _, toAnother) => Assert.Equal(toAnother, from[role]),
                    (_, role, _, fromAnother, _, _) => Assert.False(fromAnother.Exist(role)),
                ]);
        }

        [Fact]
        public void FromFromAnotherToToAnotherSetSetSetResetWithExist()
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
                    (association, _, _, _, to, _) => Assert.False(to.Exist(association)),
                    (association, _, _, _, _, toAnother) => Assert.False(toAnother.Exist(association)),
                    (_, role, from, _, _, _) => Assert.False(from.Exist(role)),
                    (_, role, _, fromAnother, _, _) => Assert.False(fromAnother.Exist(role)),
                ]);
        }

        [Fact]
        public void FromFromAnotherToToAnotherSetAndSetAndSetAndResetWithExist()
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
                    (association, _, _, _, to, _) => Assert.False(to.Exist(association)),
                    (association, _, _, _, _, toAnother) => Assert.False(toAnother.Exist(association)),
                    (_, role, from, _, _, _) => Assert.False(from.Exist(role)),
                    (_, role, _, fromAnother, _, _) => Assert.False(fromAnother.Exist(role)),
                ]);
        }

        [Fact]
        public void BeginMiddleEndSet()
        {
            foreach (var fixture in this.fixtures)
            {
                var database = this.CreateDatabase();
                var transaction = database.CreateTransaction();

                var (association, role, builders, _, _, _, _) =
                    fixture();

                if (builders.Length == 1)
                {
                    var builder = builders[0];

                    // Begin - Middle - End
                    var begin = builder(transaction);
                    var middle = builder(transaction);
                    var end = builder(transaction);

                    begin[role] = middle;
                    middle[role] = end;
                    begin[role] = end;

                    Assert.Null(middle[association]);
                    Assert.Equal(begin, end[association]);
                    Assert.Null(begin[association]);

                    Assert.Equal(end, begin[role]);
                    Assert.Null(middle[role]);
                    Assert.Null(end[role]);
                }
            }
        }

        [Fact]
        public void BeginMiddleEndRingSet()
        {
            foreach (var fixture in this.fixtures)
            {
                var database = this.CreateDatabase();
                var transaction = database.CreateTransaction();

                var (association, role, builders, fromBuilder, fromAnotherBuilder, toBuilder, toAnotherBuilder) = fixture();

                if (builders.Length == 1)
                {
                    var builder = builders[0];

                    // Begin - Middle - End
                    var begin = builder(transaction);
                    var middle = builder(transaction);
                    var end = builder(transaction);

                    begin[role] = middle;
                    middle[role] = end;
                    end[role] = begin;

                    Assert.Equal(begin, middle[association]);
                    Assert.Equal(middle, end[association]);
                    Assert.Equal(end, begin[association]);

                    Assert.Equal(middle, begin[role]);
                    Assert.Equal(end, middle[role]);
                    Assert.Equal(begin, end[role]);
                }
            }
        }

        protected abstract IDatabase CreateDatabase();

        private void FromTo(
           Func<IEnumerable<Action<OneToOneAssociationTypeHandle, OneToOneRoleTypeHandle, IObject, IObject>>> acts,
           Func<IEnumerable<Action<OneToOneAssociationTypeHandle, OneToOneRoleTypeHandle, IObject, IObject>>> asserts)
        {
            var assertPermutations = asserts().Permutations().ToArray();

            foreach (var preact in this.preActs)
            {
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
                                var (association, role, _, fromBuilder, _, toBuilder, _) = fixture();

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
           Func<IEnumerable<Action<OneToOneAssociationTypeHandle, OneToOneRoleTypeHandle, IObject, IObject, IObject>>> acts,
           Func<IEnumerable<Action<OneToOneAssociationTypeHandle, OneToOneRoleTypeHandle, IObject, IObject, IObject>>> asserts)
        {
            var assertPermutations = asserts().Permutations().ToArray();

            foreach (var preact in this.preActs)
            {
                foreach (var actRepeats in new[] { 1, 2 })
                {
                    Debugger.Log(0, null, $"Act Repeats {actRepeats}\n");

                    if (actRepeats == 2)
                    {
                        Debugger.Break();
                    }

                    foreach (var assertPermutation in assertPermutations)
                    {
                        foreach (var assertRepeats in new[] { 1, 2 })
                        {
                            Debugger.Log(0, null, $"Assert Repeats {assertRepeats}\n");
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
           Func<IEnumerable<Action<OneToOneAssociationTypeHandle, OneToOneRoleTypeHandle, IObject, IObject, IObject>>> acts,
           Func<IEnumerable<Action<OneToOneAssociationTypeHandle, OneToOneRoleTypeHandle, IObject, IObject, IObject>>> asserts)
        {
            var assertPermutations = asserts().Permutations().ToArray();

            foreach (var preact in this.preActs)
            {
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

        private void FromFromAnotherToToAnother(
           Func<IEnumerable<Action<OneToOneAssociationTypeHandle, OneToOneRoleTypeHandle, IObject, IObject, IObject, IObject>>> acts,
           Func<IEnumerable<Action<OneToOneAssociationTypeHandle, OneToOneRoleTypeHandle, IObject, IObject, IObject, IObject>>> asserts)
        {
            var assertPermutations = asserts().Permutations().ToArray();

            foreach (var preact in this.preActs)
            {
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
    }
}
