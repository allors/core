namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta;

/// <summary>
/// An engine association type handle with multiplicity many.
/// </summary>
public abstract class EnginesManyToAssociationType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesCompositeAssociationType(enginesMeta, metaObject);
