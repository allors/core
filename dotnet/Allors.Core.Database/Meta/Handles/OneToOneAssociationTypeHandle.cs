namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// An association type handle with multiplicity one to one.
    /// </summary>
    public sealed record OneToOneAssociationTypeHandle : OneToAssociationTypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneToOneAssociationTypeHandle"/> class.
        /// </summary>
        internal OneToOneAssociationTypeHandle(Guid id)
            : base(id)
        {
        }
    }
}
