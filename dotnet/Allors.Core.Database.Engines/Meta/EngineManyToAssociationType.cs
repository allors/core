namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine association type handle with multiplicity many.
    /// </summary>
    public abstract class EngineManyToAssociationType(EngineMeta engineMeta, MetaObject metaObject) : EngineCompositeAssociationType(engineMeta, metaObject)
    {
    }
}
