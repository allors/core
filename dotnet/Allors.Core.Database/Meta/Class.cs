namespace Allors.Core.Database.Meta
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// A unit.
    /// </summary>
    public sealed class Class : Composite
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Class"/> class.
        /// </summary>
        internal Class(Meta meta, EmbeddedObject embeddedObject)
            : base(meta, embeddedObject)
        {
        }
    }
}
