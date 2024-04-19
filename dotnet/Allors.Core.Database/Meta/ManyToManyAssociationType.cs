namespace Allors.Core.Database.Meta
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// An association type with multiplicity many to many.
    /// </summary>
    public sealed class ManyToManyAssociationType : ManyToAssociationType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManyToManyAssociationType"/> class.
        /// </summary>
        internal ManyToManyAssociationType(Meta meta, EmbeddedObject embeddedObject)
            : base(meta, embeddedObject)
        {
        }
    }
}
