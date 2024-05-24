namespace Allors.Core.Database.Engines.Memory;

using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Allors.Core.Database.Engines.Meta;
using Allors.Core.Database.Meta.Domain;

/// <inheritdoc />
public class Object : IObject
{
    private Record? record;

    private Dictionary<EngineRoleType, object?>? changedRoleByRoleType;
    private Dictionary<EngineRoleType, object?>? checkpointRoleByRoleType;

    private Dictionary<EngineAssociationType, object?>? changedAssociationByAssociationType;
    private Dictionary<EngineAssociationType, object?>? checkpointAssociationByAssociationType;

    /// <summary>
    /// Initializes a new instance of the <see cref="Object"/> class.
    /// </summary>
    internal Object(Transaction transaction, EngineClass @class, long id)
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
    Class IObject.Class => (Class)this.Class.MetaObject;

    /// <summary>
    /// The class.
    /// </summary>
    public EngineClass Class { get; }

    /// <summary>
    /// The meta.
    /// </summary>
    public EngineMeta Meta => this.Transaction.Database.Meta;

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
    object? IObject.this[UnitRoleType roleType]
    {
        get => this[this.Meta[roleType]];
        set => this[this.Meta[roleType]] = value;
    }

    /// <inheritdoc />
    IObject? IObject.this[IToOneRoleType roleType]
    {
        get => this[this.Meta[roleType]];
        set => this[this.Meta[roleType]] = value;
    }

    /// <inheritdoc />
    IEnumerable<IObject> IObject.this[IToManyRoleType roleType]
    {
        get => this[this.Meta[roleType]];
        set => this[this.Meta[roleType]] = value;
    }

    /// <inheritdoc />
    IObject? IObject.this[IOneToAssociationType associationType] => this[this.Meta[associationType]];

    /// <inheritdoc />
    IEnumerable<IObject> IObject.this[IManyToAssociationType associationType] => this[this.Meta[associationType]];

    private object? this[EngineUnitRoleType roleType]
    {
        get => this.GetUnitRole(roleType);
        set => this.SetUnitRole(roleType, value);
    }

