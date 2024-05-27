namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta.Domain;

/// <summary>
/// An engine role type.
/// </summary>
public abstract class EnginesRoleType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesRelationEndType(enginesMeta, metaObject)
{
    /// <summary>
    /// The association type.
    /// </summary>
    public abstract EnginesAssociationType AssociationType { get; }

    /// <summary>
    /// The object type.
    /// </summary>
    public abstract EnginesObjectType ObjectType { get; }
}
