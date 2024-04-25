namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// An association type handle.
    /// </summary>
    public abstract record AssociationTypeHandle : RelationEndTypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssociationTypeHandle"/> class.
        /// </summary>
        protected AssociationTypeHandle(Guid id)
            : base(id)
        {
        }
    }
}
