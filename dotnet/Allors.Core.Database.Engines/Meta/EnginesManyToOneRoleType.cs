namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine role type handle with multiplicity many to one.
    /// </summary>
    public sealed class EnginesManyToOneRoleType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesToOneRoleType(enginesMeta, metaObject)
    {
        private EnginesManyToOneAssociationType? associationType;

        /// <inheritdoc/>
        public override EnginesCompositeAssociationType CompositeAssociationType => this.ManyToOneAssociationType;

        /// <summary>
        /// The association type.
        /// </summary>
        public EnginesManyToOneAssociationType ManyToOneAssociationType => this.associationType ??= (EnginesManyToOneAssociationType)this.EnginesMeta[this.MetaObject[this.M.RoleTypeAssociationType]!];
    }
}
