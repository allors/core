namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine composite role type.
    /// </summary>
    public abstract class EnginesCompositeRoleType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesRoleType(enginesMeta, metaObject)
    {
        /// <summary>
        /// The association type.
        /// </summary>
        public abstract EnginesCompositeAssociationType CompositeAssociationType { get; }
    }
}
