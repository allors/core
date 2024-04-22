using Allors.Embedded.Domain;

namespace Allors.Core.Database.Adapters.Memory;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Allors.Core.Database.Meta;

/// <inheritdoc />
public class Transaction : ITransaction
{
    private readonly IDictionary<long, Object> instantiatedObjectByObjectId;

    /// <summary>
    /// Initializes a new instance of the <see cref="Transaction"/> class.
    /// </summary>
    public Transaction(Database database)
    {
        this.Database = database;
        this.OriginalState = database.State;

        this.instantiatedObjectByObjectId = new Dictionary<long, Object>();
    }

    IDatabase ITransaction.Database => this.Database;

    internal Database Database { get; }

    internal ImmutableDictionary<long, State> OriginalState { get; private set; }

    /// <inheritdoc/>
    public IObject Build(Class @class)
    {
        return new Object(this, @class, this.Database.NextObjectId());
    }

    /// <inheritdoc />
    public IObject Instantiate(long id)
    {
        if (this.instantiatedObjectByObjectId.TryGetValue(id, out var @object))
        {
            return @object;
        }

        if (this.OriginalState.TryGetValue(id, out var originalState))
        {
            var instantiatedObject = new Object(this, originalState);
            this.instantiatedObjectByObjectId.Add(instantiatedObject.Id, instantiatedObject);
            return instantiatedObject;
        }

        throw new ArgumentException($"Could not instantiate object with id {id} ", nameof(id));
    }

    /// <inheritdoc />
    public IChangeSet Checkpoint()
    {
        var created = new HashSet<IObject>();
        var deleted = new HashSet<IObject>();
        var roleTypesByAssociation = new Dictionary<IObject, ISet<RoleType>>();
        var associationTypesByRole = new Dictionary<IObject, ISet<AssociationType>>();

        foreach (var (_, @object) in this.instantiatedObjectByObjectId)
        {
            @object.Checkpoint(created, deleted, roleTypesByAssociation, associationTypesByRole);
        }

        return new ChangeSet(created, deleted, roleTypesByAssociation, associationTypesByRole);
    }

    /// <inheritdoc />
    public void Commit()
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc />
    public void Rollback()
    {
        this.OriginalState = this.Database.State;

        foreach (var (_, @object) in this.instantiatedObjectByObjectId)
        {
            @object.Rollback();
        }
    }
}
