namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// An association type handle for a composite role.
    /// </summary>
    public abstract record CompositeAssociationTypeHandle : AssociationTypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeAssociationTypeHandle"/> class.
        /// </summary>
        protected CompositeAssociationTypeHandle(Guid id)
            : base(id)
        {
        }
    }
}
