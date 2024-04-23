namespace Allors.Core.Database.Meta
{
    using System;
    using Allors.Embedded.Domain;

    /// <summary>
    /// A meta object.
    /// </summary>
    public abstract class MetaObject : IEquatable<MetaObject>
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

        /// <inheritdoc/>
        public bool Equals(MetaObject? other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.Id.Equals(other.Id);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((MetaObject)obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
