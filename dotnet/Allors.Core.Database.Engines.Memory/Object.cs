namespace Allors.Core.Database.Engines.Memory;

using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Allors.Core.Database.Engines.Meta;
using Allors.Core.Database.Meta;
using Allors.Core.Database.MetaMeta;

/// <inheritdoc />
public class Object : IObject
{
    private Record? record;

    private Dictionary<EnginesRoleType, object?>? changedRoleByRoleType;
    private Dictionary<EnginesRoleType, object?>? checkpointRoleByRoleType;

    private Dictionary<EnginesAssociationType, object?>? changedAssociationByAssociationType;
    private Dictionary<EnginesAssociationType, object?>? checkpointAssociationByAssociationType;

    /// <summary>
    /// Initializes a new instance of the <see cref="Object"/> class.
    /// </summary>
    internal Object(Transaction transaction, EnginesClass @class, long id)
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
    public EnginesClass Class { get; }

    /// <inheritdoc/>
    public long Id { get; }

    /// <inheritdoc/>
    public long Version => this.record?.Version ?? 0;

    /// <inheritdoc />
    public bool IsNew => this.Version == 0;

    ITransaction IObject.Transaction => this.Transaction;

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

    private Record? Record => this.record;

    private Transaction Transaction { get; }

    /// <summary>
    /// The meta.
    /// </summary>
    private EnginesMeta Meta => this.Transaction.Meta;

    IEnumerable<IObject> IObject.this[Func<IManyToAssociationType> associationType] => this[associationType()];

    IObject? IObject.this[Func<IOneToAssociationType> associationType] => this[associationType()];

    IEnumerable<IObject> IObject.this[Func<IToManyRoleType> roleType] { get => this[roleType()]; set => this[roleType()] = value; }

    IObject? IObject.this[Func<IToOneRoleType> roleType] { get => this[roleType()]; set => this[roleType()] = value; }

    Guid? IObject.this[Func<UniqueRoleType> roleType] { get => this[roleType()]; set => this[roleType()] = value; }

    string? IObject.this[Func<StringRoleType> roleType] { get => this[roleType()]; set => this[roleType()] = value; }

    int? IObject.this[Func<IntegerRoleType> roleType] { get => this[roleType()]; set => this[roleType()] = value; }

    double? IObject.this[Func<FloatRoleType> roleType] { get => this[roleType()]; set => this[roleType()] = value; }

    decimal? IObject.this[Func<DecimalRoleType> roleType] { get => this[roleType()]; set => this[roleType()] = value; }

    DateTime? IObject.this[Func<DateTimeRoleType> roleType] { get => this[roleType()]; set => this[roleType()] = value; }

    bool? IObject.this[Func<BooleanRoleType> roleType] { get => this[roleType()]; set => this[roleType()] = value; }

    byte[]? IObject.this[Func<BinaryRoleType> roleType] { get => this[roleType()]; set => this[roleType()] = value; }

    /// <inheritdoc />
    public byte[]? this[BinaryRoleType roleType]
    {
        get => this[this.Meta[roleType]];
        set => this[this.Meta[roleType]] = value;
    }

    /// <inheritdoc />
    public bool? this[BooleanRoleType roleType]
    {
        get => this[this.Meta[roleType]];
        set => this[this.Meta[roleType]] = value;
    }

    /// <inheritdoc />
    public DateTime? this[DateTimeRoleType roleType]
    {
        get => this[this.Meta[roleType]];
        set => this[this.Meta[roleType]] = value;
    }

    /// <inheritdoc />
    public decimal? this[DecimalRoleType roleType]
    {
        get => this[this.Meta[roleType]];
        set => this[this.Meta[roleType]] = value;
    }

    /// <inheritdoc />
    public double? this[FloatRoleType roleType]
    {
        get => this[this.Meta[roleType]];
        set => this[this.Meta[roleType]] = value;
    }

    /// <inheritdoc />
    public int? this[IntegerRoleType roleType]
    {
        get => this[this.Meta[roleType]];
        set => this[this.Meta[roleType]] = value;
    }

    /// <inheritdoc />
    public string? this[StringRoleType roleType]
    {
        get => this[this.Meta[roleType]];
        set => this[this.Meta[roleType]] = value;
    }

    /// <inheritdoc />
    public Guid? this[UniqueRoleType roleType]
    {
        get => this[this.Meta[roleType]];
        set => this[this.Meta[roleType]] = value;
    }

