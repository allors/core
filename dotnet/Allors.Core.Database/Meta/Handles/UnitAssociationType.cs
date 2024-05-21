namespace Allors.Core.Database.Meta.Handles
{
    using Allors.Core.Meta.Domain;
    using Allors.Core.Meta.Meta;

    /// <summary>
    /// An association type handle for a unit role.
    /// </summary>
    public sealed class UnitAssociationType : MetaObject, IAssociationType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitAssociationType"/> class.
        /// </summary>
        public UnitAssociationType(MetaPopulation population, MetaObjectType objectType)
            : base(population, objectType)
        {
        }
    }
}
