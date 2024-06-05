namespace Allors.Core.Meta.Tests.Domain;

using System;
using Allors.Core.Meta.Domain;
using Allors.Core.Meta.Meta;
using Xunit;

public class SnapshotTests
{
    [Fact]
    public void Unit()
    {
        var meta = new MetaMeta();
        var domain = meta.AddDomain(Guid.NewGuid(), "Domain");
        var @string = meta.AddUnit(domain, Guid.NewGuid(), "String");
        var person = meta.AddClass(domain, Guid.NewGuid(), "Person");
        var firstName = meta.AddUnitRelation(domain, Guid.NewGuid(), Guid.NewGuid(), person, @string, "FirstName");
        var lastName = meta.AddUnitRelation(domain, Guid.NewGuid(), Guid.NewGuid(), person, @string, "LastName");

        var population = new MetaPopulation(meta);

        var john = population.Build(person);
        var jane = population.Build(person);

        john["FirstName"] = "John";
        john["LastName"] = "Doe";

        var snapshot1 = population.Checkpoint();

        jane["FirstName"] = "Jane";
        jane["LastName"] = "Doe";

        var changedFirstNames = snapshot1.ChangedRoles(firstName);
        var changedLastNames = snapshot1.ChangedRoles(lastName);

        Assert.Single(changedFirstNames.Keys);
        Assert.Single(changedLastNames.Keys);
        Assert.Contains(john, changedFirstNames.Keys);
        Assert.Contains(john, changedLastNames.Keys);

        var snapshot2 = population.Checkpoint();

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
        var meta = new MetaMeta();
        var domain = meta.AddDomain(Guid.NewGuid(), "Domain");
        var @string = meta.AddUnit(domain, Guid.NewGuid(), "String");
        var person = meta.AddClass(domain, Guid.NewGuid(), "Person");
        var organization = meta.AddClass(domain, Guid.NewGuid(), "Organization");
        meta.AddUnitRelation(domain, Guid.NewGuid(), Guid.NewGuid(), person, @string, "FirstName");
        meta.AddUnitRelation(domain, Guid.NewGuid(), Guid.NewGuid(), person, @string, "LastName");
        meta.AddUnitRelation(domain, Guid.NewGuid(), Guid.NewGuid(), organization, @string, "Name");
        var employees = meta.AddManyToManyRelation(domain, Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var population = new MetaPopulation(meta);

        var john = population.Build(person);
        var jane = population.Build(person);

        john["FirstName"] = "John";
        john["LastName"] = "Doe";

        jane["FirstName"] = "Jane";
        jane["LastName"] = "Doe";

        var acme = population.Build(organization);

        acme["Name"] = "Acme";

        acme["Employees"] = new[] { john, jane };

        var snapshot = population.Checkpoint();
        var changedEmployees = snapshot.ChangedRoles(employees);
        Assert.Single(changedEmployees);

        acme["Employees"] = new[] { jane, john };

        snapshot = population.Checkpoint();
        changedEmployees = snapshot.ChangedRoles(employees);
        Assert.Empty(changedEmployees);

        acme["Employees"] = Array.Empty<IMetaObject>();

        acme["Employees"] = new[] { jane, john };

        snapshot = population.Checkpoint();
        changedEmployees = snapshot.ChangedRoles(employees);
        Assert.Empty(changedEmployees);
    }
}
