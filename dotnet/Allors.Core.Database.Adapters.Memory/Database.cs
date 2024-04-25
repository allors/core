namespace Allors.Core.Database.Adapters.Memory;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Allors.Core.Database.Meta;
using Allors.Core.Database.Meta.Handles;

/// <inheritdoc />
public class Database : IDatabase
{
    private readonly object commitLock = new();

    private long nextObjectId;

    /// <summary>
    /// Initializes a new instance of the <see cref="Database"/> class.
    /// </summary>
    public Database(CoreMeta meta)
    {
        this.Meta = meta;
        this.Store = new Store(ImmutableDictionary<long, Record>.Empty);
        this.nextObjectId = 0;
    }

    internal CoreMeta Meta { get; }

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

            recordById = recordById.AddRange(addedObjects.Select(v => new KeyValuePair<long, Record>(v.Id, v.ToRecord())));
            recordById = recordById.SetItems(existingObjects.Select(v => new KeyValuePair<long, Record>(v.Id, v.ToRecord())));

            this.Store = this.Store with
            {
                RecordById = recordById,
            };
        }
    }

    internal ManyToOneAssociationTypeHandle AssociationTypeHandle(ManyToOneRoleTypeHandle manyToOneRoleTypeHandle)
    {
        // TODO: cache value
        var manyToOneRoleType = this.Meta[manyToOneRoleTypeHandle];
        var manyToOneAssociationType = manyToOneRoleType[this.Meta.RoleTypeAssociationType];
        var manyToOneAssociationTypeHandle = this.Meta[manyToOneAssociationType!];
        return (ManyToOneAssociationTypeHandle)manyToOneAssociationTypeHandle;
    }
}
