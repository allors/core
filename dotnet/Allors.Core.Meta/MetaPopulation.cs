namespace Allors.Core.Meta;

using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using Allors.Core.Meta.Meta;

public sealed class MetaPopulation(MetaMeta meta)
{
    private readonly MetaRelations relations = new();
    private readonly MetaRelations changedRelations = new();

    private IList<IMetaObject>? newObjects = null;
    private IImmutableList<IMetaObject> objects = ImmutableArray<IMetaObject>.Empty;

    public MetaMeta Meta { get; } = meta;

    public Dictionary<string, IMetaDerivation> DerivationById { get; } = [];

    public IReadOnlyList<IMetaObject> Objects => this.objects;

    public IMetaObject Build(MetaObjectType @class, params Action<IMetaObject>[] builders)
    {
        var @new = @class.Type != null ? (IMetaObject)Activator.CreateInstance(@class.Type, this, @class)! : new MetaObject(this, @class);
        this.objects = this.objects.Add(@new);

        foreach (var builder in builders)
        {
            builder(@new);
        }

        return @new;
    }

    public T Build<T>(params Action<T>[] builders)
        where T : IMetaObject
    {
        var className = typeof(T).Name;
        var @class = this.Meta.ObjectTypeByName[className] ?? throw new ArgumentException($"Class with name {className} not found");

        if (@class.Type == null)
        {
            throw new ArgumentException("Class has no static type");
        }

        var @new = (T)Activator.CreateInstance(@class.Type, this, @class)!;

        this.newObjects ??= new List<IMetaObject>();
        this.newObjects.Add(@new);
        this.objects = this.objects.Add(@new);

        foreach (var builder in builders)
        {
            builder(@new);
        }

        return @new;
    }

    public MetaChangeSet Checkpoint()
    {
        var newObjectSet = this.newObjects != null ? this.newObjects.ToFrozenSet() : FrozenSet<IMetaObject>.Empty;
        this.newObjects = null;
        return this.changedRelations.Snapshot(this.relations, newObjectSet);
    }

    public void Derive()
    {
        var changeSet = this.Checkpoint();

        while (changeSet.HasChanges)
        {
            foreach (var derivation in this.DerivationById.Select(kvp => kvp.Value))
            {
                derivation.Derive(changeSet);
            }

            changeSet = this.Checkpoint();
        }
    }

    internal object? GetUnitRole(MetaObject association, MetaUnitRoleType roleType) => this.GetRole(association, roleType);

    internal void SetUnitRole(MetaObject association, MetaUnitRoleType roleType, object? role)
    {
        var normalizedRole = this.Normalize(roleType, role);

        var currentRole = this.GetUnitRole(association, roleType);
        if (Equals(currentRole, normalizedRole))
        {
            return;
        }

        // Role
        this.changedRelations.RoleByAssociation(roleType)[association] = normalizedRole;
    }

    internal IMetaObject? GetToOneRole(IMetaObject association, IMetaToOneRoleType roleType) => (IMetaObject?)this.GetRole(association, roleType);

    internal void SetToOneRole(MetaObject association, IMetaToOneRoleType roleType, IMetaObject? value)
    {
        switch (roleType)
        {
            case MetaOneToOneRoleType oneToOneRoleType:
                if (value == null)
                {
                    this.RemoveOneToOneRole(association, oneToOneRoleType);
                    return;
                }

                this.SetOneToOneRole(association, oneToOneRoleType, (MetaObject)value);
                return;

            case MetaManyToOneRoleType manyToOneRoleType:
                if (value == null)
                {
                    this.RemoveManyToOneRole(association, manyToOneRoleType);
                    return;
                }

                this.SetManyToOneRole(association, manyToOneRoleType, (MetaObject)value);
                return;

            default:
                throw new InvalidOperationException();
        }
    }

    internal IImmutableSet<IMetaObject>? GetToManyRole(IMetaObject association, IMetaToManyRoleType roleType) => (IImmutableSet<IMetaObject>?)this.GetRole(association, roleType);

