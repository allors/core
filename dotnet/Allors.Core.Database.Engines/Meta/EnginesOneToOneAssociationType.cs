namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta;

/// <summary>
/// An engine association type handle with multiplicity one to one.
/// </summary>
public sealed class EnginesOneToOneAssociationType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesOneToAssociationType(enginesMeta, metaObject);
