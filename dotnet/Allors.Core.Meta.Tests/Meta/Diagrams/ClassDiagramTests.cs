namespace Allors.Core.Meta.Tests.Meta.Diagrams;

using System;
using Allors.Core.Meta.Meta;
using Allors.Core.Meta.Meta.Diagrams;
using Xunit;

public class ClassDiagramTests
{
    [Fact]
    public void Inheritance()
    {
        var meta = new MetaMeta();
        var domain = meta.AddDomain(Guid.NewGuid(), "Domain");
        var s1 = meta.AddInterface(domain, Guid.NewGuid(), "S1");
        var i1 = meta.AddInterface(domain, Guid.NewGuid(), "I1", s1);
        meta.AddClass(domain, Guid.NewGuid(), "C1", i1);

        var diagram = new ClassDiagram(meta).Render();

        Assert.Equal(
            @"classDiagram
    class C1
    I1 <|-- C1
    class I1
    S1 <|-- I1
    class S1
",
            diagram);
    }

    [Fact]
    public void Roles()
    {
        var meta = new MetaMeta();
        var domain = meta.AddDomain(Guid.NewGuid(), "Domain");
        var organization = meta.AddClass(domain, Guid.NewGuid(), "Organization");
        var person = meta.AddClass(domain, Guid.NewGuid(), "Person");
        meta.AddOneToManyRelation(domain, Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var diagram = new ClassDiagram(meta).Render();

        Assert.Equal(
            """
            classDiagram
                class Organization
                Organization o-- Person : Employees
                class Person

            """,
            diagram);
    }

    [Fact]
    public void InheritedRoles()
    {
        var meta = new MetaMeta();
        var domain = meta.AddDomain(Guid.NewGuid(), "Domain");
        var internalOrganization = meta.AddClass(domain, Guid.NewGuid(), "InternalOrganization");
        var organization = meta.AddClass(domain, Guid.NewGuid(), "Organization");
        var person = meta.AddClass(domain, Guid.NewGuid(), "Person");

        organization.AddDirectSupertype(internalOrganization);

        meta.AddOneToManyRelation(domain, Guid.NewGuid(), Guid.NewGuid(), internalOrganization, person, "Employee");
        meta.AddOneToManyRelation(domain, Guid.NewGuid(), Guid.NewGuid(), organization, person, "Customer");

        var diagram = new ClassDiagram(meta).Render();

        Assert.Equal(
            """
            classDiagram
                class InternalOrganization
                InternalOrganization o-- Person : Employees
                class Organization
                InternalOrganization <|-- Organization
                Organization o-- Person : Customers
                class Person

            """,
            diagram);
    }

    [Fact]
    public void Title()
    {
        var meta = new MetaMeta();

        var config = new ClassDiagram.Config { Title = "My Empty Diagram" };
        var diagram = new ClassDiagram(meta, config).Render();

        Assert.Equal(
            """
            ---
            title: My Empty Diagram
            ---
            classDiagram

            """,
            diagram);
    }

    [Fact]
    public void Multiplicity()
    {
        var meta = new MetaMeta();
        var domain = meta.AddDomain(Guid.NewGuid(), "Domain");
        var organization = meta.AddClass(domain, Guid.NewGuid(), "Organization");
        var person = meta.AddClass(domain, Guid.NewGuid(), "Person");
        meta.AddOneToManyRelation(domain, Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var config = new ClassDiagram.Config { OneMultiplicity = "1", ManyMultiplicity = "1..*" };
        var diagram = new ClassDiagram(meta, config).Render();

        Assert.Equal(
            """
            classDiagram
                class Organization
                Organization "1" o-- "1..*" Person : Employees
                class Person

            """,
            diagram);
    }

    [Fact]
    public void MultiplicityOne()
    {
        var meta = new MetaMeta();
        var domain = meta.AddDomain(Guid.NewGuid(), "Domain");
        var organization = meta.AddClass(domain, Guid.NewGuid(), "Organization");
        var person = meta.AddClass(domain, Guid.NewGuid(), "Person");
        meta.AddOneToManyRelation(domain, Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var config = new ClassDiagram.Config { OneMultiplicity = "one" };
        var diagram = new ClassDiagram(meta, config).Render();

        Assert.Equal(
            """
            classDiagram
                class Organization
                Organization "one" o-- Person : Employees
                class Person

            """,
            diagram);
    }

    [Fact]
    public void MultiplicityMany()
    {
        var meta = new MetaMeta();
        var domain = meta.AddDomain(Guid.NewGuid(), "Domain");
        var organization = meta.AddClass(domain, Guid.NewGuid(), "Organization");
        var person = meta.AddClass(domain, Guid.NewGuid(), "Person");
        meta.AddOneToManyRelation(domain, Guid.NewGuid(), Guid.NewGuid(), organization, person, "Employee");

        var config = new ClassDiagram.Config { ManyMultiplicity = "many" };
        var diagram = new ClassDiagram(meta, config).Render();

        Assert.Equal(
            """
            classDiagram
                class Organization
                Organization o-- "many" Person : Employees
                class Person

            """,
            diagram);
    }
}
