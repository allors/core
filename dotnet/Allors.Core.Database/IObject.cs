namespace Allors.Core.Database;

using System.Collections.Generic;
using Allors.Core.Database.Meta.Handles;

/// <summary>
/// The object.
/// </summary>
public interface IObject
{
    /// <summary>
    /// The transaction.
    /// </summary>
    public ITransaction Transaction { get; }

    /// <summary>
    /// The class.
    /// </summary>
    public ClassHandle ClassHandle { get; }

    /// <summary>
    /// The id.
    /// </summary>
    public long Id { get; }

    /// <summary>
    /// The version.
    /// </summary>
    public long Version { get; }

    /// <summary>
    /// Gets or sets the unit role.
    /// </summary>
    object? this[UnitRoleTypeHandle roleTypeHandle] { get; set; }

    /// <summary>
    /// Gets or sets the OneToOne role.
    /// </summary>
    IObject? this[OneToOneRoleTypeHandle roleTypeHandle] { get; set; }

    /// <summary>
    /// Gets or sets the ManyToOne role.
    /// </summary>
    IObject? this[ManyToOneRoleTypeHandle roleTypeHandle] { get; set; }

    /// <summary>
    /// Gets the OneTo role.
    /// </summary>
    IObject? this[OneToAssociationTypeHandle associationTypeHandle] { get; }

    /// <summary>
    /// Gets the ManyTo role.
    /// </summary>
    IEnumerable<IObject> this[ManyToAssociationTypeHandle associationTypeHandle] { get; }

    /// <summary>
    /// Is there a value for this role type.
    /// </summary>
    bool Exist(RoleTypeHandle roleTypeHandle);

    /// <summary>
    /// Is there a value for this role type.
    /// </summary>
    bool Exist(AssociationTypeHandle associationTypeHandle);
}
