namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// A domain handle.
    /// </summary>
    public sealed record DomainHandle : MetaHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainHandle"/> class.
        /// </summary>
        internal DomainHandle(Guid id)
            : base(id)
        {
        }
    }
}
