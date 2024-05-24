namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine role type.
    /// </summary>
    public abstract class EngineRoleType(EngineMeta engineMeta, MetaObject metaObject) : EngineRelationEndType(engineMeta, metaObject)
    {
    }
}
