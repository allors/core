namespace Allors.Core.Database.Engines.Tests;

using FluentAssertions;
using Xunit;

public abstract class ExtentTests : Tests
{
    [Fact]
    public void ToDo()
    {
        true.Should().BeTrue();
    }

    protected abstract IDatabase CreateDatabase();
}
