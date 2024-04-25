namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// A composite role type handle.
    /// </summary>
    public abstract record CompositeRoleTypeHandle : RoleTypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeRoleTypeHandle"/> class.
        /// </summary>
        protected CompositeRoleTypeHandle(Guid id)
            : base(id)
        {
        }
    }
}
