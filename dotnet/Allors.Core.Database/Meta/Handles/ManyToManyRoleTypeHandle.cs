namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// A role type handle with multiplicity many to many.
    /// </summary>
    public sealed record ManyToManyRoleTypeHandle : ToManyRoleTypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManyToManyRoleTypeHandle"/> class.
        /// </summary>
        internal ManyToManyRoleTypeHandle(Guid id)
            : base(id)
        {
        }
    }
}
