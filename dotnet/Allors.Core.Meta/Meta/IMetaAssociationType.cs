namespace Allors.Core.Meta.Meta;

using System;

public interface IMetaAssociationType
{
    MetaDomain Domain { get; }

    Guid Id { get; }

    MetaObjectType ObjectType { get; }

    IMetaRoleType RoleType { get; }

    string SingularName { get; }

    string PluralName { get; }

    string Name { get; }
}
