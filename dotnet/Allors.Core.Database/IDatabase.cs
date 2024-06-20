namespace Allors.Core.Database;

/// <summary>
/// The database.
/// </summary>
public interface IDatabase
{
    /// <summary>
    /// Meta.
    /// </summary>
    Core.Meta.Meta Meta { get; }

    /// <summary>
    /// Creates a new transaction.
    /// </summary>
    /// <returns></returns>
    ITransaction CreateTransaction();
}
