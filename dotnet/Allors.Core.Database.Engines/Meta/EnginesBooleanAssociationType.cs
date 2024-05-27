namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta.Domain;

/// <summary>
/// An engine association type handle for a boolean role.
/// </summary>
public sealed class EnginesBooleanAssociationType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesUnitAssociationType(enginesMeta, metaObject);
