namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Database.Meta.Domain;
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine composite role type.
    /// </summary>
    public abstract class EnginesCompositeRoleType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesRoleType(enginesMeta, metaObject)
    {
        private EnginesComposite? composite;

        /// <summary>
        /// The association type.
        /// </summary>
        public abstract EnginesCompositeAssociationType CompositeAssociationType { get; }

        /// <summary>
        /// The composite.
        /// </summary>
        public EnginesComposite Composite => this.composite ??= this.EnginesMeta[(IComposite)this.MetaObject[this.M.RoleTypeObjectType]!];
    }
}
