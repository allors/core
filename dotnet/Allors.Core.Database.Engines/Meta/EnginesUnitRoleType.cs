namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta;

/// <summary>
/// An engine unit role type.
/// </summary>
public abstract class EnginesUnitRoleType(EnginesMeta enginesMeta, MetaObject metaObject)
    : EnginesRoleType(enginesMeta, metaObject);
