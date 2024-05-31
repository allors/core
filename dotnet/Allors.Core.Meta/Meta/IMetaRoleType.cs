namespace Allors.Core.Meta.Meta;

using System;

public interface IMetaRoleType
{
    Guid Id { get; }

    IMetaAssociationType AssociationType { get; }

    MetaObjectType ObjectType { get; }

    string SingularName { get; }

    string PluralName { get; }

    string Name { get; }

    void Deconstruct(out IMetaAssociationType associationType, out IMetaRoleType roleType);
}
