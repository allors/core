namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// A workspace handle.
    /// </summary>
    public sealed record WorkspaceHandle : MetaHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceHandle"/> class.
        /// </summary>
        internal WorkspaceHandle(Guid id)
            : base(id)
        {
        }
    }
}
