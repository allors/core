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
    public Class Class { get; }

    /// <summary>
    /// The id.
    /// </summary>
    public long Id { get; }

    /// <summary>
    /// The version.
    /// </summary>
    public long Version { get; }

    /// <summary>
    /// Is new.
    /// </summary>
    public bool IsNew { get; }

    /// <summary>
    /// Gets or sets the unit role.
    /// </summary>
    object? this[UnitRoleType roleTypeHandle] { get; set; }

    /// <summary>
    /// Gets or sets the ToOne role.
    /// </summary>
    IObject? this[IToOneRoleType roleTypeHandle] { get; set; }

    /// <summary>
    /// Gets the ManyTo role.
    /// </summary>
    IEnumerable<IObject> this[IToManyRoleType roleTypeHandle] { get; set; }

    /// <summary>
    /// Gets the OneTo role.
    /// </summary>
    IObject? this[IOneToAssociationType associationTypeHandle] { get; }

    /// <summary>
    /// Gets the ManyTo role.
    /// </summary>
    IEnumerable<IObject> this[IManyToAssociationType associationTypeHandle] { get; }

    /// <summary>
    /// Add an object to the role.
    /// </summary>
    void Add(IToManyRoleType roleTypeHandle, IObject value);

    /// <summary>
    /// Remove an object from the role.
    /// </summary>
    void Remove(IToManyRoleType roleTypeHandle, IObject value);

    /// <summary>
    /// Is there a value for this role type.
    /// </summary>
    bool Exist(IRoleType roleTypeHandle);

    /// <summary>
    /// Is there a value for this role type.
    /// </summary>
    bool Exist(IAssociationType associationTypeHandle);
}
