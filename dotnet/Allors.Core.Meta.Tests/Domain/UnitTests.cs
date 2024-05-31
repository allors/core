namespace Allors.Core.Meta.Tests.Domain;

using System;
using Allors.Core.Meta.Domain;
using Allors.Core.Meta.Meta;
using Xunit;

public class UnitTests
{
    [Fact]
    public void SameRoleTypeName()
    {
        var meta = new MetaMeta();
        var @string = meta.AddUnit(Guid.NewGuid(), "String");
        var c1 = meta.AddClass(Guid.NewGuid(), "C1");
        var c2 = meta.AddClass(Guid.NewGuid(), "C2");
        meta.AddUnit(Guid.NewGuid(), Guid.NewGuid(), c1, @string, "Same");
        meta.AddUnit(Guid.NewGuid(), Guid.NewGuid(), c2, @string, "Same");

        var population = new MetaPopulation(meta);

        var c1a = population.Build(c1, v =>
        {
            v["Same"] = "c1";
        });

        var c2a = population.Build(c2, v =>
        {
            v["Same"] = "c2";
        });

        Assert.Equal("c1", c1a["Same"]);
        Assert.Equal("c2", c2a["Same"]);
    }

    [Fact]
    public void PropertySetByString()
    {
        var meta = new MetaMeta();
        var @string = meta.AddUnit(Guid.NewGuid(), "String");
        var person = meta.AddClass(Guid.NewGuid(), "Person");
        var unitRoleType = meta.AddUnit(Guid.NewGuid(), Guid.NewGuid(), person, @string, "FirstName");

        var population = new MetaPopulation(meta);

        var john = population.Build(person);
        var jane = population.Build(person);

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
        var meta = new MetaMeta();
        var @string = meta.AddUnit(Guid.NewGuid(), "String");
        var person = meta.AddClass(Guid.NewGuid(), "Person");
        var unitRoleType = meta.AddUnit(Guid.NewGuid(), Guid.NewGuid(), person, @string, "FirstName");

        var population = new MetaPopulation(meta);

        var john = population.Build(person);
        var jane = population.Build(person);

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
