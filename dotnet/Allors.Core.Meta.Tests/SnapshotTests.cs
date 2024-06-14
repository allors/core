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

        john[firstName] = "John";
        john[lastName] = "Doe";

        var snapshot1 = meta.Checkpoint();

        jane[firstName] = "Jane";
        jane[lastName] = "Doe";

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
        var firstName = metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), person, @string, "FirstName");
        var lastName = metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), person, @string, "LastName");
        var name = metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), organization, @string, "Name");
        var employees = metaMeta.AddManyToManyRelation(Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var meta = new Meta(metaMeta);

        var john = meta.Build(person);
        var jane = meta.Build(person);

        john[firstName] = "John";
        john[lastName] = "Doe";

        jane[firstName] = "Jane";
        jane[lastName] = "Doe";

        var acme = meta.Build(organization);

        acme[name] = "Acme";

        acme[employees] = new[] { john, jane };

        var snapshot = meta.Checkpoint();
        var changedEmployees = snapshot.ChangedRoles(employees);
        changedEmployees.Should().HaveCount(1);

        acme[employees] = new[] { jane, john };

        snapshot = meta.Checkpoint();
        changedEmployees = snapshot.ChangedRoles(employees);
        changedEmployees.Should().BeEmpty();

        acme[employees] = Array.Empty<IMetaObject>();

        acme[employees] = new[] { jane, john };

        snapshot = meta.Checkpoint();
        changedEmployees = snapshot.ChangedRoles(employees);
        changedEmployees.Should().BeEmpty();
    }
}
