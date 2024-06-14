namespace Allors.Core.Meta.Tests;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;
using FluentAssertions;
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

        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(jane);
        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(john);
        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(jenny);

        jane["OrganizationWhereEmployee"].Should().Be(acme);
        john["OrganizationWhereEmployee"].Should().Be(acme);
        jenny["OrganizationWhereEmployee"].Should().Be(acme);
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

        ((IEnumerable<IMetaObject>)hooli["Employees"]!).Should().Contain(jane);

        Assert.DoesNotContain(jane, (IEnumerable<IMetaObject>)acme["Employees"]!);
        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(john);
        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(jenny);

        jane["OrganizationWhereEmployee"].Should().Be(hooli);

        Assert.NotEqual(acme, jane["OrganizationWhereEmployee"]);
        john["OrganizationWhereEmployee"].Should().Be(acme);
        jenny["OrganizationWhereEmployee"].Should().Be(acme);
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
        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(john);
        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(jenny);

        Assert.NotEqual(acme, jane["OrganizationWhereEmployee"]);
        john["OrganizationWhereEmployee"].Should().Be(acme);
        jenny["OrganizationWhereEmployee"].Should().Be(acme);

        acme.Remove(employees, john);

        Assert.DoesNotContain(jane, (IEnumerable<IMetaObject>)acme["Employees"]!);
        Assert.DoesNotContain(john, (IEnumerable<IMetaObject>)acme["Employees"]!);
        ((IEnumerable<IMetaObject>)acme["Employees"]!).Should().Contain(jenny);

        Assert.NotEqual(acme, jane["OrganizationWhereEmployee"]);
        Assert.NotEqual(acme, john["OrganizationWhereEmployee"]);
        jenny["OrganizationWhereEmployee"].Should().Be(acme);

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
