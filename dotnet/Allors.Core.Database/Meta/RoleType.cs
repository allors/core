namespace Allors.Core.Database.Meta
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// A role type.
    /// </summary>
    public abstract class RoleType : RelationEndType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleType"/> class.
        /// </summary>
        protected RoleType(Meta meta, EmbeddedObject embeddedObject)
            : base(meta, embeddedObject)
        {
        }
    }
}
