namespace Allors.Core.Database.Meta
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// An association type with multiplicity many to one.
    /// </summary>
    public sealed class ManyToOneAssociationType : ManyToAssociationType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManyToOneAssociationType"/> class.
        /// </summary>
        internal ManyToOneAssociationType(MetaPopulation metaPopulation, EmbeddedObject embeddedObject)
            : base(metaPopulation, embeddedObject)
        {
        }
    }
}
