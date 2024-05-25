namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine unit role type.
    /// </summary>
    public sealed class EnginesUnitRoleType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesRoleType(enginesMeta, metaObject)
    {
    }
}
