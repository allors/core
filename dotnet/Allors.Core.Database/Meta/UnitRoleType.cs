namespace Allors.Core.Database.Meta
{
    using System;
    using Allors.Embedded.Domain;

    /// <summary>
    /// A unit role type.
    /// </summary>
    public sealed class UnitRoleType : RoleType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitRoleType"/> class.
        /// </summary>
        internal UnitRoleType(Guid id, EmbeddedObject embeddedObject)
            : base(id, embeddedObject)
        {
        }
    }
}
