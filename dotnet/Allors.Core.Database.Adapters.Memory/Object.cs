namespace Allors.Core.Database.Adapters.Memory;

using System;
using System.Collections.Generic;
using System.Transactions;
using Allors.Core.Database.Meta;

/// <inheritdoc />
public class Object : IObject
{
    private State? originalState;
    private Dictionary<Guid, object?>? changedRoleByRoleTypeId;
    private Dictionary<Guid, object?>? checkpointRoleByRoleTypeId;

    /// <summary>
    /// Initializes a new instance of the <see cref="Object"/> class.
    /// </summary>
    internal Object(Transaction transaction, Class @class, long id)
    {
        this.Transaction = transaction;
        this.Class = @class;
        this.Id = id;

        transaction.OriginalState.TryGetValue(this.Id, out this.originalState);
    }

    /// <summary>
    /// Initializes an existing instance of the <see cref="Object"/> class.
    /// </summary>
    internal Object(Transaction transaction, State originalState)
    {
        this.Transaction = transaction;
        this.originalState = originalState;
        this.Id = originalState.Id;
        this.Class = originalState.Class;
    }

    /// <inheritdoc />
    public Class Class { get; }

    /// <inheritdoc/>
    public long Id { get; }

    ITransaction IObject.Transaction => this.Transaction;

    internal Transaction Transaction { get; }

    /// <inheritdoc />
    public object? this[UnitRoleType unitRoleType]
    {
        get
        {
            if (this.changedRoleByRoleTypeId != null && this.changedRoleByRoleTypeId.TryGetValue(unitRoleType.Id, out var changedRole))
            {
                return changedRole;
            }

            if (this.originalState != null && this.originalState.RoleByRoleTypeId.TryGetValue(unitRoleType.Id, out var role))
            {
                return role;
            }

            return null;
        }

        set
        {
            var currentRole = this[unitRoleType];
            if (Equals(currentRole, value))
            {
                return;
            }

            this.changedRoleByRoleTypeId ??= [];
            this.changedRoleByRoleTypeId[unitRoleType.Id] = value;
        }
    }

    internal void Checkpoint(
        HashSet<IObject> created,
        HashSet<IObject> deleted,
        Dictionary<IObject, ISet<RoleType>> roleTypesByAssociation,
        Dictionary<IObject, ISet<AssociationType>> associationTypesByRole)
    {
        if (this.changedRoleByRoleTypeId == null)
        {
            return;
        }

        // TODO:
        this.checkpointRoleByRoleTypeId = this.changedRoleByRoleTypeId;
    }

    internal void Rollback()
    {
        this.Transaction.OriginalState.TryGetValue(this.Id, out this.originalState);
        this.changedRoleByRoleTypeId = null;
        this.checkpointRoleByRoleTypeId = null;
    }
}