    internal void SetToManyRole(MetaObject association, IMetaToManyRoleType roleType, IEnumerable<IMetaObject> items)
    {
        var normalizedRole = this.Normalize(roleType, items);

        switch (roleType)
        {
            case MetaOneToManyRoleType toManyRoleType:
                if (normalizedRole.Length == 0)
                {
                    this.RemoveOneToManyRole(association, toManyRoleType);
                    return;
                }

                this.SetOneToManyRole(association, toManyRoleType, normalizedRole);
                return;

            case MetaManyToManyRoleType toManyRoleType:
                if (normalizedRole.Length == 0)
                {
                    this.RemoveManyToManyRole(association, toManyRoleType);
                    return;
                }

                this.SetManyToManyRole(association, toManyRoleType, normalizedRole);
                return;

            default:
                throw new InvalidOperationException();
        }
    }

    internal void AddToManyRole(IMetaObject association, IMetaToManyRoleType roleType, IMetaObject item)
    {
        switch (roleType)
        {
            case MetaOneToManyRoleType toManyRoleType:
                this.AddOneToManyRole(association, toManyRoleType, item);
                return;

            case MetaManyToManyRoleType toManyRoleType:
                this.AddManyToManyRole(association, toManyRoleType, item);
                return;

            default:
                throw new InvalidOperationException();
        }
    }

    internal void RemoveToManyRole(IMetaObject association, IMetaToManyRoleType roleType, IMetaObject item)
    {
        switch (roleType)
        {
            case MetaOneToManyRoleType toManyRoleType:
                this.RemoveOneToManyRole(association, toManyRoleType, item);
                return;

            case MetaManyToManyRoleType toManyRoleType:
                this.RemoveManyToManyRole(association, toManyRoleType, item);
                return;

            default:
                throw new InvalidOperationException();
        }
    }

    internal IMetaObject? GetToOneAssociation(IMetaObject role, IMetaCompositeAssociationType associationType)
    {
        if (!this.changedRelations.TryGetAssociation(role, associationType, out var association))
        {
            this.relations.AssociationByRole(associationType).TryGetValue(role, out association);
        }

        return (IMetaObject?)association;
    }

    internal IImmutableSet<IMetaObject>? GetToManyAssociation(IMetaObject role, IMetaCompositeAssociationType associationType)
    {
        if (!this.changedRelations.TryGetAssociation(role, associationType, out var association))
        {
            this.relations.AssociationByRole(associationType).TryGetValue(role, out association);
        }

        return (IImmutableSet<IMetaObject>?)association;
    }

    private void SetOneToOneRole(IMetaObject association, MetaOneToOneRoleType roleType, object value)
    {
        /*  [if exist]        [then remove]        set
         *
         *  RA ----- R         RA --x-- R       RA    -- R       RA    -- R
         *                ->                +        -        =       -
         *   A ----- PR         A --x-- PR       A --    PR       A --    PR
         */
        var role = this.Normalize(roleType, value);
        var previousRole = this.GetRole(association, roleType);

        // R = PR
        if (Equals(role, previousRole))
        {
            return;
        }

        var associationType = roleType.AssociationType;

        // A --x-- PR
        if (previousRole != null)
        {
            this.RemoveOneToOneRole(association, roleType);
        }

        var roleAssociation = this.GetToOneAssociation(role, associationType);

        // RA --x-- R
        if (roleAssociation != null)
        {
            this.RemoveOneToOneRole(roleAssociation, roleType);
        }

        // A <---- R
        this.SetOneToAssociation(role, associationType, association);

        // A ----> R
        var changedRoleByAssociation = this.changedRelations.RoleByAssociation(roleType);
        changedRoleByAssociation[association] = role;
    }

    private void RemoveOneToOneRole(IMetaObject association, MetaOneToOneRoleType roleType)
    {
        /*                        delete
         *
         *   A ----- R    ->     A       R  =   A       R
         */

        var previousRole = (IMetaObject?)this.GetRole(association, roleType);
        if (previousRole == null)
        {
            return;
        }

        var associationType = roleType.AssociationType;

        // A <---- R
        this.RemoveOneToAssociation(previousRole, associationType);

        // A ----> R
        var changedRoleByAssociation = this.changedRelations.RoleByAssociation(roleType);
        changedRoleByAssociation[association] = null;
    }

