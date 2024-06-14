namespace Allors.Core.Meta.Tests;

using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;
using FluentAssertions;
using Xunit;

public class ManyToManyTests
{
    [Fact]
    public void AddSingleActiveLink()
    {
        var metaMeta = new MetaMeta();

        var @string = metaMeta.AddUnit(Guid.NewGuid(), "String");
        var organization = metaMeta.AddClass(Guid.NewGuid(), "Organization");
        var person = metaMeta.AddClass(Guid.NewGuid(), "Person");
        var name = metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), organization, @string, "Name");
        var (organizationWhereEmployee, employees) = metaMeta.AddManyToManyRelation(Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var meta = new Meta(metaMeta);

        var acme = meta.Build(organization, v => v[name] = "Acme");
        var hooli = meta.Build(organization, v => v[name] = "Hooli");

        var jane = meta.Build(person);
        var john = meta.Build(person);
        var jenny = meta.Build(person);

        acme.Add(employees, jane);
        acme.Add(employees, john);
        acme.Add(employees, jenny);

        jane[organizationWhereEmployee].Should().HaveCount(1);
        jane[organizationWhereEmployee].Should().Contain(acme);

        john[organizationWhereEmployee].Should().HaveCount(1);
        john[organizationWhereEmployee].Should().Contain(acme);

        jenny[organizationWhereEmployee].Should().HaveCount(1);
        jenny[organizationWhereEmployee].Should().Contain(acme);

        acme[employees].Count().Should().Be(3);
        acme[employees].Should().Contain(jane);
        acme[employees].Should().Contain(john);
        acme[employees].Should().Contain(jenny);

        hooli[employees].Should().BeEmpty();
    }

    [Fact]
    public void SetSingleActiveLink()
    {
        var metaMeta = new MetaMeta();

        var @string = metaMeta.AddUnit(Guid.NewGuid(), "String");
        var organization = metaMeta.AddClass(Guid.NewGuid(), "Organization");
        var person = metaMeta.AddClass(Guid.NewGuid(), "Person");
        var name = metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), organization, @string, "Name");
        var (employers, employees) = metaMeta.AddManyToManyRelation(Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var meta = new Meta(metaMeta);

        var acme = meta.Build(organization, v => v[name] = "Acme");
        var hooli = meta.Build(organization, v => v[name] = "Hooli");

        var jane = meta.Build(person);
        var john = meta.Build(person);
        var jenny = meta.Build(person);

        acme[employees] = new[] { jane }.ToFrozenSet();

        jane[employers].Should().HaveCount(1);
        jane[employers].Should().Contain(acme);

        john[employers].Should().BeEmpty();

        jenny[employers].Should().BeEmpty();

        acme[employees].Should().HaveCount(1);
        acme[employees].Should().Contain(jane);

        hooli[employees].Should().BeEmpty();

        acme[employees] = new[] { jane, john };

        jane[employers].Should().HaveCount(1);
        jane[employers].Should().Contain(acme);

        john[employers].Should().HaveCount(1);
        john[employers].Should().Contain(acme);

        jenny[employers].Should().BeEmpty();

        acme[employees].Count().Should().Be(2);
        acme[employees].Should().Contain(jane);
        acme[employees].Should().Contain(john);

        hooli[employees].Should().BeEmpty();

        acme[employees] = new[] { jane, john, jenny };

        jane[employers].Should().HaveCount(1);
        jane[employers].Should().Contain(acme);

        john[employers].Should().HaveCount(1);
        john[employers].Should().Contain(acme);

        jenny[employers].Should().HaveCount(1);
        jenny[employers].Should().Contain(acme);

        acme[employees].Count().Should().Be(3);
        acme[employees].Should().Contain(jane);
        acme[employees].Should().Contain(john);
        acme[employees].Should().Contain(jenny);

        hooli[employees].Should().BeEmpty();

        acme[employees] = [];

        jane[employers].Should().BeEmpty();
        john[employers].Should().BeEmpty();
        jenny[employers].Should().BeEmpty();

        acme[employees].Should().BeEmpty();
        hooli[employees].Should().BeEmpty();
    }

    [Fact]
    public void RemoveSingleActiveLink()
    {
        var metaMeta = new MetaMeta();

        var @string = metaMeta.AddUnit(Guid.NewGuid(), "String");
        var organization = metaMeta.AddClass(Guid.NewGuid(), "Organization");
        var person = metaMeta.AddClass(Guid.NewGuid(), "Person");
        metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), organization, @string, "Name");
        var (employers, employees) = metaMeta.AddManyToManyRelation(Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var meta = new Meta(metaMeta);

        var acme = meta.Build(organization, v => v["Name"] = "Acme");
        var hooli = meta.Build(organization, v => v["Name"] = "Hooli");

        var jane = meta.Build(person);
        var john = meta.Build(person);
        var jenny = meta.Build(person);

        acme[employees] = new[] { jane, john, jenny };

        acme.Remove(employees, jenny);

        jane[employers].Should().HaveCount(1);
        jane[employers].Should().Contain(acme);

        john[employers].Should().HaveCount(1);
        john[employers].Should().Contain(acme);

        jenny[employers].Should().BeEmpty();

        acme[employees].Count().Should().Be(2);
        acme[employees].Should().Contain(jane);
        acme[employees].Should().Contain(john);

        hooli[employees].Should().BeEmpty();

        acme.Remove(employees, john);

        jane[employers].Should().HaveCount(1);
        jane[employers].Should().Contain(acme);

        john[employers].Should().BeEmpty();

        jenny[employers].Should().BeEmpty();

        acme[employees].Should().HaveCount(1);
        acme[employees].Should().Contain(jane);

        hooli[employees].Should().BeEmpty();

        acme.Remove(employees, jane);

        jane[employers].Should().BeEmpty();
        john[employers].Should().BeEmpty();
        jenny[employers].Should().BeEmpty();

        acme[employees].Should().BeEmpty();
        hooli[employees].Should().BeEmpty();
    }

    [Fact]
    public void MultipleActiveLinks()
    {
        var metaMeta = new MetaMeta();

        var @string = metaMeta.AddUnit(Guid.NewGuid(), "String");
        var organization = metaMeta.AddClass(Guid.NewGuid(), "Organization");
        var person = metaMeta.AddClass(Guid.NewGuid(), "Person");
        metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), organization, @string, "Name");
        var (employers, employees) = metaMeta.AddManyToManyRelation(Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var meta = new Meta(metaMeta);

        var acme = meta.Build(organization, v => v["Name"] = "Acme");
        var hooli = meta.Build(organization, v => v["Name"] = "Hooli");

        var jane = meta.Build(person);
        var john = meta.Build(person);
        var jenny = meta.Build(person);

        acme.Add(employees, jane);
        acme.Add(employees, john);
        acme.Add(employees, jenny);

        hooli.Add(employees, jane);

        jane[employers].Count().Should().Be(2);
        jane[employers].Should().Contain(acme);
        jane[employers].Should().Contain(hooli);

        john[employers].Should().HaveCount(1);
        john[employers].Should().Contain(acme);

        jenny[employers].Should().HaveCount(1);
        jenny[employers].Should().Contain(acme);

        acme[employees].Count().Should().Be(3);
        acme[employees].Should().Contain(jane);
        acme[employees].Should().Contain(john);
        acme[employees].Should().Contain(jenny);

        hooli[employees].Should().HaveCount(1);
        hooli[employees].Should().Contain(jane);

        hooli.Add(employees, john);

        jane[employers].Count().Should().Be(2);
        jane[employers].Should().Contain(acme);
        jane[employers].Should().Contain(hooli);

        john[employers].Count().Should().Be(2);
        john[employers].Should().Contain(acme);
        john[employers].Should().Contain(hooli);

        jenny[employers].Should().HaveCount(1);
        jenny[employers].Should().Contain(acme);

        acme[employees].Count().Should().Be(3);
        acme[employees].Should().Contain(jane);
        acme[employees].Should().Contain(john);
        acme[employees].Should().Contain(jenny);

        hooli[employees].Count().Should().Be(2);
        hooli[employees].Should().Contain(jane);
        hooli[employees].Should().Contain(john);

        hooli.Add(employees, jenny);

        jane[employers].Count().Should().Be(2);
        jane[employers].Should().Contain(acme);
        jane[employers].Should().Contain(hooli);

        john[employers].Count().Should().Be(2);
        john[employers].Should().Contain(acme);
        john[employers].Should().Contain(hooli);

        jenny[employers].Count().Should().Be(2);
        jenny[employers].Should().Contain(acme);
        jenny[employers].Should().Contain(hooli);

        acme[employees].Count().Should().Be(3);
        acme[employees].Should().Contain(jane);
        acme[employees].Should().Contain(john);
        acme[employees].Should().Contain(jenny);

        hooli[employees].Count().Should().Be(3);
        hooli[employees].Should().Contain(jane);
        hooli[employees].Should().Contain(john);
        hooli[employees].Should().Contain(jenny);
    }

    [Fact]
    public void DefaultRoleName()
    {
        var metaMeta = new MetaMeta();

        var @string = metaMeta.AddUnit(Guid.NewGuid(), "String");
        var organization = metaMeta.AddClass(Guid.NewGuid(), "Organization");
        var person = metaMeta.AddClass(Guid.NewGuid(), "Person");
        metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), organization, @string, "Name");
        var people = metaMeta.AddManyToManyRelation(Guid.NewGuid(), Guid.NewGuid(), organization, person);

        var meta = new Meta(metaMeta);

        var acme = meta.Build(organization, v => v["Name"] = "Acme");

        var jane = meta.Build(person);

        acme.Add(people, jane);

        ((IEnumerable<IMetaObject>)jane["OrganizationsWherePerson"]!).Should().HaveCount(1);
        ((IEnumerable<IMetaObject>)jane["OrganizationsWherePerson"]!).Should().Contain(acme);

        ((IEnumerable<IMetaObject>)acme["Persons"]!).Should().HaveCount(1);
        ((IEnumerable<IMetaObject>)acme["Persons"]!).Should().Contain(jane);
    }
}
