namespace Allors.Core.Database.Meta
{
    using System;
    using Allors.Embedded.Domain;

    /// <summary>
    /// An association type for a unit role.
    /// </summary>
    public sealed class UnitAssociationType : AssociationType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitAssociationType"/> class.
        /// </summary>
        internal UnitAssociationType(Guid id, EmbeddedObject embeddedObject)
            : base(id, embeddedObject)
        {
        }
    }
}
