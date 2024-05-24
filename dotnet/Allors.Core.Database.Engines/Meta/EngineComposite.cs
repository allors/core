namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine composite.
    /// </summary>
    public abstract class EngineComposite(EngineMeta engineMeta, MetaObject metaObject) : EngineObjectType(engineMeta, metaObject)
    {
    }
}
