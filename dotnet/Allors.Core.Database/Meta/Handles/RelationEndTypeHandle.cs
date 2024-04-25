namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// A relation type handle.
    /// </summary>
    public abstract record RelationEndTypeHandle : OperandTypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RelationEndTypeHandle"/> class.
        /// </summary>
        protected RelationEndTypeHandle(Guid id)
            : base(id)
        {
        }
    }
}
