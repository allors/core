namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine role type handle with a multiplicity one.
    /// </summary>
    public abstract class EngineToOneRoleType(EngineMeta engineMeta, MetaObject metaObject) : EngineCompositeRoleType(engineMeta, metaObject)
    {
    }
}
