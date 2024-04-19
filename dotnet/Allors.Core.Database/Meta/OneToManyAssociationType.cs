namespace Allors.Core.Database.Meta
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// An association type with multiplicity one to many.
    /// </summary>
    public sealed class OneToManyAssociationType : OneToAssociationType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneToManyAssociationType"/> class.
        /// </summary>
        internal OneToManyAssociationType(Meta meta, EmbeddedObject embeddedObject)
            : base(meta, embeddedObject)
        {
        }
    }
}
