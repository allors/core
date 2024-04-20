namespace Allors.Core.Database.Meta
{
    using System;
    using Allors.Embedded.Domain;

    /// <summary>
    /// A role type with multiplicity one to many.
    /// </summary>
    public sealed class OneToManyRoleType : ToManyRoleType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneToManyRoleType"/> class.
        /// </summary>
        internal OneToManyRoleType(Guid id, EmbeddedObject embeddedObject)
            : base(id, embeddedObject)
        {
        }
    }
}
