namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta.Domain;

/// <summary>
/// An engine association type handle for a binary role.
/// </summary>
public sealed class EnginesBinaryAssociationType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesUnitAssociationType(enginesMeta, metaObject);
