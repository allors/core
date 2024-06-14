namespace Allors.Core.MetaMeta.Tests;

using System;
using FluentAssertions;
using Xunit;

public class MetaObjectTypeTests
{
    [Fact]
    public void Supertypes()
    {
        var meta = new MetaMeta();

        var s1 = meta.AddInterface(Guid.NewGuid(), "S1");
        var i1 = meta.AddInterface(Guid.NewGuid(), "I1", s1);
        var c1 = meta.AddClass(Guid.NewGuid(), "C1", i1);

        c1.Supertypes.Count.Should().Be(2);
        c1.Supertypes.Should().Contain(i1);
        c1.Supertypes.Should().Contain(s1);

        i1.Supertypes.Should().HaveCount(1);
        i1.Supertypes.Should().Contain(s1);

        s1.Supertypes.Should().BeEmpty();
    }

    [Fact]
    public void IsAssignableFrom()
    {
        var meta = new MetaMeta();

        var s1 = meta.AddInterface(Guid.NewGuid(), "S1");
        var i1 = meta.AddInterface(Guid.NewGuid(), "I1", s1);
        var c1 = meta.AddClass(Guid.NewGuid(), "C1", i1);

        c1.IsAssignableFrom(c1).Should().BeTrue();
        i1.IsAssignableFrom(c1).Should().BeTrue();
        s1.IsAssignableFrom(c1).Should().BeTrue();

        c1.IsAssignableFrom(i1).Should().BeFalse();
        i1.IsAssignableFrom(i1).Should().BeTrue();
        s1.IsAssignableFrom(i1).Should().BeTrue();

        c1.IsAssignableFrom(s1).Should().BeFalse();
        i1.IsAssignableFrom(s1).Should().BeFalse();
        s1.IsAssignableFrom(s1).Should().BeTrue();
    }
}
