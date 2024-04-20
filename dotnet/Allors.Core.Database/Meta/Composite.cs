namespace Allors.Core.Database.Meta
{
    using System;
    using Allors.Embedded.Domain;

    /// <summary>
    /// An object type.
    /// </summary>
    public abstract class Composite : ObjectType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Composite"/> class.
        /// </summary>
        protected Composite(Guid id, EmbeddedObject embeddedObject)
            : base(id, embeddedObject)
        {
        }
    }
}
