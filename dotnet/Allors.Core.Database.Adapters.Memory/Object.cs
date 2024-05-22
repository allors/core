namespace Allors.Core.Database.Adapters.Memory;

using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Allors.Core.Database.Meta.Domain;

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
    public object? this[UnitRoleType roleType]
    {
        get => this.GetUnitRole(roleType);

        set => this.SetUnitRole(roleType, value);
    }

    /// <inheritdoc />
    public IObject? this[IToOneRoleType roleType]
    {
        get => this.GetToOneRole(roleType);

        set
        {
            if (roleType is OneToOneRoleType oneToOneRoleType)
            {
                if (value == null)
                {
                    this.RemoveOneToOneRole(oneToOneRoleType);
                    return;
                }

                this.SetOneToOneRole(oneToOneRoleType, (Object)value);
            }
            else
            {
                var manyToOneRoleType = (ManyToOneRoleType)roleType;

                if (value == null)
                {
                    this.RemoveManyToOneRole(manyToOneRoleType);
                    return;
                }

                this.SetManyToOneRole(manyToOneRoleType, (Object)value);
            }
        }
    }

    /// <inheritdoc />
    public IEnumerable<IObject> this[IToManyRoleType roleType]
    {
        get => this.GetToManyRole(roleType)?.Select(this.Transaction.Instantiate) ?? [];
        set
        {
            if (roleType is OneToManyRoleType oneToManyRoleType)
            {
                this.SetOneToManyRole(oneToManyRoleType, value);
            }
            else
            {
                var manyToManyRoleType = (ManyToManyRoleType)roleType;
                this.SetManyToManyRole(manyToManyRoleType, value);
            }
        }
    }

    /// <inheritdoc />
    public IObject? this[IOneToAssociationType associationType]
    {
        get
        {
            var association = this.GetOneToAssociation(associationType);
            return association != null ? this.Transaction.Instantiate(association.Value) : null;
        }
    }

    /// <inheritdoc />
    public IEnumerable<IObject> this[IManyToAssociationType associationType] => this.ManyToAssociation(associationType)?.Select(this.Transaction.Instantiate) ?? [];

    /// <inheritdoc />
    public void Add(IToManyRoleType roleType, IObject value)
    {
        if (roleType is OneToManyRoleType oneToManyRoleType)
        {
            this.AddOneToManyRole(oneToManyRoleType, (Object)value);
        }
        else
        {
            var manyToManyRoleType = (ManyToManyRoleType)roleType;
            this.AddManyToManyRole(manyToManyRoleType, (Object)value);
        }
    }

    /// <inheritdoc />
    public void Remove(IToManyRoleType roleType, IObject value)
    {
        if (roleType is OneToManyRoleType oneToManyRoleType)
        {
            this.RemoveOneToManyRole(oneToManyRoleType, (Object)value);
        }
        else
        {
            var manyToManyRoleType = (ManyToManyRoleType)roleType;
            this.RemoveManyToManyRole(manyToManyRoleType, (Object)value);
        }
    }

    /// <inheritdoc />
    public bool Exist(IRoleType roleType)
    {
        if (this.changedRoleByRoleType != null && this.changedRoleByRoleType.TryGetValue(roleType, out var changedRole))
        {
            return changedRole != null;
        }

        return this.Record?.RoleByRoleTypeId.ContainsKey(roleType) == true;
    }

    /// <inheritdoc />
    public bool Exist(IAssociationType associationType)
    {
        if (this.changedAssociationByAssociationType != null && this.changedAssociationByAssociationType.TryGetValue(associationType, out var changedAssociation))
        {
            return changedAssociation != null;
        }

        return this.Record?.AssociationByAssociationTypeId.ContainsKey(associationType) == true;
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

    private object? GetUnitRole(UnitRoleType roleType)
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

    private void SetUnitRole(UnitRoleType roleType, object? value)
    {
        var currentRole = this[roleType];
        if (Equals(currentRole, value))
        {
            return;
        }

        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleType] = value;
    }

    private IObject? GetToOneRole(IToOneRoleType roleType)
    {
        if (this.changedRoleByRoleType != null && this.changedRoleByRoleType.TryGetValue(roleType, out var changedRole))
        {
            return changedRole != null ? this.Transaction.Instantiate((long)changedRole) : null;
        }

        if (this.record != null && this.record.RoleByRoleTypeId.TryGetValue(roleType, out var role))
        {
            return this.Transaction.Instantiate((long)role);
        }

        return null;
    }

    private void SetOneToOneRole(OneToOneRoleType roleType, Object role)
    {
        /*  [if exist]        [then remove]        set
         *
         *  RA ----- R         RA --x-- R       RA    -- R       RA    -- R
         *                ->                +        -        =       -
         *   A ----- PR         A --x-- PR       A --    PR       A --    PR
         */
        var previousRole = (Object?)this[roleType];

        // R = PR
        if (Equals(role, previousRole))
        {
            return;
        }

        var associationType = this.Transaction.Database.AssociationType(roleType);

        // A --x-- PR
        if (previousRole != null)
        {
            this.RemoveOneToOneRole(roleType);
        }

        var roleAssociation = (Object?)role[associationType];

        // RA --x-- R
        roleAssociation?.RemoveOneToOneRole(roleType);

        // A <---- R
        role.SetOneToAssociation(associationType, this);

        // A ----> R
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleType] = role.Id;
    }

    private void RemoveOneToOneRole(OneToOneRoleType roleType)
    {
        /*                        delete
         *
         *   A ----- R    ->     A       R  =   A       R
         */

        var previousRole = (Object?)this[roleType];

        if (previousRole == null)
        {
            return;
        }

        var associationType = this.Transaction.Database.AssociationType(roleType);

        // A <---- R
        previousRole.RemoveOneToAssociation(associationType);

        // A ----> R
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleType] = null;
    }

    private void SetManyToOneRole(ManyToOneRoleType roleType, Object role)
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

        var associationType = this.Transaction.Database.AssociationType(roleType);

        // A --x-- PR
        previousRole?.RemoveManyToAssociation(associationType, this);

        // A <---- R
        role.AddManyToAssociation(associationType, this);

        // A ----> R
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleType] = role.Id;
    }

    private void RemoveManyToOneRole(ManyToOneRoleType roleType)
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

        var associationType = this.Transaction.Database.AssociationType(roleType);

        // A <---- R
        previousRole.RemoveManyToAssociation(associationType, this);

        // A ----> R
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleType] = null;
    }

    private ImmutableHashSet<long>? GetToManyRole(IToManyRoleType roleType)
    {
        ImmutableHashSet<long>? role = null;

        if (this.changedRoleByRoleType != null && this.changedRoleByRoleType.TryGetValue(roleType, out var changedRole))
        {
            role = (ImmutableHashSet<long>?)changedRole;
        }
        else if (this.record != null && this.record.RoleByRoleTypeId.TryGetValue(roleType, out var recordRole))
        {
            role = (ImmutableHashSet<long>?)recordRole;
        }

        return role;
    }

    private void SetOneToManyRole(OneToManyRoleType roleType, IEnumerable<IObject> value)
    {
        var objects = value.Where(v => (IObject?)v != null).Distinct().Cast<Object>().ToArray();

        // TODO: Optimize
        var previousRole = this.GetToManyRole(roleType);

        if (previousRole == null)
        {
            foreach (var @object in objects)
            {
                this.AddOneToManyRole(roleType, @object);
            }
        }
        else
        {
            var add = objects.Where(v => !previousRole.Contains(v.Id));
            foreach (var @object in add)
            {
                this.AddOneToManyRole(roleType, @object);
            }

            var remove = previousRole.Except(objects.Select(v => v.Id));
            foreach (var @object in remove.Select(this.Transaction.Instantiate).Cast<Object>())
            {
                this.RemoveOneToManyRole(roleType, @object);
            }
        }
    }

    private void AddOneToManyRole(OneToManyRoleType roleType, Object role)
    {
        /*  [if exist]        [then remove]        add
         *
         *  RA ----- R         RA --x-- R       RA    -- R       RA    -- R
         *                ->                +        -        =       -
         *   A ----- PR         A       PR       A --    PR       A ----- PR
         */

        var previousRole = this.GetToManyRole(roleType);

        // R in PR
        if (previousRole?.Contains(role.Id) == true)
        {
            return;
        }

        var associationType = this.Transaction.Database.AssociationType(roleType);

        // RA --x-- R
        var roleAssociation = (Object?)role[associationType];
        roleAssociation?.RemoveOneToManyRole(roleType, role);

        // A <---- R
        role.SetOneToAssociation(associationType, this);

        // A ----> R
        var newRole = previousRole != null ? previousRole.Add(role.Id) : ImmutableHashSet.CreateRange([role.Id]);
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleType] = newRole;
    }

    private void RemoveOneToManyRole(OneToManyRoleType roleType, Object role)
    {
        /*  [if exist]            remove
         *
         *        -- R2                 R2             -- R2
         *       -         ->                =       -
         *   A ----- R          A --x-- R       A --      R
         */

        var previousRoleIds = this.GetToManyRole(roleType);

        // R not in PR
        if (previousRoleIds?.Contains(role.Id) != true)
        {
            return;
        }

        var associationType = this.Transaction.Database.AssociationType(roleType);

        // A <---- R
        role.RemoveOneToAssociation(associationType);

        // A ----> R
        var newRole = previousRoleIds.Remove(role.Id);
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleType] = newRole.Count == 0 ? null : newRole;
    }

    private void SetManyToManyRole(ManyToManyRoleType roleType, IEnumerable<IObject> value)
    {
        var objects = value.Where(v => (IObject?)v != null).Distinct().Cast<Object>().ToArray();

        // TODO: Optimize
        var previousRole = this.GetToManyRole(roleType);

        if (previousRole == null)
        {
            foreach (var @object in objects)
            {
                this.AddManyToManyRole(roleType, @object);
            }
        }
        else
        {
            var remove = previousRole.Except(objects.Select(v => v.Id));
            foreach (var @object in remove.Select(this.Transaction.Instantiate).Cast<Object>())
            {
                this.RemoveManyToManyRole(roleType, @object);
            }

            var add = objects.Where(v => !previousRole.Contains(v.Id));
            foreach (var @object in add.Cast<Object>())
            {
                this.AddManyToManyRole(roleType, @object);
            }
        }
    }

    private void AddManyToManyRole(ManyToManyRoleType roleType, Object role)
    {
        /*  [if exist]        [no remove]         set
         *
         *  RA ----- R         RA       R       RA    -- R       RA ----- R
         *                ->                +        -        =       -
         *   A ----- PR         A       PR       A --    PR       A ----- PR
         */
        var previousRole = this.GetToManyRole(roleType);

        // R in PR
        if (previousRole?.Contains(role.Id) == true)
        {
            return;
        }

        var associationType = this.Transaction.Database.AssociationType(roleType);

        // A <---- R
        role.AddManyToAssociation(associationType, this);

        // A ----> R
        var newRole = previousRole != null ? previousRole.Add(role.Id) : ImmutableHashSet.CreateRange([role.Id]);
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleType] = newRole;
    }

    private void RemoveManyToManyRole(ManyToManyRoleType roleType, Object role)
    {
        /*  [if exist]            remove
         *
         *        -- R2                   R2              -- R2
         *       -                                      -
         *   A ----- R     ->     A --x-- R   =   A  --  --  R
         *       -                                     -
         *  A2 -                 A2       R       A2 --
         */

        var previousRole = this.GetToManyRole(roleType);

        // R not in PR
        if (previousRole?.Contains(role.Id) != true)
        {
            return;
        }

        var associationType = this.Transaction.Database.AssociationType(roleType);

        // A <---- R
        role.RemoveManyToAssociation(associationType, this);

        // A ----> R
        var newRole = previousRole.Remove(role.Id);
        if (newRole.Count == 0)
        {
            this.changedRoleByRoleType ??= [];
            this.changedRoleByRoleType.Remove(roleType);
        }
        else
        {
            this.changedRoleByRoleType ??= [];
            this.changedRoleByRoleType[roleType] = newRole;
        }
    }

    private long? GetOneToAssociation(IOneToAssociationType associationType)
    {
        if (this.changedAssociationByAssociationType != null && this.changedAssociationByAssociationType.TryGetValue(associationType, out var changedAssociation))
        {
            return (long?)changedAssociation;
        }

        if (this.record != null && this.record.AssociationByAssociationTypeId.TryGetValue(associationType, out var recordAssociation))
        {
            return (long)recordAssociation;
        }

        return null;
    }

    private void SetOneToAssociation(IOneToAssociationType associationType, Object value)
    {
        this.changedAssociationByAssociationType ??= [];
        this.changedAssociationByAssociationType[associationType] = value.Id;
    }

    private void RemoveOneToAssociation(IOneToAssociationType associationType)
    {
        this.changedAssociationByAssociationType ??= [];
        this.changedAssociationByAssociationType[associationType] = null;
    }

    private ImmutableHashSet<long>? ManyToAssociation(IManyToAssociationType associationType)
    {
        ImmutableHashSet<long>? association = null;

        if (this.changedAssociationByAssociationType != null && this.changedAssociationByAssociationType.TryGetValue(associationType, out var changedAssociation))
        {
            association = (ImmutableHashSet<long>?)changedAssociation;
        }
        else if (this.record != null && this.record.AssociationByAssociationTypeId.TryGetValue(associationType, out var recordAssociation))
        {
            association = (ImmutableHashSet<long>?)recordAssociation;
        }

        return association;
    }

    private void AddManyToAssociation(IManyToAssociationType associationType, Object value)
    {
        var previousAssociation = this.ManyToAssociation(associationType);

        if (previousAssociation?.Contains(value.Id) == true)
        {
            return;
        }

        this.changedAssociationByAssociationType ??= [];
        this.changedAssociationByAssociationType[associationType] = previousAssociation == null ?
            ImmutableHashSet.CreateRange([value.Id]) :
            previousAssociation.Add(value.Id);
    }

    private void RemoveManyToAssociation(IManyToAssociationType associationType, Object value)
    {
        var previousAssociation = this.ManyToAssociation(associationType);

        if (previousAssociation?.Contains(value.Id) != true)
        {
            return;
        }

        var newAssociation = previousAssociation.Remove(value.Id);

        this.changedAssociationByAssociationType ??= [];
        this.changedAssociationByAssociationType[associationType] = newAssociation.Count != 0 ? newAssociation : null;
    }
}
