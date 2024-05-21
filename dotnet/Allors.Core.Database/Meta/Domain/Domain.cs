namespace Allors.Core.Database.Meta.Domain
{
    using Allors.Core.Meta.Domain;
    using Allors.Core.Meta.Meta;

    /// <summary>
    /// A domain handle.
    /// </summary>
    public class Domain : MetaObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Domain"/> class.
        /// </summary>
        public Domain(MetaPopulation population, MetaObjectType objectType)
            : base(population, objectType)
        {
        }
    }
}
