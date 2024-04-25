namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// A role type handle with multiplicity one to one.
    /// </summary>
    public sealed record OneToOneRoleTypeHandle : ToOneRoleTypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneToOneRoleTypeHandle"/> class.
        /// </summary>
        internal OneToOneRoleTypeHandle(Guid id)
            : base(id)
        {
        }
    }
}