    private IObject? this[EngineToOneRoleType roleType]
    {
        get => this.GetToOneRole(roleType);

        set
        {
            this.AssertIsAssignable(roleType, value);

            if (roleType is EngineOneToOneRoleType oneToOneRoleType)
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
                var manyToOneRoleType = (EngineManyToOneRoleType)roleType;

                if (value == null)
                {
                    this.RemoveManyToOneRole(manyToOneRoleType);
                    return;
                }

                this.SetManyToOneRole(manyToOneRoleType, (Object)value);
            }
        }
    }

    private IEnumerable<IObject> this[EngineToManyRoleType roleType]
    {
        get => this.GetToManyRole(roleType)?.Select(this.Transaction.Instantiate) ?? [];
        set
        {
            var objects = value.Where(v => (IObject?)v != null).Distinct().Cast<Object>().ToArray();

            this.AssertIsAssignable(roleType, objects);

            if (roleType is EngineOneToManyRoleType oneToManyRoleType)
            {
                this.SetOneToManyRole(oneToManyRoleType, objects);
            }
            else
            {
                var manyToManyRoleType = (EngineManyToManyRoleType)roleType;
                this.SetManyToManyRole(manyToManyRoleType, objects);
            }
        }
    }

    private IObject? this[EngineOneToAssociationType associationType]
    {
        get
        {
            var association = this.GetOneToAssociation(associationType);
            return association != null ? this.Transaction.Instantiate(association.Value) : null;
        }
    }

    private IEnumerable<IObject> this[EngineManyToAssociationType associationType] => this.ManyToAssociation(associationType)?.Select(this.Transaction.Instantiate) ?? [];

    /// <inheritdoc />
    void IObject.Add(IToManyRoleType roleType, IObject value) => this.Add(this.Meta[roleType], value);

    /// <inheritdoc />
    void IObject.Remove(IToManyRoleType roleType, IObject value) => this.Remove(this.Meta[roleType], value);

    /// <inheritdoc />
    bool IObject.Exist(IRoleType roleType) => this.Exist(this.Meta[roleType]);

    /// <inheritdoc />
    bool IObject.Exist(IAssociationType associationType) => this.Exist(this.Meta[associationType]);

    /// <inheritdoc/>
    public override string ToString() => $"{this.Class.SingularName}:{this.Id}";

    internal void Checkpoint(ChangeSet changeSet)
    {
        if (this.changedRoleByRoleType != null)
        {
            foreach (var (roleType, changedRole) in this.changedRoleByRoleType)
            {
                if (this.checkpointRoleByRoleType != null &&
                    this.checkpointRoleByRoleType.TryGetValue(roleType, out var checkpointRole))
                {
                    if (roleType is EngineToManyRoleType ?
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

                    if (roleType is EngineToManyRoleType ?
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

            this.checkpointRoleByRoleType = new Dictionary<EngineRoleType, object?>(this.changedRoleByRoleType);
        }

        if (this.changedAssociationByAssociationType != null)
        {
            foreach (var (associationType, changedAssociation) in this.changedAssociationByAssociationType)
            {
                if (this.checkpointAssociationByAssociationType != null &&
                    this.checkpointAssociationByAssociationType.TryGetValue(associationType, out var checkpointAssociation))
                {
                    if (associationType is EngineManyToAssociationType ?
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

                    if (associationType is EngineManyToAssociationType ?
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

            this.checkpointAssociationByAssociationType = new Dictionary<EngineAssociationType, object?>(this.changedAssociationByAssociationType);
        }
    }

    internal Record ToRecord()
    {
        if (this.IsNew)
        {
            var roleByRoleTypeId = this.changedRoleByRoleType?
                .Where(kvp => kvp.Value != null)
                .Select(RoleNotNull)
                .ToFrozenDictionary() ?? FrozenDictionary<EngineRoleType, object>.Empty;

            var associationByAssociationTypeId = this.changedAssociationByAssociationType?
                .Where(kvp => kvp.Value != null)
                .Select(AssociationNotNull)
                .ToFrozenDictionary() ?? FrozenDictionary<EngineAssociationType, object>.Empty;

            return new Record(this.Class, this.Id, this.Version + 1, roleByRoleTypeId, associationByAssociationTypeId);
        }
        else
        {
            var roleByRoleTypeId = this.changedRoleByRoleType != null ? this.Record!.RoleByRoleTypeId
                .Where(kvp => !this.changedRoleByRoleType.ContainsKey(kvp.Key))
                .Union(this.changedRoleByRoleType!
                    .Where(kvp => kvp.Value != null)
                    .Select(RoleNotNull))
                .ToFrozenDictionary() : FrozenDictionary<EngineRoleType, object>.Empty;

            var associationByAssociationTypeId = this.changedAssociationByAssociationType != null ? this.Record!.AssociationByAssociationTypeId
                .Where(kvp => !this.changedAssociationByAssociationType.ContainsKey(kvp.Key))
                .Union(this.changedAssociationByAssociationType!
                    .Where(kvp => kvp.Value != null)
                    .Select(AssociationNotNull))
                .ToFrozenDictionary() : FrozenDictionary<EngineAssociationType, object>.Empty;

            return new Record(this.Class, this.Id, this.Version + 1, roleByRoleTypeId, associationByAssociationTypeId);
        }

        KeyValuePair<EngineRoleType, object> RoleNotNull(KeyValuePair<EngineRoleType, object?> kvp) =>
            new(kvp.Key, kvp.Value!);

        KeyValuePair<EngineAssociationType, object> AssociationNotNull(KeyValuePair<EngineAssociationType, object?> kvp) =>
            new(kvp.Key, kvp.Value!);
    }

    internal void Commit(Transaction commitTransaction)
    {
        var commitObject = (Object)commitTransaction.Instantiate(this.Id);

        if (this.changedRoleByRoleType == null)
        {
            return;
        }

        foreach (var (roleType, changedRole) in this.changedRoleByRoleType)
        {
            switch (roleType)
            {
                case EngineUnitRoleType unitRoleType:
                    commitObject[unitRoleType] = changedRole;
                    return;

                case EngineToOneRoleType toOneRoleType:
                    commitObject[toOneRoleType] = changedRole != null
                        ? commitTransaction.Instantiate((long)changedRole)
                        : null;
                    return;

                case EngineToManyRoleType toManyRoleType:
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

    private void Add(EngineToManyRoleType roleType, IObject value)
    {
        this.AssertIsAssignable(roleType, value);

        if (roleType is EngineOneToManyRoleType oneToManyRoleType)
        {
            this.AddOneToManyRole(oneToManyRoleType, (Object)value);
        }
        else
        {
            var manyToManyRoleType = (EngineManyToManyRoleType)roleType;
            this.AddManyToManyRole(manyToManyRoleType, (Object)value);
        }
    }

    private void Remove(EngineToManyRoleType roleType, IObject value)
    {
        this.AssertIsAssignable(roleType, value);

        if (roleType is EngineOneToManyRoleType oneToManyRoleType)
        {
            this.RemoveOneToManyRole(oneToManyRoleType, (Object)value);
        }
        else
        {
            var manyToManyRoleType = (EngineManyToManyRoleType)roleType;
            this.RemoveManyToManyRole(manyToManyRoleType, (Object)value);
        }
    }

    private bool Exist(EngineRoleType roleType)
    {
        if (this.changedRoleByRoleType != null && this.changedRoleByRoleType.TryGetValue(roleType, out var changedRole))
        {
            return changedRole != null;
        }

        return this.Record?.RoleByRoleTypeId.ContainsKey(roleType) == true;
    }

    private bool Exist(EngineAssociationType associationType)
    {
        if (this.changedAssociationByAssociationType != null && this.changedAssociationByAssociationType.TryGetValue(associationType, out var changedAssociation))
        {
            return changedAssociation != null;
        }

        return this.Record?.AssociationByAssociationTypeId.ContainsKey(associationType) == true;
    }

    private object? GetUnitRole(EngineUnitRoleType roleType)
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

    private void SetUnitRole(EngineUnitRoleType roleType, object? value)
    {
        var currentRole = this[roleType];
        if (Equals(currentRole, value))
        {
            return;
        }

        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleType] = value;
    }

    private IObject? GetToOneRole(EngineToOneRoleType roleType)
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

    private void SetOneToOneRole(EngineOneToOneRoleType roleType, Object value)
    {
        /*  [if exist]        [then remove]        set
             *
             *  RA ----- R         RA --x-- R       RA    -- R       RA    -- R
             *                ->                +        -        =       -
             *   A ----- PR         A --x-- PR       A --    PR       A --    PR
             */
        var previousRole = (Object?)this[roleType];

        // R = PR
        if (Equals(value, previousRole))
        {
            return;
        }

        var associationType = roleType.AssociationType;

        // A --x-- PR
        if (previousRole != null)
        {
            this.RemoveOneToOneRole(roleType);
        }

        var roleAssociation = (Object?)value[associationType];

        // RA --x-- R
        roleAssociation?.RemoveOneToOneRole(roleType);

        // A <---- R
        value.SetOneToAssociation(associationType, this);

        // A ----> R
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleType] = value.Id;
    }

    private void RemoveOneToOneRole(EngineOneToOneRoleType roleType)
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

    private void SetManyToOneRole(EngineManyToOneRoleType roleType, Object role)
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

    private void RemoveManyToOneRole(EngineManyToOneRoleType roleType)
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

    private ImmutableHashSet<long>? GetToManyRole(EngineToManyRoleType roleType)
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

    private void SetOneToManyRole(EngineOneToManyRoleType roleType, Object[] objects)
    {
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

    private void AddOneToManyRole(EngineOneToManyRoleType roleType, Object role)
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

    private void RemoveOneToManyRole(EngineOneToManyRoleType roleType, Object role)
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

    private void SetManyToManyRole(EngineManyToManyRoleType roleType, Object[] objects)
    {
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

    private void AddManyToManyRole(EngineManyToManyRoleType roleType, Object role)
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

    private void RemoveManyToManyRole(EngineManyToManyRoleType roleType, Object role)
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
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleType] = newRole.Count == 0 ? null : newRole;
    }

    private long? GetOneToAssociation(EngineOneToAssociationType associationType)
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

    private void SetOneToAssociation(EngineOneToAssociationType associationType, Object value)
    {
        this.changedAssociationByAssociationType ??= [];
        this.changedAssociationByAssociationType[associationType] = value.Id;
    }

    private void RemoveOneToAssociation(EngineOneToAssociationType associationType)
    {
        this.changedAssociationByAssociationType ??= [];
        this.changedAssociationByAssociationType[associationType] = null;
    }

    private ImmutableHashSet<long>? ManyToAssociation(EngineManyToAssociationType associationType)
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

    private void AddManyToAssociation(EngineManyToAssociationType associationType, Object value)
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

    private void RemoveManyToAssociation(EngineManyToAssociationType associationType, Object value)
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

    private void AssertIsAssignable(EngineCompositeRoleType roleType, Object[] objects)
    {
        var m = this.Transaction.Database.Meta;
        var objectType = roleType[m.Meta.RoleTypeObjectType]!;

        if (objects.Any(@object => !m.IsAssignableFrom(objectType, @object.Class)))
        {
            throw new ArgumentException($"{roleType} should be assignable to {roleType.ObjectType.Name} but was a {objectType}");
        }
    }

    private void AssertIsAssignable(EngineCompositeRoleType roleType, IObject? @object)
    {
        if (@object == null)
        {
            return;
        }

        var m = this.Transaction.Database.Meta;
        var objectType = roleType[m.Meta.RoleTypeObjectType]!;

        if (!m.IsAssignableFrom(objectType, @object.Class))
        {
            throw new ArgumentException($"{roleType} should be assignable to {roleType.ObjectType.Name} but was a {objectType}");
        }
    }
}
