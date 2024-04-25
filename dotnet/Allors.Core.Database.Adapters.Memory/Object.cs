namespace Allors.Core.Database.Adapters.Memory;

using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using Allors.Core.Database.Meta.Handles;

/// <inheritdoc />
public class Object : IObject
{
    private Record? record;
    private Dictionary<RoleTypeHandle, object?>? changedRoleByRoleType;
    private Dictionary<RoleTypeHandle, object?>? checkpointRoleByRoleType;

    /// <summary>
    /// Initializes a new instance of the <see cref="Object"/> class.
    /// </summary>
    internal Object(Transaction transaction, ClassHandle classHandle, long id)
    {
        this.Transaction = transaction;
        this.ClassHandle = classHandle;
        this.Id = id;

        transaction.Store.RecordById.TryGetValue(this.Id, out this.record);
    }

    /// <summary>
    /// Initializes an existing instance of the <see cref="Object"/> class.
    /// </summary>
    internal Object(Transaction transaction, Record record)
    {
        this.Transaction = transaction;
        this.record = record;
        this.Id = record.Id;
        this.ClassHandle = record.ClassHandle;
    }

    /// <inheritdoc />
    public ClassHandle ClassHandle { get; }

    /// <inheritdoc/>
    public long Id { get; }

    /// <inheritdoc/>
    public long Version => this.record?.Version ?? 0;

    ITransaction IObject.Transaction => this.Transaction;

    internal Record? Record => this.record;

    internal bool IsChanged
    {
        get
        {
            if (this.changedRoleByRoleType == null)
            {
                return false;
            }

            foreach (var (roleType, changedRole) in this.changedRoleByRoleType)
            {
                if (this.record != null)
                {
                    this.record.RoleByRoleTypeId.TryGetValue(roleType, out var recordRole);
                    if (!Equals(changedRole, recordRole))
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }

            return false;
        }
    }

    private Transaction Transaction { get; }

    /// <inheritdoc />
    public object? this[UnitRoleTypeHandleHandle roleTypeHandleHandle]
    {
        get
        {
            if (this.changedRoleByRoleType != null && this.changedRoleByRoleType.TryGetValue(roleTypeHandleHandle, out var changedRole))
            {
                return changedRole;
            }

            if (this.record != null && this.record.RoleByRoleTypeId.TryGetValue(roleTypeHandleHandle, out var role))
            {
                return role;
            }

            return null;
        }

        set
        {
            var currentRole = this[roleTypeHandleHandle];
            if (Equals(currentRole, value))
            {
                return;
            }

            this.changedRoleByRoleType ??= [];
            this.changedRoleByRoleType[roleTypeHandleHandle] = value;
        }
    }

    /// <inheritdoc />
    public IObject? this[ManyToOneRoleTypeHandle roleTypeHandle]
    {
        get
        {
            if (this.changedRoleByRoleType != null && this.changedRoleByRoleType.TryGetValue(roleTypeHandle, out var changedRole))
            {
                return (IObject?)changedRole;
            }

            if (this.record != null && this.record.RoleByRoleTypeId.TryGetValue(roleTypeHandle, out var role))
            {
                return this.Transaction.Instantiate((long)role);
            }

            return null;
        }

        set
        {
            if (value == null)
            {
                this.RemoveMany2OneRole(roleTypeHandle);
                return;
            }

            this.SetMany2OneRole(roleTypeHandle, (Object)value);
        }
    }

    internal void Checkpoint(ChangeSet changeSet)
    {
        if (this.changedRoleByRoleType == null)
        {
            return;
        }

        foreach (var (roleType, changedRole) in this.changedRoleByRoleType)
        {
            if (this.checkpointRoleByRoleType != null &&
                this.checkpointRoleByRoleType.TryGetValue(roleType, out var checkpointRole))
            {
                if (!Equals(changedRole, checkpointRole))
                {
                    changeSet.AddChangedRoleByRoleTypeId(this, roleType);
                }
            }
            else if (this.record != null)
            {
                this.record.RoleByRoleTypeId.TryGetValue(roleType, out var recordRole);
                if (!Equals(changedRole, recordRole))
                {
                    changeSet.AddChangedRoleByRoleTypeId(this, roleType);
                }
            }
            else
            {
                changeSet.AddChangedRoleByRoleTypeId(this, roleType);
            }
        }

        this.checkpointRoleByRoleType = new Dictionary<RoleTypeHandle, object?>(this.changedRoleByRoleType);
    }

    internal Record NewRecord()
    {
        if (this.Record == null)
        {
            var roleByRoleTypeId = this.changedRoleByRoleType!
                .Where(kvp => kvp.Value != null)
                .Select(kvp => new KeyValuePair<RoleTypeHandle, object>(kvp.Key, kvp.Value!))
                .ToFrozenDictionary();

            return new Record(this.ClassHandle, this.Id, this.Version + 1, roleByRoleTypeId);
        }
        else
        {
            var roleByRoleTypeId = this.Record.RoleByRoleTypeId
                .Where(kvp => this.changedRoleByRoleType!.ContainsKey(kvp.Key))
                .Union(this.changedRoleByRoleType!
                    .Where(kvp => kvp.Value != null)
                    .Cast<KeyValuePair<RoleTypeHandle, object>>())
                .ToFrozenDictionary();

            return new Record(this.ClassHandle, this.Id, this.Version + 1, roleByRoleTypeId);
        }
    }

    internal void Rollback()
    {
        this.Transaction.Store.RecordById.TryGetValue(this.Id, out this.record);
        this.changedRoleByRoleType = null;
        this.checkpointRoleByRoleType = null;
    }

    private void SetMany2OneRole(ManyToOneRoleTypeHandle roleTypeHandle, Object role)
    {
        /*  [if exist]        [then remove]        set
         *
         *  RA ----- R         RA       R       RA    -- R       RA ----- R
         *                ->                +        -        =       -
         *   A ----- PR         A --x-- PR       A --    PR       A --    PR
         */
        var previousRole = (Object?)this[roleTypeHandle];

        // R = PR
        if (Equals(role, previousRole))
        {
            return;
        }

        // A --x-- PR
        if (previousRole != null)
        {
            // TODO:
            // previousRole.RemoveAssociation(roleType.AssociationType, this);
        }

        // A <---- R
        // TODO:
        // role.AddAssociation(roleType.AssociationType, this);

        // A ----> R
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleTypeHandle] = role;
    }

    private void RemoveMany2OneRole(ManyToOneRoleTypeHandle roleTypeHandle)
    {
        /*                        delete
         *  RA --                                RA --
         *       -        ->                 =        -
         *   A ----- R           A --x-- R             -- R
         */

        var previousRole = (Object?)this[roleTypeHandle];
        if (previousRole == null)
        {
            return;
        }

        // A <---- R
        // TODO:
        // previousRole.RemoveAssociation(roleType.AssociationType, this);

        // A ----> R
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleTypeHandle] = null;
    }

    private void AddAssociation(ManyToOneAssociationTypeHandle associationTypeHandle, Object association)
    {
    }

    private void RemoveAssociation(ManyToOneAssociationTypeHandle associationTypeHandle, Object association)
    {
    }
}
