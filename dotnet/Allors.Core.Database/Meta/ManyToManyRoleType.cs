namespace Allors.Core.Database.Meta
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// A role type with multiplicity many to many.
    /// </summary>
    public sealed class ManyToManyRoleType : ToManyRoleType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManyToManyRoleType"/> class.
        /// </summary>
        internal ManyToManyRoleType(MetaPopulation metaPopulation, EmbeddedObject embeddedObject)
            : base(metaPopulation, embeddedObject)
        {
        }
    }
}
