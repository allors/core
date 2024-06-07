namespace Allors.Core.Meta.Tests.Static;

using System;
using Allors.Core.MetaMeta;

public class Meta
{
    public Meta()
    {
        this.MetaMeta = new MetaMeta();

        var m = this.MetaMeta;

        this.Domain = m.AddDomain(Guid.NewGuid(), "Domain");

        MetaDomain d = this.Domain;

        this.String = m.AddUnit(d, Guid.NewGuid(), "String");

        this.I1 = m.AddInterface(d, Guid.NewGuid(), "I1");
        this.I2 = m.AddInterface(d, Guid.NewGuid(), "I2");
        this.I12 = m.AddInterface(d, Guid.NewGuid(), "I12");

        this.C1 = m.AddClass(d, Guid.NewGuid(), "C1");
        this.C2 = m.AddClass(d, Guid.NewGuid(), "C2");
        this.C3 = m.AddClass(this.Domain, Guid.NewGuid(), "C3");
        this.C4 = m.AddClass(d, Guid.NewGuid(), "C4");

        m.AddInheritance(d, Guid.NewGuid(), this.I1, this.I12);
        m.AddInheritance(d, Guid.NewGuid(), this.I2, this.I12);

        m.AddInheritance(d, Guid.NewGuid(), this.C1, this.I1);
        m.AddInheritance(d, Guid.NewGuid(), this.C2, this.I2);

        (_, this.I1AllorsString) = m.AddUnitRelation(this.Domain, Guid.NewGuid(), Guid.NewGuid(), this.I1, this.String, "I1AllorsString");
        (_, this.C1AllorsString) = m.AddUnitRelation(this.Domain, Guid.NewGuid(), Guid.NewGuid(), this.C1, this.String, "C1AllorsString");
        (_, this.C2AllorsString) = m.AddUnitRelation(this.Domain, Guid.NewGuid(), Guid.NewGuid(), this.C2, this.String, "C2AllorsString");
        (_, this.C3AllorsString) = m.AddUnitRelation(this.Domain, Guid.NewGuid(), Guid.NewGuid(), this.C3, this.String, "C3AllorsString");
        (_, this.C4AllorsString) = m.AddUnitRelation(this.Domain, Guid.NewGuid(), Guid.NewGuid(), this.C4, this.String, "C4AllorsString");

        (this.C1WhereC1OneToOne, this.C1C1OneToOne) = m.AddOneToOneRelation(this.Domain, Guid.NewGuid(), Guid.NewGuid(), this.C1, this.C1, "C1OneToOne");
        (this.C1WhereI1OneToOne, this.C1I1OneToOne) = m.AddOneToOneRelation(this.Domain, Guid.NewGuid(), Guid.NewGuid(), this.C1, this.I1, "I1OneToOne");
        (this.C1WhereC2OneToOne, this.C1C2OneToOne) = m.AddOneToOneRelation(this.Domain, Guid.NewGuid(), Guid.NewGuid(), this.C1, this.C2, "C2OneToOne");
        (this.C1WhereI2OneToOne, this.C1I2OneToOne) = m.AddOneToOneRelation(this.Domain, Guid.NewGuid(), Guid.NewGuid(), this.C1, this.I2, "I2OneToOne");
        (this.C1sWhereC1ManyToOne, this.C1C1ManyToOne) = m.AddManyToOneRelation(this.Domain, Guid.NewGuid(), Guid.NewGuid(), this.C1, this.C1, "C1ManyToOne");
        (this.C1WhereC1C1one2many, this.C1C1OneToManies) = m.AddOneToManyRelation(this.Domain, Guid.NewGuid(), Guid.NewGuid(), this.C1, this.C1, "C1OneToMany");
    }

    public MetaMeta MetaMeta { get; }

    public MetaDomain Domain { get; }

    public MetaObjectType String { get; }

    public MetaObjectType C1 { get; }

    public MetaOneToOneRoleType C1C1OneToOne { get; }

    public MetaOneToOneAssociationType C1WhereC1OneToOne { get; }

    public MetaOneToOneRoleType C1I1OneToOne { get; }

    public MetaOneToOneAssociationType C1WhereI1OneToOne { get; }

    public MetaOneToOneRoleType C1C2OneToOne { get; }

    public MetaOneToOneAssociationType C1WhereC2OneToOne { get; }

    public MetaOneToOneRoleType C1I2OneToOne { get; }

    public MetaOneToOneAssociationType C1WhereI2OneToOne { get; }

    public MetaManyToOneRoleType C1C1ManyToOne { get; }

    public MetaManyToOneAssociationType C1sWhereC1ManyToOne { get; }

    public MetaOneToManyRoleType C1C1OneToManies { get; }

    public MetaOneToManyAssociationType C1WhereC1C1one2many { get; }

    public MetaObjectType C2 { get; }

    public MetaObjectType C3 { get; }

    public MetaObjectType C4 { get; }

    public MetaObjectType I1 { get; }

    public MetaObjectType I2 { get; }

    public MetaObjectType I12 { get; }

    public MetaUnitRoleType I1AllorsString { get; }

    public MetaUnitRoleType C1AllorsString { get; }

    public MetaUnitRoleType C2AllorsString { get; }

    public MetaUnitRoleType C3AllorsString { get; }

    public MetaUnitRoleType C4AllorsString { get; }
}
