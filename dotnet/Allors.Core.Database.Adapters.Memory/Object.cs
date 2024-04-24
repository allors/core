namespace Allors.Core.Database.Adapters.Memory;

using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using Allors.Core.Database.Meta;

/// <inheritdoc />
public class Object : IObject
{
    private Record? record;
    private Dictionary<RoleType, object?>? changedRoleByRoleType;
    private Dictionary<RoleType, object?>? checkpointRoleByRoleType;

    /// <summary>
    /// Initializes a new instance of the <see cref="Object"/> class.
    /// </summary>
    internal Object(Transaction transaction, Class @class, long id)
    {
        this.Transaction = transaction;
        this.Class = @class;
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
        this.Class = record.Class;
    }

    /// <inheritdoc />
    public Class Class { get; }

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
    public object? this[UnitRoleType roleType]
    {
        get
        {
            if (this.changedRoleByRoleType != null && this.changedRoleByRoleType.TryGetValue(roleType, out var changedRole))
            {
                return changedRole;
            }

            if (this.record != null && this.record.RoleByRoleTypeId.TryGetValue(roleType, out var role))
            {
                return role;
            }

            return null;
        }

        set
        {
            var currentRole = this[roleType];
            if (Equals(currentRole, value))
            {
                return;
            }

            this.changedRoleByRoleType ??= [];
            this.changedRoleByRoleType[roleType] = value;
        }
    }

    /// <inheritdoc />
    public IObject? this[ManyToOneRoleType roleType]
    {
        get
        {
            if (this.changedRoleByRoleType != null && this.changedRoleByRoleType.TryGetValue(roleType, out var changedRole))
            {
                return (IObject?)changedRole;
            }

            if (this.record != null && this.record.RoleByRoleTypeId.TryGetValue(roleType, out var role))
            {
                return this.Transaction.Instantiate((long)role);
            }

            return null;
        }

        set
        {
            if (value == null)
            {
                this.RemoveMany2OneRole(roleType);
                return;
            }

            this.SetMany2OneRole(roleType, (Object)value);
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

        this.checkpointRoleByRoleType = new Dictionary<RoleType, object?>(this.changedRoleByRoleType);
    }

    internal Record NewRecord()
    {
        if (this.Record == null)
        {
            var roleByRoleTypeId = this.changedRoleByRoleType!
                .Where(kvp => kvp.Value != null)
                .Select(kvp => new KeyValuePair<RoleType, object>(kvp.Key, kvp.Value!))
                .ToFrozenDictionary();

            return new Record(this.Class, this.Id, this.Version + 1, roleByRoleTypeId);
        }
        else
        {
            var roleByRoleTypeId = this.Record.RoleByRoleTypeId
                .Where(kvp => this.changedRoleByRoleType!.ContainsKey(kvp.Key))
                .Union(this.changedRoleByRoleType!
                    .Where(kvp => kvp.Value != null)
                    .Cast<KeyValuePair<RoleType, object>>())
                .ToFrozenDictionary();

            return new Record(this.Class, this.Id, this.Version + 1, roleByRoleTypeId);
        }
    }

    internal void Rollback()
    {
        this.Transaction.Store.RecordById.TryGetValue(this.Id, out this.record);
        this.changedRoleByRoleType = null;
        this.checkpointRoleByRoleType = null;
    }

    private void SetMany2OneRole(ManyToOneRoleType roleType, Object role)
    {
        /*  [if exist]        [then remove]        set
         *
         *  RA ----- R         RA       R       RA    -- R       RA ----- R
         *                ->                +        -        =       -
         *   A ----- PR         A --x-- PR       A --    PR       A --    PR
         */
        var previousRole = (Object?)this[roleType];

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
        this.changedRoleByRoleType[roleType] = role;
    }

    private void RemoveMany2OneRole(ManyToOneRoleType roleType)
    {
        /*                        delete
         *  RA --                                RA --
         *       -        ->                 =        -
         *   A ----- R           A --x-- R             -- R
         */

        var previousRole = (Object?)this[roleType];
        if (previousRole == null)
        {
            return;
        }

        // A <---- R
        // TODO:
        // previousRole.RemoveAssociation(roleType.AssociationType, this);

        // A ----> R
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleType] = null;
    }

    private void AddAssociation(ManyToOneAssociationType associationType, Object association)
    {
    }

    private void RemoveAssociation(ManyToOneAssociationType associationType, Object association)
    {
    }
}
