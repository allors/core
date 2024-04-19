namespace Allors.Core.Database.Meta
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// A relation type.
    /// </summary>
    public abstract class RelationEndType : OperandType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RelationEndType"/> class.
        /// </summary>
        protected RelationEndType(Meta meta, EmbeddedObject embeddedObject)
            : base(meta, embeddedObject)
        {
        }
    }
}
