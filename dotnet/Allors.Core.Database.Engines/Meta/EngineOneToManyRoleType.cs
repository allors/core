namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine role type handle with multiplicity one to many.
    /// </summary>
    public sealed class EngineOneToManyRoleType(EngineMeta engineMeta, MetaObject metaObject) : EngineToManyRoleType(engineMeta, metaObject)
    {
        /// <inheritdoc/>
        public override EngineCompositeAssociationType AssociationType { get; }
    }
}
