namespace Allors.Core.Meta.Tests;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Allors.Core.Meta;
using Allors.Core.Meta.Meta;
using Xunit;

public class OneToManyTests
{
    [Fact]
    public void AddSameAssociation()
    {
        var meta = new MetaMeta();
        var domain = meta.AddDomain(Guid.NewGuid(), "Domain");
        var organization = meta.AddClass(domain, Guid.NewGuid(), "Organization");
        var person = meta.AddClass(domain, Guid.NewGuid(), "Person");
        var employees = meta.AddOneToManyRelation(domain, Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var population = new MetaPopulation(meta);

        var acme = population.Build(organization);
        var jane = population.Build(person);
        var john = population.Build(person);
        var jenny = population.Build(person);

        acme.Add(employees, jane);
        acme.Add(employees, john);
        acme.Add(employees, jenny);

        Assert.Contains(jane, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.Contains(john, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.Contains(jenny, (IEnumerable<IMetaObject>)acme["Employees"]!);

        Assert.Equal(acme, jane["OrganizationWhereEmployee"]);
        Assert.Equal(acme, john["OrganizationWhereEmployee"]);
        Assert.Equal(acme, jenny["OrganizationWhereEmployee"]);
    }

    [Fact]
    public void AddDifferentAssociation()
    {
        var meta = new MetaMeta();
        var domain = meta.AddDomain(Guid.NewGuid(), "Domain");
        var named = meta.AddInterface(domain, Guid.NewGuid(), "Named");
        var organization = meta.AddClass(domain, Guid.NewGuid(), "Organization", named);
        var person = meta.AddClass(domain, Guid.NewGuid(), "Person", named);
        var employees = meta.AddOneToManyRelation(domain, Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var population = new MetaPopulation(meta);

        var acme = population.Build(organization);

        var jane = population.Build(person);
        var john = population.Build(person);
        var jenny = population.Build(person);

        acme.Add(employees, jane);
        acme.Add(employees, john);
        acme.Add(employees, jenny);

        var hooli = population.Build(organization);

        hooli.Add(employees, jane);

        Assert.Contains(jane, (IEnumerable<IMetaObject>)hooli["Employees"]!);

        Assert.DoesNotContain(jane, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.Contains(john, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.Contains(jenny, (IEnumerable<IMetaObject>)acme["Employees"]!);

        Assert.Equal(hooli, jane["OrganizationWhereEmployee"]);

        Assert.NotEqual(acme, jane["OrganizationWhereEmployee"]);
        Assert.Equal(acme, john["OrganizationWhereEmployee"]);
        Assert.Equal(acme, jenny["OrganizationWhereEmployee"]);
    }

    [Fact]
    public void Remove()
    {
        var meta = new MetaMeta();
        var domain = meta.AddDomain(Guid.NewGuid(), "Domain");
        var organization = meta.AddClass(domain, Guid.NewGuid(), "Organization");
        var person = meta.AddClass(domain, Guid.NewGuid(), "Person");
        var employees = meta.AddOneToManyRelation(domain, Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var population = new MetaPopulation(meta);

        var acme = population.Build(organization);
        var jane = population.Build(person);
        var john = population.Build(person);
        var jenny = population.Build(person);

        acme.Add(employees, jane);
        acme.Add(employees, john);
        acme.Add(employees, jenny);

        acme.Remove(employees, jane);

        Assert.DoesNotContain(jane, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.Contains(john, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.Contains(jenny, (IEnumerable<IMetaObject>)acme["Employees"]!);

        Assert.NotEqual(acme, jane["OrganizationWhereEmployee"]);
        Assert.Equal(acme, john["OrganizationWhereEmployee"]);
        Assert.Equal(acme, jenny["OrganizationWhereEmployee"]);

        acme.Remove(employees, john);

        Assert.DoesNotContain(jane, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.DoesNotContain(john, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.Contains(jenny, (IEnumerable<IMetaObject>)acme["Employees"]!);

        Assert.NotEqual(acme, jane["OrganizationWhereEmployee"]);
        Assert.NotEqual(acme, john["OrganizationWhereEmployee"]);
        Assert.Equal(acme, jenny["OrganizationWhereEmployee"]);

        acme.Remove(employees, jenny);

        Assert.DoesNotContain(jane, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.DoesNotContain(john, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.DoesNotContain(jenny, (IEnumerable<IMetaObject>)acme["Employees"]!);

        Assert.NotEqual(acme, jane["OrganizationWhereEmployee"]);
        Assert.NotEqual(acme, john["OrganizationWhereEmployee"]);
        Assert.NotEqual(acme, jenny["OrganizationWhereEmployee"]);
    }

    [Fact]
    public void RemoveAll()
    {
        var meta = new MetaMeta();
        var domain = meta.AddDomain(Guid.NewGuid(), "Domain");
        var organization = meta.AddClass(domain, Guid.NewGuid(), "Organization");
        var person = meta.AddClass(domain, Guid.NewGuid(), "Person");
        var employees = meta.AddOneToManyRelation(domain, Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var population = new MetaPopulation(meta);

        var acme = population.Build(organization);
        var jane = population.Build(person);
        var john = population.Build(person);
        var jenny = population.Build(person);

        acme.Add(employees, jane);
        acme.Add(employees, john);
        acme.Add(employees, jenny);

        acme["Employees"] = null;

        Assert.DoesNotContain(jane, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.DoesNotContain(john, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.DoesNotContain(jenny, (IEnumerable<IMetaObject>)acme["Employees"]!);

        Assert.NotEqual(acme, jane["OrganizationWhereEmployee"]);
        Assert.NotEqual(acme, john["OrganizationWhereEmployee"]);
        Assert.NotEqual(acme, jenny["OrganizationWhereEmployee"]);

        acme.Add(employees, jane);
        acme.Add(employees, john);
        acme.Add(employees, jenny);

        acme["Employees"] = Array.Empty<IMetaObject>();

        Assert.DoesNotContain(jane, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.DoesNotContain(john, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.DoesNotContain(jenny, (IEnumerable<IMetaObject>)acme["Employees"]!);

        Assert.NotEqual(acme, jane["OrganizationWhereEmployee"]);
        Assert.NotEqual(acme, john["OrganizationWhereEmployee"]);
        Assert.NotEqual(acme, jenny["OrganizationWhereEmployee"]);

        acme.Add(employees, jane);
        acme.Add(employees, john);
        acme.Add(employees, jenny);

        acme["Employees"] = ImmutableHashSet<IMetaObject>.Empty;

        Assert.DoesNotContain(jane, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.DoesNotContain(john, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.DoesNotContain(jenny, (IEnumerable<IMetaObject>)acme["Employees"]!);

        Assert.NotEqual(acme, jane["OrganizationWhereEmployee"]);
        Assert.NotEqual(acme, john["OrganizationWhereEmployee"]);
        Assert.NotEqual(acme, jenny["OrganizationWhereEmployee"]);
    }
}
