namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// A unit handle.
    /// </summary>
    public sealed record UnitHandle : ObjectTypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitHandle"/> class.
        /// </summary>
        public UnitHandle(Guid id)
            : base(id)
        {
        }
    }
}
