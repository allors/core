namespace Allors.Core.Database.Meta
{
    using System;
    using Allors.Embedded.Domain;

    /// <summary>
    /// A type.
    /// </summary>
    public abstract class Type : MetaObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Type"/> class.
        /// </summary>
        protected Type(Guid id, EmbeddedObject embeddedObject)
            : base(id, embeddedObject)
        {
        }
    }
}
