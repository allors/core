namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine association type.
    /// </summary>
    public abstract class EngineCompositeAssociationType(EngineMeta engineMeta, MetaObject metaObject) : EngineAssociationType(engineMeta, metaObject)
    {
    }
}
