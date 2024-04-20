namespace Allors.Core.Database.Meta
{
    using System;
    using Allors.Embedded.Domain;

    /// <summary>
    /// An association type with multiplicity many to one.
    /// </summary>
    public sealed class ManyToOneAssociationType : ManyToAssociationType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManyToOneAssociationType"/> class.
        /// </summary>
        internal ManyToOneAssociationType(Guid id, EmbeddedObject embeddedObject)
            : base(id, embeddedObject)
        {
        }
    }
}
