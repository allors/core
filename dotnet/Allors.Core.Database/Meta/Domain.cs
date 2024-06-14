namespace Allors.Core.Database.Meta;

using System;
using System.Collections.Generic;
using Allors.Core.Database.MetaMeta;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// A domain.
/// </summary>
public class Domain : MetaObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Domain"/> class.
    /// </summary>
    public Domain(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }

    /// <summary>
    /// Add an inheritance relation between a subtype and a supertype.
    /// </summary>
    public IEnumerable<Inheritance> AddInheritance(Guid id, IComposite subtype, params Interface[] supertypes)
    {
        foreach (var supertype in supertypes)
        {
            yield return this.AddInheritance(id, subtype, supertype);
        }
    }

    /// <summary>
    /// Add an inheritance relation between a subtype and a supertype.
    /// </summary>
    public Inheritance AddInheritance(Guid id, IComposite subtype, Interface supertype)
    {
        var m = this.MetaMeta;

        var inheritance = this.Meta.Build<Inheritance>(v =>
        {
            v[m.MetaObjectId] = id;
            v[m.InheritanceSubtype] = subtype;
            v[m.InheritanceSupertype] = supertype;
        });

        this.Add(m.DomainTypes(), inheritance);

        return inheritance;
    }

    /// <summary>
    /// Adds a new unit.
    /// </summary>
    public Unit AddUnit(Guid id, string singularName, string? assignedPluralName = null)
    {
        var m = this.MetaMeta;

        var unit = this.Meta.Build<Unit>(v =>
        {
            v[m.MetaObjectId] = id;
            v[m.ObjectTypeSingularName] = singularName;
            v[m.ObjectTypeAssignedPluralName] = assignedPluralName;
        });

        this.Add(m.DomainTypes(), unit);

        return unit;
    }

    /// <summary>
    /// Creates a new interface.
    /// </summary>
    public Interface AddInterface(Guid id, string singularName, string? assignedPluralName = null)
    {
        var m = this.MetaMeta;

        var @interface = this.Meta.Build<Interface>(v =>
        {
            v[m.MetaObjectId] = id;
            v[m.ObjectTypeSingularName] = singularName;
            v[m.ObjectTypeAssignedPluralName] = assignedPluralName;
        });

        this.Add(m.DomainTypes(), @interface);

        return @interface;
    }

    /// <summary>
    /// Creates a new class.
    /// </summary>
    public Class AddClass(Guid id, string singularName, string? assignedPluralName = null)
    {
        var m = this.MetaMeta;

        var @class = this.Meta.Build<Class>(v =>
        {
            v[m.MetaObjectId] = id;
            v[m.ObjectTypeSingularName] = singularName;
            v[m.ObjectTypeAssignedPluralName] = assignedPluralName;
        });

        this.Add(m.DomainTypes(), @class);

        return @class;
    }

    /// <summary>
    /// Creates new binary relation end types.
    /// </summary>
    public (BinaryAssociationType AssociationType, BinaryRoleType RoleType) AddBinaryRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, string singularName, string? assignedPluralName = null)
    {
        var m = this.MetaMeta;

        var associationType = this.Meta.Build<BinaryAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = this.Meta.Build<BinaryRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = this.Meta.Binary();
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        this.Add(m.DomainTypes(), associationType);
        this.Add(m.DomainTypes(), roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new boolean relation end types.
    /// </summary>
    public (BooleanAssociationType AssociationType, BooleanRoleType RoleType) AddBooleanRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, string singularName, string? assignedPluralName = null)
    {
        var m = this.MetaMeta;

        var associationType = this.Meta.Build<BooleanAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = this.Meta.Build<BooleanRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = this.Meta.Boolean();
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        this.Add(m.DomainTypes(), associationType);
        this.Add(m.DomainTypes(), roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new datetime relation end types.
    /// </summary>
    public (DateTimeAssociationType AssociationType, DateTimeRoleType RoleType) AddDateTimeRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, string singularName, string? assignedPluralName = null)
    {
        var m = this.MetaMeta;

        var associationType = this.Meta.Build<DateTimeAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = this.Meta.Build<DateTimeRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = this.Meta.DateTime();
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        this.Add(m.DomainTypes(), associationType);
        this.Add(m.DomainTypes(), roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new decimal relation end types.
    /// </summary>
    public (DecimalAssociationType AssociationType, DecimalRoleType RoleType) AddDecimalRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, string singularName, string? assignedPluralName = null)
    {
        var m = this.MetaMeta;

        var associationType = this.Meta.Build<DecimalAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = this.Meta.Build<DecimalRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = this.Meta.Decimal();
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        this.Add(m.DomainTypes(), associationType);
        this.Add(m.DomainTypes(), roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new float relation end types.
    /// </summary>
    public (FloatAssociationType AssociationType, FloatRoleType RoleType) AddFloatRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, string singularName, string? assignedPluralName = null)
    {
        var m = this.MetaMeta;

        var associationType = this.Meta.Build<FloatAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = this.Meta.Build<FloatRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = this.Meta.Float();
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        this.Add(m.DomainTypes(), associationType);
        this.Add(m.DomainTypes(), roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new integer relation end types.
    /// </summary>
    public (IntegerAssociationType AssociationType, IntegerRoleType RoleType) AddIntegerRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, string singularName, string? assignedPluralName = null)
    {
        var m = this.MetaMeta;

        var associationType = this.Meta.Build<IntegerAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = this.Meta.Build<IntegerRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = this.Meta.Integer();
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        this.Add(m.DomainTypes(), associationType);
        this.Add(m.DomainTypes(), roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new string relation end types.
    /// </summary>
    public (StringAssociationType AssociationType, StringRoleType RoleType) AddStringRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, string singularName, string? assignedPluralName = null)
    {
        var m = this.MetaMeta;

        var associationType = this.Meta.Build<StringAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = this.Meta.Build<StringRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = this.Meta.String();
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        this.Add(m.DomainTypes(), associationType);
        this.Add(m.DomainTypes(), roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new unique relation end types.
    /// </summary>
    public (UniqueAssociationType AssociationType, UniqueRoleType RoleType) AddUniqueRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, string singularName, string? assignedPluralName = null)
    {
        var m = this.MetaMeta;

        var associationType = this.Meta.Build<UniqueAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = this.Meta.Build<UniqueRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = this.Meta.Unique();
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        this.Add(m.DomainTypes(), associationType);
        this.Add(m.DomainTypes(), roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new OneToOne relation end types.
    /// </summary>
    public (OneToOneAssociationType AssociationType, OneToOneRoleType RoleType) AddOneToOneRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string? singularName = null, string? assignedPluralName = null)
    {
        var m = this.MetaMeta;

        var associationType = this.Meta.Build<OneToOneAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = this.Meta.Build<OneToOneRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = roleComposite;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        this.Add(m.DomainTypes(), associationType);
        this.Add(m.DomainTypes(), roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new ManyToOne relation end types.
    /// </summary>
    public (ManyToOneAssociationType AssociationType, ManyToOneRoleType RoleType) AddManyToOneRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string? singularName = null, string? assignedPluralName = null)
    {
        var m = this.MetaMeta;

        var associationType = this.Meta.Build<ManyToOneAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = this.Meta.Build<ManyToOneRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = roleComposite;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        this.Add(m.DomainTypes(), associationType);
        this.Add(m.DomainTypes(), roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new OneToMany relation end types.
    /// </summary>
    public (OneToManyAssociationType AssociationType, OneToManyRoleType RoleType) AddOneToManyRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string? singularName = null, string? assignedPluralName = null)
    {
        var m = this.MetaMeta;

        var associationType = this.Meta.Build<OneToManyAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = this.Meta.Build<OneToManyRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = roleComposite;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        this.Add(m.DomainTypes(), associationType);
        this.Add(m.DomainTypes(), roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new ManyToMany relation end types.
    /// </summary>
    public (ManyToManyAssociationType AssociationType, ManyToManyRoleType RoleType) AddManyToManyRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string? singularName = null, string? assignedPluralName = null)
    {
        var m = this.MetaMeta;

        var associationType = this.Meta.Build<ManyToManyAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = this.Meta.Build<ManyToManyRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = roleComposite;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        this.Add(m.DomainTypes(), associationType);
        this.Add(m.DomainTypes(), roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Adds a method type.
    /// </summary>
    public MethodType AddMethodType(Guid id, IComposite composite, string name)
    {
        var m = this.MetaMeta;

        var methodType = this.Meta.Build<MethodType>(v =>
        {
            v[m.MetaObjectId] = id;
            v[m.MethodTypeName] = name;
        });

        composite.Add(m.CompositeMethodTypes, methodType);
        this.Add(m.DomainTypes(), methodType);

        return methodType;
    }

    /// <inheritdoc/>
    public override string ToString() => (string)this[this.MetaMeta.DomainName]!;
}
