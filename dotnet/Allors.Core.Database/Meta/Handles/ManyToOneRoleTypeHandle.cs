namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// A role type handle with multiplicity many to one.
    /// </summary>
    public sealed record ManyToOneRoleTypeHandle : ToOneRoleTypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManyToOneRoleTypeHandle"/> class.
        /// </summary>
        internal ManyToOneRoleTypeHandle(Guid id)
            : base(id)
        {
        }
    }
}
