namespace Allors.Core.Meta.Tests;

using System;
using Allors.Core.MetaMeta;
using FluentAssertions;
using Xunit;

public class UnitTests
{
    [Fact]
    public void SameRoleTypeName()
    {
        var metaMeta = new MetaMeta();

        var @string = metaMeta.AddUnit(Guid.NewGuid(), "String");
        var c1 = metaMeta.AddClass(Guid.NewGuid(), "C1");
        var c2 = metaMeta.AddClass(Guid.NewGuid(), "C2");
        metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), c1, @string, "Same");
        metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), c2, @string, "Same");

        var meta = new Meta(metaMeta);

        var c1a = meta.Build(c1, v =>
        {
            v["Same"] = "c1";
        });

        var c2a = meta.Build(c2, v =>
        {
            v["Same"] = "c2";
        });

        c1a["Same"].Should().Be("c1");
        c2a["Same"].Should().Be("c2");
    }

    [Fact]
    public void PropertySetByString()
    {
        var metaMeta = new MetaMeta();

        var @string = metaMeta.AddUnit(Guid.NewGuid(), "String");
        var person = metaMeta.AddClass(Guid.NewGuid(), "Person");
        var unitRoleType = metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), person, @string, "FirstName");

        var meta = new Meta(metaMeta);

        var john = meta.Build(person);
        var jane = meta.Build(person);

        john["FirstName"] = "John";
        jane["FirstName"] = "Jane";

        john["FirstName"].Should().Be("John");
        jane["FirstName"].Should().Be("Jane");
        john[unitRoleType].Should().Be("John");
        jane[unitRoleType].Should().Be("Jane");

        jane["FirstName"] = null;

        john["FirstName"].Should().Be("John");
        jane["FirstName"].Should().BeNull();
        john[unitRoleType].Should().Be("John");
        jane[unitRoleType].Should().BeNull();
    }

    [Fact]
    public void PropertySetByUnitRoleType()
    {
        var metaMeta = new MetaMeta();

        var @string = metaMeta.AddUnit(Guid.NewGuid(), "String");
        var person = metaMeta.AddClass(Guid.NewGuid(), "Person");
        var unitRoleType = metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), person, @string, "FirstName");

        var meta = new Meta(metaMeta);

        var john = meta.Build(person);
        var jane = meta.Build(person);

        john[unitRoleType] = "John";
        jane[unitRoleType] = "Jane";

        john["FirstName"].Should().Be("John");
        jane["FirstName"].Should().Be("Jane");
        john[unitRoleType].Should().Be("John");
        jane[unitRoleType].Should().Be("Jane");

        jane[unitRoleType] = null;

        john["FirstName"].Should().Be("John");
        jane["FirstName"].Should().BeNull();
        john[unitRoleType].Should().Be("John");
        jane[unitRoleType].Should().BeNull();
    }
}
