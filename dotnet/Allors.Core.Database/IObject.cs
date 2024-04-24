namespace Allors.Core.Database;

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
    /// Gets or sets the unit role.
    /// </summary>
    object? this[UnitRoleType roleType] { get; set; }

    /// <summary>
    /// Gets or sets the ManyToOne role.
    /// </summary>
    IObject? this[ManyToOneRoleType roleType] { get; set; }
}
