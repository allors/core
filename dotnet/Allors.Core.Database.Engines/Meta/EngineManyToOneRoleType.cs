namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine role type handle with multiplicity many to one.
    /// </summary>
    public sealed class EngineManyToOneRoleType(EngineMeta engineMeta, MetaObject metaObject) : EngineToOneRoleType(engineMeta, metaObject)
    {
        /// <inheritdoc/>
        public override EngineCompositeAssociationType AssociationType { get; }
    }
}
