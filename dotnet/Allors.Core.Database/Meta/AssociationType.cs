namespace Allors.Core.Database.Meta
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// An association type.
    /// </summary>
    public abstract class AssociationType : RelationEndType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssociationType"/> class.
        /// </summary>
        protected AssociationType(MetaPopulation metaPopulation, EmbeddedObject embeddedObject)
            : base(metaPopulation, embeddedObject)
        {
        }
    }
}
