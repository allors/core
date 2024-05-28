namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta.Domain;

/// <summary>
/// An engine role type.
/// </summary>
public abstract class EnginesRoleType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesRelationEndType(enginesMeta, metaObject)
{
    private string? name;

    /// <summary>
    /// The association type.
    /// </summary>
    public abstract EnginesAssociationType AssociationType { get; }

    /// <summary>
    /// The object type.
    /// </summary>
    public abstract EnginesObjectType ObjectType { get; }

    /// <summary>
    /// The name.
    /// </summary>
    public string Name => this.name ??= (string)this.MetaObject[this.M.RoleTypeSingularName]!;
}
