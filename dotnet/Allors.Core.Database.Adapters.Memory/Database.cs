namespace Allors.Core.Database.Adapters.Memory;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

/// <inheritdoc />
public class Database : IDatabase
{
    private readonly object commitLock = new object();

    private long nextObjectId;

    /// <summary>
    /// Initializes a new instance of the <see cref="Database"/> class.
    /// </summary>
    public Database()
    {
        this.Store = new Store(ImmutableDictionary<long, Record>.Empty);
        this.nextObjectId = 0;
    }

    internal Store Store { get; set; }

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
            var recordById = this.Store.RecordById;

            var objects = transaction.InstantiatedObjectByObjectId
                .Where(kvp => kvp.Value is { IsChanged: true })
                .Select(kvp => kvp.Value)
                .ToArray();

            var addedObjects = objects
                .Where(v => v.Record == null)
                .ToArray();

            var existingObjects = objects
                .Where(v => v.Record != null)
                .ToArray();

            // Assert concurrency with object versions
            foreach (var existingObject in existingObjects)
            {
                if (!recordById.TryGetValue(existingObject.Id, out var record))
                {
                    throw new Exception("Concurrency error");
                }

                if (record.Version != existingObject.Record!.Version)
                {
                    throw new Exception("Concurrency error");
                }
            }

            recordById = recordById.AddRange(addedObjects.Select(v => new KeyValuePair<long, Record>(v.Id, v.NewRecord())));
            recordById = recordById.SetItems(existingObjects.Select(v => new KeyValuePair<long, Record>(v.Id, v.NewRecord())));

            this.Store = this.Store with
            {
                RecordById = recordById,
            };
        }
    }
}
