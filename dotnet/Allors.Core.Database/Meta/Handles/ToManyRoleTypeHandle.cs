namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// A role type handle with a multiplicity many.
    /// </summary>
    public abstract record ToManyRoleTypeHandle : CompositeRoleTypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToManyRoleTypeHandle"/> class.
        /// </summary>
        protected ToManyRoleTypeHandle(Guid id)
            : base(id)
        {
        }
    }
}
