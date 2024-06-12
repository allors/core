namespace Allors.Core.Database.Meta;

using System;
using System.Collections.Generic;
using System.Linq;
using Allors.Core.Database.Meta.Domain;
using Allors.Core.Database.MetaMeta;
using Allors.Core.Meta;

/// <summary>
/// Meta Extensions.
/// </summary>
public static class MetaExtensions
{
    /// <summary>
    /// Creates a new domain.
    /// </summary>
    public static Domain.Domain AddDomain(this Meta @this, Guid id, string name)
    {
        var m = @this.MetaMeta;

        var domain = @this.Build<Domain.Domain>(v =>
        {
            v[m.MetaObjectId] = id;
            v[m.DomainName] = name;
        });

        domain.Add(m.DomainTypes(), domain);

        return domain;
    }

    /// <summary>
    /// Add an inheritance relation between a subtype and a supertype.
    /// </summary>
    public static IEnumerable<Inheritance> AddInheritance(this Meta @this, Domain.Domain domain, Guid id, IComposite subtype, params Interface[] supertypes)
    {
        foreach (var supertype in supertypes)
        {
            yield return @this.AddInheritance(domain, id, subtype, supertype);
        }
    }

    /// <summary>
    /// Add an inheritance relation between a subtype and a supertype.
    /// </summary>
    public static Inheritance AddInheritance(this Meta @this, Domain.Domain domain, Guid id, IComposite subtype, Interface supertype)
    {
        var m = @this.MetaMeta;

        var inheritance = @this.Build<Inheritance>(v =>
        {
            v[m.MetaObjectId] = id;
            v[m.InheritanceSubtype] = subtype;
            v[m.InheritanceSupertype] = supertype;
        });

        domain.Add(m.DomainTypes(), inheritance);

        return inheritance;
    }

