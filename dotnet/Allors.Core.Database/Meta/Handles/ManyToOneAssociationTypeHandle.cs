namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// An association type handle with multiplicity many to one.
    /// </summary>
    public sealed record ManyToOneAssociationTypeHandle : ManyToAssociationTypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManyToOneAssociationTypeHandle"/> class.
        /// </summary>
        internal ManyToOneAssociationTypeHandle(Guid id)
            : base(id)
        {
        }
    }
}
