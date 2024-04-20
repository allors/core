namespace Allors.Core.Database.Meta
{
    using System;
    using Allors.Embedded.Domain;

    /// <summary>
    /// A relation type.
    /// </summary>
    public abstract class RelationEndType : OperandType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RelationEndType"/> class.
        /// </summary>
        protected RelationEndType(Guid id, EmbeddedObject embeddedObject)
            : base(id, embeddedObject)
        {
        }
    }
}
