﻿namespace Allors.Core.Meta.Meta;

public sealed class MetaOneToManyAssociationType : IMetaOneToAssociationType
{
    internal MetaOneToManyAssociationType(MetaObjectType objectType, MetaOneToManyRoleType roleType, string singularName, string pluralName, string name)
    {
        this.ObjectType = objectType;
        this.RoleType = roleType;
        this.SingularName = singularName;
        this.PluralName = pluralName;
        this.Name = name;
    }

    IMetaRoleType IMetaAssociationType.RoleType => this.RoleType;

    IMetaCompositeRoleType IMetaCompositeAssociationType.RoleType => this.RoleType;

    public MetaOneToManyRoleType RoleType { get; }

    public MetaObjectType ObjectType { get; }

    public string SingularName { get; }

    public string PluralName { get; }

    public string Name { get; }

    public bool IsOne => true;

    public bool IsMany => false;
}
