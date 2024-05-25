namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine association type.
    /// </summary>
    public abstract class EnginesAssociationType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesRelationEndType(enginesMeta, metaObject)
    {
    }
}
