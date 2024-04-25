namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// An association type handle with multiplicity one to many.
    /// </summary>
    public sealed record OneToManyAssociationTypeHandle : OneToAssociationTypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneToManyAssociationTypeHandle"/> class.
        /// </summary>
        internal OneToManyAssociationTypeHandle(Guid id)
            : base(id)
        {
        }
    }
}
