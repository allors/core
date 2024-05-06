namespace Allors.Core.Database.Adapters.Tests
{
    using System;
    using Allors.Core.Database.Meta;
    using Allors.Core.Database.Meta.Handles;
    using Xunit;

    public abstract class OneToOneTests
    {
        private readonly Func<(OneToOneAssociationTypeHandle Association, OneToOneRoleTypeHandle Role, Func<ITransaction, IObject>[] Builders)>[] fixtures;

        protected OneToOneTests()
        {
            var coreMeta = new CoreMeta();
            this.Meta = new AdaptersMeta(coreMeta);

            this.fixtures =
            [
                () =>
                {
                    var association = this.Meta.C1WhereC1OneToOne;
                    var role = this.Meta.C1C1OneToOne;

                    return (association, role, [Builder]);

                    IObject Builder(ITransaction transaction) => transaction.Build(this.Meta.C1);
                }
            ];
        }

        public AdaptersMeta Meta { get; }

        [Fact]
        public void C1_C1OneToOne()
        {
            foreach (var fixture in this.fixtures)
            {
                var database = this.CreateDatabase();
                var transaction = database.CreateTransaction();

                (OneToOneAssociationTypeHandle association, OneToOneRoleTypeHandle role, Func<ITransaction, IObject>[] builders) = fixture();

                var fromBuilder = builders[0];
                var fromAnotherBuilder = builders.Length switch
                {
                    4 => builders[2],
                    _ => builders[0],
                };
                var toBuilder = builders.Length switch
                {
                    2 => builders[1],
                    4 => builders[2],
                    _ => builders[0],
                };
                var toAnotherBuilder = builders.Length switch
                {
                    2 => builders[1],
                    4 => builders[3],
                    _ => builders[0],
                };

                var from = fromBuilder(transaction);
                var fromAnother = fromAnotherBuilder(transaction);
                var to = toBuilder(transaction);
                var toAnother = toAnotherBuilder(transaction);

                // To 1 and back
                // Get
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
                from[role] = to;
                from[role] = null;
                from[role] = null;

                Assert.Null(from[role]);
                Assert.Null(from[role]);
                Assert.Null(to[association]);
                Assert.Null(to[association]);

                // Exist
                Assert.False(from.Exist(role));
                Assert.False(from.Exist(role));
                Assert.False(to.Exist(association));
                Assert.False(to.Exist(association));

                // 1-1
                from[role] = to;
                from[role] = to;

                Assert.True(from.Exist(role));
                Assert.True(from.Exist(role));
                Assert.True(to.Exist(association));
                Assert.True(to.Exist(association));

                // 0-0
                from[role] = null;
                from[role] = null;
                from[role] = to;
                from[role] = to;
                from[role] = null;
                from[role] = null;

                Assert.False(from.Exist(role));
                Assert.False(from.Exist(role));
                Assert.False(to.Exist(association));
                Assert.False(to.Exist(association));

                // Multiplicity
                // Same New / Same To
                // Get
                Assert.Null(from[role]);
                Assert.Null(from[role]);
                Assert.Null(to[association]);
                Assert.Null(to[association]);

                from[role] = to;
                from[role] = to;

                Assert.Equal(to, from[role]);
                Assert.Equal(to, from[role]);
                Assert.Equal(from, to[association]);
                Assert.Equal(from, to[association]);

                from[role] = null;
                from[role] = null;

                Assert.Null(from[role]);
                Assert.Null(from[role]);
                Assert.Null(to[association]);
                Assert.Null(to[association]);

                // Exist
                Assert.False(from.Exist(role));
                Assert.False(from.Exist(role));
                Assert.False(to.Exist(association));
                Assert.False(to.Exist(association));

                from[role] = to;
                from[role] = to;

                Assert.True(from.Exist(role));
                Assert.True(from.Exist(role));
                Assert.True(to.Exist(association));
                Assert.True(to.Exist(association));

                from[role] = null;
                from[role] = null;

                Assert.Null(from[role]);
                Assert.Null(from[role]);
                Assert.Null(to[association]);
                Assert.Null(to[association]);

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
    }
}
