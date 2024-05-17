namespace Allors.Core.Meta.Meta
{
    public interface IMetaAssociationType
    {
        MetaObjectType ObjectType { get; }

        IMetaRoleType RoleType { get; }

        string SingularName { get; }

        string PluralName { get; }

        string Name { get; }
    }
}
