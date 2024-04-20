namespace Allors.Core.Database.Meta
{
    using System;
    using Allors.Embedded.Domain;

    /// <summary>
    /// An association type with multiplicity one to many.
    /// </summary>
    public sealed class OneToManyAssociationType : OneToAssociationType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneToManyAssociationType"/> class.
        /// </summary>
        internal OneToManyAssociationType(Guid id, EmbeddedObject embeddedObject)
            : base(id, embeddedObject)
        {
        }
    }
}
