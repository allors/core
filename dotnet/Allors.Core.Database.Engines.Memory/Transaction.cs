﻿namespace Allors.Core.Database.Engines.Memory;

using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using Allors.Core.Database.Engines.Meta;
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
        this.Meta = this.Database.Meta;
        this.Store = this.Database.Store;

        this.InstantiatedObjectByObjectId = new Dictionary<long, Object>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Transaction"/> class.
    /// </summary>
    public Transaction(Transaction transaction)
    {
        this.Database = transaction.Database;
        this.Meta = this.Database.Meta;

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
                    FrozenDictionary<EnginesRoleType, object>.Empty,
                    FrozenDictionary<EnginesAssociationType, object>.Empty)));

        this.Store = this.Database.Store with
        {
            RecordById = this.Database.Store.RecordById.AddRange(newRecords),
        };

        this.InstantiatedObjectByObjectId = new Dictionary<long, Object>();
    }

    IDatabase ITransaction.Database => this.Database;

    internal EnginesMeta Meta { get; }

    internal Store Store { get; private set; }

    internal IDictionary<long, Object> InstantiatedObjectByObjectId { get; }

    internal Database Database { get; }

    IObject ITransaction.Build(Func<Class> @class) => this.Build(@class());

    IEnumerable<IObject> ITransaction.Build(Func<Class> @class, int amount) => this.Build(@class(), amount);

    /// <inheritdoc/>
    public IObject Build(Class @class) => this.BuildWithLifecycleMethods(@class);

    /// <inheritdoc/>
    public IEnumerable<IObject> Build(Class @class, int amount)
    {
        var newObjects = new IObject[amount];
        for (var i = 0; i < amount; i++)
        {
            newObjects[i] = this.BuildWithLifecycleMethods(@class);
        }

        return newObjects;
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

    private Object BuildWithLifecycleMethods(Class @class)
    {
        var newObject = new Object(this, this.Meta[@class], this.Database.NextObjectId());
        this.InstantiatedObjectByObjectId.Add(newObject.Id, newObject);

        var m = this.Meta.Meta;
        newObject.Call(m.ObjectOnBuild());
        newObject.Call(m.ObjectOnPostBuild());

        return newObject;
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
