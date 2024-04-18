namespace Allors.Core.Database.Meta
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// A unit role type.
    /// </summary>
    public sealed class UnitRoleType : RoleType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitRoleType"/> class.
        /// </summary>
        internal UnitRoleType(MetaPopulation metaPopulation, EmbeddedObject embeddedObject)
            : base(metaPopulation, embeddedObject)
        {
        }
    }
}
