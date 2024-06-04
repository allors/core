namespace Allors.Core.Database.Meta;

using System;
using System.Collections.Generic;
using System.Linq;
using Allors.Core.Database.Meta.Domain;
using Allors.Core.Meta.Domain;
using Allors.Core.Meta.Meta;

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

        var m = this.Meta;

        // Meta
        // Objects
        this.AssociationType = this.AddInterface(m.AssociationType);
        this.Class = this.AddClass(m.Class);
        this.Composite = this.AddInterface(m.Composite);
        this.CompositeAssociationType = this.AddInterface(m.CompositeAssociationType);
        this.CompositeRoleType = this.AddInterface(m.CompositeRoleType);
        this.Domain = this.AddClass(m.Domain);
        this.Interface = this.AddClass(m.Interface);
        this.ManyToAssociationType = this.AddInterface(m.ManyToAssociationType);
        this.ManyToManyAssociationType = this.AddClass(m.ManyToManyAssociationType);
        this.ManyToManyRoleType = this.AddClass(m.ManyToManyRoleType);
        this.ManyToOneAssociationType = this.AddClass(m.ManyToOneAssociationType);
        this.ManyToOneRoleType = this.AddClass(m.ManyToOneRoleType);
        this.MetaObject = this.AddInterface(m.MetaObject);
        this.ObjectType = this.AddInterface(m.ObjectType);
        this.OneToAssociationType = this.AddInterface(m.OneToAssociationType);
        this.OneToManyAssociationType = this.AddClass(m.OneToManyAssociationType);
        this.OneToManyRoleType = this.AddClass(m.OneToManyRoleType);
        this.OneToOneAssociationType = this.AddClass(m.OneToOneAssociationType);
        this.OneToOneRoleType = this.AddClass(m.OneToOneRoleType);
        this.OperandType = this.AddInterface(m.OperandType);
        this.RelationEndType = this.AddInterface(m.RelationEndType);
        this.RoleType = this.AddInterface(m.RoleType);
        this.BinaryAssociationType = this.AddClass(m.BinaryAssociationType);
        this.BinaryRoleType = this.AddClass(m.BinaryRoleType);
        this.BooleanAssociationType = this.AddClass(m.BooleanAssociationType);
        this.BooleanRoleType = this.AddClass(m.BooleanRoleType);
        this.DateTimeAssociationType = this.AddClass(m.DateTimeAssociationType);
        this.DateTimeRoleType = this.AddClass(m.DateTimeRoleType);
        this.DecimalAssociationType = this.AddClass(m.DecimalAssociationType);
        this.DecimalRoleType = this.AddClass(m.DecimalRoleType);
        this.FloatAssociationType = this.AddClass(m.FloatAssociationType);
        this.FloatRoleType = this.AddClass(m.FloatRoleType);
        this.Inheritance = this.AddClass(m.Inheritance);
        this.IntegerAssociationType = this.AddClass(m.IntegerAssociationType);
        this.IntegerRoleType = this.AddClass(m.IntegerRoleType);
        this.StringAssociationType = this.AddClass(m.StringAssociationType);
        this.StringRoleType = this.AddClass(m.StringRoleType);
        this.UniqueAssociationType = this.AddClass(m.UniqueAssociationType);
        this.UniqueRoleType = this.AddClass(m.UniqueRoleType);
        this.ToManyRoleType = this.AddInterface(m.ToManyRoleType);
        this.ToOneRoleType = this.AddInterface(m.ToOneRoleType);
        this.Type = this.AddInterface(m.Type);
        this.Unit = this.AddClass(m.Unit);
        this.UnitAssociationType = this.AddInterface(m.UnitAssociationType);
        this.UnitRoleType = this.AddInterface(m.UnitRoleType);

        // Relations
        (_, this.AssociationTypeComposite) = this.AddManyToOneRelation(m.AssociationTypeComposite);

        (_, this.CompositeDirectSupertypes) = this.AddManyToManyRelation(m.CompositeDirectSupertypes);
        (_, this.CompositeSupertypes) = this.AddManyToManyRelation(m.CompositeSupertypes);

        (_, this.DecimalRoleTypeAssignedPrecision) = this.AddIntegerRelation(m.DecimalRoleTypeAssignedPrecision);
        (_, this.DecimalRoleTypeDerivedPrecision) = this.AddIntegerRelation(m.DecimalRoleTypeDerivedPrecision);
        (_, this.DecimalRoleTypeAssignedScale) = this.AddIntegerRelation(m.DecimalRoleTypeAssignedScale);
        (_, this.DecimalRoleTypeDerivedScale) = this.AddIntegerRelation(m.DecimalRoleTypeDerivedScale);

        (_, this.DomainName) = this.AddStringRelation(m.DomainName);
        (_, this.DomainTypes) = this.AddManyToManyRelation(m.DomainTypes);

        (_, this.InheritanceSubtype) = this.AddManyToOneRelation(m.InheritanceSubtype);
        (_, this.InheritanceSupertype) = this.AddManyToOneRelation(m.InheritanceSupertype);

        (_, this.ObjectTypeAssignedPluralName) = this.AddStringRelation(m.ObjectTypeAssignedPluralName);
        (_, this.ObjectTypeDerivedPluralName) = this.AddStringRelation(m.ObjectTypeDerivedPluralName);
        (_, this.ObjectTypeSingularName) = this.AddStringRelation(m.ObjectTypeSingularName);

        (_, this.MetaObjectId) = this.AddUniqueRelation(m.MetaObjectId);

        (_, this.RelationEndTypeIsMany) = this.AddBooleanRelation(m.RelationEndTypeIsMany);

        (_, this.RoleTypeAssociationType) = this.AddOneToOneRelation(m.RoleTypeAssociationType);
        (_, this.RoleTypeAssignedPluralName) = this.AddStringRelation(m.RoleTypeAssignedPluralName);
        (_, this.RoleTypeDerivedPluralName) = this.AddStringRelation(m.RoleTypeDerivedPluralName);
        (_, this.RoleTypeObjectType) = this.AddManyToOneRelation(m.RoleTypeObjectType);
        (_, this.RoleTypeName) = this.AddStringRelation(m.RoleTypeName);
        (_, this.RoleTypeSingularName) = this.AddStringRelation(m.RoleTypeSingularName);

        (_, this.StringRoleTypeAssignedSize) = this.AddIntegerRelation(m.StringRoleTypeAssignedSize);
        (_, this.StringRoleTypeDerivedSize) = this.AddIntegerRelation(m.StringRoleTypeDerivedSize);

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
    /// An association type.
    /// </summary>
    public Interface AssociationType { get; init; }

    /// <summary>
    /// The composite of an association type.
    /// </summary>
    public ManyToOneRoleType AssociationTypeComposite { get; init; }

    /// <summary>
    /// A class.
    /// </summary>
    public Class Class { get; init; }

    /// <summary>
    /// A composite.
    /// </summary>
    public Interface Composite { get; init; }

    /// <summary>
    /// Composite association type.
    /// </summary>
    public Interface CompositeAssociationType { get; set; }

    /// <summary>
    /// Composite role type.
    /// </summary>
    public Interface CompositeRoleType { get; set; }

    /// <summary>
    /// The supertypes of a composite.
    /// </summary>
    public ManyToManyRoleType CompositeSupertypes { get; init; }

    /// <summary>
    /// The direct supertypes of a composite.
    /// </summary>
    public ManyToManyRoleType CompositeDirectSupertypes { get; init; }

    /// <summary>
    /// A domain.
    /// </summary>
    public Class Domain { get; init; }

    /// <summary>
    /// The name of a domain.
    /// </summary>
    public StringRoleType DomainName { get; init; }

    /// <summary>
    /// The types of a domain.
    /// </summary>
    public ManyToManyRoleType DomainTypes { get; init; }

    /// <summary>
    /// An inheritance.
    /// </summary>
    public Class Inheritance { get; init; }

    /// <summary>
    /// The subtype of an inheritance.
    /// </summary>
    public ManyToOneRoleType InheritanceSubtype { get; init; }

    /// <summary>
    /// The supertype of an inheritance.
    /// </summary>
    public ManyToOneRoleType InheritanceSupertype { get; init; }

    /// <summary>
    /// An interface.
    /// </summary>
    public Class Interface { get; init; }

    /// <summary>
    /// Many to association type
    /// </summary>
    public Interface ManyToAssociationType { get; set; }

    /// <summary>
    /// Many to many association type.
    /// </summary>
    public Class ManyToManyAssociationType { get; set; }

    /// <summary>
    /// Many to many role type.
    /// </summary>
    public Class ManyToManyRoleType { get; set; }

    /// <summary>
    /// Many to one association type.
    /// </summary>
    public Class ManyToOneAssociationType { get; set; }

    /// <summary>
    /// Many to one role type.
    /// </summary>
    public Class ManyToOneRoleType { get; set; }

    /// <summary>
    /// An object type.
    /// </summary>
    public Interface MetaObject { get; init; }

    /// <summary>
    /// The id of the meta object.
    /// </summary>
    public UniqueRoleType MetaObjectId { get; }

    /// <summary>
    /// An object type.
    /// </summary>
    public Interface ObjectType { get; init; }

    /// <summary>
    /// The assigned plural name of an object type.
    /// </summary>
    public StringRoleType ObjectTypeAssignedPluralName { get; init; }

    /// <summary>
    /// The derived plural name of an object type.
    /// </summary>
    public StringRoleType ObjectTypeDerivedPluralName { get; init; }

    /// <summary>
    /// The singular name of an object type.
    /// </summary>
    public StringRoleType ObjectTypeSingularName { get; init; }

    /// <summary>
    /// One to association type.
    /// </summary>
    public Interface OneToAssociationType { get; set; }

    /// <summary>
    /// One to many association type.
    /// </summary>
    public Class OneToManyAssociationType { get; set; }

    /// <summary>
    /// One to many role type.
    /// </summary>
    public Class OneToManyRoleType { get; set; }

    /// <summary>
    /// One to one association type.
    /// </summary>
    public Class OneToOneAssociationType { get; set; }

    /// <summary>
    /// One to one role type.
    /// </summary>
    public Class OneToOneRoleType { get; set; }

    /// <summary>
    /// An operand type.
    /// </summary>public ObjectType OperandType { get; init; }
    public Interface OperandType { get; init; }

    /// <summary>
    /// A relation end type.
    /// </summary>public ObjectType OperandType { get; init; }
    public Interface RelationEndType { get; init; }

    /// <summary>
    /// The is many of a role type.
    /// </summary>
    public BooleanRoleType RelationEndTypeIsMany { get; }

    /// <summary>
    /// A role type.
    /// </summary>
    public Interface RoleType { get; init; }

    /// <summary>
    /// The association type a role type.
    /// </summary>
    public OneToOneRoleType RoleTypeAssociationType { get; init; }

    /// <summary>
    /// The assigned plural name of a role type.
    /// </summary>
    public StringRoleType RoleTypeAssignedPluralName { get; init; }

    /// <summary>
    /// The derived plural name of a role type.
    /// </summary>
    public StringRoleType RoleTypeDerivedPluralName { get; init; }

    /// <summary>
    /// The role type of object type.
    /// </summary>
    public ManyToOneRoleType RoleTypeObjectType { get; init; }

    /// <summary>
    /// The name of role type.
    /// </summary>
    public StringRoleType RoleTypeName { get; init; }

    /// <summary>
    /// The singular name of role type.
    /// </summary>
    public StringRoleType RoleTypeSingularName { get; init; }

    /// <summary>
    /// The binary association type.
    /// </summary>
    public Class BinaryAssociationType { get; set; }

    /// <summary>
    /// The binary role type.
    /// </summary>
    public Class BinaryRoleType { get; set; }

    /// <summary>
    /// The boolean association type.
    /// </summary>
    public Class BooleanAssociationType { get; set; }

    /// <summary>
    /// The boolean role type.
    /// </summary>
    public Class BooleanRoleType { get; set; }

    /// <summary>
    /// The dateTime association type.
    /// </summary>
    public Class DateTimeAssociationType { get; set; }

    /// <summary>
    /// The dateTime role type.
    /// </summary>
    public Class DateTimeRoleType { get; set; }

    /// <summary>
    /// The decimal association type.
    /// </summary>
    public Class DecimalAssociationType { get; set; }

    /// <summary>
    /// The decimal role type.
    /// </summary>
    public Class DecimalRoleType { get; set; }

    /// <summary>
    /// The precision of the decimal role type.
    /// </summary>
    public IntegerRoleType DecimalRoleTypeAssignedPrecision { get; init; }

    /// <summary>
    /// The derived precision of the decimal role type.
    /// </summary>
    public IntegerRoleType DecimalRoleTypeDerivedPrecision { get; init; }

    /// <summary>
    /// The assigned scale of the decimal role type.
    /// </summary>
    public IntegerRoleType DecimalRoleTypeAssignedScale { get; init; }

    /// <summary>
    /// The derived scale of the decimal role type.
    /// </summary>
    public IntegerRoleType DecimalRoleTypeDerivedScale { get; init; }

    /// <summary>
    /// The float association type.
    /// </summary>
    public Class FloatAssociationType { get; set; }

    /// <summary>
    /// The float role type.
    /// </summary>
    public Class FloatRoleType { get; set; }

    /// <summary>
    /// The integer association type.
    /// </summary>
    public Class IntegerAssociationType { get; set; }

    /// <summary>
    /// The integer role type.
    /// </summary>
    public Class IntegerRoleType { get; set; }

    /// <summary>
    /// The string association type.
    /// </summary>
    public Class StringAssociationType { get; set; }

    /// <summary>
    /// The string role type.
    /// </summary>
    public Class StringRoleType { get; set; }

    /// <summary>
    /// The assigned size of the string role type.
    /// </summary>
    public IntegerRoleType StringRoleTypeAssignedSize { get; init; }

    /// <summary>
    /// The derived size of the string role type.
    /// </summary>
    public IntegerRoleType StringRoleTypeDerivedSize { get; init; }

    /// <summary>
    /// The unique association type.
    /// </summary>
    public Class UniqueAssociationType { get; set; }

    /// <summary>
    /// The unique role type.
    /// </summary>
    public Class UniqueRoleType { get; set; }

    /// <summary>
    /// To many role type.
    /// </summary>
    public Interface ToManyRoleType { get; set; }

    /// <summary>
    /// To one role type.
    /// </summary>
    public Interface ToOneRoleType { get; set; }

    /// <summary>
    /// A type.
    /// </summary>
    public Interface Type { get; init; }

    /// <summary>
    /// A unit.
    /// </summary>
    public Class Unit { get; init; }

    /// <summary>
    /// Unit association type.
    /// </summary>
    public Interface UnitAssociationType { get; set; }

    /// <summary>
    /// Unit role type.
    /// </summary>
    public Interface UnitRoleType { get; set; }

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
    /// Looks up a meta object by id
    /// </summary>
    public IMetaObject this[Guid id]
    {
        get
        {
            // TODO: Add optimizaiton after freeze
            return this.MetaPopulation.Objects.First(v => ((Guid)v[this.Meta.MetaObjectId]!) == id);
        }
    }

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
    public Interface AddInterface(Domain.Domain domain, MetaObjectType metaObjectType) => this.AddInterface(domain, metaObjectType.Id, metaObjectType.Name);

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
    public Class AddClass(Domain.Domain domain, MetaObjectType metaObjectType) => this.AddClass(domain, metaObjectType.Id, metaObjectType.Name);

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
    public (BinaryAssociationType AssociationType, BinaryRoleType RoleType) AddBinaryRelation(Domain.Domain domain, MetaUnitRoleType metaUnitRoleType)
        => this.AddBinaryRelation(domain, metaUnitRoleType.AssociationType.Id, metaUnitRoleType.Id, (IComposite)this[metaUnitRoleType.AssociationType.ObjectType.Id], (Unit)this[metaUnitRoleType.ObjectType.Id], metaUnitRoleType.Name);

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
    public (BooleanAssociationType AssociationType, BooleanRoleType RoleType) AddBooleanRelation(Domain.Domain domain, MetaUnitRoleType metaUnitRoleType)
        => this.AddBooleanRelation(domain, metaUnitRoleType.AssociationType.Id, metaUnitRoleType.Id, (IComposite)this[metaUnitRoleType.AssociationType.ObjectType.Id], (Unit)this[metaUnitRoleType.ObjectType.Id], metaUnitRoleType.Name);

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
    public (DateTimeAssociationType AssociationType, DateTimeRoleType RoleType) AddDateTimeRelation(Domain.Domain domain, MetaUnitRoleType metaUnitRoleType)
        => this.AddDateTimeRelation(domain, metaUnitRoleType.AssociationType.Id, metaUnitRoleType.Id, (IComposite)this[metaUnitRoleType.AssociationType.ObjectType.Id], (Unit)this[metaUnitRoleType.ObjectType.Id], metaUnitRoleType.Name);

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
    public (DecimalAssociationType AssociationType, DecimalRoleType RoleType) AddDecimalRelation(Domain.Domain domain, MetaUnitRoleType metaUnitRoleType)
        => this.AddDecimalRelation(domain, metaUnitRoleType.AssociationType.Id, metaUnitRoleType.Id, (IComposite)this[metaUnitRoleType.AssociationType.ObjectType.Id], (Unit)this[metaUnitRoleType.ObjectType.Id], metaUnitRoleType.Name);

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
    public (FloatAssociationType AssociationType, FloatRoleType RoleType) AddFloatRelation(Domain.Domain domain, MetaUnitRoleType metaUnitRoleType)
        => this.AddFloatRelation(domain, metaUnitRoleType.AssociationType.Id, metaUnitRoleType.Id, (IComposite)this[metaUnitRoleType.AssociationType.ObjectType.Id], (Unit)this[metaUnitRoleType.ObjectType.Id], metaUnitRoleType.Name);

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
    public (IntegerAssociationType AssociationType, IntegerRoleType RoleType) AddIntegerRelation(Domain.Domain domain, MetaUnitRoleType metaUnitRoleType)
        => this.AddIntegerRelation(domain, metaUnitRoleType.AssociationType.Id, metaUnitRoleType.Id, (IComposite)this[metaUnitRoleType.AssociationType.ObjectType.Id], (Unit)this[metaUnitRoleType.ObjectType.Id], metaUnitRoleType.Name);

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
    public (StringAssociationType AssociationType, StringRoleType RoleType) AddStringRelation(Domain.Domain domain, MetaUnitRoleType metaUnitRoleType)
        => this.AddStringRelation(domain, metaUnitRoleType.AssociationType.Id, metaUnitRoleType.Id, (IComposite)this[metaUnitRoleType.AssociationType.ObjectType.Id], (Unit)this[metaUnitRoleType.ObjectType.Id], metaUnitRoleType.Name);

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
    public (UniqueAssociationType AssociationType, UniqueRoleType RoleType) AddUniqueRelation(Domain.Domain domain, MetaUnitRoleType metaUnitRoleType)
        => this.AddUniqueRelation(domain, metaUnitRoleType.AssociationType.Id, metaUnitRoleType.Id, (IComposite)this[metaUnitRoleType.AssociationType.ObjectType.Id], (Unit)this[metaUnitRoleType.ObjectType.Id], metaUnitRoleType.Name);

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
    /// Creates new onetoone relation end types.
    /// </summary>
    public (OneToOneAssociationType AssociationType, OneToOneRoleType RoleType) AddOneToOneRelation(Domain.Domain domain, MetaOneToOneRoleType metaOneToOneRoleType)
        => this.AddOneToOneRelation(domain, metaOneToOneRoleType.AssociationType.Id, metaOneToOneRoleType.Id, (IComposite)this[metaOneToOneRoleType.AssociationType.ObjectType.Id], (IComposite)this[metaOneToOneRoleType.ObjectType.Id], metaOneToOneRoleType.Name);

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
    /// Creates new manytoone relation end types.
    /// </summary>
    public (ManyToOneAssociationType AssociationType, ManyToOneRoleType RoleType) AddManyToOneRelation(Domain.Domain domain, MetaManyToOneRoleType metaManyToOneRoleType)
        => this.AddManyToOneRelation(domain, metaManyToOneRoleType.AssociationType.Id, metaManyToOneRoleType.Id, (IComposite)this[metaManyToOneRoleType.AssociationType.ObjectType.Id], (IComposite)this[metaManyToOneRoleType.ObjectType.Id], metaManyToOneRoleType.Name);

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
    /// Creates new onetomany relation end types.
    /// </summary>
    public (OneToManyAssociationType AssociationType, OneToManyRoleType RoleType) AddOneToManyRelation(Domain.Domain domain, MetaOneToManyRoleType metaOneToManyRoleType)
        => this.AddOneToManyRelation(domain, metaOneToManyRoleType.AssociationType.Id, metaOneToManyRoleType.Id, (IComposite)this[metaOneToManyRoleType.AssociationType.ObjectType.Id], (IComposite)this[metaOneToManyRoleType.ObjectType.Id], metaOneToManyRoleType.Name);

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
    /// Creates new manytomany relation end types.
    /// </summary>
    public (ManyToManyAssociationType AssociationType, ManyToManyRoleType RoleType) AddManyToManyRelation(Domain.Domain domain, MetaManyToManyRoleType metaManyToManyRoleType)
        => this.AddManyToManyRelation(domain, metaManyToManyRoleType.AssociationType.Id, metaManyToManyRoleType.Id, (IComposite)this[metaManyToManyRoleType.AssociationType.ObjectType.Id], (IComposite)this[metaManyToManyRoleType.ObjectType.Id], metaManyToManyRoleType.Name);

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

    internal Interface AddInterface(MetaObjectType metaObjectType) => this.AddInterface(this.AllorsCore, metaObjectType);

    internal Interface AddInterface(Guid id, string singularName, string? assignedPluralName = null) => this.AddInterface(this.AllorsCore, id, singularName, assignedPluralName);

    internal Class AddClass(MetaObjectType metaObjectType) => this.AddClass(this.AllorsCore, metaObjectType);

    internal Class AddClass(Guid id, string singularName, string? assignedPluralName = null) => this.AddClass(this.AllorsCore, id, singularName, assignedPluralName);

    internal (BinaryAssociationType AssociationType, BinaryRoleType RoleType) AddBinaryRelation(MetaUnitRoleType metaUnitRoleType)
        => this.AddBinaryRelation(this.AllorsCore, metaUnitRoleType);

    internal (BinaryAssociationType AssociationType, BinaryRoleType RoleType) AddBinaryRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
        => this.AddBinaryRelation(this.AllorsCore, associationTypeId, roleTypeId, associationComposite, unit, singularName, assignedPluralName);

    internal (BooleanAssociationType AssociationType, BooleanRoleType RoleType) AddBooleanRelation(MetaUnitRoleType metaUnitRoleType)
     => this.AddBooleanRelation(this.AllorsCore, metaUnitRoleType);

    internal (BooleanAssociationType AssociationType, BooleanRoleType RoleType) AddBooleanRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
        => this.AddBooleanRelation(this.AllorsCore, associationTypeId, roleTypeId, associationComposite, unit, singularName, assignedPluralName);

    internal (DateTimeAssociationType AssociationType, DateTimeRoleType RoleType) AddDateTimeRelation(MetaUnitRoleType metaUnitRoleType)
     => this.AddDateTimeRelation(this.AllorsCore, metaUnitRoleType);

    internal (DateTimeAssociationType AssociationType, DateTimeRoleType RoleType) AddDateTimeRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
        => this.AddDateTimeRelation(this.AllorsCore, associationTypeId, roleTypeId, associationComposite, unit, singularName, assignedPluralName);

    internal (DecimalAssociationType AssociationType, DecimalRoleType RoleType) AddDecimalRelation(MetaUnitRoleType metaUnitRoleType)
     => this.AddDecimalRelation(this.AllorsCore, metaUnitRoleType);

    internal (DecimalAssociationType AssociationType, DecimalRoleType RoleType) AddDecimalRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
        => this.AddDecimalRelation(this.AllorsCore, associationTypeId, roleTypeId, associationComposite, unit, singularName, assignedPluralName);

    internal (FloatAssociationType AssociationType, FloatRoleType RoleType) AddFloatRelation(MetaUnitRoleType metaUnitRoleType)
     => this.AddFloatRelation(this.AllorsCore, metaUnitRoleType);

    internal (FloatAssociationType AssociationType, FloatRoleType RoleType) AddFloatRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
        => this.AddFloatRelation(this.AllorsCore, associationTypeId, roleTypeId, associationComposite, unit, singularName, assignedPluralName);

    internal (IntegerAssociationType AssociationType, IntegerRoleType RoleType) AddIntegerRelation(MetaUnitRoleType metaUnitRoleType)
     => this.AddIntegerRelation(this.AllorsCore, metaUnitRoleType);

    internal (IntegerAssociationType AssociationType, IntegerRoleType RoleType) AddIntegerRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
        => this.AddIntegerRelation(this.AllorsCore, associationTypeId, roleTypeId, associationComposite, unit, singularName, assignedPluralName);

    internal (StringAssociationType AssociationType, StringRoleType RoleType) AddStringRelation(MetaUnitRoleType metaUnitRoleType)
        => this.AddStringRelation(this.AllorsCore, metaUnitRoleType);

    internal (StringAssociationType AssociationType, StringRoleType RoleType) AddStringRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
        => this.AddStringRelation(this.AllorsCore, associationTypeId, roleTypeId, associationComposite, unit, singularName, assignedPluralName);

    internal (UniqueAssociationType AssociationType, UniqueRoleType RoleType) AddUniqueRelation(MetaUnitRoleType metaUnitRoleType)
     => this.AddUniqueRelation(this.AllorsCore, metaUnitRoleType);

    internal (UniqueAssociationType AssociationType, UniqueRoleType RoleType) AddUniqueRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
        => this.AddUniqueRelation(this.AllorsCore, associationTypeId, roleTypeId, associationComposite, unit, singularName, assignedPluralName);

    internal (OneToOneAssociationType AssociationType, OneToOneRoleType RoleType) AddOneToOneRelation(MetaOneToOneRoleType metaOneToOneRoleType)
     => this.AddOneToOneRelation(this.AllorsCore, metaOneToOneRoleType);

    internal (OneToOneAssociationType AssociationType, OneToOneRoleType RoleType) AddOneToOneRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string singularName, string? assignedPluralName = null)
        => this.AddOneToOneRelation(this.AllorsCore, associationTypeId, roleTypeId, associationComposite, roleComposite, singularName, assignedPluralName);

    internal (ManyToOneAssociationType AssociationType, ManyToOneRoleType RoleType) AddManyToOneRelation(MetaManyToOneRoleType metaManyToOneRoleType)
     => this.AddManyToOneRelation(this.AllorsCore, metaManyToOneRoleType);

    internal (ManyToOneAssociationType AssociationType, ManyToOneRoleType RoleType) AddManyToOneRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string singularName, string? assignedPluralName = null)
        => this.AddManyToOneRelation(this.AllorsCore, associationTypeId, roleTypeId, associationComposite, roleComposite, singularName, assignedPluralName);

    internal (OneToManyAssociationType AssociationType, OneToManyRoleType RoleType) AddOneToManyRelation(MetaOneToManyRoleType metaOneToManyRoleType)
     => this.AddOneToManyRelation(this.AllorsCore, metaOneToManyRoleType);

    internal (OneToManyAssociationType AssociationType, OneToManyRoleType RoleType) AddOneToManyRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string singularName, string? assignedPluralName = null)
        => this.AddOneToManyRelation(this.AllorsCore, associationTypeId, roleTypeId, associationComposite, roleComposite, singularName, assignedPluralName);

    internal (ManyToManyAssociationType AssociationType, ManyToManyRoleType RoleType) AddManyToManyRelation(MetaManyToManyRoleType metaManyToManyRoleType)
     => this.AddManyToManyRelation(this.AllorsCore, metaManyToManyRoleType);

    internal (ManyToManyAssociationType AssociationType, ManyToManyRoleType RoleType) AddManyToManyRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string singularName, string? assignedPluralName = null)
        => this.AddManyToManyRelation(this.AllorsCore, associationTypeId, roleTypeId, associationComposite, roleComposite, singularName, assignedPluralName);
}
