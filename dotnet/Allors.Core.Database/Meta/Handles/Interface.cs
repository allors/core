namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// A unit handle.
    /// </summary>
    public sealed record Interface : CompositeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Interface"/> class.
        /// </summary>
        internal Interface(Guid id)
            : base(id)
        {
        }
    }
}
