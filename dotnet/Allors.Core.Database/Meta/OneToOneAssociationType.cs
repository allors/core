namespace Allors.Core.Database.Meta
{
    using System;
    using Allors.Embedded.Domain;

    /// <summary>
    /// An association type with multiplicity one to one.
    /// </summary>
    public sealed class OneToOneAssociationType : OneToAssociationType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneToOneAssociationType"/> class.
        /// </summary>
        internal OneToOneAssociationType(Guid id, EmbeddedObject embeddedObject)
            : base(id, embeddedObject)
        {
        }
    }
}
