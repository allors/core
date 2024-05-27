﻿namespace Allors.Core.Meta.Meta;

using System;
using System.Collections.Generic;
using System.Linq;

public sealed class MetaObjectType
{
    private readonly Dictionary<string, IMetaAssociationType> declaredAssociationTypeByName;
    private readonly Dictionary<string, IMetaRoleType> declaredRoleTypeByName;
    private readonly HashSet<MetaObjectType> directSupertypes;

    private IDictionary<string, IMetaAssociationType>? derivedAssociationTypeByName;
    private IDictionary<string, IMetaRoleType>? derivedRoleTypeByName;
    private HashSet<MetaObjectType>? derivedSupertypes;

    internal MetaObjectType(MetaMeta meta, MetaObjectTypeKind kind, string name)
    {
        this.Meta = meta;
        this.Kind = kind;
        this.Name = name;
        this.directSupertypes = [];
        this.declaredAssociationTypeByName = [];
        this.declaredRoleTypeByName = [];

        this.Meta.ResetDerivations();
    }

    internal MetaObjectType(MetaMeta meta, MetaObjectTypeKind kind, Type type)
        : this(meta, kind, type.Name)
    {
        this.Type = type;
        this.TypeCode = Type.GetTypeCode(type);
    }

    public MetaMeta Meta { get; }

    public MetaObjectTypeKind Kind { get; set; }

    public string Name { get; }

    public Type? Type { get; }

    public TypeCode? TypeCode { get; }

    public IReadOnlySet<MetaObjectType> DirectSupertypes => this.directSupertypes;

    public IReadOnlySet<MetaObjectType> Supertypes
    {
        get
        {
            if (this.derivedSupertypes != null)
            {
                return this.derivedSupertypes;
            }

            this.derivedSupertypes = [];
            this.AddSupertypes(this.derivedSupertypes);
            return this.derivedSupertypes;
        }
    }

    public IReadOnlyDictionary<string, IMetaAssociationType> DeclaredAssociationTypeByName => this.declaredAssociationTypeByName;

    public IDictionary<string, IMetaAssociationType> AssociationTypeByName
    {
        get
        {
            if (this.derivedAssociationTypeByName == null)
            {
                this.derivedAssociationTypeByName = new Dictionary<string, IMetaAssociationType>(this.declaredAssociationTypeByName);
                foreach (var item in this.Supertypes.SelectMany(v => v.declaredAssociationTypeByName))
                {
                    this.derivedAssociationTypeByName[item.Key] = item.Value;
                }
            }

            return this.derivedAssociationTypeByName;
        }
    }

    public IReadOnlyDictionary<string, IMetaRoleType> DeclaredRoleTypeByName => this.declaredRoleTypeByName;

    public IDictionary<string, IMetaRoleType> RoleTypeByName
    {
        get
        {
            if (this.derivedRoleTypeByName != null)
            {
                return this.derivedRoleTypeByName;
            }

            this.derivedRoleTypeByName = new Dictionary<string, IMetaRoleType>(this.declaredRoleTypeByName);
            foreach (var item in this.Supertypes.SelectMany(v => v.declaredRoleTypeByName))
            {
                this.derivedRoleTypeByName[item.Key] = item.Value;
            }

            return this.derivedRoleTypeByName;
        }
    }

    public override string ToString() => this.Name;

    public void AddDirectSupertype(MetaObjectType directSupertype)
    {
        this.directSupertypes.Add(directSupertype);
        this.Meta.ResetDerivations();
    }

    public bool IsAssignableFrom(MetaObjectType other)
    {
        return this == other || other.Supertypes.Contains(this);
    }

    internal MetaUnitRoleType AddUnit(MetaObjectType objectType, string? roleSingularName, string? associationSingularName)
    {
        roleSingularName ??= objectType.Name;
        var rolePluralName = this.Meta.Pluralize(roleSingularName);

        var roleType = new MetaUnitRoleType(
            objectType,
            roleSingularName,
            rolePluralName,
            roleSingularName);

        string associationPluralName;
        if (associationSingularName != null)
        {
            associationPluralName = this.Meta.Pluralize(associationSingularName);
        }
        else
        {
            associationSingularName = roleType.SingularNameForAssociationType(this);
            associationPluralName = roleType.PluralNameForAssociationType(this);
        }

        roleType.AssociationType = new MetaUnitAssociationType(
            this,
            roleType,
            associationSingularName,
            associationPluralName,
            associationSingularName);

        this.AddRoleType(roleType);
        objectType.AddAssociationType(roleType.AssociationType);

        this.Meta.ResetDerivations();

        return roleType;
    }

