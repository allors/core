namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine association type handle with multiplicity many to one.
    /// </summary>
    public sealed class EnginesManyToOneAssociationType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesManyToAssociationType(enginesMeta, metaObject)
    {
    }
}
