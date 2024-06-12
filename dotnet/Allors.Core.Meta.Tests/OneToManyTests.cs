namespace Allors.Core.Meta.Tests;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;
using Xunit;

public class OneToManyTests
{
    [Fact]
    public void AddSameAssociation()
    {
        var metaMeta = new MetaMeta();

        var organization = metaMeta.AddClass(Guid.NewGuid(), "Organization");
        var person = metaMeta.AddClass(Guid.NewGuid(), "Person");
        var employees = metaMeta.AddOneToManyRelation(Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var meta = new Meta(metaMeta);

        var acme = meta.Build(organization);
        var jane = meta.Build(person);
        var john = meta.Build(person);
        var jenny = meta.Build(person);

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
        var metaMeta = new MetaMeta();

        var named = metaMeta.AddInterface(Guid.NewGuid(), "Named");
        var organization = metaMeta.AddClass(Guid.NewGuid(), "Organization", named);
        var person = metaMeta.AddClass(Guid.NewGuid(), "Person", named);
        var employees = metaMeta.AddOneToManyRelation(Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var meta = new Meta(metaMeta);

        var acme = meta.Build(organization);

        var jane = meta.Build(person);
        var john = meta.Build(person);
        var jenny = meta.Build(person);

        acme.Add(employees, jane);
        acme.Add(employees, john);
        acme.Add(employees, jenny);

        var hooli = meta.Build(organization);

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
        var metaMeta = new MetaMeta();

        var organization = metaMeta.AddClass(Guid.NewGuid(), "Organization");
        var person = metaMeta.AddClass(Guid.NewGuid(), "Person");
        var employees = metaMeta.AddOneToManyRelation(Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var meta = new Meta(metaMeta);

        var acme = meta.Build(organization);
        var jane = meta.Build(person);
        var john = meta.Build(person);
        var jenny = meta.Build(person);

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
        var metaMeta = new MetaMeta();

        var organization = metaMeta.AddClass(Guid.NewGuid(), "Organization");
        var person = metaMeta.AddClass(Guid.NewGuid(), "Person");
        var employees = metaMeta.AddOneToManyRelation(Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var meta = new Meta(metaMeta);

        var acme = meta.Build(organization);
        var jane = meta.Build(person);
        var john = meta.Build(person);
        var jenny = meta.Build(person);

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
