namespace Allors.Core.Meta.Meta;

public interface IMetaRoleType
{
    IMetaAssociationType AssociationType { get; }

    MetaObjectType ObjectType { get; }

    string SingularName { get; }

    string PluralName { get; }

    string Name { get; }

    void Deconstruct(out IMetaAssociationType associationType, out IMetaRoleType roleType);
}
