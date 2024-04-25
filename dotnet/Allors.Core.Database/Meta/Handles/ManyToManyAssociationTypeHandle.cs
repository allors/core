namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// An association type handle with multiplicity many to many.
    /// </summary>
    public sealed record ManyToManyAssociationTypeHandle : ManyToAssociationTypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManyToManyAssociationTypeHandle"/> class.
        /// </summary>
        internal ManyToManyAssociationTypeHandle(Guid id)
            : base(id)
        {
        }
    }
}
