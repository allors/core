namespace Allors.Core.Database;

/// <summary>
/// The database.
/// </summary>
public interface IDatabase
{
    /// <summary>
    /// Creates a new transaction.
    /// </summary>
    /// <returns></returns>
    ITransaction CreateTransaction();
}
