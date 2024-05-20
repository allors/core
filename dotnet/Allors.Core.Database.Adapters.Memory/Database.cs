﻿namespace Allors.Core.Database.Adapters.Memory;

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
            var commitTransaction = new Transaction(transaction);

            foreach (var kvp in transaction.InstantiatedObjectByObjectId)
            {
                var @object = kvp.Value;
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

    internal OneToOneAssociationTypeHandle AssociationTypeHandle(OneToOneRoleTypeHandle oneToOneRoleTypeHandle)
    {
        // TODO: cache value
        var oneToOneRoleType = this.Meta[oneToOneRoleTypeHandle];
        var oneToOneAssociationType = oneToOneRoleType[this.Meta.Meta.RoleTypeAssociationType];
        var oneToOneAssociationTypeHandle = this.Meta[oneToOneAssociationType!];
        return (OneToOneAssociationTypeHandle)oneToOneAssociationTypeHandle;
    }

    internal OneToManyAssociationTypeHandle AssociationTypeHandle(OneToManyRoleTypeHandle oneToManyRoleTypeHandle)
    {
        // TODO: cache value
        var oneToManyRoleType = this.Meta[oneToManyRoleTypeHandle];
        var oneToManyAssociationType = oneToManyRoleType[this.Meta.Meta.RoleTypeAssociationType];
        var oneToManyAssociationTypeHandle = this.Meta[oneToManyAssociationType!];
        return (OneToManyAssociationTypeHandle)oneToManyAssociationTypeHandle;
    }

    internal ManyToOneAssociationTypeHandle AssociationTypeHandle(ManyToOneRoleTypeHandle manyToOneRoleTypeHandle)
    {
        // TODO: cache value
        var manyToOneRoleType = this.Meta[manyToOneRoleTypeHandle];
        var manyToOneAssociationType = manyToOneRoleType[this.Meta.Meta.RoleTypeAssociationType];
        var manyToOneAssociationTypeHandle = this.Meta[manyToOneAssociationType!];
        return (ManyToOneAssociationTypeHandle)manyToOneAssociationTypeHandle;
    }

    internal ManyToManyAssociationTypeHandle AssociationTypeHandle(ManyToManyRoleTypeHandle manyToOneRoleTypeHandle)
    {
        // TODO: cache value
        var manyToOneRoleType = this.Meta[manyToOneRoleTypeHandle];
        var manyToOneAssociationType = manyToOneRoleType[this.Meta.Meta.RoleTypeAssociationType];
        var manyToOneAssociationTypeHandle = this.Meta[manyToOneAssociationType!];
        return (ManyToManyAssociationTypeHandle)manyToOneAssociationTypeHandle;
    }
}
