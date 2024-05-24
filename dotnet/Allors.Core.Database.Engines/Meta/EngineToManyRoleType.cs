namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine role type handle with a multiplicity many.
    /// </summary>
    public abstract class EngineToManyRoleType(EngineMeta engineMeta, MetaObject metaObject) : EngineCompositeRoleType(engineMeta, metaObject)
    {
    }
}