    private void SetManyToOneRole(IMetaObject association, MetaManyToOneRoleType roleType, object value)
    {
        /*  [if exist]        [then remove]        set
         *
         *  RA ----- R         RA       R       RA    -- R       RA ----- R
         *                ->                +        -        =       -
         *   A ----- PR         A --x-- PR       A --    PR       A --    PR
         */
        var role = this.Normalize(roleType, value);

        var associationType = roleType.AssociationType;
        var previousRole = this.GetToOneRole(association, roleType);

        // R = PR
        if (role.Equals(previousRole))
        {
            return;
        }

        // A --x-- PR
        if (previousRole != null)
        {
            this.RemoveManyToAssociation(previousRole, associationType, association);
        }

        // A <---- R
        this.AddManyToAssociation(role, associationType, association);

        // A ----> R
        var changedRoleByAssociation = this.changedRelations.RoleByAssociation(roleType);
        changedRoleByAssociation[association] = role;
    }

    private void RemoveManyToOneRole(IMetaObject association, MetaManyToOneRoleType roleType)
    {
        /*                        delete
         *  RA --                                RA --
         *       -        ->                 =        -
         *   A ----- R           A --x-- R             -- R
         */

        var previousRole = (IMetaObject?)this.GetRole(association, roleType);
        if (previousRole == null)
        {
            return;
        }

        var associationType = roleType.AssociationType;

        // A <---- R
        this.RemoveManyToAssociation(previousRole, associationType, association);

        // A ----> R
        var changedRoleByAssociation = this.changedRelations.RoleByAssociation(roleType);
        changedRoleByAssociation[association] = null;
    }

    private void SetOneToManyRole(IMetaObject association, MetaOneToManyRoleType roleType, IMetaObject[] role)
    {
        var previousRole = this.GetToManyRole(association, roleType);

        if (previousRole == null)
        {
            foreach (var addedRole in role)
            {
                this.AddOneToManyRole(association, roleType, addedRole);
            }
        }
        else
        {
            // Use Diff (Add/Remove)
            foreach (var addedRole in role.Except(previousRole))
            {
                this.AddOneToManyRole(association, roleType, addedRole);
            }

            foreach (var removeRole in previousRole.Except(role))
            {
                this.RemoveOneToManyRole(association, roleType, removeRole);
            }
        }
    }

    private void AddOneToManyRole(IMetaObject association, MetaOneToManyRoleType roleType, IMetaObject role)
    {
        /*  [if exist]        [then remove]        add
         *
         *  RA ----- R         RA --x-- R       RA    -- R       RA    -- R
         *                ->                +        -        =       -
         *   A ----- PR         A       PR       A --    PR       A ----- PR
         */

        var previousRole = (IImmutableSet<IMetaObject>?)this.GetRole(association, roleType);

        // R in PR
        if (previousRole?.Contains(role) == true)
        {
            return;
        }

        var associationType = roleType.AssociationType;

        // RA --x-- R
        var roleAssociation = this.GetToOneAssociation(role, associationType);

        if (roleAssociation != null)
        {
            this.RemoveOneToManyRole(roleAssociation, roleType, role);
        }

        // A <---- R
        this.SetOneToAssociation(role, associationType, association);

        // A ----> R
        var changedRoleByAssociation = this.changedRelations.RoleByAssociation(roleType);
        changedRoleByAssociation[association] = previousRole != null ? previousRole.Add(role) : ImmutableHashSet.Create(role);
    }

    private void RemoveOneToManyRole(IMetaObject association, MetaOneToManyRoleType roleType)
    {
        var associationType = roleType.AssociationType;

        var previousRole = (IImmutableSet<IMetaObject>?)this.GetRole(association, roleType);
        if (previousRole != null)
        {
            // Role
            var changedRoleByAssociation = this.changedRelations.RoleByAssociation(roleType);
            changedRoleByAssociation.Remove(association);

            // Association
            var changedAssociationByRole = this.changedRelations.AssociationByRole(associationType);
            foreach (var role in previousRole)
            {
                if (associationType.IsOne)
                {
                    // One to Many
                    changedAssociationByRole[role] = null;
                }
                else
                {
                    var previousAssociation = this.GetToManyAssociation(role, associationType);

                    // Many to Many
                    if (previousAssociation?.Contains(association) == true)
                    {
                        changedAssociationByRole[role] = previousAssociation.Remove(association);
                    }
                }
            }
        }
    }

