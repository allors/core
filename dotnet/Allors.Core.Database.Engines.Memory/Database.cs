﻿namespace Allors.Core.Database.Engines.Memory;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Allors.Core.Database.Engines.Meta;
using Allors.Core.Meta;

/// <inheritdoc />
public class Database : IDatabase
{
    private readonly object commitLock = new();

    private long nextObjectId;

    /// <summary>
    /// Initializes a new instance of the <see cref="Database"/> class.
    /// </summary>
    public Database(EnginesMeta meta)
    {
        this.Meta = meta;
        this.Store = new Store(ImmutableDictionary<long, Record>.Empty);
        this.nextObjectId = 0;
    }

    Meta IDatabase.Meta => this.Meta.Meta;

    internal EnginesMeta Meta { get; }

    internal Store Store { get; private set; }

    /// <inheritdoc />
    public ITransaction CreateTransaction()
    {
        return new Transaction(this);
    }

    internal long NextObjectId()
    {
        return this.nextObjectId++;
    }

    internal void Commit(Transaction transaction)
    {
        lock (this.commitLock)
        {
            var commitTransaction = new Transaction(transaction);

            foreach (var @object in transaction.InstantiatedObjectByObjectId.Values)
            {
                @object.Commit(commitTransaction);
            }

            var newObjects = transaction.InstantiatedObjectByObjectId
                .Where(kvp => kvp.Value.IsNew)
                .Select(kvp => (Object)commitTransaction.Instantiate(kvp.Value.Id));

            var changedObjects = commitTransaction.InstantiatedObjectByObjectId
                .Where(kvp => kvp.Value is { ShouldCommit: true })
                .Select(kvp => kvp.Value);

            var objects = newObjects.Union(changedObjects).Distinct()
                .ToArray();

            var recordById = commitTransaction.Store.RecordById;
            recordById = recordById.SetItems(objects.Select(v => new KeyValuePair<long, Record>(v.Id, v.ToRecord())));

            this.Store = this.Store with
            {
                RecordById = recordById,
            };
        }
    }
}
