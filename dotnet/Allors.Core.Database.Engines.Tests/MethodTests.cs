namespace Allors.Core.Database.Engines.Tests;

using Allors.Core.Database.Engines.Tests.Meta;
using FluentAssertions;
using Xunit;

public abstract class MethodTests : Tests
{
    [Fact]
    public void Call()
    {
        var database = this.CreateDatabase();
        var transaction = database.CreateTransaction();

        var m = this.Meta;

        var c1a = transaction.Build(m.C1);

        c1a.Call(m.C1DoIt);

        c1a[m.C1DidIt].Should().BeTrue();
    }

    protected abstract IDatabase CreateDatabase();
}
