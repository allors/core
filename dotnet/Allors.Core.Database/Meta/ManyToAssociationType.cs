namespace Allors.Core.Database.Meta
{
    using System;
    using Allors.Embedded.Domain;

    /// <summary>
    /// An association type with multiplicity many.
    /// </summary>
    public abstract class ManyToAssociationType : CompositeAssociationType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManyToAssociationType"/> class.
        /// </summary>
        protected ManyToAssociationType(Guid id, EmbeddedObject embeddedObject)
            : base(id, embeddedObject)
        {
        }
    }
}
