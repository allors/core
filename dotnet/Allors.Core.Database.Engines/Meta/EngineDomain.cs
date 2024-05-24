namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Database.Meta;
    using Allors.Core.Database.Meta.Domain;
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine domain.
    /// </summary>
    public class EngineDomain(EngineMeta engineMeta, MetaObject metaObject) : EngineMetaObject(engineMeta, metaObject)
    {
    }
}
