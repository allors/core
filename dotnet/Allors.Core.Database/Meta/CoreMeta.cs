namespace Allors.Core.Database.Meta
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using Allors.Embedded.Domain;
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
            this.MetaObjectById = new ConcurrentDictionary<Guid, MetaObject>();

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
            this.AssociationTypeComposite = this.EmbeddedMeta.AddManyToOne(this.AssociationType, this.Composite);

            this.CompositeDirectSupertypes = this.EmbeddedMeta.AddManyToMany(this.Composite, this.Interface, "DirectSupertype");

            this.DomainTypes = this.EmbeddedMeta.AddManyToMany(this.Domain, this.Type);

            this.ObjectTypeAssignedPluralName = this.EmbeddedMeta.AddUnit<string>(this.ObjectType, "AssignedPluralName");
            this.ObjectTypeDerivedPluralName = this.EmbeddedMeta.AddUnit<string>(this.ObjectType, "DerivedPluralName");
            this.ObjectTypeSingularName = this.EmbeddedMeta.AddUnit<string>(this.ObjectType, "SingularName");

            this.MetaObjectId = this.EmbeddedMeta.AddUnit<Guid>(this.MetaObject, "Id");

            this.RelationEndTypeIsMany = this.EmbeddedMeta.AddUnit<bool>(this.RelationEndType, "IsMany");

            this.RoleTypeAssociationType = this.EmbeddedMeta.AddOneToOne(this.RoleType, this.AssociationType);
            this.RoleTypeAssignedPluralName = this.EmbeddedMeta.AddUnit<string>(this.RoleType, "AssignedPluralName");
            this.RoleTypeDerivedPluralName = this.EmbeddedMeta.AddUnit<string>(this.RoleType, "DerivedPluralName");
            this.RoleTypeObjectType = this.EmbeddedMeta.AddManyToOne(this.RoleType, this.ObjectType);
            this.RoleTypeSingularName = this.EmbeddedMeta.AddUnit<string>(this.RoleType, "SingularName");

            this.WorkspaceTypes = this.EmbeddedMeta.AddManyToMany(this.Workspace, this.Type);

            // Meta
            this.Object = this.NewInterface(new Guid("8904EE32-CF11-4019-9FD7-FB9631F9ACAC"), "Object");
            this.String = this.NewUnit(new Guid("58BB7632-4724-4F92-869B-B30D7A7BEE9E"), "String");
        }

        /// <summary>
        /// Lookup a meta object by id.
        /// </summary>
        public IDictionary<Guid, MetaObject> MetaObjectById { get; }

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
        public Unit String { get; set; }

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
        public Unit NewUnit(Guid id, string singularName, string? assignedPluralName = null)
        {
            var unit = new Unit(id, this.EmbeddedPopulation.Create(this.Unit, v =>
            {
                v[this.MetaObjectId] = id;
                v[this.ObjectTypeSingularName] = singularName;
                v[this.ObjectTypeAssignedPluralName] = assignedPluralName;
            }));

            this.MetaObjectById.Add(id, unit);
            return unit;
        }

        /// <summary>
        /// Creates a new interface.
        /// </summary>
        public Interface NewInterface(Guid id, string singularName, string? assignedPluralName = null)
        {
            var @interface = new Interface(id, this.EmbeddedPopulation.Create(this.Interface, v =>
            {
                v[this.MetaObjectId] = id;
                v[this.ObjectTypeSingularName] = singularName;
                v[this.ObjectTypeAssignedPluralName] = assignedPluralName;
            }));

            this.MetaObjectById.Add(id, @interface);
            return @interface;
        }

        /// <summary>
        /// Creates a new class.
        /// </summary>
        public Class NewClass(Guid id, string singularName, string? assignedPluralName = null)
        {
            var @class = new Class(id, this.EmbeddedPopulation.Create(this.Class, v =>
            {
                v[this.MetaObjectId] = id;
                v[this.ObjectTypeSingularName] = singularName;
                v[this.ObjectTypeAssignedPluralName] = assignedPluralName;
            }));

            this.MetaObjectById.Add(id, @class);
            return @class;
        }
    }
}
