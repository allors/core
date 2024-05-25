namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine association type handle with multiplicity one.
    /// </summary>
    public abstract class EnginesOneToAssociationType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesCompositeAssociationType(enginesMeta, metaObject)
    {
    }
}
