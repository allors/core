namespace Allors.Core.Meta.Tests.Domain;

using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using Allors.Core.Meta.Domain;
using Allors.Core.Meta.Meta;
using Allors.Core.Meta.Meta.Diagrams;
using Xunit;

public class ManyToManyTests
{
    [Fact]
    public void AddSingleActiveLink()
    {
        var meta = new MetaMeta();
        var @string = meta.AddUnit("String");
        var organization = meta.AddClass("Organization");
        var person = meta.AddClass("Person");
        var name = meta.AddUnit(organization, @string, "Name");
        var (organizationWhereEmployee, employees) = meta.AddManyToMany(organization, person, "Employee");

        var diagram = new ClassDiagram(meta).Render();

        var population = new MetaPopulation(meta);

        var acme = population.Build(organization, v => v[name] = "Acme");
        var hooli = population.Build(organization, v => v[name] = "Hooli");

        var jane = population.Build(person);
        var john = population.Build(person);
        var jenny = population.Build(person);

        acme.Add(employees, jane);
        acme.Add(employees, john);
        acme.Add(employees, jenny);

        Assert.Single(jane[organizationWhereEmployee]);
        Assert.Contains(acme, jane[organizationWhereEmployee]);

        Assert.Single(john[organizationWhereEmployee]);
        Assert.Contains(acme, john[organizationWhereEmployee]);

        Assert.Single(jenny[organizationWhereEmployee]);
        Assert.Contains(acme, jenny[organizationWhereEmployee]);

        Assert.Equal(3, acme[employees].Count());
        Assert.Contains(jane, acme[employees]);
        Assert.Contains(john, acme[employees]);
        Assert.Contains(jenny, acme[employees]);

        Assert.Empty(hooli[employees]);
    }

