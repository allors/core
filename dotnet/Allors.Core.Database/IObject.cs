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
    /// Gets or sets the unit role value.
    /// </summary>
    object? this[UnitRoleType unitRoleType] { get; set; }
}
