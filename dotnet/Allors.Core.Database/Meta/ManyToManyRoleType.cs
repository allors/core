namespace Allors.Core.Database.Meta
{
    using System;
    using Allors.Embedded.Domain;

    /// <summary>
    /// A role type with multiplicity many to many.
    /// </summary>
    public sealed class ManyToManyRoleType : ToManyRoleType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManyToManyRoleType"/> class.
        /// </summary>
        internal ManyToManyRoleType(Guid id, EmbeddedObject embeddedObject)
            : base(id, embeddedObject)
        {
        }
    }
}
