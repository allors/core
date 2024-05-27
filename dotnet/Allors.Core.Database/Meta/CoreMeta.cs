namespace Allors.Core.Database.Meta;

using System;
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

        this.Object = this.AddInterface(new Guid("8904EE32-CF11-4019-9FD7-FB9631F9ACAC"), "Object");
        this.Boolean = this.AddUnit(new Guid("8906C53F-62CF-41C8-B95D-2084A74AC233"), "Boolean");
        this.Decimal = this.AddUnit(new Guid("083579BF-2E72-48CD-B491-37D776C70F44"), "Decimal");
        this.Double = this.AddUnit(new Guid("D81420B6-6773-4D94-8BA0-805418327612"), "Double");
        this.Integer = this.AddUnit(new Guid("66E81092-0903-4DE4-9741-9968BC94D68E"), "Integer");
        this.String = this.AddUnit(new Guid("58BB7632-4724-4F92-869B-B30D7A7BEE9E"), "String");
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
    /// Creates a new unit.
    /// </summary>
    public Unit AddUnit(Guid id, string singularName, string? assignedPluralName = null)
    {
        var m = this.Meta;

        var unit = this.MetaPopulation.Build<Unit>(v =>
        {
            v[m.MetaObjectId] = id;
            v[m.ObjectTypeSingularName] = singularName;
            v[m.ObjectTypeAssignedPluralName] = assignedPluralName;
        });

        return unit;
    }

    /// <summary>
    /// Creates a new interface.
    /// </summary>
    public Interface AddInterface(Guid id, string singularName, string? assignedPluralName = null)
    {
        var m = this.Meta;

        var @interface = this.MetaPopulation.Build<Interface>(v =>
        {
            v[m.MetaObjectId] = id;
            v[m.ObjectTypeSingularName] = singularName;
            v[m.ObjectTypeAssignedPluralName] = assignedPluralName;
        });

        return @interface;
    }

    /// <summary>
    /// Creates a new class.
    /// </summary>
    public Class AddClass(Guid id, string singularName, string? assignedPluralName = null)
    {
        var m = this.Meta;

        var @class = this.MetaPopulation.Build<Class>(v =>
        {
            v[m.MetaObjectId] = id;
            v[m.ObjectTypeSingularName] = singularName;
            v[m.ObjectTypeAssignedPluralName] = assignedPluralName;
        });

        return @class;
    }

    /// <summary>
    /// Creates new unit relation end types.
    /// </summary>
    public (StringAssociationType AssociationType, StringRoleType RoleType) AddStringRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
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

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new OneToOne relation end types.
    /// </summary>
    public (OneToOneAssociationType AssociationType, OneToOneRoleType RoleType) AddOneToOneRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string singularName, string? assignedPluralName = null)
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

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new ManyToOne relation end types.
    /// </summary>
    public (ManyToOneAssociationType AssociationType, ManyToOneRoleType RoleType) AddManyToOneRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string singularName, string? assignedPluralName = null)
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

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new OneToMany relation end types.
    /// </summary>
    public (OneToManyAssociationType AssociationType, OneToManyRoleType RoleType) AddOneToManyRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string singularName, string? assignedPluralName = null)
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

        return (associationType, roleType);
    }

    /// <summary>
    /// Creates new ManyToMany relation end types.
    /// </summary>
    public (ManyToManyAssociationType AssociationType, ManyToManyRoleType RoleType) AddManyToManyRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string singularName, string? assignedPluralName = null)
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

        return (associationType, roleType);
    }

    /// <summary>
    /// Subtype implements supertype.
    /// </summary>
    public void AddDirectSupertype(IComposite subtype, params Interface[] supertypes)
    {
        foreach (var supertype in supertypes)
        {
            subtype.Add(this.Meta.CompositeDirectSupertypes, supertype);
        }
    }
}
