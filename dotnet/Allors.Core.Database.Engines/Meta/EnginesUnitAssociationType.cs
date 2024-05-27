namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta.Domain;

/// <summary>
/// An engine association type handle for a unit role.
/// </summary>
public sealed class EnginesUnitAssociationType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesAssociationType(enginesMeta, metaObject)
{
}
