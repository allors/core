namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine association type handle with multiplicity one to one.
    /// </summary>
    public sealed class EngineOneToOneAssociationType(EngineMeta engineMeta, MetaObject metaObject) : EngineOneToAssociationType(engineMeta, metaObject)
    {
    }
}
