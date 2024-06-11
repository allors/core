namespace Allors.Core.Database.Engines.Tests;

using Allors.Core.Database.Engines.Tests.Meta;
using FluentAssertions;
using Xunit;

public abstract class SandboxTests
{
    protected abstract Core.Meta.Meta Meta { get; }

    [Fact]
    public void OneToOne()
    {
        var database = this.CreateDatabase();
        var transaction = database.CreateTransaction();

        var m = this.Meta;

        var association = m.C1sWhereC1ManyToMany();
        var role = m.C1C1ManyToMany();

        var from = transaction.Build(m.C1());
        var to = transaction.Build(m.C1());

        from[role] = [to];

        transaction.Commit();

        from[role] = [];

        to[association].Should().BeEmpty();
        from[role].Should().BeEmpty();
    }

    protected abstract IDatabase CreateDatabase();
}
