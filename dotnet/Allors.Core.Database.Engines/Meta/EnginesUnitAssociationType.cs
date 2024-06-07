namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta;

/// <summary>
/// An engine association type handle for a unit role.
/// </summary>
public abstract class EnginesUnitAssociationType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesAssociationType(enginesMeta, metaObject);
