namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta.Domain;

/// <summary>
/// An engine type.
/// </summary>
public abstract class EnginesType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesMetaObject(enginesMeta, metaObject);
