namespace Allors.Core.Database.Meta
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// A unit.
    /// </summary>
    public sealed class Unit : ObjectType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Unit"/> class.
        /// </summary>
        internal Unit(Meta meta, EmbeddedObject embeddedObject)
            : base(meta, embeddedObject)
        {
        }
    }
}
