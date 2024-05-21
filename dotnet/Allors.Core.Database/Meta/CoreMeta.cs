namespace Allors.Core.Database.Meta
{
    using System;
    using System.Collections.Frozen;
    using System.Collections.Generic;
    using Allors.Core.Database.Meta.Domain;
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// Core Meta.
    /// </summary>
    public sealed class CoreMeta
    {
        private IDictionary<Guid, IMetaObject> metaObjectById;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreMeta"/> class.
        /// </summary>
        public CoreMeta()
        {
            this.IsFrozen = false;

            this.Meta = new CoreMetaMeta();

            this.metaObjectById = new Dictionary<Guid, IMetaObject>();

            this.MetaPopulation = this.Meta.CreateMetaPopulation();

            this.Object = this.NewInterface(new Guid("8904EE32-CF11-4019-9FD7-FB9631F9ACAC"), "Object");
            this.String = this.NewUnit(new Guid("58BB7632-4724-4F92-869B-B30D7A7BEE9E"), "String");
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
        /// The String unit.
        /// </summary>
        public Unit String { get; init; }

        /// <summary>
        /// Gets meta object by meta handle.
        /// </summary>
        public Core.Meta.Domain.IMetaObject this[Guid id] => this.metaObjectById[id];

        /// <summary>
        /// Add a new meta object.
        /// </summary>
        public void Register(IMetaObject metaObject)
        {
            var id = (Guid)(metaObject[this.Meta.MetaObjectId] ?? throw new InvalidOperationException());
            this.metaObjectById.Add(id, metaObject);
        }

        /// <summary>
        /// Freezes meta.
        /// </summary>
        public void Freeze()
        {
            if (!this.IsFrozen)
            {
                this.IsFrozen = true;

                // TODO: Add freeze to Allors.Meta
                // this.MetaMeta.Freeze();
                // this.MetaPopulation.Freeze();
                this.metaObjectById = this.metaObjectById.ToFrozenDictionary();
            }
        }

        /// <summary>
        /// Creates a new unit.
        /// </summary>
        public Unit NewUnit(Guid id, string singularName, string? assignedPluralName = null)
        {
            var m = this.Meta;

            var unit = this.MetaPopulation.Build<Unit>(v =>
            {
                v[m.MetaObjectId] = id;
                v[m.ObjectTypeSingularName] = singularName;
                v[m.ObjectTypeAssignedPluralName] = assignedPluralName;
            });

            this.Register(unit);

            return unit;
        }

        /// <summary>
        /// Creates a new interface.
        /// </summary>
        public Interface NewInterface(Guid id, string singularName, string? assignedPluralName = null)
        {
            var m = this.Meta;

            var @interface = this.MetaPopulation.Build<Interface>(v =>
            {
                v[m.MetaObjectId] = id;
                v[m.ObjectTypeSingularName] = singularName;
                v[m.ObjectTypeAssignedPluralName] = assignedPluralName;
            });

            this.Register(@interface);

            return @interface;
        }

        /// <summary>
        /// Creates a new class.
        /// </summary>
        public Class NewClass(Guid id, string singularName, string? assignedPluralName = null)
        {
            var m = this.Meta;

            var @class = this.MetaPopulation.Build<Class>(v =>
            {
                v[m.MetaObjectId] = id;
                v[m.ObjectTypeSingularName] = singularName;
                v[m.ObjectTypeAssignedPluralName] = assignedPluralName;
            });

            this.Register(@class);

            return @class;
        }

        /// <summary>
        /// Creates new unit relation end types.
        /// </summary>
        public (UnitAssociationType AssociationType, UnitRoleType RoleType) NewUnitRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, Unit unit, string singularName, string? assignedPluralName = null)
        {
            var m = this.Meta;

            var associationType = this.MetaPopulation.Build<UnitAssociationType>(v =>
            {
                v[m.MetaObjectId] = associationTypeId;
                v[m.AssociationTypeComposite] = associationComposite;
            });

            this.Register(associationType);

            var roleType = this.MetaPopulation.Build<UnitRoleType>(v =>
            {
                v[m.MetaObjectId] = roleTypeId;
                v[m.RoleTypeAssociationType] = associationType;
                v[m.RoleTypeObjectType] = unit;
                v[m.RoleTypeSingularName] = singularName;
                v[m.RoleTypeAssignedPluralName] = assignedPluralName;
            });

            this.Register(roleType);

            return (associationType, roleType);
        }

        /// <summary>
        /// Creates new OneToOne relation end types.
        /// </summary>
        public (OneToOneAssociationType AssociationType, OneToOneRoleType RoleType) NewOneToOneRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string singularName, string? assignedPluralName = null)
        {
            var m = this.Meta;

            var associationType = this.MetaPopulation.Build<OneToOneAssociationType>(v =>
            {
                v[m.MetaObjectId] = associationTypeId;
                v[m.AssociationTypeComposite] = associationComposite;
            });

            this.Register(associationType);

            var roleType = this.MetaPopulation.Build<OneToOneRoleType>(v =>
            {
                v[m.MetaObjectId] = roleTypeId;
                v[m.RoleTypeAssociationType] = associationType;
                v[m.RoleTypeObjectType] = roleComposite;
                v[m.RoleTypeSingularName] = singularName;
                v[m.RoleTypeAssignedPluralName] = assignedPluralName;
            });

            this.Register(roleType);

            return (associationType, roleType);
        }

        /// <summary>
        /// Creates new ManyToOne relation end types.
        /// </summary>
        public (ManyToOneAssociationType AssociationType, ManyToOneRoleType RoleType) NewManyToOneRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string singularName, string? assignedPluralName = null)
        {
            var m = this.Meta;

            var associationType = this.MetaPopulation.Build<ManyToOneAssociationType>(v =>
            {
                v[m.MetaObjectId] = associationTypeId;
                v[m.AssociationTypeComposite] = associationComposite;
            });

            this.Register(associationType);

            var roleType = this.MetaPopulation.Build<ManyToOneRoleType>(v =>
            {
                v[m.MetaObjectId] = roleTypeId;
                v[m.RoleTypeAssociationType] = associationType;
                v[m.RoleTypeObjectType] = roleComposite;
                v[m.RoleTypeSingularName] = singularName;
                v[m.RoleTypeAssignedPluralName] = assignedPluralName;
            });

            this.Register(roleType);

            return (associationType, roleType);
        }

        /// <summary>
        /// Creates new OneToMany relation end types.
        /// </summary>
        public (OneToManyAssociationType AssociationType, OneToManyRoleType RoleType) NewOneToManyRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string singularName, string? assignedPluralName = null)
        {
            var m = this.Meta;

            var associationType = this.MetaPopulation.Build<OneToManyAssociationType>(v =>
            {
                v[m.MetaObjectId] = associationTypeId;
                v[m.AssociationTypeComposite] = associationComposite;
            });

            this.Register(associationType);

            var roleType = this.MetaPopulation.Build<OneToManyRoleType>(v =>
            {
                v[m.MetaObjectId] = roleTypeId;
                v[m.RoleTypeAssociationType] = associationType;
                v[m.RoleTypeObjectType] = roleComposite;
                v[m.RoleTypeSingularName] = singularName;
                v[m.RoleTypeAssignedPluralName] = assignedPluralName;
            });

            this.Register(roleType);

            return (associationType, roleType);
        }

        /// <summary>
        /// Creates new ManyToMany relation end types.
        /// </summary>
        public (ManyToManyAssociationType AssociationType, ManyToManyRoleType RoleType) NewManyToManyRelation(Guid associationTypeId, Guid roleTypeId, IComposite associationComposite, IComposite roleComposite, string singularName, string? assignedPluralName = null)
        {
            var m = this.Meta;

            var associationType = this.MetaPopulation.Build<ManyToManyAssociationType>(v =>
            {
                v[m.MetaObjectId] = associationTypeId;
                v[m.AssociationTypeComposite] = associationComposite;
            });

            this.Register(associationType);

            var roleType = this.MetaPopulation.Build<ManyToManyRoleType>(v =>
            {
                v[m.MetaObjectId] = roleTypeId;
                v[m.RoleTypeAssociationType] = associationType;
                v[m.RoleTypeObjectType] = roleComposite;
                v[m.RoleTypeSingularName] = singularName;
                v[m.RoleTypeAssignedPluralName] = assignedPluralName;
            });

            this.Register(roleType);

            return (associationType, roleType);
        }

        /// <summary>
        /// Subtype implements supertype.
        /// </summary>
        public void AddDirectSupertype(IComposite subtype, Interface supertype)
        {
            subtype.Add(this.Meta.CompositeDirectSupertypes, supertype);
        }
    }
}
