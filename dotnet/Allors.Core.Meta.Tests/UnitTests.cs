namespace Allors.Core.Meta.Tests;

using System;
using Allors.Core.MetaMeta;
using Xunit;

public class UnitTests
{
    [Fact]
    public void SameRoleTypeName()
    {
        var metaMeta = new MetaMeta();
        var domain = metaMeta.AddDomain(Guid.NewGuid(), "Domain");
        var @string = metaMeta.AddUnit(domain, Guid.NewGuid(), "String");
        var c1 = metaMeta.AddClass(domain, Guid.NewGuid(), "C1");
        var c2 = metaMeta.AddClass(domain, Guid.NewGuid(), "C2");
        metaMeta.AddUnitRelation(domain, Guid.NewGuid(), Guid.NewGuid(), c1, @string, "Same");
        metaMeta.AddUnitRelation(domain, Guid.NewGuid(), Guid.NewGuid(), c2, @string, "Same");

        var meta = new Meta(metaMeta);

        var c1a = meta.Build(c1, v =>
        {
            v["Same"] = "c1";
        });

        var c2a = meta.Build(c2, v =>
        {
            v["Same"] = "c2";
        });

        Assert.Equal("c1", c1a["Same"]);
        Assert.Equal("c2", c2a["Same"]);
    }

    [Fact]
    public void PropertySetByString()
    {
        var metaMeta = new MetaMeta();
        var domain = metaMeta.AddDomain(Guid.NewGuid(), "Domain");
        var @string = metaMeta.AddUnit(domain, Guid.NewGuid(), "String");
        var person = metaMeta.AddClass(domain, Guid.NewGuid(), "Person");
        var unitRoleType = metaMeta.AddUnitRelation(domain, Guid.NewGuid(), Guid.NewGuid(), person, @string, "FirstName");

        var meta = new Meta(metaMeta);

        var john = meta.Build(person);
        var jane = meta.Build(person);

        john["FirstName"] = "John";
        jane["FirstName"] = "Jane";

        Assert.Equal("John", john["FirstName"]);
        Assert.Equal("Jane", jane["FirstName"]);
        Assert.Equal("John", john[unitRoleType]);
        Assert.Equal("Jane", jane[unitRoleType]);

        jane["FirstName"] = null;

        Assert.Equal("John", john["FirstName"]);
        Assert.Null(jane["FirstName"]);
        Assert.Equal("John", john[unitRoleType]);
        Assert.Null(jane[unitRoleType]);
    }

    [Fact]
    public void PropertySetByUnitRoleType()
    {
        var metaMeta = new MetaMeta();
        var domain = metaMeta.AddDomain(Guid.NewGuid(), "Domain");
        var @string = metaMeta.AddUnit(domain, Guid.NewGuid(), "String");
        var person = metaMeta.AddClass(domain, Guid.NewGuid(), "Person");
        var unitRoleType = metaMeta.AddUnitRelation(domain, Guid.NewGuid(), Guid.NewGuid(), person, @string, "FirstName");

        var meta = new Meta(metaMeta);

        var john = meta.Build(person);
        var jane = meta.Build(person);

        john[unitRoleType] = "John";
        jane[unitRoleType] = "Jane";

        Assert.Equal("John", john["FirstName"]);
        Assert.Equal("Jane", jane["FirstName"]);
        Assert.Equal("John", john[unitRoleType]);
        Assert.Equal("Jane", jane[unitRoleType]);

        jane[unitRoleType] = null;

        Assert.Equal("John", john["FirstName"]);
        Assert.Null(jane["FirstName"]);
        Assert.Equal("John", john[unitRoleType]);
        Assert.Null(jane[unitRoleType]);
    }
}
