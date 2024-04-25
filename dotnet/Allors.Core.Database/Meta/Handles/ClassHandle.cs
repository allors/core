namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// A class handle.
    /// </summary>
    public sealed record ClassHandle : CompositeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassHandle"/> class.
        /// </summary>
        internal ClassHandle(Guid id)
            : base(id)
        {
        }
    }
}
