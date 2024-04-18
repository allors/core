namespace Allors.Core.Database.Meta
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// A role type with multiplicity one to one.
    /// </summary>
    public sealed class OneToOneRoleType : ToOneRoleType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneToOneRoleType"/> class.
        /// </summary>
        internal OneToOneRoleType(MetaPopulation metaPopulation, EmbeddedObject embeddedObject)
            : base(metaPopulation, embeddedObject)
        {
        }
    }
}
