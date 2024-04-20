namespace Allors.Core.Database.Meta
{
    using System;
    using Allors.Embedded.Domain;

    /// <summary>
    /// A method type.
    /// </summary>
    public sealed class MethodType : OperandType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodType"/> class.
        /// </summary>
        internal MethodType(Guid id, EmbeddedObject embeddedObject)
            : base(id, embeddedObject)
        {
        }
    }
}
