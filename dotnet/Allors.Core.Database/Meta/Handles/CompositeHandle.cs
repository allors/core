namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// An object type handle.
    /// </summary>
    public abstract record CompositeHandle : ObjectTypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeHandle"/> class.
        /// </summary>
        protected CompositeHandle(Guid id)
            : base(id)
        {
        }
    }
}
