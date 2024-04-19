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
        protected MetaObject(Meta meta, EmbeddedObject embeddedObject)
        {
            this.Meta = meta;
            this.EmbeddedObject = embeddedObject;
            this.Id = (Guid?)embeddedObject["Id"] ?? throw new ArgumentException("Meta object has no id");
        }

        /// <summary>
        /// The meta population.
        /// </summary>
        public Meta Meta { get; }

        /// <summary>
        /// The embedded object.
        /// </summary>
        public EmbeddedObject EmbeddedObject { get; }

        /// <summary>
        /// The id.
        /// </summary>
        public Guid Id { get; }
    }
}
