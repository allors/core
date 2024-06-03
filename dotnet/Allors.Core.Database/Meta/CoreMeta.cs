namespace Allors.Core.Database.Meta;

using System;
using System.Collections.Generic;
using Allors.Core.Database.Meta.Domain;
using Allors.Core.Meta.Domain;

/// <summary>
/// Core Meta.
/// </summary>
public sealed class CoreMeta
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CoreMeta"/> class.
    /// </summary>
    public CoreMeta()
    {
        this.IsFrozen = false;
        this.Meta = new CoreMetaMeta();
        this.MetaPopulation = this.Meta.CreateMetaPopulation();

        this.AllorsCore = this.AddDomain(new Guid("74b21070-a04e-41c6-a896-902c8b04a2fd"), "AllorsCore");

        // Meta
        this.Domain = this.AddClass(this.Meta.Domain.Id, this.Meta.Domain.Name);

        // Domain
        // Units
        this.Boolean = this.AddUnit(new Guid("8906C53F-62CF-41C8-B95D-2084A74AC233"), "Boolean");
        this.Decimal = this.AddUnit(new Guid("083579BF-2E72-48CD-B491-37D776C70F44"), "Decimal");
        this.Double = this.AddUnit(new Guid("D81420B6-6773-4D94-8BA0-805418327612"), "Double");
        this.Integer = this.AddUnit(new Guid("66E81092-0903-4DE4-9741-9968BC94D68E"), "Integer");
        this.String = this.AddUnit(new Guid("58BB7632-4724-4F92-869B-B30D7A7BEE9E"), "String");

        // Composites
        this.Object = this.AddInterface(new Guid("8904EE32-CF11-4019-9FD7-FB9631F9ACAC"), "Object");
    }

    /// <summary>
    /// Meta Meta.
    /// </summary>
    public CoreMetaMeta Meta { get; }

    /// <summary>
    /// Is this meta frozen.
    /// </summary>
    public bool IsFrozen { get; private set; }

    /// <summary>
    /// The meta population.
    /// </summary>
    public MetaPopulation MetaPopulation { get; init; }

    /// <summary>
    /// The domain.
    /// </summary>
    public Class Domain { get; init; }

    /// <summary>
    /// The allors core domain.
    /// </summary>
    public Domain.Domain AllorsCore { get; init; }

    /// <summary>
    /// The Object interface.
    /// </summary>
    public Interface Object { get; init; }

    /// <summary>
    /// The Boolean unit.
    /// </summary>
    public Unit Boolean { get; init; }

    /// <summary>
    /// The Decimal unit.
    /// </summary>
    public Unit Decimal { get; init; }

    /// <summary>
    /// The Double unit.
    /// </summary>
    public Unit Double { get; init; }

    /// <summary>
    /// The Integer unit.
    /// </summary>
    public Unit Integer { get; init; }

    /// <summary>
    /// The String unit.
    /// </summary>
    public Unit String { get; init; }

    /// <summary>
    /// Freezes meta.
    /// </summary>
    public void Freeze()
    {
        if (!this.IsFrozen)
        {
            this.IsFrozen = true;
            this.MetaPopulation.Derive();

            // TODO: Add freeze to Allors.Meta
            // this.MetaMeta.Freeze();
            // this.MetaPopulation.Freeze();
        }
    }

    /// <summary>
    /// Creates a new domain.
    /// </summary>
    public Domain.Domain AddDomain(Guid id, string name)
    {
        var m = this.Meta;

        var domain = this.MetaPopulation.Build<Domain.Domain>(v =>
        {
            v[m.MetaObjectId] = id;
            v[m.DomainName] = name;
        });

        domain.Add(this.Meta.DomainTypes, domain);

        return domain;
    }

    /// <summary>
    /// Add an inheritance relation between a subtype and a supertype.
    /// </summary>
    public IEnumerable<Inheritance> AddInheritance(Domain.Domain domain, IComposite subtype, params Interface[] supertypes)
    {
        foreach (var supertype in supertypes)
        {
            yield return this.AddInheritance(domain, subtype, supertype);
        }
    }

    /// <summary>
    /// Add an inheritance relation between a subtype and a supertype.
    /// </summary>
    public Inheritance AddInheritance(Domain.Domain domain, IComposite subtype, Interface supertype)
    {
        var m = this.Meta;

        var inheritance = this.MetaPopulation.Build<Inheritance>(v =>
        {
            v[m.InheritanceSubtype] = subtype;
            v[m.InheritanceSupertype] = supertype;
        });

        domain.Add(this.Meta.DomainTypes, inheritance);

        return inheritance;
    }

    /// <summary>
    /// Creates a new unit.
    /// </summary>
    public Unit AddUnit(Domain.Domain domain, Guid id, string singularName, string? assignedPluralName = null)
    {
        var m = this.Meta;

        var unit = this.MetaPopulation.Build<Unit>(v =>
        {
            v[m.MetaObjectId] = id;
            v[m.ObjectTypeSingularName] = singularName;
            v[m.ObjectTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(this.Meta.DomainTypes, unit);

        return unit;
    }

    /// <summary>
    /// Creates a new interface.
    /// </summary>
    public Interface AddInterface(Domain.Domain domain, Guid id, string singularName, string? assignedPluralName = null)
    {
        var m = this.Meta;

        var @interface = this.MetaPopulation.Build<Interface>(v =>
        {
            v[m.MetaObjectId] = id;
            v[m.ObjectTypeSingularName] = singularName;
            v[m.ObjectTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(this.Meta.DomainTypes, @interface);

        return @interface;
    }

    /// <summary>
    /// Creates a new class.
    /// </summary>
    public Class AddClass(Domain.Domain domain, Guid id, string singularName, string? assignedPluralName = null)
    {
        var m = this.Meta;

        var @class = this.MetaPopulation.Build<Class>(v =>
        {
            v[m.MetaObjectId] = id;
            v[m.ObjectTypeSingularName] = singularName;
            v[m.ObjectTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(this.Meta.DomainTypes, @class);

        return @class;
    }

    /// <summary>
    /// Creates new binary relation end types.
    /// </summary>
    public (BinaryAssociationType AssociationType, BinaryRoleType RoleType) AddBinaryRelation(Domain.Domain domain, Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
    {
        var m = this.Meta;

        var associationType = this.MetaPopulation.Build<BinaryAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = this.MetaPopulation.Build<BinaryRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = unit;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(this.Meta.DomainTypes, unit);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new boolean relation end types.
    /// </summary>
    public (BooleanAssociationType AssociationType, BooleanRoleType RoleType) AddBooleanRelation(Domain.Domain domain, Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
    {
        var m = this.Meta;

        var associationType = this.MetaPopulation.Build<BooleanAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = this.MetaPopulation.Build<BooleanRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = unit;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(this.Meta.DomainTypes, associationType);
        domain.Add(this.Meta.DomainTypes, roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new datetime relation end types.
    /// </summary>
    public (DateTimeAssociationType AssociationType, DateTimeRoleType RoleType) AddDateTimeRelation(Domain.Domain domain, Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
    {
        var m = this.Meta;

        var associationType = this.MetaPopulation.Build<DateTimeAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = this.MetaPopulation.Build<DateTimeRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = unit;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(this.Meta.DomainTypes, associationType);
        domain.Add(this.Meta.DomainTypes, roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new decimal relation end types.
    /// </summary>
    public (DecimalAssociationType AssociationType, DecimalRoleType RoleType) AddDecimalRelation(Domain.Domain domain, Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
    {
        var m = this.Meta;

        var associationType = this.MetaPopulation.Build<DecimalAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = this.MetaPopulation.Build<DecimalRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = unit;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(this.Meta.DomainTypes, associationType);
        domain.Add(this.Meta.DomainTypes, roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new float relation end types.
    /// </summary>
    public (FloatAssociationType AssociationType, FloatRoleType RoleType) AddFloatRelation(Domain.Domain domain, Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
    {
        var m = this.Meta;

        var associationType = this.MetaPopulation.Build<FloatAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = this.MetaPopulation.Build<FloatRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = unit;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(this.Meta.DomainTypes, associationType);
        domain.Add(this.Meta.DomainTypes, roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new integer relation end types.
    /// </summary>
    public (IntegerAssociationType AssociationType, IntegerRoleType RoleType) AddIntegerRelation(Domain.Domain domain, Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
    {
        var m = this.Meta;

        var associationType = this.MetaPopulation.Build<IntegerAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = this.MetaPopulation.Build<IntegerRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = unit;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(this.Meta.DomainTypes, associationType);
        domain.Add(this.Meta.DomainTypes, roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new string relation end types.
    /// </summary>
    public (StringAssociationType AssociationType, StringRoleType RoleType) AddStringRelation(Domain.Domain domain, Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
    {
        var m = this.Meta;

        var associationType = this.MetaPopulation.Build<StringAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = this.MetaPopulation.Build<StringRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = unit;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(this.Meta.DomainTypes, associationType);
        domain.Add(this.Meta.DomainTypes, roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new unique relation end types.
    /// </summary>
    public (UniqueAssociationType AssociationType, UniqueRoleType RoleType) AddUniqueRelation(Domain.Domain domain, Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
    {
        var m = this.Meta;

        var associationType = this.MetaPopulation.Build<UniqueAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = this.MetaPopulation.Build<UniqueRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = unit;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(this.Meta.DomainTypes, associationType);
        domain.Add(this.Meta.DomainTypes, roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new OneToOne relation end types.
    /// </summary>
    public (OneToOneAssociationType AssociationType, OneToOneRoleType RoleType) AddOneToOneRelation(Domain.Domain domain, Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string singularName, string? assignedPluralName = null)
    {
        var m = this.Meta;

        var associationType = this.MetaPopulation.Build<OneToOneAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = this.MetaPopulation.Build<OneToOneRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = roleComposite;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(this.Meta.DomainTypes, associationType);
        domain.Add(this.Meta.DomainTypes, roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new ManyToOne relation end types.
    /// </summary>
    public (ManyToOneAssociationType AssociationType, ManyToOneRoleType RoleType) AddManyToOneRelation(Domain.Domain domain, Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string singularName, string? assignedPluralName = null)
    {
        var m = this.Meta;

        var associationType = this.MetaPopulation.Build<ManyToOneAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = this.MetaPopulation.Build<ManyToOneRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = roleComposite;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(this.Meta.DomainTypes, associationType);
        domain.Add(this.Meta.DomainTypes, roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new OneToMany relation end types.
    /// </summary>
    public (OneToManyAssociationType AssociationType, OneToManyRoleType RoleType) AddOneToManyRelation(Domain.Domain domain, Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string singularName, string? assignedPluralName = null)
    {
        var m = this.Meta;

        var associationType = this.MetaPopulation.Build<OneToManyAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = this.MetaPopulation.Build<OneToManyRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = roleComposite;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(this.Meta.DomainTypes, associationType);
        domain.Add(this.Meta.DomainTypes, roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new ManyToMany relation end types.
    /// </summary>
    public (ManyToManyAssociationType AssociationType, ManyToManyRoleType RoleType) AddManyToManyRelation(Domain.Domain domain, Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string singularName, string? assignedPluralName = null)
    {
        var m = this.Meta;

        var associationType = this.MetaPopulation.Build<ManyToManyAssociationType>(v =>
        {
            v[m.MetaObjectId] = associationTypeId;
            v[m.AssociationTypeComposite] = associationComposite;
        });

        var roleType = this.MetaPopulation.Build<ManyToManyRoleType>(v =>
        {
            v[m.MetaObjectId] = roleTypeId;
            v[m.RoleTypeAssociationType] = associationType;
            v[m.RoleTypeObjectType] = roleComposite;
            v[m.RoleTypeSingularName] = singularName;
            v[m.RoleTypeAssignedPluralName] = assignedPluralName;
        });

        domain.Add(this.Meta.DomainTypes, associationType);
        domain.Add(this.Meta.DomainTypes, roleType);

        return (associationType, roleType);
    }

    /// <summary>
    /// Add an inheritance relation between a subtype and a supertype.
    /// </summary>
    internal IEnumerable<Inheritance> AddInheritance(IComposite subtype, params Interface[] supertypes)
    {
        foreach (var supertype in supertypes)
        {
            yield return this.AddInheritance(subtype, supertype);
        }
    }

    /// <summary>
    /// Add an inheritance relation between a subtype and a supertype.
    /// </summary>
    internal Inheritance AddInheritance(IComposite subtype, Interface supertype)
    {
        var m = this.Meta;

        var inheritance = this.MetaPopulation.Build<Inheritance>(v =>
        {
            v[m.InheritanceSubtype] = subtype;
            v[m.InheritanceSupertype] = supertype;
        });

        this.Domain.Add(this.Meta.DomainTypes, inheritance);

        return inheritance;
    }

    internal Unit AddUnit(Guid id, string singularName, string? assignedPluralName = null) => this.AddUnit(this.AllorsCore, id, singularName, assignedPluralName);

    internal Interface AddInterface(Guid id, string singularName, string? assignedPluralName = null) => this.AddInterface(this.AllorsCore, id, singularName, assignedPluralName);

    private Class AddClass(Guid id, string singularName, string? assignedPluralName = null) => this.AddClass(this.AllorsCore, id, singularName, assignedPluralName);

    private (BinaryAssociationType AssociationType, BinaryRoleType RoleType) AddBinaryRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
        => this.AddBinaryRelation(this.AllorsCore, associationTypeId, roleTypeId, associationComposite, unit, singularName, assignedPluralName);

    private (BooleanAssociationType AssociationType, BooleanRoleType RoleType) AddBooleanRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
        => this.AddBooleanRelation(this.AllorsCore, associationTypeId, roleTypeId, associationComposite, unit, singularName, assignedPluralName);

    private (DateTimeAssociationType AssociationType, DateTimeRoleType RoleType) AddDateTimeRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
        => this.AddDateTimeRelation(this.AllorsCore, associationTypeId, roleTypeId, associationComposite, unit, singularName, assignedPluralName);

    private (DecimalAssociationType AssociationType, DecimalRoleType RoleType) AddDecimalRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
        => this.AddDecimalRelation(this.AllorsCore, associationTypeId, roleTypeId, associationComposite, unit, singularName, assignedPluralName);

    private (FloatAssociationType AssociationType, FloatRoleType RoleType) AddFloatRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
        => this.AddFloatRelation(this.AllorsCore, associationTypeId, roleTypeId, associationComposite, unit, singularName, assignedPluralName);

    private (IntegerAssociationType AssociationType, IntegerRoleType RoleType) AddIntegerRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
        => this.AddIntegerRelation(this.AllorsCore, associationTypeId, roleTypeId, associationComposite, unit, singularName, assignedPluralName);

    private (StringAssociationType AssociationType, StringRoleType RoleType) AddStringRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
        => this.AddStringRelation(this.AllorsCore, associationTypeId, roleTypeId, associationComposite, unit, singularName, assignedPluralName);

    private (UniqueAssociationType AssociationType, UniqueRoleType RoleType) AddUniqueRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
        => this.AddUniqueRelation(this.AllorsCore, associationTypeId, roleTypeId, associationComposite, unit, singularName, assignedPluralName);

    private (OneToOneAssociationType AssociationType, OneToOneRoleType RoleType) AddOneToOneRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string singularName, string? assignedPluralName = null)
        => this.AddOneToOneRelation(this.AllorsCore, associationTypeId, roleTypeId, associationComposite, roleComposite, singularName, assignedPluralName);

    private (ManyToOneAssociationType AssociationType, ManyToOneRoleType RoleType) AddManyToOneRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string singularName, string? assignedPluralName = null)
        => this.AddManyToOneRelation(this.AllorsCore, associationTypeId, roleTypeId, associationComposite, roleComposite, singularName, assignedPluralName);

    private (OneToManyAssociationType AssociationType, OneToManyRoleType RoleType) AddOneToManyRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string singularName, string? assignedPluralName = null)
        => this.AddOneToManyRelation(this.AllorsCore, associationTypeId, roleTypeId, associationComposite, roleComposite, singularName, assignedPluralName);

    private (ManyToManyAssociationType AssociationType, ManyToManyRoleType RoleType) AddManyToManyRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string singularName, string? assignedPluralName = null)
        => this.AddManyToManyRelation(this.AllorsCore, associationTypeId, roleTypeId, associationComposite, roleComposite, singularName, assignedPluralName);
}
