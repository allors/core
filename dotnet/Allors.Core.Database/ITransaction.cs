namespace Allors.Core.Database;

using Allors.Core.Database.Meta;

/// <summary>
/// The transaction.
/// </summary>
public interface ITransaction
{
    /// <summary>
    /// The database.
    /// </summary>
    IDatabase Database { get; }

    /// <summary>
    /// Builds a new object.
    /// </summary>
    IObject Build(Class @class);
}
