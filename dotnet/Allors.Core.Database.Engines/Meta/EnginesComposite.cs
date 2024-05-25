namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine composite.
    /// </summary>
    public abstract class EnginesComposite(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesObjectType(enginesMeta, metaObject)
    {
    }
}
