namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// An association type handle with multiplicity many.
    /// </summary>
    public abstract record ManyToAssociationTypeHandle : CompositeAssociationTypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManyToAssociationTypeHandle"/> class.
        /// </summary>
        protected ManyToAssociationTypeHandle(Guid id)
            : base(id)
        {
        }
    }
}
