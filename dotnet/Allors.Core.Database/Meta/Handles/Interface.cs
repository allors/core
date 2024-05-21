namespace Allors.Core.Database.Meta.Handles
{
    using Allors.Core.Meta.Domain;
    using Allors.Core.Meta.Meta;

    /// <summary>
    /// A unit handle.
    /// </summary>
    public sealed class Interface : MetaObject, IComposite
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Interface"/> class.
        /// </summary>
        public Interface(MetaPopulation population, MetaObjectType objectType)
            : base(population, objectType)
        {
        }
    }
}
