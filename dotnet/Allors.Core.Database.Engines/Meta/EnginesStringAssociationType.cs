namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta.Domain;

/// <summary>
/// An engine association type handle for a string role.
/// </summary>
public sealed class EnginesStringAssociationType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesUnitAssociationType(enginesMeta, metaObject);
