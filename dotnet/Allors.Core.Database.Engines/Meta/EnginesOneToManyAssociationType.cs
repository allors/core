namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta;

/// <summary>
/// An engine association type handle with multiplicity one to many.
/// </summary>
public sealed class EnginesOneToManyAssociationType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesOneToAssociationType(enginesMeta, metaObject);
