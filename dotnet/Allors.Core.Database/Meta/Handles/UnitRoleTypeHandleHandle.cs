namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// A unit role type handle.
    /// </summary>
    public sealed record UnitRoleTypeHandleHandle : RoleTypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitRoleTypeHandleHandle"/> class.
        /// </summary>
        internal UnitRoleTypeHandleHandle(Guid id)
            : base(id)
        {
        }
    }
}
