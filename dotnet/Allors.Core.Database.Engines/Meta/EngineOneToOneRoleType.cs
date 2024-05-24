namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine role type handle with multiplicity one to one.
    /// </summary>
    public sealed class EngineOneToOneRoleType(EngineMeta engineMeta, MetaObject metaObject) : EngineToOneRoleType(engineMeta, metaObject)
    {
        /// <inheritdoc/>
        public override EngineCompositeAssociationType AssociationType { get; }
    }
}
