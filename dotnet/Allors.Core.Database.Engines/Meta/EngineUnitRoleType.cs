namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine unit role type.
    /// </summary>
    public sealed class EngineUnitRoleType(EngineMeta engineMeta, MetaObject metaObject) : EngineRoleType(engineMeta, metaObject)
    {
    }
}
