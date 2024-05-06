namespace Allors.Core.Database.Adapters.Tests
{
    using System;
    using System.Linq;
    using Allors.Core.Database.Meta;
    using Allors.Core.Database.Meta.Handles;
    using Xunit;

    public abstract class LifeCycleTests
    {
        private readonly Func<(
            OneToOneAssociationTypeHandle Association,
            OneToOneRoleTypeHandle Role,
            Func<ITransaction, IObject>[] Builders,
            Func<ITransaction, IObject> FromBuilder,
            Func<ITransaction, IObject> FromAnotherBuilder,
            Func<ITransaction, IObject> ToBuilder,
            Func<ITransaction, IObject> ToAnotherBuilder)>[] fixtures;

        protected LifeCycleTests()
        {
            var coreMeta = new CoreMeta();
            this.Meta = new AdaptersMeta(coreMeta);

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

        public AdaptersMeta Meta { get; }

        [Fact]
        public void Construction()
        {
            foreach (var fixture in this.fixtures)
            {
                var database = this.CreateDatabase();
                var transaction = database.CreateTransaction();

                var (association, role, builders, _, _, _, _) = fixture();

                var objects = builders.Select(v => v(transaction)).ToArray();

                foreach (var @object in objects)
                {
                    Assert.NotNull(@object);
                }
            }
        }

        protected abstract IDatabase CreateDatabase();
    }
}
