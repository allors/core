namespace Allors.Core.Database.Meta
{
    using System;
    using System.Collections.Frozen;
    using System.Collections.Generic;
    using Allors.Core.Database.Meta.Handles;
    using Allors.Embedded.Domain;
    using Allors.Embedded.Meta;

    /// <summary>
    /// Meta Core.
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

            this.metaHandleByMetaObject = new Dictionary<EmbeddedObject, MetaHandle>();
            this.metaObjectByMetaHandle = new Dictionary<MetaHandle, EmbeddedObject>();
            this.metaObjectById = new Dictionary<Guid, EmbeddedObject>();
            this.metaHandles = null;

            this.EmbeddedMeta = new EmbeddedMeta();
            this.EmbeddedPopulation = new EmbeddedPopulation();

            // Meta Meta
            // ObjectTypes
            this.AssociationType = this.NewMetaClass("AssociationType");
            this.Class = this.NewMetaClass("Class");
            this.Composite = this.NewMetaInterface("Composite");
            this.Domain = this.NewMetaClass("Domain");
            this.Interface = this.NewMetaClass("Interface");
            this.MetaObject = this.NewMetaInterface("MetaObject");
            this.MethodType = this.NewMetaClass("MethodType");
            this.ObjectType = this.NewMetaInterface("ObjectType");
            this.OperandType = this.NewMetaInterface("OperandType");
            this.RelationEndType = this.NewMetaInterface("RelationEndType");
            this.RoleType = this.NewMetaClass("RoleType");
            this.Type = this.NewMetaInterface("Type");
            this.Unit = this.NewMetaClass("Unit");
            this.Workspace = this.NewMetaClass("Workspace");

            // Inheritance
            this.AssociationType.AddDirectSupertype(this.RelationEndType);
            this.Class.AddDirectSupertype(this.Composite);
            this.Composite.AddDirectSupertype(this.ObjectType);
            this.Domain.AddDirectSupertype(this.MetaObject);
            this.Interface.AddDirectSupertype(this.Composite);
            this.MethodType.AddDirectSupertype(this.OperandType);
            this.ObjectType.AddDirectSupertype(this.Type);
            this.OperandType.AddDirectSupertype(this.Type);
            this.RelationEndType.AddDirectSupertype(this.OperandType);
            this.RoleType.AddDirectSupertype(this.RelationEndType);
            this.Type.AddDirectSupertype(this.MetaObject);
            this.Unit.AddDirectSupertype(this.ObjectType);
            this.Workspace.AddDirectSupertype(this.MetaObject);

            // Relations
            var embeddedMeta = this.EmbeddedMeta;

            this.AssociationTypeComposite = embeddedMeta.AddManyToOne(this.AssociationType, this.Composite);

            this.CompositeDirectSupertypes = embeddedMeta.AddManyToMany(this.Composite, this.Interface, "DirectSupertype");

            this.DomainTypes = embeddedMeta.AddManyToMany(this.Domain, this.Type);

            this.ObjectTypeAssignedPluralName = embeddedMeta.AddUnit<string>(this.ObjectType, "AssignedPluralName");
            this.ObjectTypeDerivedPluralName = embeddedMeta.AddUnit<string>(this.ObjectType, "DerivedPluralName");
            this.ObjectTypeSingularName = embeddedMeta.AddUnit<string>(this.ObjectType, "SingularName");

            this.MetaObjectId = embeddedMeta.AddUnit<Guid>(this.MetaObject, "Id");

            this.RelationEndTypeIsMany = embeddedMeta.AddUnit<bool>(this.RelationEndType, "IsMany");

            this.RoleTypeAssociationType = embeddedMeta.AddOneToOne(this.RoleType, this.AssociationType);
            this.RoleTypeAssignedPluralName = embeddedMeta.AddUnit<string>(this.RoleType, "AssignedPluralName");
            this.RoleTypeDerivedPluralName = embeddedMeta.AddUnit<string>(this.RoleType, "DerivedPluralName");
            this.RoleTypeObjectType = embeddedMeta.AddManyToOne(this.RoleType, this.ObjectType);
            this.RoleTypeSingularName = embeddedMeta.AddUnit<string>(this.RoleType, "SingularName");

            this.WorkspaceTypes = embeddedMeta.AddManyToMany(this.Workspace, this.Type);

            // Meta
            this.Object = this.NewInterface(new Guid("8904EE32-CF11-4019-9FD7-FB9631F9ACAC"), "Object");
            this.String = this.NewUnit(new Guid("58BB7632-4724-4F92-869B-B30D7A7BEE9E"), "String");
        }

        /// <summary>
        /// Is this meta frozen.
        /// </summary>
        public bool IsFrozen { get; private set; }

        /// <summary>
        /// The embedded meta.
        /// </summary>
        public EmbeddedMeta EmbeddedMeta { get; }

        /// <summary>
        /// The embedded population.
        /// </summary>
        public EmbeddedPopulation EmbeddedPopulation { get; set; }

        /// <summary>
        /// An association type.
        /// </summary>
        public EmbeddedObjectType AssociationType { get; set; }

        /// <summary>
        /// The composite of an association type.
        /// </summary>
        public EmbeddedManyToOneRoleType AssociationTypeComposite { get; set; }

        /// <summary>
        /// A class.
        /// </summary>
        public EmbeddedObjectType Class { get; set; }

        /// <summary>
        /// A composite.
        /// </summary>
        public EmbeddedObjectType Composite { get; set; }

        /// <summary>
        /// The direct supertypes of a composite.
        /// </summary>
        public EmbeddedManyToManyRoleType CompositeDirectSupertypes { get; set; }

        /// <summary>
        /// A domain.
        /// </summary>
        public EmbeddedObjectType Domain { get; set; }

        /// <summary>
        /// The types of a domain.
        /// </summary>
        public EmbeddedManyToManyRoleType DomainTypes { get; set; }

        /// <summary>
        /// An interface.
        /// </summary>
        public EmbeddedObjectType Interface { get; set; }

        /// <summary>
        /// A meta object.
        /// </summary>
        public EmbeddedObjectType MetaObject { get; set; }

        /// <summary>
        /// The id of a meta object.
        /// </summary>
        public EmbeddedUnitRoleType MetaObjectId { get; set; }

        /// <summary>
        /// A method type.
        /// </summary>
        public EmbeddedObjectType MethodType { get; set; }

        /// <summary>
        /// An object type.
        /// </summary>
        public EmbeddedObjectType ObjectType { get; set; }

        /// <summary>
        /// The assigned plural name of an object type.
        /// </summary>
        public EmbeddedUnitRoleType ObjectTypeAssignedPluralName { get; set; }

        /// <summary>
        /// The derived plural name of an object type.
        /// </summary>
        public EmbeddedUnitRoleType ObjectTypeDerivedPluralName { get; set; }

        /// <summary>
        /// The singular name of an object type.
        /// </summary>
        public EmbeddedUnitRoleType ObjectTypeSingularName { get; set; }

        /// <summary>
        /// An operand type.
        /// </summary>public EmbeddedObjectType OperandType { get; set; }
        public EmbeddedObjectType OperandType { get; set; }

        /// <summary>
        /// A relation end type.
        /// </summary>public EmbeddedObjectType OperandType { get; set; }
        public EmbeddedObjectType RelationEndType { get; set; }

        /// <summary>
        /// The is many of a role type.
        /// </summary>
        public EmbeddedUnitRoleType RelationEndTypeIsMany { get; }

        /// <summary>
        /// A role type.
        /// </summary>
        public EmbeddedObjectType RoleType { get; set; }

        /// <summary>
        /// The association type a role type.
        /// </summary>
        public EmbeddedOneToOneRoleType RoleTypeAssociationType { get; set; }

        /// <summary>
        /// The assigned plural name of a role type.
        /// </summary>
        public EmbeddedUnitRoleType RoleTypeAssignedPluralName { get; set; }

        /// <summary>
        /// The derived plural name of a role type.
        /// </summary>
        public EmbeddedUnitRoleType RoleTypeDerivedPluralName { get; set; }

        /// <summary>
        /// The role type of object type.
        /// </summary>
        public EmbeddedManyToOneRoleType RoleTypeObjectType { get; set; }

        /// <summary>
        /// The singular name of object type.
        /// </summary>
        public EmbeddedUnitRoleType RoleTypeSingularName { get; set; }

        /// <summary>
        /// A type.
        /// </summary>
        public EmbeddedObjectType Type { get; set; }

        /// <summary>
        /// A unit.
        /// </summary>
        public EmbeddedObjectType Unit { get; set; }

        /// <summary>
        /// A workspace.
        /// </summary>
        public EmbeddedObjectType Workspace { get; set; }

        /// <summary>
        /// The types of a workspace.
        /// </summary>
        public EmbeddedManyToManyRoleType WorkspaceTypes { get; set; }

        /// <summary>
        /// The Object interface.
        /// </summary>
        public Interface Object { get; set; }

        /// <summary>
        /// The String unit.
        /// </summary>
        public UnitHandle String { get; set; }

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
        public void Add(MetaHandle metaHandle, EmbeddedObject embeddedObject)
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
        /// Creates a new meta class.
        /// </summary>
        public EmbeddedObjectType NewMetaClass(string name)
        {
            return this.EmbeddedMeta.AddClass(name);
        }

        /// <summary>
        /// Creates a new meta interface.
        /// </summary>
        public EmbeddedObjectType NewMetaInterface(string name)
        {
            return this.EmbeddedMeta.AddInterface(name);
        }

        /// <summary>
        /// Creates a new unit.
        /// </summary>
        public UnitHandle NewUnit(Guid id, string singularName, string? assignedPluralName = null)
        {
            var unit = this.EmbeddedPopulation.Create(this.Unit, v =>
            {
                v[this.MetaObjectId] = id;
                v[this.ObjectTypeSingularName] = singularName;
                v[this.ObjectTypeAssignedPluralName] = assignedPluralName;
            });

            var unitHandle = new UnitHandle(id);

            this.Add(unitHandle, unit);

            return unitHandle;
        }

        /// <summary>
        /// Creates a new interface.
        /// </summary>
        public Interface NewInterface(Guid id, string singularName, string? assignedPluralName = null)
        {
            var @interface = this.EmbeddedPopulation.Create(this.Interface, v =>
            {
                v[this.MetaObjectId] = id;
                v[this.ObjectTypeSingularName] = singularName;
                v[this.ObjectTypeAssignedPluralName] = assignedPluralName;
            });

            var interfaceHandle = new Interface(id);

            this.Add(interfaceHandle, @interface);

            return interfaceHandle;
        }

        /// <summary>
        /// Creates a new class.
        /// </summary>
        public ClassHandle NewClass(Guid id, string singularName, string? assignedPluralName = null)
        {
            var @class = this.EmbeddedPopulation.Create(this.Class, v =>
            {
                v[this.MetaObjectId] = id;
                v[this.ObjectTypeSingularName] = singularName;
                v[this.ObjectTypeAssignedPluralName] = assignedPluralName;
            });

            var classHandle = new ClassHandle(id);

            this.Add(classHandle, @class);

            return classHandle;
        }

        /// <summary>
        /// Creates new unit relation end types.
        /// </summary>
        public (UnitAssociationTypeHandleHandle AssociationType, UnitRoleTypeHandleHandle RoleType) NewUnitRelationEndTypes(Guid associationTypeId, Guid roleTypeId, CompositeHandle associationCompositeHandle, UnitHandle unitHandle, string singularName, string? assignedPluralName = null)
        {
            var associationType = this.EmbeddedPopulation.Create(this.AssociationType, v =>
            {
                v[this.MetaObjectId] = associationTypeId;
                v[this.AssociationTypeComposite] = this[associationCompositeHandle.Id];
            });

            var unitAssociationTypeHandle = new UnitAssociationTypeHandleHandle(associationTypeId);
            this.Add(unitAssociationTypeHandle, associationType);

            var roleType = this.EmbeddedPopulation.Create(this.RoleType, v =>
            {
                v[this.MetaObjectId] = roleTypeId;
                v[this.RoleTypeAssociationType] = associationType;
                v[this.RoleTypeObjectType] = this[unitHandle.Id];
                v[this.RoleTypeSingularName] = singularName;
                v[this.RoleTypeAssignedPluralName] = assignedPluralName;
            });

            var unitRoleTypeHandle = new UnitRoleTypeHandleHandle(roleTypeId);
            this.Add(unitRoleTypeHandle, roleType);

            return (unitAssociationTypeHandle, unitRoleTypeHandle);
        }

        /// <summary>
        /// Creates new unit relation end types.
        /// </summary>
        public (ManyToOneAssociationTypeHandle AssociationType, ManyToOneRoleTypeHandle RoleType) NewManyToOneRelationEndTypes(Guid associationTypeId, Guid roleTypeId, CompositeHandle associationCompositeHandle, CompositeHandle roleCompositeHandle, string singularName, string? assignedPluralName = null)
        {
            var associationType = this.EmbeddedPopulation.Create(this.AssociationType, v =>
            {
                v[this.MetaObjectId] = associationTypeId;
                v[this.AssociationTypeComposite] = this[associationCompositeHandle.Id];
            });

            var manyToOneAssociationTypeHandle = new ManyToOneAssociationTypeHandle(associationTypeId);
            this.Add(manyToOneAssociationTypeHandle, associationType);

            var roleType = this.EmbeddedPopulation.Create(this.RoleType, v =>
            {
                v[this.MetaObjectId] = roleTypeId;
                v[this.RoleTypeAssociationType] = associationType;
                v[this.RoleTypeObjectType] = this[roleCompositeHandle.Id];
                v[this.RoleTypeSingularName] = singularName;
                v[this.RoleTypeAssignedPluralName] = assignedPluralName;
            });

            var manyToOneRoleTypeHandle = new ManyToOneRoleTypeHandle(roleTypeId);
            this.Add(manyToOneRoleTypeHandle, roleType);

            return (manyToOneAssociationTypeHandle, manyToOneRoleTypeHandle);
        }
    }
}