    internal MetaOneToOneRoleType AddOneToOne(MetaObjectType objectType, string? roleSingularName, string? associationSingularName)
    {
        roleSingularName ??= objectType.Name;
        var rolePluralName = this.Meta.Pluralize(roleSingularName);

        var roleType = new MetaOneToOneRoleType(
            objectType,
            roleSingularName,
            rolePluralName,
            roleSingularName);

        string associationPluralName;
        if (associationSingularName != null)
        {
            associationPluralName = this.Meta.Pluralize(associationSingularName);
        }
        else
        {
            associationSingularName = roleType.SingularNameForAssociationType(this);
            associationPluralName = roleType.PluralNameForAssociationType(this);
        }

        roleType.AssociationType = new MetaOneToOneAssociationType(
            this,
            roleType,
            associationSingularName,
            associationPluralName,
            associationSingularName);

        this.AddRoleType(roleType);
        objectType.AddAssociationType(roleType.AssociationType);

        this.Meta.ResetDerivations();

        return roleType;
    }

    internal MetaManyToOneRoleType AddManyToOne(MetaObjectType objectType, string? roleSingularName, string? associationSingularName)
    {
        roleSingularName ??= objectType.Name;
        var rolePluralName = this.Meta.Pluralize(roleSingularName);

        var roleType = new MetaManyToOneRoleType(
            objectType,
            roleSingularName,
            rolePluralName,
            roleSingularName);

        string associationPluralName;
        if (associationSingularName != null)
        {
            associationPluralName = this.Meta.Pluralize(associationSingularName);
        }
        else
        {
            associationSingularName = roleType.SingularNameForAssociationType(this);
            associationPluralName = roleType.PluralNameForAssociationType(this);
        }

        roleType.AssociationType = new MetaManyToOneAssociationType(
            this,
            roleType,
            associationSingularName,
            associationPluralName,
            associationPluralName);

        this.AddRoleType(roleType);
        objectType.AddAssociationType(roleType.AssociationType);

        this.Meta.ResetDerivations();

        return roleType;
    }

    internal MetaOneToManyRoleType AddOneToMany(MetaObjectType objectType, string? roleSingularName, string? associationSingularName)
    {
        roleSingularName ??= objectType.Name;
        var rolePluralName = this.Meta.Pluralize(roleSingularName);

        var roleType = new MetaOneToManyRoleType(
            objectType,
            roleSingularName,
            rolePluralName,
            rolePluralName);

        string associationPluralName;
        if (associationSingularName != null)
        {
            associationPluralName = this.Meta.Pluralize(associationSingularName);
        }
        else
        {
            associationSingularName = roleType.SingularNameForAssociationType(this);
            associationPluralName = roleType.PluralNameForAssociationType(this);
        }

        roleType.AssociationType = new MetaOneToManyAssociationType(
            this,
            roleType,
            associationSingularName,
            associationPluralName,
            associationSingularName);

        this.AddRoleType(roleType);
        objectType.AddAssociationType(roleType.AssociationType);

        this.Meta.ResetDerivations();

        return roleType;
    }

    internal MetaManyToManyRoleType AddManyToMany(MetaObjectType objectType, string? roleSingularName, string? associationSingularName)
    {
        roleSingularName ??= objectType.Name;
        var rolePluralName = this.Meta.Pluralize(roleSingularName);

        var roleType = new MetaManyToManyRoleType(
            objectType,
            roleSingularName,
            rolePluralName,
            rolePluralName);

        string associationPluralName;
        if (associationSingularName != null)
        {
            associationPluralName = this.Meta.Pluralize(associationSingularName);
        }
        else
        {
            associationSingularName = roleType.SingularNameForAssociationType(this);
            associationPluralName = roleType.PluralNameForAssociationType(this);
        }

        roleType.AssociationType = new MetaManyToManyAssociationType(
            this,
            roleType,
            associationSingularName,
            associationPluralName,
            associationPluralName);

        this.AddRoleType(roleType);
        objectType.AddAssociationType(roleType.AssociationType);

        this.Meta.ResetDerivations();

        return roleType;
    }

    internal void ResetDerivations()
    {
        this.derivedSupertypes = null;
        this.derivedAssociationTypeByName = null;
        this.derivedRoleTypeByName = null;
    }

    private void AddSupertypes(HashSet<MetaObjectType> newDerivedSupertypes)
    {
        foreach (var supertype in this.directSupertypes.Where(supertype => !newDerivedSupertypes.Contains(supertype)))
        {
            newDerivedSupertypes.Add(supertype);
            supertype.AddSupertypes(newDerivedSupertypes);
        }
    }

    private void AddAssociationType(IMetaAssociationType associationType)
    {
        this.CheckNames(associationType.SingularName, associationType.PluralName);

        this.declaredAssociationTypeByName.Add(associationType.Name, associationType);
    }

    private void AddRoleType(IMetaRoleType roleType)
    {
        this.CheckNames(roleType.SingularName, roleType.PluralName);

        this.declaredRoleTypeByName.Add(roleType.Name, roleType);
    }

    private void CheckNames(string singularName, string pluralName)
    {
        if (this.RoleTypeByName.ContainsKey(singularName) ||
            this.AssociationTypeByName.ContainsKey(singularName))
        {
            throw new ArgumentException($"{singularName} is not unique");
        }

        if (this.RoleTypeByName.ContainsKey(pluralName) ||
            this.AssociationTypeByName.ContainsKey(pluralName))
        {
            throw new ArgumentException($"{pluralName} is not unique");
        }
    }
}
