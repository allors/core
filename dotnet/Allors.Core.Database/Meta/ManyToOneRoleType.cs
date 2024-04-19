namespace Allors.Core.Database.Meta
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// A role type with multiplicity many to one.
    /// </summary>
    public sealed class ManyToOneRoleType : ToOneRoleType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManyToOneRoleType"/> class.
        /// </summary>
        internal ManyToOneRoleType(Meta meta, EmbeddedObject embeddedObject)
            : base(meta, embeddedObject)
        {
        }
    }
}
