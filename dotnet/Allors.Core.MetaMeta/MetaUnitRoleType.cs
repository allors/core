﻿namespace Allors.Core.MetaMeta;

using System;

public sealed class MetaUnitRoleType : IMetaRoleType
{
    internal MetaUnitRoleType(MetaMeta metaMeta, Guid id, MetaObjectType objectType, string singularName, string pluralName, string name)
    {
        this.MetaMeta = metaMeta;
        this.Id = id;
        this.ObjectType = objectType;
        this.SingularName = singularName;
        this.PluralName = pluralName;
        this.Name = name;
    }

    public MetaMeta MetaMeta { get; }

    public Guid Id { get; }

    IMetaAssociationType IMetaRoleType.AssociationType => this.AssociationType;

    public MetaUnitAssociationType AssociationType { get; internal set; } = null!;

    public MetaObjectType ObjectType { get; }

    public string SingularName { get; }

    public string PluralName { get; }

    public string Name { get; }

    void IMetaRoleType.Deconstruct(out IMetaAssociationType associationType, out IMetaRoleType roleType)
    {
        associationType = this.AssociationType;
        roleType = this;
    }

    public void Deconstruct(out MetaUnitAssociationType associationType, out MetaUnitRoleType roleType)
    {
        associationType = this.AssociationType;
        roleType = this;
    }

    public override string ToString()
    {
        return this.Name;
    }

    internal string SingularNameForAssociationType(MetaObjectType metaObjectType)
    {
        return $"{metaObjectType.Name}Where{this.SingularName}";
    }

    internal string PluralNameForAssociationType(MetaObjectType metaObjectType)
    {
        return $"{metaObjectType.Name.Pluralize()}Where{this.SingularName}";
    }
}
