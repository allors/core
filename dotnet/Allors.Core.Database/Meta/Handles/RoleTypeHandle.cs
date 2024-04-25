namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// A role type handle.
    /// </summary>
    public abstract record RoleTypeHandle : RelationEndTypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleTypeHandle"/> class.
        /// </summary>
        protected RoleTypeHandle(Guid id)
            : base(id)
        {
        }
    }
}
