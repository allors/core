namespace Allors.Core.Database;

using System.Collections.Generic;
using Allors.Core.Database.Meta.Domain;

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
    object? this[UnitRoleType roleType] { get; set; }

    /// <summary>
    /// Gets or sets the ToOne role.
    /// </summary>
    IObject? this[IToOneRoleType roleType] { get; set; }

    /// <summary>
    /// Gets the ManyTo role.
    /// </summary>
    IEnumerable<IObject> this[IToManyRoleType roleType] { get; set; }

    /// <summary>
    /// Gets the OneTo role.
    /// </summary>
    IObject? this[IOneToAssociationType associationType] { get; }

    /// <summary>
    /// Gets the ManyTo role.
    /// </summary>
    IEnumerable<IObject> this[IManyToAssociationType associationType] { get; }

    /// <summary>
    /// Add an object to the role.
    /// </summary>
    void Add(IToManyRoleType roleType, IObject value);

    /// <summary>
    /// Remove an object from the role.
    /// </summary>
    void Remove(IToManyRoleType roleType, IObject value);

    /// <summary>
    /// Is there a value for this role type.
    /// </summary>
    bool Exist(IRoleType roleType);

    /// <summary>
    /// Is there a value for this role type.
    /// </summary>
    bool Exist(IAssociationType associationType);
}
