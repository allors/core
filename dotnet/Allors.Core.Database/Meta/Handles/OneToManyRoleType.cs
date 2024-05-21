namespace Allors.Core.Database.Meta.Handles
{
    using Allors.Core.Meta.Domain;
    using Allors.Core.Meta.Meta;

    /// <summary>
    /// A role type handle with multiplicity one to many.
    /// </summary>
    public sealed class OneToManyRoleType : MetaObject, IToManyRoleType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneToManyRoleType"/> class.
        /// </summary>
        public OneToManyRoleType(MetaPopulation population, MetaObjectType objectType)
            : base(population, objectType)
        {
        }
    }
}
