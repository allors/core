namespace Allors.Core.Database.Meta.Domain
{
    using Allors.Core.Meta.Domain;
    using Allors.Core.Meta.Meta;

    /// <summary>
    /// A unit role type handle.
    /// </summary>
    public sealed class UnitRoleType : MetaObject, IRoleType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitRoleType"/> class.
        /// </summary>
        public UnitRoleType(MetaPopulation population, MetaObjectType objectType)
            : base(population, objectType)
        {
        }
    }
}
