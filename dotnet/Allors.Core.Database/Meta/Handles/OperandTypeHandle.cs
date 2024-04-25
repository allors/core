namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// An operand type handle.
    /// </summary>
    public abstract record OperandTypeHandle : TypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperandTypeHandle"/> class.
        /// </summary>
        protected OperandTypeHandle(Guid id)
            : base(id)
        {
        }
    }
}
