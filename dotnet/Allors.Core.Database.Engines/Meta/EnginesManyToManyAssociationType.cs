namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta.Domain;

/// <summary>
/// An engine association type handle with multiplicity many to many.
/// </summary>
public sealed class EnginesManyToManyAssociationType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesManyToAssociationType(enginesMeta, metaObject)
{
}
