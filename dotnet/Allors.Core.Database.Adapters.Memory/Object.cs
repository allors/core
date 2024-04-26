namespace Allors.Core.Database.Adapters.Memory;

using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using Allors.Core.Database.Meta.Handles;

/// <inheritdoc />
public class Object : IObject
{
    private Record? record;

    private Dictionary<RoleTypeHandle, object?>? changedRoleByRoleType;
    private Dictionary<RoleTypeHandle, object?>? checkpointRoleByRoleType;

    private Dictionary<AssociationTypeHandle, object?>? changedAssociationByAssociationType;
    private Dictionary<AssociationTypeHandle, object?>? checkpointAssociationByAssociationType;

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
    public object? this[UnitRoleTypeHandle roleTypeHandle]
    {
        get
        {
            if (this.changedRoleByRoleType != null && this.changedRoleByRoleType.TryGetValue(roleTypeHandle, out var changedRole))
            {
                return changedRole;
            }

            if (this.record != null && this.record.RoleByRoleTypeId.TryGetValue(roleTypeHandle, out var role))
            {
                return role;
            }

            return null;
        }

        set
        {
            var currentRole = this[roleTypeHandle];
            if (Equals(currentRole, value))
            {
                return;
            }

            this.changedRoleByRoleType ??= [];
            this.changedRoleByRoleType[roleTypeHandle] = value;
        }
    }

