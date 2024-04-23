namespace Allors.Core.Database.Adapters.Memory;

using System;
using System.Collections.Generic;
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
        this.Store = database.Store;

        this.InstantiatedObjectByObjectId = new Dictionary<long, Object>();
    }

    IDatabase ITransaction.Database => this.Database;

    internal Store Store { get; private set; }

    internal IDictionary<long, Object> InstantiatedObjectByObjectId { get; }

    private Database Database { get; }

    /// <inheritdoc/>
    public IObject Build(Class @class)
    {
        var newObject = new Object(this, @class, this.Database.NextObjectId());
        this.InstantiatedObjectByObjectId.Add(newObject.Id, newObject);
        return newObject;
    }

    /// <inheritdoc />
    public IObject Instantiate(long id)
    {
        if (this.InstantiatedObjectByObjectId.TryGetValue(id, out var @object))
        {
            return @object;
        }

        if (this.Store.RecordById.TryGetValue(id, out var originalState))
        {
            var instantiatedObject = new Object(this, originalState);
            this.InstantiatedObjectByObjectId.Add(instantiatedObject.Id, instantiatedObject);
            return instantiatedObject;
        }

        throw new ArgumentException($"Could not instantiate object with id {id} ", nameof(id));
    }

    /// <inheritdoc />
    public IChangeSet Checkpoint()
    {
        var changeSet = new ChangeSet();

        foreach (var (_, @object) in this.InstantiatedObjectByObjectId)
        {
            @object.Checkpoint(changeSet);
        }

        return changeSet;
    }

    /// <inheritdoc />
    public void Commit()
    {
        this.Database.Commit(this);
        this.Reset();
    }

    /// <inheritdoc />
    public void Rollback()
    {
        this.Reset();
    }

    private void Reset()
    {
        this.Store = this.Database.Store;

        foreach (var (_, @object) in this.InstantiatedObjectByObjectId)
        {
            @object.Rollback();
        }
    }
}
