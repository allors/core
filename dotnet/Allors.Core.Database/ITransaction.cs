namespace Allors.Core.Database;

using System.Collections.Generic;
using Allors.Core.Database.Meta.Domain;

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
    /// Builds amount new objects.
    /// </summary>
    IEnumerable<IObject> Build(Class @class, int amount);

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
