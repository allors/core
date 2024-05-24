namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine association type.
    /// </summary>
    public abstract class EngineAssociationType(EngineMeta engineMeta, MetaObject metaObject) : EngineRelationEndType(engineMeta, metaObject)
    {
    }
}
