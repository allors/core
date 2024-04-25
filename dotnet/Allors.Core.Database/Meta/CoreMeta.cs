namespace Allors.Core.Database.Meta
{
    using System;
    using Allors.Core.Database.Meta.Handles;
    using Allors.Embedded.Meta;

    /// <summary>
    /// Meta Core.
    /// </summary>
    public sealed class CoreMeta
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoreMeta"/> class.
        /// </summary>
        public CoreMeta()
        {
            this.Meta = new Meta();

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
            var embeddedMeta = this.Meta.EmbeddedMeta;

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
        /// The meta.
        /// </summary>
        public Meta Meta { get; set; }

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
        /// Creates a new meta class.
        /// </summary>
        public EmbeddedObjectType NewMetaClass(string name)
        {
            return this.Meta.EmbeddedMeta.AddClass(name);
        }

        /// <summary>
        /// Creates a new meta interface.
        /// </summary>
        public EmbeddedObjectType NewMetaInterface(string name)
        {
            return this.Meta.EmbeddedMeta.AddInterface(name);
        }

        /// <summary>
        /// Creates a new unit.
        /// </summary>
        public UnitHandle NewUnit(Guid id, string singularName, string? assignedPluralName = null)
        {
            var unit = this.Meta.EmbeddedPopulation.Create(this.Unit, v =>
            {
                v[this.MetaObjectId] = id;
                v[this.ObjectTypeSingularName] = singularName;
                v[this.ObjectTypeAssignedPluralName] = assignedPluralName;
            });

            var unitHandle = new UnitHandle(id);

            this.Meta.Add(unitHandle, unit);

            return unitHandle;
        }

        /// <summary>
        /// Creates a new interface.
        /// </summary>
        public Interface NewInterface(Guid id, string singularName, string? assignedPluralName = null)
        {
            var @interface = this.Meta.EmbeddedPopulation.Create(this.Interface, v =>
            {
                v[this.MetaObjectId] = id;
                v[this.ObjectTypeSingularName] = singularName;
                v[this.ObjectTypeAssignedPluralName] = assignedPluralName;
            });

            var interfaceHandle = new Interface(id);

            this.Meta.Add(interfaceHandle, @interface);

            return interfaceHandle;
        }

        /// <summary>
        /// Creates a new class.
        /// </summary>
        public ClassHandle NewClass(Guid id, string singularName, string? assignedPluralName = null)
        {
            var @class = this.Meta.EmbeddedPopulation.Create(this.Class, v =>
            {
                v[this.MetaObjectId] = id;
                v[this.ObjectTypeSingularName] = singularName;
                v[this.ObjectTypeAssignedPluralName] = assignedPluralName;
            });

            var classHandle = new ClassHandle(id);

            this.Meta.Add(classHandle, @class);

            return classHandle;
        }

        /// <summary>
        /// Creates new unit relation end types.
        /// </summary>
        public (UnitAssociationTypeHandleHandle AssociationType, UnitRoleTypeHandleHandle RoleType) NewUnitRelationEndTypes(Guid associationTypeId, Guid roleTypeId, CompositeHandle associationCompositeHandle, UnitHandle unitHandle, string singularName, string? assignedPluralName = null)
        {
            var associationType = this.Meta.EmbeddedPopulation.Create(this.AssociationType, v =>
            {
                v[this.MetaObjectId] = associationTypeId;
                v[this.AssociationTypeComposite] = this.Meta[associationCompositeHandle.Id];
            });

            var unitAssociationTypeHandle = new UnitAssociationTypeHandleHandle(associationTypeId);
            this.Meta.Add(unitAssociationTypeHandle, associationType);

            var roleType = this.Meta.EmbeddedPopulation.Create(this.RoleType, v =>
            {
                v[this.MetaObjectId] = roleTypeId;
                v[this.RoleTypeAssociationType] = associationType;
                v[this.RoleTypeObjectType] = this.Meta[unitHandle.Id];
                v[this.RoleTypeSingularName] = singularName;
                v[this.RoleTypeAssignedPluralName] = assignedPluralName;
            });

            var unitRoleTypeHandle = new UnitRoleTypeHandleHandle(roleTypeId);
            this.Meta.Add(unitRoleTypeHandle, roleType);

            return (unitAssociationTypeHandle, unitRoleTypeHandle);
        }

        /// <summary>
        /// Creates new unit relation end types.
        /// </summary>
        public (ManyToOneAssociationTypeHandle AssociationType, ManyToOneRoleTypeHandle RoleType) NewManyToOneRelationEndTypes(Guid associationTypeId, Guid roleTypeId, CompositeHandle associationCompositeHandle, CompositeHandle roleCompositeHandle, string singularName, string? assignedPluralName = null)
        {
            var associationType = this.Meta.EmbeddedPopulation.Create(this.AssociationType, v =>
            {
                v[this.MetaObjectId] = associationTypeId;
                v[this.AssociationTypeComposite] = this.Meta[associationCompositeHandle.Id];
            });

            var manyToOneAssociationTypeHandle = new ManyToOneAssociationTypeHandle(associationTypeId);
            this.Meta.Add(manyToOneAssociationTypeHandle, associationType);

            var roleType = this.Meta.EmbeddedPopulation.Create(this.RoleType, v =>
            {
                v[this.MetaObjectId] = roleTypeId;
                v[this.RoleTypeAssociationType] = associationType;
                v[this.RoleTypeObjectType] = this.Meta[roleCompositeHandle.Id];
                v[this.RoleTypeSingularName] = singularName;
                v[this.RoleTypeAssignedPluralName] = assignedPluralName;
            });

            var manyToOneRoleTypeHandle = new ManyToOneRoleTypeHandle(roleTypeId);
            this.Meta.Add(manyToOneRoleTypeHandle, roleType);

            return (manyToOneAssociationTypeHandle, manyToOneRoleTypeHandle);
        }
    }
}
