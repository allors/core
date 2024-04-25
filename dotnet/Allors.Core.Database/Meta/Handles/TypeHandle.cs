namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// A type handle.
    /// </summary>
    public abstract record TypeHandle : MetaHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeHandle"/> class.
        /// </summary>
        protected TypeHandle(Guid id)
            : base(id)
        {
        }
    }
}
