namespace Allors.Core.Database.Engines.Memory;

using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using Allors.Core.Database.Engines.Meta;


/// <inheritdoc />
public class Transaction : ITransaction
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Transaction"/> class.
    /// </summary>
    public Transaction(Database database)
    {
        this.Database = database;
        this.Store = this.Database.Store;

        this.InstantiatedObjectByObjectId = new Dictionary<long, Object>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Transaction"/> class.
    /// </summary>
    public Transaction(Transaction transaction)
    {
        this.Database = transaction.Database;

        var newObjects = transaction.InstantiatedObjectByObjectId
            .Select(kvp => kvp.Value)
            .Where(v => v.IsNew);

        var newRecords = newObjects
            .Select(v => new KeyValuePair<long, Record>(
                v.Id,
                new Record(
                    v.Class,
                    v.Id,
                    0,
                    FrozenDictionary<IRoleType, object>.Empty,
                    FrozenDictionary<IAssociationType, object>.Empty)));

        this.Store = this.Database.Store with
        {
            RecordById = this.Database.Store.RecordById.AddRange(newRecords),
        };

        this.InstantiatedObjectByObjectId = new Dictionary<long, Object>();
    }

    IDatabase ITransaction.Database => this.Database;

    internal Store Store { get; private set; }

    internal IDictionary<long, Object> InstantiatedObjectByObjectId { get; }

    internal Database Database { get; }

    /// <inheritdoc/>
    public IObject Build(EngineClass @class)
    {
        var newObject = new Object(this, @class, this.Database.NextObjectId());
        this.InstantiatedObjectByObjectId.Add(newObject.Id, newObject);
        return newObject;
    }

    /// <inheritdoc/>
    public IEnumerable<IObject> Build(EngineClass @class, int amount)
    {
        var objects = new IObject[amount];
        for (var i = 0; i < amount; i++)
        {
            objects[i] = this.Build(@class);
        }

        return objects;
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
            @object.Reset();
        }
    }
}