    private void RemoveOneToManyRole(IMetaObject association, MetaOneToManyRoleType roleType, IMetaObject item)
    {
        var associationType = roleType.AssociationType;

        var previousRole = (IImmutableSet<IMetaObject>?)this.GetRole(association, roleType);
        if (previousRole?.Contains(item) == true)
        {
            // Role
            var changedRoleByAssociation = this.changedRelations.RoleByAssociation(roleType);
            changedRoleByAssociation[association] = previousRole.Remove(item);

            // Association
            var changedAssociationByRole = this.changedRelations.AssociationByRole(associationType);
            if (associationType.IsOne)
            {
                // One to Many
                changedAssociationByRole.Remove(item);
            }
            else
            {
                var previousAssociation = this.GetToManyAssociation(item, associationType);

                // Many to Many
                if (previousAssociation?.Contains(association) == true)
                {
                    changedAssociationByRole[item] = previousAssociation.Remove(association);
                }
            }
        }
    }

    private void SetManyToManyRole(IMetaObject association, MetaManyToManyRoleType roleType, IMetaObject[] normalizedRole)
    {
        var previousRole = this.GetRole(association, roleType);

        var roles = normalizedRole.ToArray();
        var previousRoles = (IImmutableSet<IMetaObject>?)previousRole;

        if (previousRoles != null)
        {
            // Use Diff (Add/Remove)
            var addedRoles = roles.Except(previousRoles);
            var removedRoles = previousRoles.Except(roles);

            foreach (var addedRole in addedRoles)
            {
                this.AddManyToManyRole(association, roleType, addedRole);
            }

            foreach (var removeRole in removedRoles)
            {
                this.RemoveManyToManyRole(association, roleType, removeRole);
            }
        }
        else
        {
            foreach (var addedRole in roles)
            {
                this.AddManyToManyRole(association, roleType, addedRole);
            }
        }
    }

    private void AddManyToManyRole(IMetaObject association, MetaManyToManyRoleType roleType, IMetaObject item)
    {
        var associationType = roleType.AssociationType;

        // Role
        var changedRoleByAssociation = this.changedRelations.RoleByAssociation(roleType);
        var previousRole = (IImmutableSet<IMetaObject>?)this.GetRole(association, roleType);
        var newRole = previousRole != null ? previousRole.Add(item) : ImmutableHashSet.Create(item);
        changedRoleByAssociation[association] = newRole;

        // Association
        var changedAssociationByRole = this.changedRelations.AssociationByRole(associationType);
        if (associationType.IsOne)
        {
            var previousAssociation = this.GetToOneAssociation(item, associationType);

            // One to Many
            if (previousAssociation != null)
            {
                var previousAssociationRole = (IImmutableSet<IMetaObject>?)this.GetRole(previousAssociation, roleType);
                if (previousAssociationRole?.Contains(item) == true)
                {
                    changedRoleByAssociation[previousAssociation] = previousAssociationRole.Remove(item);
                }
            }

            changedAssociationByRole[item] = association;
        }
        else
        {
            var previousAssociation = this.GetToManyAssociation(item, associationType);

            // Many to Many
            changedAssociationByRole[item] = previousAssociation != null ? previousAssociation.Add(association) : ImmutableHashSet.Create(association);
        }
    }

    private void RemoveManyToManyRole(IMetaObject association, MetaManyToManyRoleType roleType, IMetaObject item)
    {
        var associationType = roleType.AssociationType;

        var previousRole = (IImmutableSet<IMetaObject>?)this.GetRole(association, roleType);
        if (previousRole?.Contains(item) == true)
        {
            // Role
            var changedRoleByAssociation = this.changedRelations.RoleByAssociation(roleType);
            changedRoleByAssociation[association] = previousRole.Remove(item);

            // Association
            var changedAssociationByRole = this.changedRelations.AssociationByRole(associationType);
            if (associationType.IsOne)
            {
                // One to Many
                changedAssociationByRole.Remove(item);
            }
            else
            {
                var previousAssociation = this.GetToManyAssociation(item, associationType);

                // Many to Many
                if (previousAssociation?.Contains(association) == true)
                {
                    changedAssociationByRole[item] = previousAssociation.Remove(association);
                }
            }
        }
    }

