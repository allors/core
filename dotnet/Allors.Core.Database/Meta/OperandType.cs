namespace Allors.Core.Database.Meta
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// An operand type.
    /// </summary>
    public abstract class OperandType : Type
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperandType"/> class.
        /// </summary>
        protected OperandType(Meta meta, EmbeddedObject embeddedObject)
            : base(meta, embeddedObject)
        {
        }
    }
}
