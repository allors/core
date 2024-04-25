namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// A domain handle.
    /// </summary>
    public sealed record Domain : MetaHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Domain"/> class.
        /// </summary>
        internal Domain(Guid id)
            : base(id)
        {
        }
    }
}
