namespace Allors.Core.MetaMeta.Tests.Diagrams;

using System;
using Allors.Core.MetaMeta.Diagrams;
using FluentAssertions;
using Xunit;

public class ClassDiagramTests
{
    [Fact]
    public void Inheritance()
    {
        var meta = new MetaMeta();

        var s1 = meta.AddInterface(Guid.NewGuid(), "S1");
        var i1 = meta.AddInterface(Guid.NewGuid(), "I1", s1);
        meta.AddClass(Guid.NewGuid(), "C1", i1);

        new ClassDiagram(meta)
            .Render()
            .Should()
            .Be(@"classDiagram
    class C1
    I1 <|-- C1
    class I1
    S1 <|-- I1
    class S1
");
    }

    [Fact]
    public void Roles()
    {
        var meta = new MetaMeta();

        var organization = meta.AddClass(Guid.NewGuid(), "Organization");
        var person = meta.AddClass(Guid.NewGuid(), "Person");
        meta.AddOneToManyRelation(Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        new ClassDiagram(meta)
            .Render()
            .Should()
            .Be("""
            classDiagram
                class Organization
                Organization o-- Person : Employees
                class Person

            """);
    }

    [Fact]
    public void InheritedRoles()
    {
        var meta = new MetaMeta();

        var internalOrganization = meta.AddClass(Guid.NewGuid(), "InternalOrganization");
        var organization = meta.AddClass(Guid.NewGuid(), "Organization");
        var person = meta.AddClass(Guid.NewGuid(), "Person");

        meta.AddInheritance(Guid.NewGuid(), organization, internalOrganization);

        meta.AddOneToManyRelation(Guid.NewGuid(), Guid.NewGuid(), internalOrganization, person, "Employee");
        meta.AddOneToManyRelation(Guid.NewGuid(), Guid.NewGuid(), organization, person, "Customer");

        new ClassDiagram(meta)
            .Render()
            .Should()
            .Be("""
            classDiagram
                class InternalOrganization
                InternalOrganization o-- Person : Employees
                class Organization
                InternalOrganization <|-- Organization
                Organization o-- Person : Customers
                class Person

            """);
    }

    [Fact]
    public void Title()
    {
        var meta = new MetaMeta();

        var config = new ClassDiagram.Config { Title = "My Empty Diagram" };
        new ClassDiagram(meta, config)
            .Render()
            .Should()
            .Be("""
            ---
            title: My Empty Diagram
            ---
            classDiagram

            """);
    }

    [Fact]
    public void Multiplicity()
    {
        var meta = new MetaMeta();

        var organization = meta.AddClass(Guid.NewGuid(), "Organization");
        var person = meta.AddClass(Guid.NewGuid(), "Person");
        meta.AddOneToManyRelation(Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var config = new ClassDiagram.Config { OneMultiplicity = "1", ManyMultiplicity = "1..*" };
        new ClassDiagram(meta, config)
            .Render()
            .Should()
            .Be("""
            classDiagram
                class Organization
                Organization "1" o-- "1..*" Person : Employees
                class Person

            """);
    }

    [Fact]
    public void MultiplicityOne()
    {
        var meta = new MetaMeta();

        var organization = meta.AddClass(Guid.NewGuid(), "Organization");
        var person = meta.AddClass(Guid.NewGuid(), "Person");
        meta.AddOneToManyRelation(Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var config = new ClassDiagram.Config { OneMultiplicity = "one" };
        new ClassDiagram(meta, config)
            .Render()
            .Should()
            .Be("""
            classDiagram
                class Organization
                Organization "one" o-- Person : Employees
                class Person

            """);
    }

    [Fact]
    public void MultiplicityMany()
    {
        var meta = new MetaMeta();

        var organization = meta.AddClass(Guid.NewGuid(), "Organization");
        var person = meta.AddClass(Guid.NewGuid(), "Person");
        meta.AddOneToManyRelation(Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var config = new ClassDiagram.Config { ManyMultiplicity = "many" };
        new ClassDiagram(meta, config)
            .Render()
            .Should()
            .Be("""
            classDiagram
                class Organization
                Organization o-- "many" Person : Employees
                class Person

            """);
    }
}
