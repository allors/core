namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta.Domain;

/// <summary>
/// An engine association type handle for a dateTime role.
/// </summary>
public sealed class EnginesDateTimeAssociationType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesUnitAssociationType(enginesMeta, metaObject);
