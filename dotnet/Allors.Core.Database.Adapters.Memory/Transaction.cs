namespace Allors.Core.Database.Adapters.Memory;

using Allors.Core.Database.Meta;

/// <inheritdoc />
public class Transaction : ITransaction
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Transaction"/> class.
    /// </summary>
    /// <param name="database"></param>
    public Transaction(Database database)
    {
        this.Database = database;
    }

    IDatabase ITransaction.Database => this.Database;

    internal Database Database { get; }

    /// <inheritdoc/>
    public IObject Build(Class @class)
    {
        return new Object(this, @class);
    }
}
