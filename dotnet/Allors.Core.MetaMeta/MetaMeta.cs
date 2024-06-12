namespace Allors.Core.MetaMeta;

using System;
using System.Collections.Generic;

public sealed class MetaMeta
{
    private readonly Dictionary<Guid, MetaObjectType> objectTypeById;
    private readonly Dictionary<string, MetaObjectType> objectTypeByName;
    private readonly Dictionary<Guid, MetaInheritance> inheritanceById;
    private readonly Dictionary<Guid, IMetaAssociationType> associationTypeById;
    private readonly Dictionary<Guid, IMetaRoleType> roleTypeById;

    public MetaMeta()
    {
        this.objectTypeById = [];
        this.objectTypeByName = [];
        this.associationTypeById = [];
        this.roleTypeById = [];
        this.inheritanceById = [];
    }

    public IReadOnlyDictionary<Guid, MetaObjectType> ObjectTypeById => this.objectTypeById;

    public IReadOnlyDictionary<string, MetaObjectType> ObjectTypeByName => this.objectTypeByName;

    public IReadOnlyDictionary<Guid, MetaInheritance> InheritanceById => this.inheritanceById;

    public IReadOnlyDictionary<Guid, IMetaAssociationType> AssociationTypeById => this.associationTypeById;

    public IReadOnlyDictionary<Guid, IMetaRoleType> RoleTypeById => this.roleTypeById;

    public MetaObjectType AddUnit(Guid id, string name)
    {
        var objectType = new MetaObjectType(this, MetaObjectTypeKind.Unit, id, name);
        this.Add(objectType);
        return objectType;
    }

    public MetaObjectType AddInterface(Guid id, string name, params MetaObjectType[] directSupertypes)
    {
        var objectType = new MetaObjectType(this, MetaObjectTypeKind.Interface, id, name);
        this.Add(objectType);

        foreach (var superType in directSupertypes)
        {
            objectType.AddDirectSupertype(superType);
        }

        return objectType;
    }

    public MetaObjectType AddClass(Guid id, string name, params MetaObjectType[] directSupertypes)
    {
        var objectType = new MetaObjectType(this, MetaObjectTypeKind.Class, id, name);
        this.Add(objectType);

        foreach (var superType in directSupertypes)
        {
            objectType.AddDirectSupertype(superType);
        }

        return objectType;
    }

    public MetaObjectType AddClass<T>(Guid id, params MetaObjectType[] directSupertypes) => this.AddClass(id, typeof(T), directSupertypes);

    public MetaObjectType AddClass(Guid id, Type type, params MetaObjectType[] directSupertypes)
    {
        var objectType = new MetaObjectType(this, MetaObjectTypeKind.Class, id, type);
        this.Add(objectType);

        foreach (var superType in directSupertypes)
        {
            objectType.AddDirectSupertype(superType);
        }

        return objectType;
    }

    public MetaUnitRoleType AddUnitRelation(Guid associationTypeId, Guid roleTypeId, MetaObjectType associationObjectType, MetaObjectType roleObjectType, string roleName, string? associationName = null)
    {
        var role = associationObjectType.AddUnit(this, associationTypeId, roleTypeId, roleObjectType, roleName, associationName);
        this.Add(role);
        return role;
    }

    public MetaOneToOneRoleType AddOneToOneRelation(Guid associationTypeId, Guid roleTypeId, MetaObjectType associationObjectType, MetaObjectType roleObjectType, string? roleName = null, string? associationName = null)
    {
        var role = associationObjectType.AddOneToOne(this, associationTypeId, roleTypeId, roleObjectType, roleName, associationName);
        this.Add(role);
        return role;
    }

    public MetaManyToOneRoleType AddManyToOneRelation(Guid associationTypeId, Guid roleTypeId, MetaObjectType associationObjectType, MetaObjectType roleObjectType, string? roleName = null, string? associationName = null)
    {
        var role = associationObjectType.AddManyToOne(this, associationTypeId, roleTypeId, roleObjectType, roleName, associationName);
        this.Add(role);
        return role;
    }

    public MetaOneToManyRoleType AddOneToManyRelation(Guid associationTypeId, Guid roleTypeId, MetaObjectType associationObjectType, MetaObjectType roleObjectType, string? roleName = null, string? associationName = null)
    {
        var role = associationObjectType.AddOneToMany(this, associationTypeId, roleTypeId, roleObjectType, roleName, associationName);
        this.Add(role);
        return role;
    }

    public MetaManyToManyRoleType AddManyToManyRelation(Guid associationTypeId, Guid roleTypeId, MetaObjectType associationObjectType, MetaObjectType roleObjectType, string? roleName = null, string? associationName = null)
    {
        var role = associationObjectType.AddManyToMany(this, associationTypeId, roleTypeId, roleObjectType, roleName, associationName);
        this.Add(role);
        return role;
    }

    public MetaInheritance AddInheritance(Guid id, MetaObjectType subtype, MetaObjectType supertype)
    {
        var inheritance = new MetaInheritance(this, id, subtype, supertype);

        subtype.AddDirectSupertype(supertype);

        this.Add(inheritance);
        return inheritance;
    }

    internal void ResetDerivations()
    {
        foreach ((_, var objectType) in this.ObjectTypeById)
        {
            objectType.ResetDerivations();
        }
    }

    private void Add(MetaObjectType objectType)
    {
        this.objectTypeById.Add(objectType.Id, objectType);
        this.objectTypeByName.Add(objectType.Name, objectType);
    }

    private void Add(IMetaRoleType roleType)
    {
        this.roleTypeById.Add(roleType.Id, roleType);
        this.associationTypeById.Add(roleType.AssociationType.Id, roleType.AssociationType);
    }

    private void Add(MetaInheritance inheritance)
    {
        this.inheritanceById.Add(inheritance.Id, inheritance);
    }
}
