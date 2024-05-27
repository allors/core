namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta.Domain;

/// <summary>
/// An engine association type handle for an integer role.
/// </summary>
public sealed class EnginesIntegerAssociationType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesUnitAssociationType(enginesMeta, metaObject);
