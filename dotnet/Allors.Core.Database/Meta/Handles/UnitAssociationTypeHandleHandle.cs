namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// An association type handle for a unit role.
    /// </summary>
    public sealed record UnitAssociationTypeHandleHandle : AssociationTypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitAssociationTypeHandleHandle"/> class.
        /// </summary>
        internal UnitAssociationTypeHandleHandle(Guid id)
            : base(id)
        {
        }
    }
}
