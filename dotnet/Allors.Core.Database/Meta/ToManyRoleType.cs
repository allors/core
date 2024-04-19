namespace Allors.Core.Database.Meta
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// A role type with a multiplicity many.
    /// </summary>
    public abstract class ToManyRoleType : CompositeRoleType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToManyRoleType"/> class.
        /// </summary>
        protected ToManyRoleType(Meta meta, EmbeddedObject embeddedObject)
            : base(meta, embeddedObject)
        {
        }
    }
}
