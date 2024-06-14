namespace Allors.Core.Meta.Tests;

using System;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;
using FluentAssertions;
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

        changedFirstNames.Keys.Should().HaveCount(1);
        changedLastNames.Keys.Should().HaveCount(1);
        changedFirstNames.Keys.Should().Contain(john);
        changedLastNames.Keys.Should().Contain(john);

        var snapshot2 = meta.Checkpoint();

        changedFirstNames = snapshot2.ChangedRoles(firstName);
        changedLastNames = snapshot2.ChangedRoles(lastName);

        changedFirstNames.Keys.Should().HaveCount(1);
        changedLastNames.Keys.Should().HaveCount(1);
        changedFirstNames.Keys.Should().Contain(jane);
        changedLastNames.Keys.Should().Contain(jane);
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
        changedEmployees.Should().HaveCount(1);

        acme["Employees"] = new[] { jane, john };

        snapshot = meta.Checkpoint();
        changedEmployees = snapshot.ChangedRoles(employees);
        changedEmployees.Should().BeEmpty();

        acme["Employees"] = Array.Empty<IMetaObject>();

        acme["Employees"] = new[] { jane, john };

        snapshot = meta.Checkpoint();
        changedEmployees = snapshot.ChangedRoles(employees);
        changedEmployees.Should().BeEmpty();
    }
}
