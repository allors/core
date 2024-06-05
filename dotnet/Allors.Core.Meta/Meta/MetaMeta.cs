namespace Allors.Core.Meta.Meta;

using System;
using System.Collections.Generic;

public sealed class MetaMeta
{
    private readonly Dictionary<Guid, MetaDomain> domainById;
    private readonly Dictionary<Guid, MetaObjectType> objectTypeById;
    private readonly Dictionary<string, MetaObjectType> objectTypeByName;
    private readonly Dictionary<Guid, IMetaAssociationType> associationTypeById;
    private readonly Dictionary<Guid, IMetaRoleType> roleTypeById;

    public MetaMeta()
    {
        this.domainById = [];
        this.objectTypeById = [];
        this.objectTypeByName = [];
        this.associationTypeById = [];
        this.roleTypeById = [];
    }

    public IReadOnlyDictionary<Guid, MetaDomain> DomainById => this.domainById;

    public IReadOnlyDictionary<Guid, MetaObjectType> ObjectTypeById => this.objectTypeById;

    public IReadOnlyDictionary<string, MetaObjectType> ObjectTypeByName => this.objectTypeByName;

    public IReadOnlyDictionary<Guid, IMetaAssociationType> AssociationTypeById => this.associationTypeById;

    public IReadOnlyDictionary<Guid, IMetaRoleType> RoleTypeById => this.roleTypeById;

    public MetaDomain AddDomain(Guid id, string name)
    {
        var domain = new MetaDomain(this, id, name);
        this.domainById.Add(domain.Id, domain);
        return domain;
    }

    public MetaObjectType AddUnit(MetaDomain domain, Guid id, string name)
    {
        var objectType = new MetaObjectType(domain, MetaObjectTypeKind.Unit, id, name);
        this.Add(objectType);
        return objectType;
    }

    public MetaObjectType AddInterface(MetaDomain domain, Guid id, string name, params MetaObjectType[] directSupertypes)
    {
        var objectType = new MetaObjectType(domain, MetaObjectTypeKind.Interface, id, name);
        this.Add(objectType);

        foreach (var superType in directSupertypes)
        {
            objectType.AddDirectSupertype(superType);
        }

        return objectType;
    }

    public MetaObjectType AddClass(MetaDomain domain, Guid id, string name, params MetaObjectType[] directSupertypes)
    {
        var objectType = new MetaObjectType(domain, MetaObjectTypeKind.Class, id, name);
        this.Add(objectType);

        foreach (var superType in directSupertypes)
        {
            objectType.AddDirectSupertype(superType);
        }

        return objectType;
    }

    public MetaObjectType AddClass<T>(MetaDomain domain, Guid id, params MetaObjectType[] directSupertypes) => this.AddClass(domain, id, typeof(T), directSupertypes);

    public MetaObjectType AddClass(MetaDomain domain, Guid id, Type type, params MetaObjectType[] directSupertypes)
    {
        var objectType = new MetaObjectType(domain, MetaObjectTypeKind.Class, id, type);
        this.Add(objectType);

        foreach (var superType in directSupertypes)
        {
            objectType.AddDirectSupertype(superType);
        }

        return objectType;
    }

    public MetaUnitRoleType AddUnitRelation(MetaDomain domain, Guid associationTypeId, Guid roleTypeId, MetaObjectType associationObjectType, MetaObjectType roleObjectType, string roleName, string? associationName = null)
    {
        var role = associationObjectType.AddUnit(domain, associationTypeId, roleTypeId, roleObjectType, roleName, associationName);
        this.Add(role);
        return role;
    }

    public MetaOneToOneRoleType AddOneToOneRelation(MetaDomain domain, Guid associationTypeId, Guid roleTypeId, MetaObjectType associationObjectType, MetaObjectType roleObjectType, string? roleName = null, string? associationName = null)
    {
        var role = associationObjectType.AddOneToOne(domain, associationTypeId, roleTypeId, roleObjectType, roleName, associationName);
        this.Add(role);
        return role;
    }

    public MetaManyToOneRoleType AddManyToOneRelation(MetaDomain domain, Guid associationTypeId, Guid roleTypeId, MetaObjectType associationObjectType, MetaObjectType roleObjectType, string? roleName = null, string? associationName = null)
    {
        var role = associationObjectType.AddManyToOne(domain, associationTypeId, roleTypeId, roleObjectType, roleName, associationName);
        this.Add(role);
        return role;
    }

    public MetaOneToManyRoleType AddOneToManyRelation(MetaDomain domain, Guid associationTypeId, Guid roleTypeId, MetaObjectType associationObjectType, MetaObjectType roleObjectType, string? roleName = null, string? associationName = null)
    {
        var role = associationObjectType.AddOneToMany(domain, associationTypeId, roleTypeId, roleObjectType, roleName, associationName);
        this.Add(role);
        return role;
    }

    public MetaManyToManyRoleType AddManyToManyRelation(MetaDomain domain, Guid associationTypeId, Guid roleTypeId, MetaObjectType associationObjectType, MetaObjectType roleObjectType, string? roleName = null, string? associationName = null)
    {
        var role = associationObjectType.AddManyToMany(domain, associationTypeId, roleTypeId, roleObjectType, roleName, associationName);
        this.Add(role);
        return role;
    }

    internal string Pluralize(string singular)
    {
        static bool EndsWith(string word, string ending) => word.EndsWith(ending, StringComparison.InvariantCultureIgnoreCase);

        if (EndsWith(singular, "y") &&
            !EndsWith(singular, "ay") &&
            !EndsWith(singular, "ey") &&
            !EndsWith(singular, "iy") &&
            !EndsWith(singular, "oy") &&
            !EndsWith(singular, "uy"))
        {
            return singular.Substring(0, singular.Length - 1) + "ies";
        }

        if (EndsWith(singular, "us"))
        {
            return singular + "es";
        }

        if (EndsWith(singular, "ss"))
        {
            return singular + "es";
        }

        if (EndsWith(singular, "x") ||
            EndsWith(singular, "ch") ||
            EndsWith(singular, "sh"))
        {
            return singular + "es";
        }

        if (EndsWith(singular, "f") && singular.Length > 1)
        {
            return singular.Substring(0, singular.Length - 1) + "ves";
        }

        if (EndsWith(singular, "fe") && singular.Length > 2)
        {
            return singular.Substring(0, singular.Length - 2) + "ves";
        }

        return singular + "s";
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

        var domain = objectType.Domain;
        domain.Add(objectType);
    }

    private void Add(IMetaRoleType roleType)
    {
        this.roleTypeById.Add(roleType.Id, roleType);
        this.associationTypeById.Add(roleType.AssociationType.Id, roleType.AssociationType);

        var domain = roleType.Domain;
        domain.Add(roleType);
    }
}
