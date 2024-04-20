namespace Allors.Core.Database.Meta
{
    using System;
    using Allors.Embedded.Domain;

    /// <summary>
    /// A composite role type.
    /// </summary>
    public abstract class CompositeRoleType : RoleType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeRoleType"/> class.
        /// </summary>
        protected CompositeRoleType(Guid id, EmbeddedObject embeddedObject)
            : base(id, embeddedObject)
        {
        }
    }
}
