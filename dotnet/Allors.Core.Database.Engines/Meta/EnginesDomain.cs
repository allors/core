namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta;

/// <summary>
/// An engine domain.
/// </summary>
public class EnginesDomain(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesMetaObject(enginesMeta, metaObject);
