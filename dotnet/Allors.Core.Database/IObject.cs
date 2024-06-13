namespace Allors.Core.Database;

using System;
using System.Collections.Generic;
using Allors.Core.Database.Meta;

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
    /// Gets or sets the binary role.
    /// </summary>
    byte[]? this[BinaryRoleType roleType] { get; set; }

    /// <summary>
    /// Gets or sets the binary role.
    /// </summary>
    byte[]? this[Func<BinaryRoleType> roleType] { get; set; }

    /// <summary>
    /// Gets or sets the boolean role.
    /// </summary>
    bool? this[BooleanRoleType roleType] { get; set; }

    /// <summary>
    /// Gets or sets the boolean role.
    /// </summary>
    bool? this[Func<BooleanRoleType> roleType] { get; set; }

    /// <summary>
    /// Gets or sets the datetime role.
    /// </summary>
    DateTime? this[DateTimeRoleType roleType] { get; set; }

    /// <summary>
    /// Gets or sets the datetime role.
    /// </summary>
    DateTime? this[Func<DateTimeRoleType> roleType] { get; set; }

    /// <summary>
    /// Gets or sets the decimal role.
    /// </summary>
    decimal? this[DecimalRoleType roleType] { get; set; }

    /// <summary>
    /// Gets or sets the decimal role.
    /// </summary>
    decimal? this[Func<DecimalRoleType> roleType] { get; set; }

    /// <summary>
    /// Gets or sets the float role.
    /// </summary>
    double? this[FloatRoleType roleType] { get; set; }

    /// <summary>
    /// Gets or sets the float role.
    /// </summary>
    double? this[Func<FloatRoleType> roleType] { get; set; }

    /// <summary>
    /// Gets or sets the integer role.
    /// </summary>
    int? this[IntegerRoleType roleType] { get; set; }

    /// <summary>
    /// Gets or sets the integer role.
    /// </summary>
    int? this[Func<IntegerRoleType> roleType] { get; set; }

    /// <summary>
    /// Gets or sets the string role.
    /// </summary>
    string? this[StringRoleType roleType] { get; set; }

    /// <summary>
    /// Gets or sets the string role.
    /// </summary>
    string? this[Func<StringRoleType> roleType] { get; set; }

    /// <summary>
    /// Gets or sets the unique role.
    /// </summary>
    Guid? this[UniqueRoleType roleType] { get; set; }

    /// <summary>
    /// Gets or sets the unique role.
    /// </summary>
    Guid? this[Func<UniqueRoleType> roleType] { get; set; }

    /// <summary>
    /// Gets or sets the ToOne role.
    /// </summary>
    IObject? this[IToOneRoleType roleType] { get; set; }

    /// <summary>
    /// Gets or sets the ToOne role.
    /// </summary>
    IObject? this[Func<IToOneRoleType> roleType] { get; set; }

    /// <summary>
    /// Gets the ManyTo role.
    /// </summary>
    IEnumerable<IObject> this[IToManyRoleType roleType] { get; set; }

    /// <summary>
    /// Gets the ManyTo role.
    /// </summary>
    IEnumerable<IObject> this[Func<IToManyRoleType> roleType] { get; set; }

    /// <summary>
    /// Gets the OneTo role.
    /// </summary>
    IObject? this[IOneToAssociationType associationType] { get; }

    /// <summary>
    /// Gets the OneTo role.
    /// </summary>
    IObject? this[Func<IOneToAssociationType> associationType] { get; }

    /// <summary>
    /// Gets the ManyTo role.
    /// </summary>
    IEnumerable<IObject> this[IManyToAssociationType associationType] { get; }

    /// <summary>
    /// Gets the ManyTo role.
    /// </summary>
    IEnumerable<IObject> this[Func<IManyToAssociationType> associationType] { get; }

    /// <summary>
    /// Add an object to the role.
    /// </summary>
    void Add(IToManyRoleType roleType, IObject value);

    /// <summary>
    /// Add an object to the role.
    /// </summary>
    void Add(Func<IToManyRoleType> roleType, IObject value);

    /// <summary>
    /// Remove an object from the role.
    /// </summary>
    void Remove(IToManyRoleType roleType, IObject value);

    /// <summary>
    /// Remove an object from the role.
    /// </summary>
    void Remove(Func<IToManyRoleType> roleType, IObject value);

    /// <summary>
    /// Is there a value for this role type.
    /// </summary>
    bool Exist(IRoleType roleType);

    /// <summary>
    /// Is there a value for this role type.
    /// </summary>
    bool Exist(Func<IRoleType> roleType);

    /// <summary>
    /// Is there a value for this role type.
    /// </summary>
    bool Exist(IAssociationType associationType);

    /// <summary>
    /// Is there a value for this role type.
    /// </summary>
    bool Exist(Func<IAssociationType> associationType);
}
