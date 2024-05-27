namespace Allors.Core.Meta.Meta;

public sealed class MetaUnitAssociationType : IMetaAssociationType
{
    internal MetaUnitAssociationType(MetaObjectType objectType, MetaUnitRoleType roleType, string singularName, string pluralName, string name)
    {
        this.ObjectType = objectType;
        this.RoleType = roleType;
        this.SingularName = singularName;
        this.PluralName = pluralName;
        this.Name = name;
    }

    IMetaRoleType IMetaAssociationType.RoleType => this.RoleType;

    public MetaUnitRoleType RoleType { get; }

    public MetaObjectType ObjectType { get; }

    public string SingularName { get; }

    public string PluralName { get; }

    public string Name { get; }
}
