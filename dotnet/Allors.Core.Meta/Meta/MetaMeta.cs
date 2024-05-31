namespace Allors.Core.Meta.Meta;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public sealed class MetaMeta
{
    private readonly Dictionary<string, MetaObjectType> objectTypeByName;

    public MetaMeta()
    {
        this.objectTypeByName = [];

        this.ObjectTypeByName = new ReadOnlyDictionary<string, MetaObjectType>(this.objectTypeByName);
    }

    public IReadOnlyDictionary<string, MetaObjectType> ObjectTypeByName { get; }

    public MetaUnitRoleType AddUnit(MetaObjectType associationObjectType, MetaObjectType roleObjectType, string roleName, string? associationName = null) => associationObjectType.AddUnit(roleObjectType, roleName, associationName);

    public MetaOneToOneRoleType AddOneToOne(MetaObjectType associationObjectType, MetaObjectType roleObjectType, string? roleName = null, string? associationName = null) => associationObjectType.AddOneToOne(roleObjectType, roleName, associationName);

    public MetaManyToOneRoleType AddManyToOne(MetaObjectType associationObjectType, MetaObjectType roleObjectType, string? roleName = null, string? associationName = null) => associationObjectType.AddManyToOne(roleObjectType, roleName, associationName);

    public MetaOneToManyRoleType AddOneToMany(MetaObjectType associationObjectType, MetaObjectType roleObjectType, string? roleName = null, string? associationName = null) => associationObjectType.AddOneToMany(roleObjectType, roleName, associationName);

    public MetaManyToManyRoleType AddManyToMany(MetaObjectType associationObjectType, MetaObjectType roleObjectType, string? roleName = null, string? associationName = null) => associationObjectType.AddManyToMany(roleObjectType, roleName, associationName);

    public MetaObjectType AddUnit(Guid id, string name)
    {
        var objectType = new MetaObjectType(this, MetaObjectTypeKind.Unit, id, name);
        this.objectTypeByName.Add(objectType.Name, objectType);
        return objectType;
    }

    public MetaObjectType AddInterface(Guid id, string name, params MetaObjectType[] directSupertypes)
    {
        var objectType = new MetaObjectType(this, MetaObjectTypeKind.Interface, id, name);
        this.objectTypeByName.Add(objectType.Name, objectType);
        foreach (var superType in directSupertypes)
        {
            objectType.AddDirectSupertype(superType);
        }

        return objectType;
    }

    public MetaObjectType AddClass(Guid id, string name, params MetaObjectType[] directSupertypes)
    {
        var objectType = new MetaObjectType(this, MetaObjectTypeKind.Class, id, name);
        this.objectTypeByName.Add(objectType.Name, objectType);
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
        this.objectTypeByName.Add(objectType.Name, objectType);
        foreach (var superType in directSupertypes)
        {
            objectType.AddDirectSupertype(superType);
        }

        return objectType;
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
        foreach ((_, var objectType) in this.ObjectTypeByName)
        {
            objectType.ResetDerivations();
        }
    }
}
