namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// A unit role type handle.
    /// </summary>
    public sealed record UnitRoleTypeHandle : RoleTypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitRoleTypeHandle"/> class.
        /// </summary>
        internal UnitRoleTypeHandle(Guid id)
            : base(id)
        {
        }
    }
}
