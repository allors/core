namespace Allors.Core.Database.Meta.Handles
{
    using System;

    /// <summary>
    /// An association type handle for a unit role.
    /// </summary>
    public sealed record UnitAssociationTypeHandle : AssociationTypeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitAssociationTypeHandle"/> class.
        /// </summary>
        internal UnitAssociationTypeHandle(Guid id)
            : base(id)
        {
        }
    }
}
