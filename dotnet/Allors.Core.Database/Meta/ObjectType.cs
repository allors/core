namespace Allors.Core.Database.Meta
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// An object type.
    /// </summary>
    public abstract class ObjectType : Type
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectType"/> class.
        /// </summary>
        protected ObjectType(Meta meta, EmbeddedObject embeddedObject)
            : base(meta, embeddedObject)
        {
        }
    }
}
