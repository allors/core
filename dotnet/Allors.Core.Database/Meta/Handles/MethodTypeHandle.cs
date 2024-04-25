namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// A method type handle.
    /// </summary>
    public sealed record MethodTypeHandle : OperandTypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodTypeHandle"/> class.
        /// </summary>
        internal MethodTypeHandle(Guid id)
            : base(id)
        {
        }
    }
}
