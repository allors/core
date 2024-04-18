namespace Allors.Core.Database.Meta
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// A role type with multiplicity one to many.
    /// </summary>
    public sealed class OneToManyRoleType : ToManyRoleType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneToManyRoleType"/> class.
        /// </summary>
        internal OneToManyRoleType(MetaPopulation metaPopulation, EmbeddedObject embeddedObject)
            : base(metaPopulation, embeddedObject)
        {
        }
    }
}
