namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// A role type handle with multiplicity one to many.
    /// </summary>
    public sealed record OneToManyRoleTypeHandle : ToManyRoleTypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneToManyRoleTypeHandle"/> class.
        /// </summary>
        internal OneToManyRoleTypeHandle(Guid id)
            : base(id)
        {
        }
    }
}
