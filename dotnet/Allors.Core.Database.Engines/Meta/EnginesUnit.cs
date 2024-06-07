namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta;

/// <summary>
/// An engine unit.
/// </summary>
public sealed class EnginesUnit(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesObjectType(enginesMeta, metaObject);
