namespace Allors.Core.Database.Meta.Handles
{
    using Allors.Core.Meta.Domain;
    using Allors.Core.Meta.Meta;

    /// <summary>
    /// An association type handle with multiplicity one to many.
    /// </summary>
    public sealed class OneToManyAssociationType : MetaObject, IOneToAssociationType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneToManyAssociationType"/> class.
        /// </summary>
        public OneToManyAssociationType(MetaPopulation population, MetaObjectType objectType)
            : base(population, objectType)
        {
        }
    }
}
