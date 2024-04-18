namespace Allors.Core.Database.Meta
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// An association type with multiplicity one.
    /// </summary>
    public abstract class OneToAssociationType : CompositeAssociationType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneToAssociationType"/> class.
        /// </summary>
        protected OneToAssociationType(MetaPopulation metaPopulation, EmbeddedObject embeddedObject)
            : base(metaPopulation, embeddedObject)
        {
        }
    }
}
