namespace Allors.Core.Database.Meta
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// A domain.
    /// </summary>
    public sealed class Domain : MetaObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Domain"/> class.
        /// </summary>
        internal Domain(Meta meta, EmbeddedObject embeddedObject)
            : base(meta, embeddedObject)
        {
        }
    }
}
