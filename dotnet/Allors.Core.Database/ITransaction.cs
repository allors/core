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

    /// <summary>
    /// Instantiates an object.
    /// </summary>
    IObject? Instantiate(long id);

    /// <summary>
    /// Returns the changeset and creates a new checkpoint.
    /// </summary>
    IChangeSet Checkpoint();

    /// <summary>
    /// Commits changes to the database.
    /// </summary>
    void Commit();

    /// <summary>
    /// Rolls back changes to the database.
    /// </summary>
    void Rollback();
}
