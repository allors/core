namespace Allors.Core.Database.Adapters.Memory;

using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Allors.Core.Database.Meta.Handles;

/// <inheritdoc />
public class Object : IObject
{
    private Record? record;

    private Dictionary<IRoleType, object?>? changedRoleByRoleType;
    private Dictionary<IRoleType, object?>? checkpointRoleByRoleType;

    private Dictionary<IAssociationType, object?>? changedAssociationByAssociationType;
    private Dictionary<IAssociationType, object?>? checkpointAssociationByAssociationType;

    /// <summary>
    /// Initializes a new instance of the <see cref="Object"/> class.
    /// </summary>
    internal Object(Transaction transaction, Class classHandle, long id)
    {
        this.Transaction = transaction;
        this.Class = classHandle;
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
        this.Class = record.ClassHandle;
    }

    /// <inheritdoc />
    public Class Class { get; }

    /// <inheritdoc/>
    public long Id { get; }

    /// <inheritdoc/>
    public long Version => this.record?.Version ?? 0;

    /// <inheritdoc />
    public bool IsNew => this.Version == 0;

    ITransaction IObject.Transaction => this.Transaction;

    internal Record? Record => this.record;

    internal bool ShouldCommit
    {
        get
        {
            if (this.IsNew)
            {
                return true;
            }

            if (this.changedRoleByRoleType == null && this.changedAssociationByAssociationType == null)
            {
                return false;
            }

            if (this.record != null && this.changedRoleByRoleType != null)
            {
                foreach (var (roleType, changedRole) in this.changedRoleByRoleType)
                {
                    this.record.RoleByRoleTypeId.TryGetValue(roleType, out var recordRole);
                    if (!Equals(changedRole, recordRole))
                    {
                        return true;
                    }
                }
            }

            if (this.record != null && this.changedAssociationByAssociationType != null)
            {
                foreach (var (associationType, changedAssociation) in this.changedAssociationByAssociationType)
                {
                    this.record.AssociationByAssociationTypeId.TryGetValue(associationType, out var recordAssociation);
                    if (!Equals(changedAssociation, recordAssociation))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }

    internal Transaction Transaction { get; }

    /// <inheritdoc />
    public object? this[UnitRoleType roleTypeHandle]
    {
        get => this.GetUnitRole(roleTypeHandle);

        set => this.SetUnitRole(roleTypeHandle, value);
    }

    /// <inheritdoc />
    public IObject? this[IToOneRoleType roleTypeHandle]
    {
        get => this.GetToOneRole(roleTypeHandle);

        set
        {
            if (roleTypeHandle is OneToOneRoleType oneToOneRoleTypeHandle)
            {
                if (value == null)
                {
                    this.RemoveOneToOneRole(oneToOneRoleTypeHandle);
                    return;
                }

                this.SetOneToOneRole(oneToOneRoleTypeHandle, (Object)value);
            }
            else
            {
                var manyToOneRoleTypeHandle = (ManyToOneRoleType)roleTypeHandle;

                if (value == null)
                {
                    this.RemoveManyToOneRole(manyToOneRoleTypeHandle);
                    return;
                }

                this.SetManyToOneRole(manyToOneRoleTypeHandle, (Object)value);
            }
        }
    }

    /// <inheritdoc />
    public IEnumerable<IObject> this[IToManyRoleType roleTypeHandle]
    {
        get => this.GetToManyRole(roleTypeHandle)?.Select(this.Transaction.Instantiate) ?? [];
        set
        {
            if (roleTypeHandle is OneToManyRoleType oneToManyRoleTypeHandle)
            {
                this.SetOneToManyRole(oneToManyRoleTypeHandle, value);
            }
            else
            {
                var manyToManyRoleTypeHandle = (ManyToManyRoleType)roleTypeHandle;
                this.SetManyToManyRole(manyToManyRoleTypeHandle, value);
            }
        }
    }

    /// <inheritdoc />
    public IObject? this[IOneToAssociationType associationTypeHandle]
    {
        get
        {
            var association = this.GetOneToAssociation(associationTypeHandle);
            return association != null ? this.Transaction.Instantiate(association.Value) : null;
        }
    }

    /// <inheritdoc />
    public IEnumerable<IObject> this[IManyToAssociationType associationTypeHandle] => this.ManyToAssociation(associationTypeHandle)?.Select(this.Transaction.Instantiate) ?? [];

    /// <inheritdoc />
    public void Add(IToManyRoleType roleTypeHandle, IObject value)
    {
        if (roleTypeHandle is OneToManyRoleType oneToManyRoleTypeHandle)
        {
            this.AddOneToManyRole(oneToManyRoleTypeHandle, (Object)value);
        }
        else
        {
            var manyToManyRoleTypeHandle = (ManyToManyRoleType)roleTypeHandle;
            this.AddManyToManyRole(manyToManyRoleTypeHandle, (Object)value);
        }
    }

    /// <inheritdoc />
    public void Remove(IToManyRoleType roleTypeHandle, IObject value)
    {
        if (roleTypeHandle is OneToManyRoleType oneToManyRoleTypeHandle)
        {
            this.RemoveOneToManyRole(oneToManyRoleTypeHandle, (Object)value);
        }
        else
        {
            var manyToManyRoleTypeHandle = (ManyToManyRoleType)roleTypeHandle;
            this.RemoveManyToManyRole(manyToManyRoleTypeHandle, (Object)value);
        }
    }

    /// <inheritdoc />
    public bool Exist(IRoleType roleTypeHandle)
    {
        if (this.changedRoleByRoleType != null && this.changedRoleByRoleType.TryGetValue(roleTypeHandle, out var changedRole))
        {
            return changedRole != null;
        }

        return this.Record?.RoleByRoleTypeId.ContainsKey(roleTypeHandle) == true;
    }

    /// <inheritdoc />
    public bool Exist(IAssociationType associationTypeHandle)
    {
        if (this.changedAssociationByAssociationType != null && this.changedAssociationByAssociationType.TryGetValue(associationTypeHandle, out var changedAssociation))
        {
            return changedAssociation != null;
        }

        return this.Record?.AssociationByAssociationTypeId.ContainsKey(associationTypeHandle) == true;
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        var meta = this.Transaction.Database.Meta;
        var className = this.Class[meta.Meta.ObjectTypeSingularName];
        return $"{className}:{this.Id}";
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
                    if (roleType is IToManyRoleType ?
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

                    if (roleType is IToManyRoleType ?
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

            this.checkpointRoleByRoleType = new Dictionary<IRoleType, object?>(this.changedRoleByRoleType);
        }

        if (this.changedAssociationByAssociationType != null)
        {
            foreach (var (associationType, changedAssociation) in this.changedAssociationByAssociationType)
            {
                if (this.checkpointAssociationByAssociationType != null &&
                    this.checkpointAssociationByAssociationType.TryGetValue(associationType, out var checkpointAssociation))
                {
                    if (associationType is IManyToAssociationType ?
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

                    if (associationType is IManyToAssociationType ?
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

            this.checkpointAssociationByAssociationType = new Dictionary<IAssociationType, object?>(this.changedAssociationByAssociationType);
        }
    }

    internal Record ToRecord()
    {
        if (this.IsNew)
        {
            var roleByRoleTypeId = this.changedRoleByRoleType?
                .Where(kvp => kvp.Value != null)
                .Select(RoleNotNull)
                .ToFrozenDictionary() ?? FrozenDictionary<IRoleType, object>.Empty;

            var associationByAssociationTypeId = this.changedAssociationByAssociationType?
                .Where(kvp => kvp.Value != null)
                .Select(AssociationNotNull)
                .ToFrozenDictionary() ?? FrozenDictionary<IAssociationType, object>.Empty;

            return new Record(this.Class, this.Id, this.Version + 1, roleByRoleTypeId, associationByAssociationTypeId);
        }
        else
        {
            var roleByRoleTypeId = this.changedRoleByRoleType != null ? this.Record!.RoleByRoleTypeId
                .Where(kvp => !this.changedRoleByRoleType.ContainsKey(kvp.Key))
                .Union(this.changedRoleByRoleType!
                    .Where(kvp => kvp.Value != null)
                    .Select(RoleNotNull))
                .ToFrozenDictionary() : FrozenDictionary<IRoleType, object>.Empty;

            var associationByAssociationTypeId = this.changedAssociationByAssociationType != null ? this.Record!.AssociationByAssociationTypeId
                .Where(kvp => !this.changedAssociationByAssociationType.ContainsKey(kvp.Key))
                .Union(this.changedAssociationByAssociationType!
                    .Where(kvp => kvp.Value != null)
                    .Select(AssociationNotNull))
                .ToFrozenDictionary() : FrozenDictionary<IAssociationType, object>.Empty;

            return new Record(this.Class, this.Id, this.Version + 1, roleByRoleTypeId, associationByAssociationTypeId);
        }

        KeyValuePair<IRoleType, object> RoleNotNull(KeyValuePair<IRoleType, object?> kvp) =>
            new(kvp.Key, kvp.Value!);

        KeyValuePair<IAssociationType, object> AssociationNotNull(KeyValuePair<IAssociationType, object?> kvp) =>
            new(kvp.Key, kvp.Value!);
    }

    internal void Commit(Transaction commitTransaction)
    {
        var commitObject = commitTransaction.Instantiate(this.Id);

        if (this.changedRoleByRoleType == null)
        {
            return;
        }

        foreach (var (roleType, changedRole) in this.changedRoleByRoleType)
        {
            switch (roleType)
            {
                case UnitRoleType unitRoleType:
                    commitObject[unitRoleType] = changedRole;
                    return;

                case IToOneRoleType toOneRoleType:
                    commitObject[toOneRoleType] = changedRole != null
                        ? commitTransaction.Instantiate((long)changedRole)
                        : null;
                    return;

                case IToManyRoleType toManyRoleType:
                    commitObject[toManyRoleType] = changedRole != null
                        ? ((IEnumerable<long>)changedRole).Select(commitTransaction.Instantiate)
                        : [];
                    return;

                default:
                    throw new InvalidOperationException();
            }
        }
    }

    internal void Reset()
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

    private object? GetUnitRole(UnitRoleType roleTypeHandle)
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

    private void SetUnitRole(UnitRoleType roleTypeHandle, object? value)
    {
        var currentRole = this[roleTypeHandle];
        if (Equals(currentRole, value))
        {
            return;
        }

        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleTypeHandle] = value;
    }

    private IObject? GetToOneRole(IToOneRoleType roleTypeHandle)
    {
        if (this.changedRoleByRoleType != null && this.changedRoleByRoleType.TryGetValue(roleTypeHandle, out var changedRole))
        {
            return changedRole != null ? this.Transaction.Instantiate((long)changedRole) : null;
        }

        if (this.record != null && this.record.RoleByRoleTypeId.TryGetValue(roleTypeHandle, out var role))
        {
            return this.Transaction.Instantiate((long)role);
        }

        return null;
    }

    private void SetOneToOneRole(OneToOneRoleType roleTypeHandle, Object role)
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

        var associationTypeHandle = this.Transaction.Database.AssociationTypeHandle(roleTypeHandle);

        // A --x-- PR
        if (previousRole != null)
        {
            this.RemoveOneToOneRole(roleTypeHandle);
        }

        var roleAssociation = (Object?)role[associationTypeHandle];

        // RA --x-- R
        roleAssociation?.RemoveOneToOneRole(roleTypeHandle);

        // A <---- R
        role.SetOneToAssociation(associationTypeHandle, this);

        // A ----> R
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleTypeHandle] = role.Id;
    }

    private void RemoveOneToOneRole(OneToOneRoleType roleTypeHandle)
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
        previousRole.RemoveOneToAssociation(associationTypeHandle);

        // A ----> R
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleTypeHandle] = null;
    }

    private void SetManyToOneRole(ManyToOneRoleType roleTypeHandle, Object role)
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
        previousRole?.RemoveManyToAssociation(associationType, this);

        // A <---- R
        role.AddManyToAssociation(associationType, this);

        // A ----> R
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleTypeHandle] = role.Id;
    }

    private void RemoveManyToOneRole(ManyToOneRoleType roleTypeHandle)
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
        previousRole.RemoveManyToAssociation(associationType, this);

        // A ----> R
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleTypeHandle] = null;
    }

    private ImmutableHashSet<long>? GetToManyRole(IToManyRoleType roleTypeHandle)
    {
        ImmutableHashSet<long>? role = null;

        if (this.changedRoleByRoleType != null && this.changedRoleByRoleType.TryGetValue(roleTypeHandle, out var changedRole))
        {
            role = (ImmutableHashSet<long>?)changedRole;
        }

        if (this.record != null && this.record.RoleByRoleTypeId.TryGetValue(roleTypeHandle, out var recordRole))
        {
            role = (ImmutableHashSet<long>?)recordRole;
        }

        return role;
    }

    private void SetOneToManyRole(OneToManyRoleType roleTypeHandle, IEnumerable<IObject> value)
    {
        var objects = value.Where(v => (IObject?)v != null).Distinct().Cast<Object>().ToArray();

        // TODO: Optimize
        var previousRole = this.GetToManyRole(roleTypeHandle);

        if (previousRole == null)
        {
            foreach (var @object in objects)
            {
                this.AddOneToManyRole(roleTypeHandle, @object);
            }
        }
        else
        {
            var add = objects.Where(v => !previousRole.Contains(v.Id));
            foreach (var @object in add)
            {
                this.AddOneToManyRole(roleTypeHandle, @object);
            }

            var remove = previousRole.Except(objects.Select(v => v.Id));
            foreach (var @object in remove.Select(this.Transaction.Instantiate).Cast<Object>())
            {
                this.RemoveOneToManyRole(roleTypeHandle, @object);
            }
        }
    }

    private void AddOneToManyRole(OneToManyRoleType roleTypeHandle, Object role)
    {
        /*  [if exist]        [then remove]        add
         *
         *  RA ----- R         RA --x-- R       RA    -- R       RA    -- R
         *                ->                +        -        =       -
         *   A ----- PR         A       PR       A --    PR       A ----- PR
         */

        var previousRole = this.GetToManyRole(roleTypeHandle);

        // R in PR
        if (previousRole?.Contains(role.Id) == true)
        {
            return;
        }

        var associationTypeHandle = this.Transaction.Database.AssociationTypeHandle(roleTypeHandle);

        // RA --x-- R
        var roleAssociation = (Object?)role[associationTypeHandle];
        roleAssociation?.RemoveOneToManyRole(roleTypeHandle, role);

        // A <---- R
        role.SetOneToAssociation(associationTypeHandle, this);

        // A ----> R
        var newRole = previousRole != null ? previousRole.Add(role.Id) : ImmutableHashSet.CreateRange([role.Id]);
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleTypeHandle] = newRole;
    }

    private void RemoveOneToManyRole(OneToManyRoleType roleTypeHandle, Object role)
    {
        /*  [if exist]            remove
         *
         *        -- R2                 R2             -- R2
         *       -         ->                =       -
         *   A ----- R          A --x-- R       A --      R
         */

        var previousRoleIds = this.GetToManyRole(roleTypeHandle);

        // R not in PR
        if (previousRoleIds?.Contains(role.Id) != true)
        {
            return;
        }

        var associationTypeHandle = this.Transaction.Database.AssociationTypeHandle(roleTypeHandle);

        // A <---- R
        role.RemoveOneToAssociation(associationTypeHandle);

        // A ----> R
        var newRole = previousRoleIds.Remove(role.Id);
        if (newRole.Count == 0)
        {
            this.changedRoleByRoleType ??= [];
            this.changedRoleByRoleType.Remove(roleTypeHandle);
        }
        else
        {
            this.changedRoleByRoleType ??= [];
            this.changedRoleByRoleType[roleTypeHandle] = newRole;
        }
    }

    private void SetManyToManyRole(ManyToManyRoleType roleTypeHandle, IEnumerable<IObject> value)
    {
        var objects = value.Where(v => (IObject?)v != null).Distinct().Cast<Object>().ToArray();

        // TODO: Optimize
        var previousRole = this.GetToManyRole(roleTypeHandle);

        if (previousRole == null)
        {
            foreach (var @object in objects)
            {
                this.AddManyToManyRole(roleTypeHandle, @object);
            }
        }
        else
        {
            var remove = previousRole.Except(objects.Select(v => v.Id));
            foreach (var @object in remove.Select(this.Transaction.Instantiate).Cast<Object>())
            {
                this.RemoveManyToManyRole(roleTypeHandle, @object);
            }

            var add = objects.Where(v => !previousRole.Contains(v.Id));
            foreach (var @object in add.Cast<Object>())
            {
                this.AddManyToManyRole(roleTypeHandle, @object);
            }
        }
    }

    private void AddManyToManyRole(ManyToManyRoleType roleTypeHandle, Object role)
    {
        /*  [if exist]        [no remove]         set
         *
         *  RA ----- R         RA       R       RA    -- R       RA ----- R
         *                ->                +        -        =       -
         *   A ----- PR         A       PR       A --    PR       A ----- PR
         */
        var previousRole = this.GetToManyRole(roleTypeHandle);

        // R in PR
        if (previousRole?.Contains(role.Id) == true)
        {
            return;
        }

        var associationTypeHandle = this.Transaction.Database.AssociationTypeHandle(roleTypeHandle);

        // A <---- R
        role.AddManyToAssociation(associationTypeHandle, this);

        // A ----> R
        var newRole = previousRole != null ? previousRole.Add(role.Id) : ImmutableHashSet.CreateRange([role.Id]);
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleTypeHandle] = newRole;
    }

    private void RemoveManyToManyRole(ManyToManyRoleType roleTypeHandle, Object role)
    {
        /*  [if exist]            remove
         *
         *        -- R2                   R2              -- R2
         *       -                                      -
         *   A ----- R     ->     A --x-- R   =   A  --  --  R
         *       -                                     -
         *  A2 -                 A2       R       A2 --
         */

        var previousRole = this.GetToManyRole(roleTypeHandle);

        // R not in PR
        if (previousRole?.Contains(role.Id) != true)
        {
            return;
        }

        var associationTypeHandle = this.Transaction.Database.AssociationTypeHandle(roleTypeHandle);

        // A <---- R
        role.RemoveManyToAssociation(associationTypeHandle, this);

        // A ----> R
        var newRole = previousRole.Remove(role.Id);
        if (newRole.Count == 0)
        {
            this.changedRoleByRoleType ??= [];
            this.changedRoleByRoleType.Remove(roleTypeHandle);
        }
        else
        {
            this.changedRoleByRoleType ??= [];
            this.changedRoleByRoleType[roleTypeHandle] = newRole;
        }
    }

    private long? GetOneToAssociation(IOneToAssociationType associationTypeHandle)
    {
        if (this.changedAssociationByAssociationType != null && this.changedAssociationByAssociationType.TryGetValue(associationTypeHandle, out var changedAssociation))
        {
            return (long?)changedAssociation;
        }

        if (this.record != null && this.record.AssociationByAssociationTypeId.TryGetValue(associationTypeHandle, out var recordAssociation))
        {
            return (long)recordAssociation;
        }

        return null;
    }

    private void SetOneToAssociation(IOneToAssociationType associationTypeHandle, Object value)
    {
        this.changedAssociationByAssociationType ??= [];
        this.changedAssociationByAssociationType[associationTypeHandle] = value.Id;
    }

    private void RemoveOneToAssociation(IOneToAssociationType associationTypeHandle)
    {
        this.changedAssociationByAssociationType ??= [];
        this.changedAssociationByAssociationType[associationTypeHandle] = null;
    }

    private ImmutableHashSet<long>? ManyToAssociation(IManyToAssociationType associationTypeHandle)
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

    private void AddManyToAssociation(IManyToAssociationType associationTypeHandle, Object value)
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

    private void RemoveManyToAssociation(IManyToAssociationType associationTypeHandle, Object value)
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
}
