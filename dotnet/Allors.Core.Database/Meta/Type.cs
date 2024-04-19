namespace Allors.Core.Database.Meta
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// A type.
    /// </summary>
    public abstract class Type : MetaObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Type"/> class.
        /// </summary>
        protected Type(Meta meta, EmbeddedObject embeddedObject)
            : base(meta, embeddedObject)
        {
        }
    }
}
