namespace Allors.Core.Database.Meta
{
    using System;
    using Allors.Embedded.Domain;

    /// <summary>
    /// A unit.
    /// </summary>
    public sealed class Unit : ObjectType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Unit"/> class.
        /// </summary>
        public Unit(Guid id, EmbeddedObject embeddedObject)
            : base(id, embeddedObject)
        {
        }
    }
}