    /// <inheritdoc />
    public IObject? this[OneToOneRoleTypeHandle roleTypeHandle]
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
                this.RemoveOneToOneRole(roleTypeHandle);
                return;
            }

            this.SetOneToOneRole(roleTypeHandle, (Object)value);
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
                this.RemoveManyToOneRole(roleTypeHandle);
                return;
            }

            this.SetManyToOneRole(roleTypeHandle, (Object)value);
        }
    }

    /// <inheritdoc />
    public IObject? this[OneToAssociationTypeHandle associationTypeHandle]
    {
        get
        {
            var association = this.OneToAssociation(associationTypeHandle);
            return association != null ? this.Transaction.Instantiate(association.Value) : null;
        }
    }

    /// <inheritdoc />
    public IEnumerable<IObject> this[ManyToAssociationTypeHandle associationTypeHandle]
    {
        get
        {
            var association = this.ManyToAssociation(associationTypeHandle);
            return association != null ? association.Select(this.Transaction.Instantiate) : [];
        }
    }

    /// <inheritdoc />
    public bool Exist(RoleTypeHandle roleTypeHandle)
    {
        if (this.changedRoleByRoleType != null && this.changedRoleByRoleType.TryGetValue(roleTypeHandle, out var changedRole))
        {
            return changedRole != null;
        }

        return this.Record?.RoleByRoleTypeId.ContainsKey(roleTypeHandle) == true;
    }

    /// <inheritdoc />
    public bool Exist(AssociationTypeHandle associationTypeHandle)
    {
        if (this.changedAssociationByAssociationType != null && this.changedAssociationByAssociationType.TryGetValue(associationTypeHandle, out var changedAssociation))
        {
            return changedAssociation != null;
        }

        return this.Record?.AssociationByAssociationTypeId.ContainsKey(associationTypeHandle) == true;
    }

    internal void Checkpoint(ChangeSet changeSet)
    {
        if (this.changedRoleByRoleType != null)
        {
            foreach (var (roleType, changedRole) in this.changedRoleByRoleType)
            {
                if (this.checkpointRoleByRoleType != null &&
                    this.checkpointRoleByRoleType.TryGetValue(roleType, out var checkpointRole))
                {
                    if (roleType is ToManyRoleTypeHandle ?
                            SetEquals((ImmutableHashSet<long>?)changedRole, (ImmutableHashSet<long>?)checkpointRole) :
                            Equals(changedRole, checkpointRole))
                    {
                        continue;
                    }

                    changeSet.AddChangedRoleByRoleTypeId(this, roleType);
                }
                else if (this.record != null)
                {
                    this.record.RoleByRoleTypeId.TryGetValue(roleType, out var recordRole);

                    if (roleType is ToManyRoleTypeHandle ?
                            SetEquals((ImmutableHashSet<long>?)changedRole, (ImmutableHashSet<long>?)recordRole) :
                            Equals(changedRole, recordRole))
                    {
                        continue;
                    }

                    changeSet.AddChangedRoleByRoleTypeId(this, roleType);
                }
                else
                {
                    changeSet.AddChangedRoleByRoleTypeId(this, roleType);
                }
            }

            this.checkpointRoleByRoleType = new Dictionary<RoleTypeHandle, object?>(this.changedRoleByRoleType);
        }

        if (this.changedAssociationByAssociationType != null)
        {
            foreach (var (associationType, changedAssociation) in this.changedAssociationByAssociationType)
            {
                if (this.checkpointAssociationByAssociationType != null &&
                    this.checkpointAssociationByAssociationType.TryGetValue(associationType, out var checkpointAssociation))
                {
                    if (associationType is ManyToAssociationTypeHandle ?
                            SetEquals((ImmutableHashSet<long>?)changedAssociation, (ImmutableHashSet<long>?)checkpointAssociation) :
                            Equals(changedAssociation, checkpointAssociation))
                    {
                        continue;
                    }

                    changeSet.AddChangedAssociationByAssociationTypeId(this, associationType);
                }
                else if (this.record != null)
                {
                    this.record.AssociationByAssociationTypeId.TryGetValue(associationType, out var recordAssociation);

                    if (associationType is ManyToAssociationTypeHandle ?
                            SetEquals((ImmutableHashSet<long>?)changedAssociation, (ImmutableHashSet<long>?)recordAssociation) :
                            Equals(changedAssociation, recordAssociation))
                    {
                        continue;
                    }

                    changeSet.AddChangedAssociationByAssociationTypeId(this, associationType);
                }
                else
                {
                    changeSet.AddChangedAssociationByAssociationTypeId(this, associationType);
                }
            }

            this.checkpointAssociationByAssociationType = new Dictionary<AssociationTypeHandle, object?>(this.changedAssociationByAssociationType);
        }
    }

    internal Record ToRecord()
    {
        if (this.Record == null)
        {
            var roleByRoleTypeId = this.changedRoleByRoleType?
                .Where(kvp => kvp.Value != null)
                .Select(RoleToFrozenSet)
                .ToFrozenDictionary() ?? FrozenDictionary<RoleTypeHandle, object>.Empty;

            var associationByAssociationTypeId = this.changedAssociationByAssociationType?
                .Where(kvp => kvp.Value != null)
                .Select(AssociationToFrozenSet)
                .ToFrozenDictionary() ?? FrozenDictionary<AssociationTypeHandle, object>.Empty;

            return new Record(this.ClassHandle, this.Id, this.Version + 1, roleByRoleTypeId, associationByAssociationTypeId);
        }
        else
        {
            var roleByRoleTypeId = this.changedRoleByRoleType != null ? this.Record.RoleByRoleTypeId
                .Where(kvp => this.changedRoleByRoleType.ContainsKey(kvp.Key))
                .Union(this.changedRoleByRoleType!
                    .Where(kvp => kvp.Value != null)
                    .Select(RoleToFrozenSet))
                .ToFrozenDictionary() : FrozenDictionary<RoleTypeHandle, object>.Empty;

            var associationByAssociationTypeId = this.changedAssociationByAssociationType != null ? this.Record.AssociationByAssociationTypeId
                .Where(kvp => this.changedAssociationByAssociationType.ContainsKey(kvp.Key))
                .Union(this.changedAssociationByAssociationType!
                    .Where(kvp => kvp.Value != null)
                    .Select(AssociationToFrozenSet))
                .ToFrozenDictionary() : FrozenDictionary<AssociationTypeHandle, object>.Empty;

            return new Record(this.ClassHandle, this.Id, this.Version + 1, roleByRoleTypeId, associationByAssociationTypeId);
        }

        KeyValuePair<RoleTypeHandle, object> RoleToFrozenSet(KeyValuePair<RoleTypeHandle, object?> kvp) =>
            new(kvp.Key, kvp.Value is ImmutableHashSet<long> set ? set.ToFrozenSet() : kvp.Value!);

        KeyValuePair<AssociationTypeHandle, object> AssociationToFrozenSet(KeyValuePair<AssociationTypeHandle, object?> kvp) =>
            new(kvp.Key, kvp.Value is ImmutableHashSet<long> set ? set.ToFrozenSet() : kvp.Value!);
    }

    internal void Rollback()
    {
        this.Transaction.Store.RecordById.TryGetValue(this.Id, out this.record);
        this.changedRoleByRoleType = null;
        this.checkpointRoleByRoleType = null;
        this.changedAssociationByAssociationType = null;
        this.checkpointAssociationByAssociationType = null;
    }

    private static bool SetEquals(ImmutableHashSet<long>? objA, ImmutableHashSet<long>? objB)
    {
        if (objA == objB)
        {
            return true;
        }

        if (objA == null || objB == null)
        {
            return false;
        }

        return objA.SetEquals(objB);
    }

    private void SetOneToOneRole(OneToOneRoleTypeHandle roleTypeHandle, Object role)
    {
        /*  [if exist]        [then remove]        set
         *
         *  RA ----- R         RA --x-- R       RA    -- R       RA    -- R
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
            this.RemoveOneToOneRole(roleTypeHandle);
        }

        var associationTypeHandle = this.Transaction.Database.AssociationTypeHandle(roleTypeHandle);

        var roleAssociation = (Object?)role[associationTypeHandle];

        // RA --x-- R
        roleAssociation?.RemoveOneToOneRole(roleTypeHandle);

        // A <---- R
        role.SetAssociation(associationTypeHandle, this);

        // A ----> R
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleTypeHandle] = role;
    }

    private void RemoveOneToOneRole(OneToOneRoleTypeHandle roleTypeHandle)
    {
        /*                        delete
         *
         *   A ----- R    ->     A       R  =   A       R
         */

        var previousRole = (Object?)this[roleTypeHandle];

        if (previousRole == null)
        {
            return;
        }

        var associationTypeHandle = this.Transaction.Database.AssociationTypeHandle(roleTypeHandle);

        // A <---- R
        previousRole.RemoveAssociation(associationTypeHandle);

        // A ----> R
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleTypeHandle] = null;
    }

    private void SetManyToOneRole(ManyToOneRoleTypeHandle roleTypeHandle, Object role)
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

        var associationType = this.Transaction.Database.AssociationTypeHandle(roleTypeHandle);

        // A --x-- PR
        if (previousRole != null)
        {
            previousRole.RemoveAssociation(associationType, this);
        }

        // A <---- R
        role.AddAssociation(associationType, this);

        // A ----> R
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleTypeHandle] = role;
    }

    private void RemoveManyToOneRole(ManyToOneRoleTypeHandle roleTypeHandle)
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

        var associationType = this.Transaction.Database.AssociationTypeHandle(roleTypeHandle);

        // A <---- R
        previousRole.RemoveAssociation(associationType, this);

        // A ----> R
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleTypeHandle] = null;
    }

    private void SetAssociation(OneToAssociationTypeHandle associationTypeHandle, Object value)
    {
        this.changedAssociationByAssociationType ??= [];
        this.changedAssociationByAssociationType[associationTypeHandle] = value.Id;
    }

    private void RemoveAssociation(OneToAssociationTypeHandle associationTypeHandle)
    {
        this.changedAssociationByAssociationType ??= [];
        this.changedAssociationByAssociationType[associationTypeHandle] = null;
    }

    private void AddAssociation(ManyToAssociationTypeHandle associationTypeHandle, Object value)
    {
        var previousAssociation = this.ManyToAssociation(associationTypeHandle);

        if (previousAssociation?.Contains(value.Id) == true)
        {
            return;
        }

        this.changedAssociationByAssociationType ??= [];
        this.changedAssociationByAssociationType[associationTypeHandle] = previousAssociation == null ?
            ImmutableHashSet.CreateRange([value.Id]) :
            previousAssociation.Add(value.Id);
    }

    private void RemoveAssociation(ManyToAssociationTypeHandle associationTypeHandle, Object value)
    {
        var previousAssociation = this.ManyToAssociation(associationTypeHandle);

        if (previousAssociation?.Contains(value.Id) != true)
        {
            return;
        }

        var newAssociation = previousAssociation.Remove(value.Id);

        this.changedAssociationByAssociationType ??= [];
        this.changedAssociationByAssociationType[associationTypeHandle] = newAssociation.Count != 0 ? newAssociation : null;
    }

    private long? OneToAssociation(OneToAssociationTypeHandle associationTypeHandle)
    {
        long? association = null;

        if (this.changedAssociationByAssociationType != null && this.changedAssociationByAssociationType.TryGetValue(associationTypeHandle, out var changedAssociation))
        {
            association = (long?)changedAssociation;
        }
        else if (this.record != null && this.record.AssociationByAssociationTypeId.TryGetValue(associationTypeHandle, out var recordAssociation))
        {
            association = (long?)recordAssociation;
        }

        return association;
    }

    private ImmutableHashSet<long>? ManyToAssociation(ManyToAssociationTypeHandle associationTypeHandle)
    {
        ImmutableHashSet<long>? association = null;

        if (this.changedAssociationByAssociationType != null && this.changedAssociationByAssociationType.TryGetValue(associationTypeHandle, out var changedAssociation))
        {
            association = (ImmutableHashSet<long>?)changedAssociation;
        }
        else if (this.record != null && this.record.AssociationByAssociationTypeId.TryGetValue(associationTypeHandle, out var recordAssociation))
        {
            association = (ImmutableHashSet<long>?)recordAssociation;
        }

        return association;
    }
}
