﻿namespace Allors.Core.MetaMeta;

using System;

public sealed class MetaOneToOneAssociationType : IMetaOneToAssociationType
{
    internal MetaOneToOneAssociationType(MetaMeta metaMeta, Guid id, MetaObjectType objectType, MetaOneToOneRoleType roleType, string singularName, string pluralName, string name)
    {
        this.MetaMeta = metaMeta;
        this.Id = id;
        this.ObjectType = objectType;
        this.RoleType = roleType;
        this.SingularName = singularName;
        this.PluralName = pluralName;
        this.Name = name;
    }

    public MetaMeta MetaMeta { get; }

    public Guid Id { get; }

    IMetaRoleType IMetaAssociationType.RoleType => this.RoleType;

    IMetaCompositeRoleType IMetaCompositeAssociationType.RoleType => this.RoleType;

    public MetaOneToOneRoleType RoleType { get; }

    public MetaObjectType ObjectType { get; }

    public string SingularName { get; }

    public string PluralName { get; }

    public string Name { get; }

    public bool IsOne => true;

    public bool IsMany => false;
}
