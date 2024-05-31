namespace Allors.Core.Meta.Meta;

using System;

public sealed class MetaUnitAssociationType : IMetaAssociationType
{
    internal MetaUnitAssociationType(Guid id, MetaObjectType objectType, MetaUnitRoleType roleType, string singularName, string pluralName, string name)
    {
        this.Id = id;
        this.ObjectType = objectType;
        this.RoleType = roleType;
        this.SingularName = singularName;
        this.PluralName = pluralName;
        this.Name = name;
    }

    public Guid Id { get; }

    IMetaRoleType IMetaAssociationType.RoleType => this.RoleType;

    public MetaUnitRoleType RoleType { get; }

    public MetaObjectType ObjectType { get; }

    public string SingularName { get; }

    public string PluralName { get; }

    public string Name { get; }
}
