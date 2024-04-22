namespace Allors.Core.Database.Adapters.Memory;

using System.Collections.Immutable;
using Allors.Core.Database.Meta;

/// <inheritdoc />
public class Transaction : ITransaction
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Transaction"/> class.
    /// </summary>
    public Transaction(Database database)
    {
        this.Database = database;
        this.OriginalState = database.State;
    }

    IDatabase ITransaction.Database => this.Database;

    internal Database Database { get; }

    internal ImmutableDictionary<long, State> OriginalState { get; }

    /// <inheritdoc/>
    public IObject Build(Class @class)
    {
        return new Object(this, @class, this.Database.NextObjectId());
    }
}
