namespace Allors.Core.Meta.Meta;

using System;

public interface IMetaAssociationType
{
    Guid Id { get; }

    MetaObjectType ObjectType { get; }

    IMetaRoleType RoleType { get; }

    string SingularName { get; }

    string PluralName { get; }

    string Name { get; }
}
