namespace Allors.Core.Database.Meta
{
    using System;
    using Allors.Embedded.Domain;

    /// <summary>
    /// An association type with multiplicity one.
    /// </summary>
    public abstract class OneToAssociationType : CompositeAssociationType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneToAssociationType"/> class.
        /// </summary>
        protected OneToAssociationType(Guid id, EmbeddedObject embeddedObject)
            : base(id, embeddedObject)
        {
        }
    }
}
