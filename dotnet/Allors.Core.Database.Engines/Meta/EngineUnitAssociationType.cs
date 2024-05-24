namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine association type handle for a unit role.
    /// </summary>
    public sealed class EngineUnitAssociationType(EngineMeta engineMeta, MetaObject metaObject) : EngineAssociationType(engineMeta, metaObject)
    {
    }
}
