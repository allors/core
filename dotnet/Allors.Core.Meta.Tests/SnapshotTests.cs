namespace Allors.Core.Meta.Tests;

using System;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;
using Xunit;

public class SnapshotTests
{
    [Fact]
    public void Unit()
    {
        var metaMeta = new MetaMeta();

        var @string = metaMeta.AddUnit(Guid.NewGuid(), "String");
        var person = metaMeta.AddClass(Guid.NewGuid(), "Person");
        var firstName = metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), person, @string, "FirstName");
        var lastName = metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), person, @string, "LastName");

        var meta = new Meta(metaMeta);

        var john = meta.Build(person);
        var jane = meta.Build(person);

        john["FirstName"] = "John";
        john["LastName"] = "Doe";

        var snapshot1 = meta.Checkpoint();

        jane["FirstName"] = "Jane";
        jane["LastName"] = "Doe";

        var changedFirstNames = snapshot1.ChangedRoles(firstName);
        var changedLastNames = snapshot1.ChangedRoles(lastName);

        Assert.Single(changedFirstNames.Keys);
        Assert.Single(changedLastNames.Keys);
        Assert.Contains(john, changedFirstNames.Keys);
        Assert.Contains(john, changedLastNames.Keys);

        var snapshot2 = meta.Checkpoint();

        changedFirstNames = snapshot2.ChangedRoles(firstName);
        changedLastNames = snapshot2.ChangedRoles(lastName);

        Assert.Single(changedFirstNames.Keys);
        Assert.Single(changedLastNames.Keys);
        Assert.Contains(jane, changedFirstNames.Keys);
        Assert.Contains(jane, changedLastNames.Keys);
    }

    [Fact]
    public void Composites()
    {
        var metaMeta = new MetaMeta();

        var @string = metaMeta.AddUnit(Guid.NewGuid(), "String");
        var person = metaMeta.AddClass(Guid.NewGuid(), "Person");
        var organization = metaMeta.AddClass(Guid.NewGuid(), "Organization");
        metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), person, @string, "FirstName");
        metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), person, @string, "LastName");
        metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), organization, @string, "Name");
        var employees = metaMeta.AddManyToManyRelation(Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var meta = new Meta(metaMeta);

        var john = meta.Build(person);
        var jane = meta.Build(person);

        john["FirstName"] = "John";
        john["LastName"] = "Doe";

        jane["FirstName"] = "Jane";
        jane["LastName"] = "Doe";

        var acme = meta.Build(organization);

        acme["Name"] = "Acme";

        acme["Employees"] = new[] { john, jane };

        var snapshot = meta.Checkpoint();
        var changedEmployees = snapshot.ChangedRoles(employees);
        Assert.Single(changedEmployees);

        acme["Employees"] = new[] { jane, john };

        snapshot = meta.Checkpoint();
        changedEmployees = snapshot.ChangedRoles(employees);
        Assert.Empty(changedEmployees);

        acme["Employees"] = Array.Empty<IMetaObject>();

        acme["Employees"] = new[] { jane, john };

        snapshot = meta.Checkpoint();
        changedEmployees = snapshot.ChangedRoles(employees);
        Assert.Empty(changedEmployees);
    }
}
