namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine class.
    /// </summary>
    public sealed class EngineClass(EngineMeta engineMeta, MetaObject metaObject) : EngineComposite(engineMeta, metaObject)
    {
    }
}
