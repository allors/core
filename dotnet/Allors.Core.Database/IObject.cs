namespace Allors.Core.Database;

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
    object? this[UnitRoleTypeHandleHandle roleTypeHandleHandle] { get; set; }

    /// <summary>
    /// Gets or sets the ManyToOne role.
    /// </summary>
    IObject? this[ManyToOneRoleTypeHandle roleTypeHandle] { get; set; }
}
