namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta.Domain;

/// <summary>
/// An engine unit.
/// </summary>
public sealed class EnginesUnit(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesObjectType(enginesMeta, metaObject);
