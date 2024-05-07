namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// A unit handle.
    /// </summary>
    public sealed record InterfaceHandle : CompositeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceHandle"/> class.
        /// </summary>
        internal InterfaceHandle(Guid id)
            : base(id)
        {
        }
    }
}
