namespace Allors.Core.Meta.Tests;

using System;
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
        var (employers, employees) = metaMeta.AddOneToManyRelation(Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var meta = new Meta(metaMeta);

        var acme = meta.Build(organization);
        var jane = meta.Build(person);
        var john = meta.Build(person);
        var jenny = meta.Build(person);

        acme.Add(employees, jane);
        acme.Add(employees, john);
        acme.Add(employees, jenny);

        acme[employees].Should().Contain(jane);
        acme[employees].Should().Contain(john);
        acme[employees].Should().Contain(jenny);

        jane[employers].Should().Be(acme);
        john[employers].Should().Be(acme);
        jenny[employers].Should().Be(acme);
    }

    [Fact]
    public void AddDifferentAssociation()
    {
        var metaMeta = new MetaMeta();

        var named = metaMeta.AddInterface(Guid.NewGuid(), "Named");
        var organization = metaMeta.AddClass(Guid.NewGuid(), "Organization", named);
        var person = metaMeta.AddClass(Guid.NewGuid(), "Person", named);
        var (employers, employees) = metaMeta.AddOneToManyRelation(Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

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

        hooli[employees].Should().Contain(jane);

        acme[employees].Should().NotContain(jane);
        acme[employees].Should().Contain(john);
        acme[employees].Should().Contain(jenny);

        jane[employers].Should().Be(hooli);

        jane[employers].Should().NotBe(acme);
        john[employers].Should().Be(acme);
        jenny[employers].Should().Be(acme);
    }

    [Fact]
    public void Remove()
    {
        var metaMeta = new MetaMeta();

        var organization = metaMeta.AddClass(Guid.NewGuid(), "Organization");
        var person = metaMeta.AddClass(Guid.NewGuid(), "Person");
        var (employers, employees) = metaMeta.AddOneToManyRelation(Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var meta = new Meta(metaMeta);

        var acme = meta.Build(organization);
        var jane = meta.Build(person);
        var john = meta.Build(person);
        var jenny = meta.Build(person);

        acme.Add(employees, jane);
        acme.Add(employees, john);
        acme.Add(employees, jenny);

        acme.Remove(employees, jane);

        acme[employees].Should().NotContain(jane);
        acme[employees].Should().Contain(john);
        acme[employees].Should().Contain(jenny);

        jane[employers].Should().NotBe(acme);
        john[employers].Should().Be(acme);
        jenny[employers].Should().Be(acme);

        acme.Remove(employees, john);

        acme[employees].Should().NotContain(jane);
        acme[employees].Should().NotContain(john);
        acme[employees].Should().Contain(jenny);

        jane[employers].Should().NotBe(acme);
        john[employers].Should().NotBe(acme);
        jenny[employers].Should().Be(acme);

        acme.Remove(employees, jenny);

        acme[employees].Should().NotContain(jane);
        acme[employees].Should().NotContain(john);
        acme[employees].Should().NotContain(jenny);

        jane[employers].Should().NotBe(acme);
        john[employers].Should().NotBe(acme);
        jenny[employers].Should().NotBe(acme);
    }

    [Fact]
    public void RemoveAll()
    {
        var metaMeta = new MetaMeta();

        var organization = metaMeta.AddClass(Guid.NewGuid(), "Organization");
        var person = metaMeta.AddClass(Guid.NewGuid(), "Person");
        var (employers, employees) = metaMeta.AddOneToManyRelation(Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var meta = new Meta(metaMeta);

        var acme = meta.Build(organization);
        var jane = meta.Build(person);
        var john = meta.Build(person);
        var jenny = meta.Build(person);

        acme.Add(employees, jane);
        acme.Add(employees, john);
        acme.Add(employees, jenny);

        acme[employees] = [];

        acme[employees].Should().NotContain(jane);
        acme[employees].Should().NotContain(john);
        acme[employees].Should().NotContain(jenny);

        jane[employers].Should().NotBe(acme);
        john[employers].Should().NotBe(acme);
        jenny[employers].Should().NotBe(acme);

        acme.Add(employees, jane);
        acme.Add(employees, john);
        acme.Add(employees, jenny);

        acme[employees] = [];

        acme[employees].Should().NotContain(jane);
        acme[employees].Should().NotContain(john);
        acme[employees].Should().NotContain(jenny);

        jane[employers].Should().NotBe(acme);
        john[employers].Should().NotBe(acme);
        jenny[employers].Should().NotBe(acme);

        acme.Add(employees, jane);
        acme.Add(employees, john);
        acme.Add(employees, jenny);

        acme[employees] = [];

        acme[employees].Should().NotContain(jane);
        acme[employees].Should().NotContain(john);
        acme[employees].Should().NotContain(jenny);

        jane[employers].Should().NotBe(acme);
        john[employers].Should().NotBe(acme);
        jenny[employers].Should().NotBe(acme);
    }
}
