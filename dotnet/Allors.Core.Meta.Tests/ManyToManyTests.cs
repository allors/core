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
        var employees = metaMeta.AddManyToManyRelation(Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var meta = new Meta(metaMeta);

        var acme = meta.Build(organization, v => v[name] = "Acme");
        var hooli = meta.Build(organization, v => v[name] = "Hooli");

        var jane = meta.Build(person);
        var john = meta.Build(person);
        var jenny = meta.Build(person);

        acme[employees] = new[] { jane }.ToFrozenSet();

        ((IReadOnlySet<IMetaObject>)jane["OrganizationsWhereEmployee"]!).Should().HaveCount(1);
        ((IReadOnlySet<IMetaObject>)jane["OrganizationsWhereEmployee"]!).Should().Contain(acme);

        ((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!).Should().BeEmpty();

        ((IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!).Should().BeEmpty();

        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().HaveCount(1);
        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(jane);

        ((IEnumerable<IMetaObject>)hooli["Employees"]!).Should().BeEmpty();

        acme["Employees"] = new[] { jane, john };

        ((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!).Should().HaveCount(1);
        ((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!).Should().Contain(acme);

        ((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!).Should().HaveCount(1);
        ((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!).Should().Contain(acme);

        ((IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!).Should().BeEmpty();

        ((IEnumerable<IMetaObject>)acme["Employees"]!).Count().Should().Be(2);
        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(jane);
        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(john);

        ((IEnumerable<IMetaObject>)hooli["Employees"]!).Should().BeEmpty();

        acme["Employees"] = new[] { jane, john, jenny };

        ((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!).Should().HaveCount(1);
        ((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!).Should().Contain(acme);

        ((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!).Should().HaveCount(1);
        ((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!).Should().Contain(acme);

        ((IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!).Should().HaveCount(1);
        ((IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!).Should().Contain(acme);

        ((IEnumerable<IMetaObject>)acme["Employees"]!).Count().Should().Be(3);
        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(jane);
        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(john);
        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(jenny);

        ((IEnumerable<IMetaObject>)hooli["Employees"]!).Should().BeEmpty();

        acme["Employees"] = Array.Empty<IMetaObject>();

        ((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!).Should().BeEmpty();
        ((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!).Should().BeEmpty();
        ((IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!).Should().BeEmpty();

        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().BeEmpty();
        ((IEnumerable<IMetaObject>)hooli["Employees"]!).Should().BeEmpty();
    }

    [Fact]
    public void RemoveSingleActiveLink()
    {
        var metaMeta = new MetaMeta();

        var @string = metaMeta.AddUnit(Guid.NewGuid(), "String");
        var organization = metaMeta.AddClass(Guid.NewGuid(), "Organization");
        var person = metaMeta.AddClass(Guid.NewGuid(), "Person");
        metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), organization, @string, "Name");
        var employees = metaMeta.AddManyToManyRelation(Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var meta = new Meta(metaMeta);

        var acme = meta.Build(organization, v => v["Name"] = "Acme");
        var hooli = meta.Build(organization, v => v["Name"] = "Hooli");

        var jane = meta.Build(person);
        var john = meta.Build(person);
        var jenny = meta.Build(person);

        acme["Employees"] = new[] { jane, john, jenny };

        acme.Remove(employees, jenny);

        ((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!).Should().HaveCount(1);
        ((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!).Should().Contain(acme);

        ((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!).Should().HaveCount(1);
        ((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!).Should().Contain(acme);

        ((IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!).Should().BeEmpty();

        ((IEnumerable<IMetaObject>)acme["Employees"]!).Count().Should().Be(2);
        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(jane);
        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(john);

        ((IEnumerable<IMetaObject>)hooli["Employees"]!).Should().BeEmpty();

        acme.Remove(employees, john);

        ((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!).Should().HaveCount(1);
        ((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!).Should().Contain(acme);

        ((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!).Should().BeEmpty();

        ((IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!).Should().BeEmpty();

        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().HaveCount(1);
        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(jane);

        ((IEnumerable<IMetaObject>)hooli["Employees"]!).Should().BeEmpty();

        acme.Remove(employees, jane);

        ((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!).Should().BeEmpty();
        ((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!).Should().BeEmpty();
        ((IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!).Should().BeEmpty();

        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().BeEmpty();
        ((IEnumerable<IMetaObject>)hooli["Employees"]!).Should().BeEmpty();
    }

    [Fact]
    public void MultipleActiveLinks()
    {
        var metaMeta = new MetaMeta();

        var @string = metaMeta.AddUnit(Guid.NewGuid(), "String");
        var organization = metaMeta.AddClass(Guid.NewGuid(), "Organization");
        var person = metaMeta.AddClass(Guid.NewGuid(), "Person");
        metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), organization, @string, "Name");
        var employees = metaMeta.AddManyToManyRelation(Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

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

        ((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!).Count().Should().Be(2);
        ((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!).Should().Contain(acme);
        ((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!).Should().Contain(hooli);

        ((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!).Should().HaveCount(1);
        ((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!).Should().Contain(acme);

        ((IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!).Should().HaveCount(1);
        ((IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!).Should().Contain(acme);

        ((IEnumerable<IMetaObject>)acme["Employees"]!).Count().Should().Be(3);
        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(jane);
        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(john);
        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(jenny);

        ((IEnumerable<IMetaObject>)hooli["Employees"]!).Should().HaveCount(1);
        ((IEnumerable<IMetaObject>)hooli["Employees"]!).Should().Contain(jane);

        hooli.Add(employees, john);

        ((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!).Count().Should().Be(2);
        ((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!).Should().Contain(acme);
        ((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!).Should().Contain(hooli);

        ((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!).Count().Should().Be(2);
        ((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!).Should().Contain(acme);
        ((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!).Should().Contain(hooli);

        ((IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!).Should().HaveCount(1);
        ((IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!).Should().Contain(acme);

        ((IEnumerable<IMetaObject>)acme["Employees"]!).Count().Should().Be(3);
        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(jane);
        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(john);
        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(jenny);

        ((IEnumerable<IMetaObject>)hooli["Employees"]!).Count().Should().Be(2);
        ((IEnumerable<IMetaObject>)hooli["Employees"]!).Should().Contain(jane);
        ((IEnumerable<IMetaObject>)hooli["Employees"]!).Should().Contain(john);

        hooli.Add(employees, jenny);

        ((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!).Count().Should().Be(2);
        ((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!).Should().Contain(acme);
        ((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!).Should().Contain(hooli);

        ((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!).Count().Should().Be(2);
        ((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!).Should().Contain(acme);
        ((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!).Should().Contain(hooli);

        ((IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!).Count().Should().Be(2);
        ((IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!).Should().Contain(acme);
        ((IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!).Should().Contain(hooli);

        ((IEnumerable<IMetaObject>)acme["Employees"]!).Count().Should().Be(3);
        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(jane);
        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(john);
        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(jenny);

        ((IEnumerable<IMetaObject>)hooli["Employees"]!).Count().Should().Be(3);
        ((IEnumerable<IMetaObject>)hooli["Employees"]!).Should().Contain(jane);
        ((IEnumerable<IMetaObject>)hooli["Employees"]!).Should().Contain(john);
        ((IEnumerable<IMetaObject>)hooli["Employees"]!).Should().Contain(jenny);
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
