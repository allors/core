namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine composite role type.
    /// </summary>
    public abstract class EngineCompositeRoleType(EngineMeta engineMeta, MetaObject metaObject) : EngineRoleType(engineMeta, metaObject)
    {
        /// <summary>
        /// The association type.
        /// </summary>
        public abstract EngineCompositeAssociationType AssociationType { get; }
    }
}
