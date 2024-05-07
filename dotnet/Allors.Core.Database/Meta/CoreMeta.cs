namespace Allors.Core.Database.Meta
{
    using System;
    using System.Collections.Frozen;
    using System.Collections.Generic;
    using Allors.Core.Database.Meta.Handles;
    using Allors.Embedded.Domain;
    using Allors.Embedded.Meta;

    /// <summary>
    /// Core Meta.
    /// </summary>
    public sealed class CoreMeta
    {
        private IDictionary<EmbeddedObject, MetaHandle> metaHandleByMetaObject;

        private IDictionary<MetaHandle, EmbeddedObject> metaObjectByMetaHandle;

        private IDictionary<Guid, EmbeddedObject> metaObjectById;

        private MetaHandle[]? metaHandles;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreMeta"/> class.
        /// </summary>
        public CoreMeta()
        {
            this.IsFrozen = false;

            this.Meta = new CoreMetaMeta();

            this.metaHandleByMetaObject = new Dictionary<EmbeddedObject, MetaHandle>();
            this.metaObjectByMetaHandle = new Dictionary<MetaHandle, EmbeddedObject>();
            this.metaObjectById = new Dictionary<Guid, EmbeddedObject>();
            this.metaHandles = null;

            this.EmbeddedPopulation = new EmbeddedPopulation();

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
        /// The embedded population.
        /// </summary>
        public EmbeddedPopulation EmbeddedPopulation { get; init; }

        /// <summary>
        /// The Object interface.
        /// </summary>
        public InterfaceHandle Object { get; init; }

        /// <summary>
        /// The String unit.
        /// </summary>
        public UnitHandle String { get; init; }

        /// <summary>
        /// Gets meta object by meta handle.
        /// </summary>
        public IEnumerable<MetaHandle> MetaHandles => this.metaHandles ??= [.. this.metaObjectByMetaHandle.Keys];

        /// <summary>
        /// Gets meta handle by meta object.
        /// </summary>
        public MetaHandle this[EmbeddedObject metaObject] => this.metaHandleByMetaObject[metaObject];

        /// <summary>
        /// Gets meta object by meta handle.
        /// </summary>
        public EmbeddedObject this[MetaHandle metaHandle] => this.metaObjectByMetaHandle[metaHandle];

        /// <summary>
        /// Gets meta object by meta handle.
        /// </summary>
        public EmbeddedObject this[Guid id] => this.metaObjectById[id];

        /// <summary>
        /// Add a new meta object.
        /// </summary>
        public void Register(MetaHandle metaHandle, EmbeddedObject embeddedObject)
        {
            this.metaHandleByMetaObject.Add(embeddedObject, metaHandle);
            this.metaObjectByMetaHandle.Add(metaHandle, embeddedObject);
            this.metaObjectById.Add(metaHandle.Id, embeddedObject);
            this.metaHandles = null;
        }

        /// <summary>
        /// Freezes meta.
        /// </summary>
        public void Freeze()
        {
            if (!this.IsFrozen)
            {
                this.IsFrozen = true;

                // TODO: Add freeze to Allors.Embedded
                // this.EmbeddedMeta.Freeze();
                // this.EmbeddedPopulation.Freeze();
                this.metaHandleByMetaObject = this.metaHandleByMetaObject.ToFrozenDictionary();
                this.metaObjectByMetaHandle = this.metaObjectByMetaHandle.ToFrozenDictionary();
                this.metaObjectById = this.metaObjectById.ToFrozenDictionary();
            }
        }

        /// <summary>
        /// Creates a new unit.
        /// </summary>
        public UnitHandle NewUnit(Guid id, string singularName, string? assignedPluralName = null)
        {
            var m = this.Meta;

            var unit = this.EmbeddedPopulation.Create(m.Unit, v =>
            {
                v[m.MetaObjectId] = id;
                v[m.ObjectTypeSingularName] = singularName;
                v[m.ObjectTypeAssignedPluralName] = assignedPluralName;
            });

            var unitHandle = new UnitHandle(id);

            this.Register(unitHandle, unit);

            return unitHandle;
        }

        /// <summary>
        /// Creates a new interface.
        /// </summary>
        public InterfaceHandle NewInterface(Guid id, string singularName, string? assignedPluralName = null)
        {
            var m = this.Meta;

            var @interface = this.EmbeddedPopulation.Create(m.Interface, v =>
            {
                v[m.MetaObjectId] = id;
                v[m.ObjectTypeSingularName] = singularName;
                v[m.ObjectTypeAssignedPluralName] = assignedPluralName;
            });

            var interfaceHandle = new InterfaceHandle(id);

            this.Register(interfaceHandle, @interface);

            return interfaceHandle;
        }

        /// <summary>
        /// Creates a new class.
        /// </summary>
        public ClassHandle NewClass(Guid id, string singularName, string? assignedPluralName = null)
        {
            var m = this.Meta;

            var @class = this.EmbeddedPopulation.Create(m.Class, v =>
            {
                v[m.MetaObjectId] = id;
                v[m.ObjectTypeSingularName] = singularName;
                v[m.ObjectTypeAssignedPluralName] = assignedPluralName;
            });

            var classHandle = new ClassHandle(id);

            this.Register(classHandle, @class);

            return classHandle;
        }

        /// <summary>
        /// Creates new unit relation end types.
        /// </summary>
        public (UnitAssociationTypeHandle AssociationType, UnitRoleTypeHandle RoleType) NewUnitRelation(Guid associationTypeId, Guid roleTypeId, CompositeHandle associationCompositeHandle, UnitHandle unitHandle, string singularName, string? assignedPluralName = null)
        {
            var m = this.Meta;

            var associationType = this.EmbeddedPopulation.Create(m.AssociationType, v =>
            {
                v[m.MetaObjectId] = associationTypeId;
                v[m.AssociationTypeComposite] = this[associationCompositeHandle.Id];
            });

            var unitAssociationTypeHandle = new UnitAssociationTypeHandle(associationTypeId);
            this.Register(unitAssociationTypeHandle, associationType);

            var roleType = this.EmbeddedPopulation.Create(m.RoleType, v =>
            {
                v[m.MetaObjectId] = roleTypeId;
                v[m.RoleTypeAssociationType] = associationType;
                v[m.RoleTypeObjectType] = this[unitHandle.Id];
                v[m.RoleTypeSingularName] = singularName;
                v[m.RoleTypeAssignedPluralName] = assignedPluralName;
            });

            var unitRoleTypeHandle = new UnitRoleTypeHandle(roleTypeId);
            this.Register(unitRoleTypeHandle, roleType);

            return (unitAssociationTypeHandle, unitRoleTypeHandle);
        }

        /// <summary>
        /// Creates new OneToOne relation end types.
        /// </summary>
        public (OneToOneAssociationTypeHandle AssociationType, OneToOneRoleTypeHandle RoleType) NewOneToOneRelation(Guid associationTypeId, Guid roleTypeId, CompositeHandle associationCompositeHandle, CompositeHandle roleCompositeHandle, string singularName, string? assignedPluralName = null)
        {
            var m = this.Meta;

            var associationType = this.EmbeddedPopulation.Create(m.AssociationType, v =>
            {
                v[m.MetaObjectId] = associationTypeId;
                v[m.AssociationTypeComposite] = this[associationCompositeHandle.Id];
            });

            var manyToOneAssociationTypeHandle = new OneToOneAssociationTypeHandle(associationTypeId);
            this.Register(manyToOneAssociationTypeHandle, associationType);

            var roleType = this.EmbeddedPopulation.Create(m.RoleType, v =>
            {
                v[m.MetaObjectId] = roleTypeId;
                v[m.RoleTypeAssociationType] = associationType;
                v[m.RoleTypeObjectType] = this[roleCompositeHandle.Id];
                v[m.RoleTypeSingularName] = singularName;
                v[m.RoleTypeAssignedPluralName] = assignedPluralName;
            });

            var manyToOneRoleTypeHandle = new OneToOneRoleTypeHandle(roleTypeId);
            this.Register(manyToOneRoleTypeHandle, roleType);

            return (manyToOneAssociationTypeHandle, manyToOneRoleTypeHandle);
        }

        /// <summary>
        /// Creates new ManyToOne relation end types.
        /// </summary>
        public (ManyToOneAssociationTypeHandle AssociationType, ManyToOneRoleTypeHandle RoleType) NewManyToOneRelation(Guid associationTypeId, Guid roleTypeId, CompositeHandle associationCompositeHandle, CompositeHandle roleCompositeHandle, string singularName, string? assignedPluralName = null)
        {
            var m = this.Meta;

            var associationType = this.EmbeddedPopulation.Create(m.AssociationType, v =>
            {
                v[m.MetaObjectId] = associationTypeId;
                v[m.AssociationTypeComposite] = this[associationCompositeHandle.Id];
            });

            var manyToOneAssociationTypeHandle = new ManyToOneAssociationTypeHandle(associationTypeId);
            this.Register(manyToOneAssociationTypeHandle, associationType);

            var roleType = this.EmbeddedPopulation.Create(m.RoleType, v =>
            {
                v[m.MetaObjectId] = roleTypeId;
                v[m.RoleTypeAssociationType] = associationType;
                v[m.RoleTypeObjectType] = this[roleCompositeHandle.Id];
                v[m.RoleTypeSingularName] = singularName;
                v[m.RoleTypeAssignedPluralName] = assignedPluralName;
            });

            var manyToOneRoleTypeHandle = new ManyToOneRoleTypeHandle(roleTypeId);
            this.Register(manyToOneRoleTypeHandle, roleType);

            return (manyToOneAssociationTypeHandle, manyToOneRoleTypeHandle);
        }

        /// <summary>
        /// Creates new OneToMany relation end types.
        /// </summary>
        public (OneToManyAssociationTypeHandle AssociationType, OneToManyRoleTypeHandle RoleType) NewOneToManyRelation(Guid associationTypeId, Guid roleTypeId, CompositeHandle associationCompositeHandle, CompositeHandle roleCompositeHandle, string singularName, string? assignedPluralName = null)
        {
            var m = this.Meta;

            var associationType = this.EmbeddedPopulation.Create(m.AssociationType, v =>
            {
                v[m.MetaObjectId] = associationTypeId;
                v[m.AssociationTypeComposite] = this[associationCompositeHandle.Id];
            });

            var oneToManyAssociationTypeHandle = new OneToManyAssociationTypeHandle(associationTypeId);
            this.Register(oneToManyAssociationTypeHandle, associationType);

            var roleType = this.EmbeddedPopulation.Create(m.RoleType, v =>
            {
                v[m.MetaObjectId] = roleTypeId;
                v[m.RoleTypeAssociationType] = associationType;
                v[m.RoleTypeObjectType] = this[roleCompositeHandle.Id];
                v[m.RoleTypeSingularName] = singularName;
                v[m.RoleTypeAssignedPluralName] = assignedPluralName;
            });

            var oneToManyRoleTypeHandle = new OneToManyRoleTypeHandle(roleTypeId);
            this.Register(oneToManyRoleTypeHandle, roleType);

            return (oneToManyAssociationTypeHandle, oneToManyRoleTypeHandle);
        }

        /// <summary>
        /// Creates new ManyToMany relation end types.
        /// </summary>
        public (ManyToManyAssociationTypeHandle AssociationType, ManyToManyRoleTypeHandle RoleType) NewManyToManyRelation(Guid associationTypeId, Guid roleTypeId, CompositeHandle associationCompositeHandle, CompositeHandle roleCompositeHandle, string singularName, string? assignedPluralName = null)
        {
            var m = this.Meta;

            var associationType = this.EmbeddedPopulation.Create(m.AssociationType, v =>
            {
                v[m.MetaObjectId] = associationTypeId;
                v[m.AssociationTypeComposite] = this[associationCompositeHandle.Id];
            });

            var manyToManyAssociationTypeHandle = new ManyToManyAssociationTypeHandle(associationTypeId);
            this.Register(manyToManyAssociationTypeHandle, associationType);

            var roleType = this.EmbeddedPopulation.Create(m.RoleType, v =>
            {
                v[m.MetaObjectId] = roleTypeId;
                v[m.RoleTypeAssociationType] = associationType;
                v[m.RoleTypeObjectType] = this[roleCompositeHandle.Id];
                v[m.RoleTypeSingularName] = singularName;
                v[m.RoleTypeAssignedPluralName] = assignedPluralName;
            });

            var manyToManyRoleTypeHandle = new ManyToManyRoleTypeHandle(roleTypeId);
            this.Register(manyToManyRoleTypeHandle, roleType);

            return (manyToManyAssociationTypeHandle, manyToManyRoleTypeHandle);
        }

        /// <summary>
        /// Subtype implements supertype.
        /// </summary>
        public void AddDirectSupertype(CompositeHandle subtypeHandle, InterfaceHandle supertypeHandle)
        {
            var m = this.Meta;

            var subtype = this[subtypeHandle];
            var supertype = this[supertypeHandle];
            subtype.Add(m.CompositeDirectSupertypes, supertype);
        }
    }
}
