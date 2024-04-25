namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// An object type handle.
    /// </summary>
    public abstract record ObjectTypeHandle : TypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectTypeHandle"/> class.
        /// </summary>
        protected ObjectTypeHandle(Guid id)
            : base(id)
        {
        }
    }
}
