namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine type.
    /// </summary>
    public abstract class EngineType(EngineMeta engineMeta, MetaObject metaObject) : EngineMetaObject(engineMeta, metaObject)
    {
    }
}