    /// <inheritdoc />
    public IObject? this[IToOneRoleType roleType]
    {
        get => this[this.Meta[roleType]];
        set => this[this.Meta[roleType]] = value;
    }

    /// <inheritdoc />
    public IEnumerable<IObject> this[IToManyRoleType roleType]
    {
        get => this[this.Meta[roleType]];
        set => this[this.Meta[roleType]] = value;
    }

    /// <inheritdoc />
    public IObject? this[IOneToAssociationType associationType] => this[this.Meta[associationType]];

    /// <inheritdoc />
    public IEnumerable<IObject> this[IManyToAssociationType associationType] => this[this.Meta[associationType]];

    private byte[]? this[EnginesBinaryRoleType roleType]
    {
        get => (byte[]?)this.GetUnitRole(roleType);
        set
        {
            this.Assert(roleType);

            var currentRole = this[roleType];
            if (Equals(currentRole, value))
            {
                return;
            }

            this.SetUnitRole(roleType, value);
        }
    }

    private bool? this[EnginesBooleanRoleType roleType]
    {
        get => (bool?)this.GetUnitRole(roleType);
        set
        {
            this.Assert(roleType);

            var currentRole = this[roleType];
            if (Equals(currentRole, value))
            {
                return;
            }

            this.SetUnitRole(roleType, value);
        }
    }

    private DateTime? this[EnginesDateTimeRoleType roleType]
    {
        get => (DateTime?)this.GetUnitRole(roleType);
        set
        {
            this.Assert(roleType);
            value = EnginesDateTimeRoleType.Normalize(value);

            var currentRole = this[roleType];
            if (Equals(currentRole, value))
            {
                return;
            }

            this.SetUnitRole(roleType, value);
        }
    }

    private decimal? this[EnginesDecimalRoleType roleType]
    {
        get => (decimal?)this.GetUnitRole(roleType);
        set
        {
            this.Assert(roleType);
            value = roleType.Normalize(value);

            var currentRole = this[roleType];
            if (Equals(currentRole, value))
            {
                return;
            }

            this.SetUnitRole(roleType, value);
        }
    }

    private double? this[EnginesFloatRoleType roleType]
    {
        get => (double?)this.GetUnitRole(roleType);
        set
        {
            this.Assert(roleType);

            var currentRole = this[roleType];
            if (Equals(currentRole, value))
            {
                return;
            }

            this.SetUnitRole(roleType, value);
        }
    }

    private int? this[EnginesIntegerRoleType roleType]
    {
        get => (int?)this.GetUnitRole(roleType);
        set
        {
            this.Assert(roleType);

            var currentRole = this[roleType];
            if (Equals(currentRole, value))
            {
                return;
            }

            this.SetUnitRole(roleType, value);
        }
    }

    private string? this[EnginesStringRoleType roleType]
    {
        get => (string?)this.GetUnitRole(roleType);
        set
        {
            this.Assert(roleType);
            value = roleType.Normalize(value);

            var currentRole = this[roleType];
            if (Equals(currentRole, value))
            {
                return;
            }

            this.SetUnitRole(roleType, value);
        }
    }

    private Guid? this[EnginesUniqueRoleType roleType]
    {
        get => (Guid?)this.GetUnitRole(roleType);
        set
        {
            this.Assert(roleType);
            value = EnginesUniqueRoleType.Normalize(value);

            var currentRole = this[roleType];
            if (Equals(currentRole, value))
            {
                return;
            }

            this.SetUnitRole(roleType, value);
        }
    }

