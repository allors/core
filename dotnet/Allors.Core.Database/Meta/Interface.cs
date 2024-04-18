namespace Allors.Core.Database.Meta
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// A unit.
    /// </summary>
    public sealed class Interface : Composite
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Interface"/> class.
        /// </summary>
        internal Interface(MetaPopulation metaPopulation, EmbeddedObject embeddedObject)
            : base(metaPopulation, embeddedObject)
        {
        }
    }
}
