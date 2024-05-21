namespace Allors.Core.Database.Meta.Domain
{
    using Allors.Core.Meta.Domain;
    using Allors.Core.Meta.Meta;

    /// <summary>
    /// An association type handle with multiplicity many to many.
    /// </summary>
    public sealed class ManyToManyAssociationType : MetaObject, IManyToAssociationType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManyToManyAssociationType"/> class.
        /// </summary>
        public ManyToManyAssociationType(MetaPopulation population, MetaObjectType objectType)
            : base(population, objectType)
        {
        }
    }
}
