namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine association type handle with multiplicity one to many.
    /// </summary>
    public sealed class EngineOneToManyAssociationType(EngineMeta engineMeta, MetaObject metaObject) : EngineOneToAssociationType(engineMeta, metaObject)
    {
    }
}