    private IObject? this[EnginesToOneRoleType roleType]
    {
        get => this.GetToOneRole(roleType);

        set
        {
            this.Assert(roleType, (Object?)value);

            if (roleType is EnginesOneToOneRoleType oneToOneRoleType)
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
                var manyToOneRoleType = (EnginesManyToOneRoleType)roleType;

                if (value == null)
                {
                    this.RemoveManyToOneRole(manyToOneRoleType);
                    return;
                }

                this.SetManyToOneRole(manyToOneRoleType, (Object)value);
            }
        }
    }

    private IEnumerable<IObject> this[EnginesToManyRoleType roleType]
    {
        get => this.GetToManyRole(roleType)?.Select(this.Transaction.Instantiate) ?? [];
        set
        {
            var objects = value.Where(v => (IObject?)v != null).Distinct().Cast<Object>().ToArray();

            this.Assert(roleType, objects);

            if (roleType is EnginesOneToManyRoleType oneToManyRoleType)
            {
                this.SetOneToManyRole(oneToManyRoleType, objects);
            }
            else
            {
                var manyToManyRoleType = (EnginesManyToManyRoleType)roleType;
                this.SetManyToManyRole(manyToManyRoleType, objects);
            }
        }
    }

    private IObject? this[EnginesOneToAssociationType associationType]
    {
        get
        {
            var association = this.GetOneToAssociation(associationType);
            return association != null ? this.Transaction.Instantiate(association.Value) : null;
        }
    }

    private IEnumerable<IObject> this[EnginesManyToAssociationType associationType] => this.ManyToAssociation(associationType)?.Select(this.Transaction.Instantiate) ?? [];

    void IObject.Add(Func<IToManyRoleType> roleType, IObject value) => this.Add(roleType(), value);

    void IObject.Remove(Func<IToManyRoleType> roleType, IObject value) => this.Remove(roleType(), value);

    bool IObject.Exist(Func<IRoleType> roleType) => this.Exist(roleType());

    bool IObject.Exist(Func<IAssociationType> associationType) => this.Exist(associationType());

    void IObject.Call(Func<MethodType> methodType) => this.Call(methodType());

    /// <inheritdoc />
    public void Add(IToManyRoleType roleType, IObject value) => this.Add(this.Meta[roleType], value);

    /// <inheritdoc />
    public void Remove(IToManyRoleType roleType, IObject value) => this.Remove(this.Meta[roleType], value);

    /// <inheritdoc />
    public bool Exist(IRoleType roleType) => this.Exist(this.Meta[roleType]);

    /// <inheritdoc />
    public bool Exist(IAssociationType associationType) => this.Exist(this.Meta[associationType]);

    /// <inheritdoc/>
    public void Call(MethodType methodType)
    {
        var m = this.Meta.Meta.MetaMeta;
        var concreteMethodType = methodType[m.MethodTypeConcreteMethodTypes]!.First(v => v[m.ConcreteMethodTypeClass] == this.Class.MetaObject);
        var actions = (Action<IObject, object>[])concreteMethodType[m.ConcreteMethodTypeActions]!;

        foreach (var action in actions)
        {
            action(this, new object());
        }
    }

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
                    if (roleType is EnginesToManyRoleType ?
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

                    if (roleType is EnginesToManyRoleType ?
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

            this.checkpointRoleByRoleType = new Dictionary<EnginesRoleType, object?>(this.changedRoleByRoleType);
        }

        if (this.changedAssociationByAssociationType != null)
        {
            foreach (var (associationType, changedAssociation) in this.changedAssociationByAssociationType)
            {
                if (this.checkpointAssociationByAssociationType != null &&
                    this.checkpointAssociationByAssociationType.TryGetValue(associationType, out var checkpointAssociation))
                {
                    if (associationType is EnginesManyToAssociationType ?
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

                    if (associationType is EnginesManyToAssociationType ?
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

            this.checkpointAssociationByAssociationType = new Dictionary<EnginesAssociationType, object?>(this.changedAssociationByAssociationType);
        }
    }

    internal Record ToRecord()
    {
        if (this.IsNew)
        {
            var roleByRoleTypeId = this.changedRoleByRoleType?
                .Where(kvp => kvp.Value != null)
                .Select(RoleNotNull)
                .ToFrozenDictionary() ?? FrozenDictionary<EnginesRoleType, object>.Empty;

            var associationByAssociationTypeId = this.changedAssociationByAssociationType?
                .Where(kvp => kvp.Value != null)
                .Select(AssociationNotNull)
                .ToFrozenDictionary() ?? FrozenDictionary<EnginesAssociationType, object>.Empty;

            return new Record(this.Class, this.Id, this.Version + 1, roleByRoleTypeId, associationByAssociationTypeId);
        }
        else
        {
            var roleByRoleTypeId = this.changedRoleByRoleType != null ? this.Record!.RoleByRoleTypeId
                .Where(kvp => !this.changedRoleByRoleType.ContainsKey(kvp.Key))
                .Union(this.changedRoleByRoleType!
                    .Where(kvp => kvp.Value != null)
                    .Select(RoleNotNull))
                .ToFrozenDictionary() : FrozenDictionary<EnginesRoleType, object>.Empty;

            var associationByAssociationTypeId = this.changedAssociationByAssociationType != null ? this.Record!.AssociationByAssociationTypeId
                .Where(kvp => !this.changedAssociationByAssociationType.ContainsKey(kvp.Key))
                .Union(this.changedAssociationByAssociationType!
                    .Where(kvp => kvp.Value != null)
                    .Select(AssociationNotNull))
                .ToFrozenDictionary() : FrozenDictionary<EnginesAssociationType, object>.Empty;

            return new Record(this.Class, this.Id, this.Version + 1, roleByRoleTypeId, associationByAssociationTypeId);
        }

        KeyValuePair<EnginesRoleType, object> RoleNotNull(KeyValuePair<EnginesRoleType, object?> kvp) =>
            new(kvp.Key, kvp.Value!);

        KeyValuePair<EnginesAssociationType, object> AssociationNotNull(KeyValuePair<EnginesAssociationType, object?> kvp) =>
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
                case EnginesStringRoleType unitRoleType:
                    commitObject[unitRoleType] = (string?)changedRole;
                    return;

                case EnginesToOneRoleType toOneRoleType:
                    commitObject[toOneRoleType] = changedRole != null
                        ? commitTransaction.Instantiate((long)changedRole)
                        : null;
                    return;

                case EnginesToManyRoleType toManyRoleType:
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

    private static void AssertRoleTypeIsAssignableFrom(EnginesCompositeRoleType roleType, Object[] objects)
    {
        var composite = roleType.Composite;

        if (Array.Exists(objects, @object => !composite.IsAssignableFrom(@object.Class)))
        {
            throw new ArgumentException($"{roleType} should be assignable to {composite.SingularName}");
        }
    }

    private static void AssertRoleTypeIsAssignableFrom(EnginesCompositeRoleType roleType, Object? @object)
    {
        if (@object == null)
        {
            return;
        }

        var composite = roleType.Composite;

        if (!composite.IsAssignableFrom(@object.Class))
        {
            throw new ArgumentException($"{roleType} should be assignable to {composite.SingularName}");
        }
    }

    private void Add(EnginesToManyRoleType roleType, IObject value)
    {
        this.Assert(roleType, (Object?)value);

        if (roleType is EnginesOneToManyRoleType oneToManyRoleType)
        {
            this.AddOneToManyRole(oneToManyRoleType, (Object)value);
        }
        else
        {
            var manyToManyRoleType = (EnginesManyToManyRoleType)roleType;
            this.AddManyToManyRole(manyToManyRoleType, (Object)value);
        }
    }

    private void Remove(EnginesToManyRoleType roleType, IObject value)
    {
        this.Assert(roleType, (Object?)value);

        if (roleType is EnginesOneToManyRoleType oneToManyRoleType)
        {
            this.RemoveOneToManyRole(oneToManyRoleType, (Object)value);
        }
        else
        {
            var manyToManyRoleType = (EnginesManyToManyRoleType)roleType;
            this.RemoveManyToManyRole(manyToManyRoleType, (Object)value);
        }
    }

    private bool Exist(EnginesRoleType roleType)
    {
        if (this.changedRoleByRoleType != null && this.changedRoleByRoleType.TryGetValue(roleType, out var changedRole))
        {
            return changedRole != null;
        }

        return this.Record?.RoleByRoleTypeId.ContainsKey(roleType) == true;
    }

    private bool Exist(EnginesAssociationType associationType)
    {
        if (this.changedAssociationByAssociationType != null && this.changedAssociationByAssociationType.TryGetValue(associationType, out var changedAssociation))
        {
            return changedAssociation != null;
        }

        return this.Record?.AssociationByAssociationTypeId.ContainsKey(associationType) == true;
    }

    private object? GetUnitRole(EnginesUnitRoleType roleType)
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

    private void SetUnitRole(EnginesUnitRoleType roleType, object? value)
    {
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleType] = value;
    }

    private IObject? GetToOneRole(EnginesToOneRoleType roleType)
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

    private void SetOneToOneRole(EnginesOneToOneRoleType roleType, Object value)
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

        var associationType = roleType.OneToOneAssociationType;

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

    private void RemoveOneToOneRole(EnginesOneToOneRoleType roleType)
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

        var associationType = roleType.OneToOneAssociationType;

        // A <---- R
        previousRole.RemoveOneToAssociation(associationType);

        // A ----> R
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleType] = null;
    }

    private void SetManyToOneRole(EnginesManyToOneRoleType roleType, Object role)
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

        var associationType = roleType.ManyToOneAssociationType;

        // A --x-- PR
        previousRole?.RemoveManyToAssociation(associationType, this);

        // A <---- R
        role.AddManyToAssociation(associationType, this);

        // A ----> R
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleType] = role.Id;
    }

    private void RemoveManyToOneRole(EnginesManyToOneRoleType roleType)
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

        var associationType = roleType.ManyToOneAssociationType;

        // A <---- R
        previousRole.RemoveManyToAssociation(associationType, this);

        // A ----> R
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleType] = null;
    }

    private ImmutableHashSet<long>? GetToManyRole(EnginesToManyRoleType roleType)
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

    private void SetOneToManyRole(EnginesOneToManyRoleType roleType, Object[] objects)
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

    private void AddOneToManyRole(EnginesOneToManyRoleType roleType, Object role)
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

        var associationType = roleType.OneToManyAssociationType;

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

    private void RemoveOneToManyRole(EnginesOneToManyRoleType roleType, Object role)
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

        var associationType = roleType.OneToManyAssociationType;

        // A <---- R
        role.RemoveOneToAssociation(associationType);

        // A ----> R
        var newRole = previousRoleIds.Remove(role.Id);
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleType] = newRole.Count == 0 ? null : newRole;
    }

    private void SetManyToManyRole(EnginesManyToManyRoleType roleType, Object[] objects)
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
            foreach (var @object in add)
            {
                this.AddManyToManyRole(roleType, @object);
            }
        }
    }

    private void AddManyToManyRole(EnginesManyToManyRoleType roleType, Object role)
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

        var associationType = roleType.ManyToManyAssociationType;

        // A <---- R
        role.AddManyToAssociation(associationType, this);

        // A ----> R
        var newRole = previousRole != null ? previousRole.Add(role.Id) : ImmutableHashSet.CreateRange([role.Id]);
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleType] = newRole;
    }

    private void RemoveManyToManyRole(EnginesManyToManyRoleType roleType, Object role)
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

        var associationType = roleType.ManyToManyAssociationType;

        // A <---- R
        role.RemoveManyToAssociation(associationType, this);

        // A ----> R
        var newRole = previousRole.Remove(role.Id);
        this.changedRoleByRoleType ??= [];
        this.changedRoleByRoleType[roleType] = newRole.Count == 0 ? null : newRole;
    }

    private long? GetOneToAssociation(EnginesOneToAssociationType associationType)
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

    private void SetOneToAssociation(EnginesOneToAssociationType associationType, Object value)
    {
        this.changedAssociationByAssociationType ??= [];
        this.changedAssociationByAssociationType[associationType] = value.Id;
    }

    private void RemoveOneToAssociation(EnginesOneToAssociationType associationType)
    {
        this.changedAssociationByAssociationType ??= [];
        this.changedAssociationByAssociationType[associationType] = null;
    }

    private ImmutableHashSet<long>? ManyToAssociation(EnginesManyToAssociationType associationType)
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

    private void AddManyToAssociation(EnginesManyToAssociationType associationType, Object value)
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

    private void RemoveManyToAssociation(EnginesManyToAssociationType associationType, Object value)
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

    private void Assert(EnginesUnitRoleType roleType)
    {
        this.AssertAssociationTypeIsAssignableFrom(roleType.AssociationType);
    }

    private void Assert(EnginesCompositeRoleType roleType, Object[] objects)
    {
        this.AssertAssociationTypeIsAssignableFrom(roleType.AssociationType);
        AssertRoleTypeIsAssignableFrom(roleType, objects);
    }

    private void Assert(EnginesCompositeRoleType roleType, Object? @object)
    {
        this.AssertAssociationTypeIsAssignableFrom(roleType.AssociationType);
        AssertRoleTypeIsAssignableFrom(roleType, @object);
    }

    private void AssertAssociationTypeIsAssignableFrom(EnginesAssociationType associationType)
    {
        if (!associationType.Composite.IsAssignableFrom(this.Class))
        {
            throw new ArgumentException($"{associationType.Composite} is not assignable from {this.Class}");
        }
    }
}
