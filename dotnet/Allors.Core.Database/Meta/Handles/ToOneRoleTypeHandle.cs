namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// A role type handle with a multiplicity one.
    /// </summary>
    public abstract record ToOneRoleTypeHandle : CompositeRoleTypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeRoleTypeHandle"/> class.
        /// </summary>
        protected ToOneRoleTypeHandle(Guid id)
            : base(id)
        {
        }
    }
}
