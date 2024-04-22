namespace Allors.Core.Database.Meta
{
    using System;
    using Allors.Embedded.Domain;

    /// <summary>
    /// A unit.
    /// </summary>
    public sealed class UnitRole : ObjectType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitRole"/> class.
        /// </summary>
        public UnitRole(Guid id, EmbeddedObject embeddedObject)
            : base(id, embeddedObject)
        {
        }
    }
}
