namespace Allors.Core.Database.Engines.Tests;

using System;
using System.Linq;
using Allors.Core.Database.Engines.Tests.Meta;
using Allors.Core.Database.Meta;
using Xunit;

public abstract class LifeCycleTests : Tests
{
    private readonly Func<(
        OneToOneAssociationType Association,
        OneToOneRoleType Role,
        Func<ITransaction, IObject>[] Builders,
        Func<ITransaction, IObject> FromBuilder,
        Func<ITransaction, IObject> FromAnotherBuilder,
        Func<ITransaction, IObject> ToBuilder,
        Func<ITransaction, IObject> ToAnotherBuilder)>[] fixtures;

    protected LifeCycleTests()
    {
        this.fixtures =
        [
            () =>
            {
                var association = this.Meta.C1WhereC1OneToOne();
                var role = this.Meta.C1C1OneToOne();

                return (association, role, [Builder], Builder, Builder, Builder, Builder);

                IObject Builder(ITransaction transaction) => transaction.Build(this.Meta.C1);
            }
        ];
    }

    [Fact]
    public void Construction()
    {
        foreach (var fixture in this.fixtures)
        {
            var database = this.CreateDatabase();
            var transaction = database.CreateTransaction();

            var (_, _, builders, _, _, _, _) = fixture();

            var objects = builders.Select(v => v(transaction)).ToArray();

            foreach (var @object in objects)
            {
                Assert.NotNull(@object);
            }
        }
    }

    protected abstract IDatabase CreateDatabase();
}
