namespace Allors.Core.Database.Meta
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// A domain.
    /// </summary>
    public abstract class Domain : MetaObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Domain"/> class.
        /// </summary>
        protected Domain(MetaPopulation metaPopulation, EmbeddedObject embeddedObject)
            : base(metaPopulation, embeddedObject)
        {
        }
    }
}