    [Fact]
    public void SetSingleActiveLink()
    {
        var meta = new MetaMeta();
        var @string = meta.AddUnit("String");
        var organization = meta.AddClass("Organization");
        var person = meta.AddClass("Person");
        var name = meta.AddUnit(organization, @string, "Name");
        var employees = meta.AddManyToMany(organization, person, "Employee");

        var population = new MetaPopulation(meta);

        var acme = population.Build(organization, v => v[name] = "Acme");
        var hooli = population.Build(organization, v => v[name] = "Hooli");

        var jane = population.Build(person);
        var john = population.Build(person);
        var jenny = population.Build(person);

        acme[employees] = new[] { jane }.ToFrozenSet();

        Assert.Single((IReadOnlySet<IMetaObject>)jane["OrganizationsWhereEmployee"]!);
        Assert.Contains(acme, (IReadOnlySet<IMetaObject>)jane["OrganizationsWhereEmployee"]!);

        Assert.Empty((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!);

        Assert.Empty((IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!);

        Assert.Single((IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.Contains(jane, (IEnumerable<IMetaObject>)acme["Employees"]!);

        Assert.Empty((IEnumerable<IMetaObject>)hooli["Employees"]!);

        acme["Employees"] = new[] { jane, john };

        Assert.Single((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!);
        Assert.Contains(acme, (IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!);

        Assert.Single((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!);
        Assert.Contains(acme, (IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!);

        Assert.Empty((IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!);

        Assert.Equal(2, ((IEnumerable<IMetaObject>)acme["Employees"]!).Count());
        Assert.Contains(jane, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.Contains(john, (IEnumerable<IMetaObject>)acme["Employees"]!);

        Assert.Empty((IEnumerable<IMetaObject>)hooli["Employees"]!);

        acme["Employees"] = new[] { jane, john, jenny };

        Assert.Single((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!);
        Assert.Contains(acme, (IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!);

        Assert.Single((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!);
        Assert.Contains(acme, (IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!);

        Assert.Single((IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!);
        Assert.Contains(acme, (IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!);

        Assert.Equal(3, ((IEnumerable<IMetaObject>)acme["Employees"]!).Count());
        Assert.Contains(jane, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.Contains(john, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.Contains(jenny, (IEnumerable<IMetaObject>)acme["Employees"]!);

        Assert.Empty((IEnumerable<IMetaObject>)hooli["Employees"]!);

        acme["Employees"] = Array.Empty<IMetaObject>();

        Assert.Empty((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!);
        Assert.Empty((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!);
        Assert.Empty((IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!);

        Assert.Empty((IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.Empty((IEnumerable<IMetaObject>)hooli["Employees"]!);
    }

    [Fact]
    public void RemoveSingleActiveLink()
    {
        var meta = new MetaMeta();
        var @string = meta.AddUnit("String");
        var organization = meta.AddClass("Organization");
        var person = meta.AddClass("Person");
        meta.AddUnit(organization, @string, "Name");
        var employees = meta.AddManyToMany(organization, person, "Employee");

        var population = new MetaPopulation(meta);

        var acme = population.Build(organization, v => v["Name"] = "Acme");
        var hooli = population.Build(organization, v => v["Name"] = "Hooli");

        var jane = population.Build(person);
        var john = population.Build(person);
        var jenny = population.Build(person);

        acme["Employees"] = new[] { jane, john, jenny };

        acme.Remove(employees, jenny);

        Assert.Single((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!);
        Assert.Contains(acme, (IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!);

        Assert.Single((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!);
        Assert.Contains(acme, (IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!);

        Assert.Empty((IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!);

        Assert.Equal(2, ((IEnumerable<IMetaObject>)acme["Employees"]!).Count());
        Assert.Contains(jane, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.Contains(john, (IEnumerable<IMetaObject>)acme["Employees"]!);

        Assert.Empty((IEnumerable<IMetaObject>)hooli["Employees"]!);

        acme.Remove(employees, john);

        Assert.Single((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!);
        Assert.Contains(acme, (IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!);

        Assert.Empty((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!);

        Assert.Empty((IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!);

        Assert.Single((IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.Contains(jane, (IEnumerable<IMetaObject>)acme["Employees"]!);

        Assert.Empty((IEnumerable<IMetaObject>)hooli["Employees"]!);

        acme.Remove(employees, jane);

        Assert.Empty((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!);
        Assert.Empty((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!);
        Assert.Empty((IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!);

        Assert.Empty((IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.Empty((IEnumerable<IMetaObject>)hooli["Employees"]!);
    }

    [Fact]
    public void MultipleActiveLinks()
    {
        var meta = new MetaMeta();
        var @string = meta.AddUnit("String");
        var organization = meta.AddClass("Organization");
        var person = meta.AddClass("Person");
        meta.AddUnit(organization, @string, "Name");
        var employees = meta.AddManyToMany(organization, person, "Employee");

        var population = new MetaPopulation(meta);

        var acme = population.Build(organization, v => v["Name"] = "Acme");
        var hooli = population.Build(organization, v => v["Name"] = "Hooli");

        var jane = population.Build(person);
        var john = population.Build(person);
        var jenny = population.Build(person);

        acme.Add(employees, jane);
        acme.Add(employees, john);
        acme.Add(employees, jenny);

        hooli.Add(employees, jane);

        Assert.Equal(2, ((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!).Count());
        Assert.Contains(acme, (IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!);
        Assert.Contains(hooli, (IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!);

        Assert.Single((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!);
        Assert.Contains(acme, (IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!);

        Assert.Single((IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!);
        Assert.Contains(acme, (IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!);

        Assert.Equal(3, ((IEnumerable<IMetaObject>)acme["Employees"]!).Count());
        Assert.Contains(jane, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.Contains(john, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.Contains(jenny, (IEnumerable<IMetaObject>)acme["Employees"]!);

        Assert.Single((IEnumerable<IMetaObject>)hooli["Employees"]!);
        Assert.Contains(jane, (IEnumerable<IMetaObject>)hooli["Employees"]!);

        hooli.Add(employees, john);

        Assert.Equal(2, ((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!).Count());
        Assert.Contains(acme, (IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!);
        Assert.Contains(hooli, (IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!);

        Assert.Equal(2, ((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!).Count());
        Assert.Contains(acme, (IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!);
        Assert.Contains(hooli, (IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!);

        Assert.Single((IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!);
        Assert.Contains(acme, (IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!);

        Assert.Equal(3, ((IEnumerable<IMetaObject>)acme["Employees"]!).Count());
        Assert.Contains(jane, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.Contains(john, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.Contains(jenny, (IEnumerable<IMetaObject>)acme["Employees"]!);

        Assert.Equal(2, ((IEnumerable<IMetaObject>)hooli["Employees"]!).Count());
        Assert.Contains(jane, (IEnumerable<IMetaObject>)hooli["Employees"]!);
        Assert.Contains(john, (IEnumerable<IMetaObject>)hooli["Employees"]!);

        hooli.Add(employees, jenny);

        Assert.Equal(2, ((IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!).Count());
        Assert.Contains(acme, (IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!);
        Assert.Contains(hooli, (IEnumerable<IMetaObject>)jane["OrganizationsWhereEmployee"]!);

        Assert.Equal(2, ((IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!).Count());
        Assert.Contains(acme, (IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!);
        Assert.Contains(hooli, (IEnumerable<IMetaObject>)john["OrganizationsWhereEmployee"]!);

        Assert.Equal(2, ((IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!).Count());
        Assert.Contains(acme, (IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!);
        Assert.Contains(hooli, (IEnumerable<IMetaObject>)jenny["OrganizationsWhereEmployee"]!);

        Assert.Equal(3, ((IEnumerable<IMetaObject>)acme["Employees"]!).Count());
        Assert.Contains(jane, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.Contains(john, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.Contains(jenny, (IEnumerable<IMetaObject>)acme["Employees"]!);

        Assert.Equal(3, ((IEnumerable<IMetaObject>)hooli["Employees"]!).Count());
        Assert.Contains(jane, (IEnumerable<IMetaObject>)hooli["Employees"]!);
        Assert.Contains(john, (IEnumerable<IMetaObject>)hooli["Employees"]!);
        Assert.Contains(jenny, (IEnumerable<IMetaObject>)hooli["Employees"]!);
    }

    [Fact]
    public void DefaultRoleName()
    {
        var meta = new MetaMeta();
        var @string = meta.AddUnit("String");
        var organization = meta.AddClass("Organization");
        var person = meta.AddClass("Person");
        meta.AddUnit(organization, @string, "Name");
        var people = meta.AddManyToMany(organization, person);

        var population = new MetaPopulation(meta);

        var acme = population.Build(organization, v => v["Name"] = "Acme");

        var jane = population.Build(person);

        acme.Add(people, jane);

        Assert.Single((IEnumerable<IMetaObject>)jane["OrganizationsWherePerson"]!);
        Assert.Contains(acme, (IEnumerable<IMetaObject>)jane["OrganizationsWherePerson"]!);

        Assert.Single((IEnumerable<IMetaObject>)acme["Persons"]!);
        Assert.Contains(jane, (IEnumerable<IMetaObject>)acme["Persons"]!);
    }
}
