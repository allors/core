namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// An association type handle with multiplicity one.
    /// </summary>
    public abstract record OneToAssociationTypeHandle : CompositeAssociationTypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneToAssociationTypeHandle"/> class.
        /// </summary>
        protected OneToAssociationTypeHandle(Guid id)
            : base(id)
        {
        }
    }
}
