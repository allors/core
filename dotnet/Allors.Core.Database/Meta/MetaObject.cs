namespace Allors.Core.Database.Meta
{
    using System;
    using Allors.Embedded.Domain;

    /// <summary>
    /// A meta object.
    /// </summary>
    public abstract class MetaObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetaObject"/> class.
        /// </summary>
        protected MetaObject(Guid id, EmbeddedObject embeddedObject)
        {
            this.Id = id;
            this.EmbeddedObject = embeddedObject;
        }

        /// <summary>
        /// The id.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// The embedded object.
        /// </summary>
        public EmbeddedObject EmbeddedObject { get; }
    }
}
