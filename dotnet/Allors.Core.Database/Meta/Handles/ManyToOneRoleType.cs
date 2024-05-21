namespace Allors.Core.Database.Meta.Handles
{
    using Allors.Core.Meta.Domain;
    using Allors.Core.Meta.Meta;

    /// <summary>
    /// A role type handle with multiplicity many to one.
    /// </summary>
    public sealed class ManyToOneRoleType : MetaObject, IToOneRoleType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManyToOneRoleType"/> class.
        /// </summary>
        public ManyToOneRoleType(MetaPopulation population, MetaObjectType objectType)
            : base(population, objectType)
        {
        }
    }
}