    private void RemoveManyToManyRole(IMetaObject association, MetaManyToManyRoleType roleType)
    {
        var associationType = roleType.AssociationType;

        var previousRole = (IImmutableSet<IMetaObject>?)this.GetRole(association, roleType);
        if (previousRole != null)
        {
            // Role
            var changedRoleByAssociation = this.changedRelations.RoleByAssociation(roleType);
            changedRoleByAssociation.Remove(association);

            // Association
            var changedAssociationByRole = this.changedRelations.AssociationByRole(associationType);
            foreach (var role in previousRole)
            {
                if (associationType.IsOne)
                {
                    // One to Many
                    changedAssociationByRole[role] = null;
                }
                else
                {
                    var previousAssociation = this.GetToManyAssociation(role, associationType);

                    // Many to Many
                    if (previousAssociation?.Contains(association) == true)
                    {
                        changedAssociationByRole[role] = previousAssociation.Remove(association);
                    }
                }
            }
        }
    }

    private void SetOneToAssociation(IMetaObject role, IMetaOneToAssociationType associationType, IMetaObject association)
    {
        var changedAssociationByRole = this.changedRelations.AssociationByRole(associationType);
        changedAssociationByRole[role] = association;
    }

    private void RemoveOneToAssociation(IMetaObject role, IMetaOneToAssociationType associationType)
    {
        var changedAssociationByRole = this.changedRelations.AssociationByRole(associationType);
        changedAssociationByRole[role] = null;
    }

    private void AddManyToAssociation(IMetaObject role, IMetaManyToAssociationType associationType, IMetaObject association)
    {
        var previousAssociation = this.GetToManyAssociation(role, associationType);

        if (previousAssociation?.Contains(role) != true)
        {
            return;
        }

        var changedAssociationByRole = this.changedRelations.AssociationByRole(associationType);
        changedAssociationByRole[role] = previousAssociation.Remove(association);
    }

    private void RemoveManyToAssociation(IMetaObject role, IMetaManyToAssociationType associationType, IMetaObject association)
    {
        var previousAssociation = this.GetToManyAssociation(role, associationType);

        if (previousAssociation == null || previousAssociation.Contains(role))
        {
            return;
        }

        var changedAssociationByRole = this.changedRelations.AssociationByRole(associationType);
        changedAssociationByRole[role] = previousAssociation.Remove(association);
    }

    private object? GetRole(IMetaObject association, IMetaRoleType roleType)
    {
        if (!this.changedRelations.TryGetRole(association, roleType, out var role))
        {
            this.relations.RoleByAssociation(roleType).TryGetValue(association, out role);
        }

        return role;
    }

    private object? Normalize(MetaUnitRoleType roleType, object? value)
    {
        if (value == null)
        {
            return value;
        }

        if (value is DateTime dateTime && dateTime != DateTime.MinValue && dateTime != DateTime.MaxValue)
        {
            dateTime = dateTime.Kind switch
            {
                DateTimeKind.Local => dateTime.ToUniversalTime(),
                DateTimeKind.Unspecified => throw new ArgumentException(@"DateTime value is of DateTimeKind.Kind Unspecified.
Unspecified is only allowed for DateTime.MaxValue and DateTime.MinValue. 
Use DateTimeKind.Utc or DateTimeKind.Local."),
                _ => dateTime,
            };

            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond, DateTimeKind.Utc);
        }

        if (value.GetType() != roleType.ObjectType.Type && roleType.ObjectType.TypeCode.HasValue)
        {
            value = Convert.ChangeType(value, roleType.ObjectType.TypeCode.Value, CultureInfo.InvariantCulture);
        }

        return value;
    }

    private IMetaObject Normalize(IMetaToOneRoleType roleType, object value)
    {
        if (value is MetaObject metaObject)
        {
            if (!roleType.ObjectType.IsAssignableFrom(metaObject.ObjectType))
            {
                throw new ArgumentException($"{roleType.Name} should be assignable to {roleType.ObjectType.Name} but was a {metaObject.ObjectType.Name}");
            }

            return metaObject;
        }

        throw new ArgumentException($"{roleType.Name} should be an meta object but was a {value.GetType()}");
    }

    private IMetaObject[] Normalize(IMetaToManyRoleType roleType, IEnumerable<IMetaObject?> value)
        => value
            .Where(v =>
            {
                if (v == null)
                {
                    return false;
                }

                if (!roleType.ObjectType.IsAssignableFrom(v.ObjectType))
                {
                    throw new ArgumentException($"{roleType.Name} should be assignable to {roleType.ObjectType.Name} but was a {v.ObjectType.Name}");
                }

                return true;
            })
            .Cast<IMetaObject>()
            .Distinct()
            .ToArray();
}
