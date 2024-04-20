namespace Allors.Core.Database.Meta
{
    using System;
    using Allors.Embedded.Domain;

    /// <summary>
    /// An operand type.
    /// </summary>
    public abstract class OperandType : Type
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperandType"/> class.
        /// </summary>
        protected OperandType(Guid id, EmbeddedObject embeddedObject)
            : base(id, embeddedObject)
        {
        }
    }
}