    /// <summary>
    /// Creates a new unit.
    /// </summary>
    public static Unit AddUnit(this Meta @this, Domain.Domain domain, Guid id, string singularName, string? assignedPluralName = null)
    {
        var m = @this.MetaMeta;

        var unit = @this.Build<Unit>(v =>
        {
            v[m.MetaObjectId] = id;
            v[m.ObjectTypeSingularName] = singularName;
            v[m.ObjectTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(m.DomainTypes(), unit);

        return unit;
    }

    /// <summary>
    /// Creates a new interface.
    /// </summary>
    public static Interface AddInterface(this Meta @this, Domain.Domain domain, Guid id, string singularName, string? assignedPluralName = null)
    {
        var m = @this.MetaMeta;

        var @interface = @this.Build<Interface>(v =>
        {
            v[m.MetaObjectId] = id;
            v[m.ObjectTypeSingularName] = singularName;
            v[m.ObjectTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(m.DomainTypes(), @interface);

        return @interface;
    }

    /// <summary>
    /// Creates a new class.
    /// </summary>
    public static Class AddClass(this Meta @this, Domain.Domain domain, Guid id, string singularName, string? assignedPluralName = null)
    {
        var m = @this.MetaMeta;

        var @class = @this.Build<Class>(v =>
        {
            v[m.MetaObjectId] = id;
            v[m.ObjectTypeSingularName] = singularName;
            v[m.ObjectTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(m.DomainTypes(), @class);

        return @class;
    }

    /// <summary>
    /// Creates new binary relation end types.
    /// </summary>
    public static (BinaryAssociationType AssociationType, BinaryRoleType RoleType) AddBinaryRelation(this Meta @this, Domain.Domain domain, Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
    {
        var m = @this.MetaMeta;

        var associationType = @this.Build<BinaryAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = @this.Build<BinaryRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = unit;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(m.DomainTypes(), unit);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new boolean relation end types.
    /// </summary>
    public static (BooleanAssociationType AssociationType, BooleanRoleType RoleType) AddBooleanRelation(this Meta @this, Domain.Domain domain, Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
    {
        var m = @this.MetaMeta;

        var associationType = @this.Build<BooleanAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = @this.Build<BooleanRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = unit;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(m.DomainTypes(), associationType);
        domain.Add(m.DomainTypes(), roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new datetime relation end types.
    /// </summary>
    public static (DateTimeAssociationType AssociationType, DateTimeRoleType RoleType) AddDateTimeRelation(this Meta @this, Domain.Domain domain, Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
    {
        var m = @this.MetaMeta;

        var associationType = @this.Build<DateTimeAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = @this.Build<DateTimeRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = unit;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(m.DomainTypes(), associationType);
        domain.Add(m.DomainTypes(), roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new decimal relation end types.
    /// </summary>
    public static (DecimalAssociationType AssociationType, DecimalRoleType RoleType) AddDecimalRelation(this Meta @this, Domain.Domain domain, Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
    {
        var m = @this.MetaMeta;

        var associationType = @this.Build<DecimalAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = @this.Build<DecimalRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = unit;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(m.DomainTypes(), associationType);
        domain.Add(m.DomainTypes(), roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new float relation end types.
    /// </summary>
    public static (FloatAssociationType AssociationType, FloatRoleType RoleType) AddFloatRelation(this Meta @this, Domain.Domain domain, Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
    {
        var m = @this.MetaMeta;

        var associationType = @this.Build<FloatAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = @this.Build<FloatRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = unit;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(m.DomainTypes(), associationType);
        domain.Add(m.DomainTypes(), roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new integer relation end types.
    /// </summary>
    public static (IntegerAssociationType AssociationType, IntegerRoleType RoleType) AddIntegerRelation(this Meta @this, Domain.Domain domain, Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
    {
        var m = @this.MetaMeta;

        var associationType = @this.Build<IntegerAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = @this.Build<IntegerRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = unit;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(m.DomainTypes(), associationType);
        domain.Add(m.DomainTypes(), roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new string relation end types.
    /// </summary>
    public static (StringAssociationType AssociationType, StringRoleType RoleType) AddStringRelation(this Meta @this, Domain.Domain domain, Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
    {
        var m = @this.MetaMeta;

        var associationType = @this.Build<StringAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = @this.Build<StringRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = unit;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(m.DomainTypes(), associationType);
        domain.Add(m.DomainTypes(), roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new unique relation end types.
    /// </summary>
    public static (UniqueAssociationType AssociationType, UniqueRoleType RoleType) AddUniqueRelation(this Meta @this, Domain.Domain domain, Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
    {
        var m = @this.MetaMeta;

        var associationType = @this.Build<UniqueAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = @this.Build<UniqueRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = unit;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(m.DomainTypes(), associationType);
        domain.Add(m.DomainTypes(), roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new OneToOne relation end types.
    /// </summary>
    public static (OneToOneAssociationType AssociationType, OneToOneRoleType RoleType) AddOneToOneRelation(this Meta @this, Domain.Domain domain, Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string? singularName = null, string? assignedPluralName = null)
    {
        var m = @this.MetaMeta;

        var associationType = @this.Build<OneToOneAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = @this.Build<OneToOneRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = roleComposite;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(m.DomainTypes(), associationType);
        domain.Add(m.DomainTypes(), roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new ManyToOne relation end types.
    /// </summary>
    public static (ManyToOneAssociationType AssociationType, ManyToOneRoleType RoleType) AddManyToOneRelation(this Meta @this, Domain.Domain domain, Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string? singularName = null, string? assignedPluralName = null)
    {
        var m = @this.MetaMeta;

        var associationType = @this.Build<ManyToOneAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = @this.Build<ManyToOneRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = roleComposite;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(m.DomainTypes(), associationType);
        domain.Add(m.DomainTypes(), roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new OneToMany relation end types.
    /// </summary>
    public static (OneToManyAssociationType AssociationType, OneToManyRoleType RoleType) AddOneToManyRelation(this Meta @this, Domain.Domain domain, Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string? singularName = null, string? assignedPluralName = null)
    {
        var m = @this.MetaMeta;

        var associationType = @this.Build<OneToManyAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = @this.Build<OneToManyRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = roleComposite;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(m.DomainTypes(), associationType);
        domain.Add(m.DomainTypes(), roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new ManyToMany relation end types.
    /// </summary>
    public static (ManyToManyAssociationType AssociationType, ManyToManyRoleType RoleType) AddManyToManyRelation(this Meta @this, Domain.Domain domain, Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string? singularName = null, string? assignedPluralName = null)
    {
        var m = @this.MetaMeta;

        var associationType = @this.Build<ManyToManyAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = @this.Build<ManyToManyRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = roleComposite;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(m.DomainTypes(), associationType);
        domain.Add(m.DomainTypes(), roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Binary.
    /// </summary>
    public static Unit Binary(this Meta @this) => (Unit)@this.Get(CoreMeta.Binary);

    /// <summary>
    /// Binary.
    /// </summary>
    public static Unit Boolean(this Meta @this) => (Unit)@this.Get(CoreMeta.Boolean);

    /// <summary>
    /// DateTime.
    /// </summary>
    public static Unit DateTime(this Meta @this) => (Unit)@this.Get(CoreMeta.DateTime);

    /// <summary>
    /// Decimal.
    /// </summary>
    public static Unit Decimal(this Meta @this) => (Unit)@this.Get(CoreMeta.Decimal);

    /// <summary>
    /// Float.
    /// </summary>
    public static Unit Float(this Meta @this) => (Unit)@this.Get(CoreMeta.Float);

    /// <summary>
    /// Integer.
    /// </summary>
    public static Unit Integer(this Meta @this) => (Unit)@this.Get(CoreMeta.Integer);

    /// <summary>
    /// String.
    /// </summary>
    public static Unit String(this Meta @this) => (Unit)@this.Get(CoreMeta.String);

    /// <summary>
    /// Unique.
    /// </summary>
    public static Unit Unique(this Meta @this) => (Unit)@this.Get(CoreMeta.Unique);

    /// <summary>
    /// Object.
    /// </summary>
    public static Interface Object(this Meta @this) => (Interface)@this.Get(CoreMeta.Object);

    private static IMetaObject Get(this Meta @this, Guid id) => @this.Objects.First(v => ((Guid)v[@this.MetaMeta.MetaObjectId]!) == id);
}
