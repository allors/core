namespace Allors.Core.Database.Adapters.Memory;

using Allors.Core.Database.Meta;

/// <inheritdoc />
public class Object : IObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Object"/> class.
    /// </summary>
    public Object(Transaction transaction, Class @class)
    {
        this.Transaction = transaction;
        this.Class = @class;
    }

    /// <inheritdoc />
    public Class Class { get; }

    ITransaction IObject.Transaction => this.Transaction;

    internal Transaction Transaction { get; }
}
