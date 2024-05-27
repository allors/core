namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta.Domain;

/// <summary>
/// An engine interface.
/// </summary>
public sealed class EnginesInterface(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesComposite(enginesMeta, metaObject)
{
}
