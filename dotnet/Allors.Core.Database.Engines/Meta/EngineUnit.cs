namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine unit.
    /// </summary>
    public sealed class EngineUnit(EngineMeta engineMeta, MetaObject metaObject) : EngineObjectType(engineMeta, metaObject)
    {
    }
}
