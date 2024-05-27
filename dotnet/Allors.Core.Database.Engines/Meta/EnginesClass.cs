namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta.Domain;

/// <summary>
/// An engine class.
/// </summary>
public sealed class EnginesClass(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesComposite(enginesMeta, metaObject);
