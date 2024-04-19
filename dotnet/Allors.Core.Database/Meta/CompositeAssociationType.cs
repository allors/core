namespace Allors.Core.Database.Meta
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// An association type for a composite role.
    /// </summary>
    public abstract class CompositeAssociationType : AssociationType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeAssociationType"/> class.
        /// </summary>
        protected CompositeAssociationType(Meta meta, EmbeddedObject embeddedObject)
            : base(meta, embeddedObject)
        {
        }
    }
}
