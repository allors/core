namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine interface.
    /// </summary>
    public sealed class EngineInterface(EngineMeta engineMeta, MetaObject metaObject) : EngineComposite(engineMeta, metaObject)
    {
    }
}
