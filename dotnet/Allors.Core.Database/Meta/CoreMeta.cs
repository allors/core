namespace Allors.Core.Database.Meta
{
    using System;
    using Allors.Embedded.Domain;
    using Allors.Embedded.Meta;

    /// <summary>
    /// Meta Core.
    /// </summary>
    public sealed class CoreMeta
    {
        /// <summary>
        /// The id for Object.
        /// </summary>
        public static readonly Guid ID1595A154CEE841FCA88FCE3EEACA8B57 = new("1595A154-CEE8-41FC-A88F-CE3EEACA8B57");

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreMeta"/> class.
        /// </summary>
        public CoreMeta()
        {
            this.EmbeddedMeta = new EmbeddedMeta();
            this.EmbeddedPopulation = new EmbeddedPopulation();

            // Meta Meta
            var meta = this.EmbeddedMeta;

            // ObjectTypes
            this.AssociationType = meta.AddClass("AssociationType");
            this.Class = meta.AddClass("Class");
            this.Composite = meta.AddInterface("Composite");
            this.Domain = meta.AddClass("Domain");
            this.Interface = meta.AddClass("Interface");
            this.MetaObject = meta.AddInterface("MetaObject");
            this.MethodType = meta.AddClass("MethodType");
            this.ObjectType = meta.AddInterface("ObjectType");
            this.OperandType = meta.AddInterface("OperandType");
            this.RelationEndType = meta.AddInterface("RelationEndType");
            this.RoleType = meta.AddClass("RoleType");
            this.Type = meta.AddInterface("Type");
            this.Unit = meta.AddClass("Unit");
            this.Workspace = meta.AddClass("Workspace");

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
            this.AssociationTypeComposite = meta.AddManyToOne(this.AssociationType, this.Composite);

            this.CompositeDirectSupertypes = meta.AddManyToMany(this.Composite, this.Interface, "DirectSupertype");

            this.DomainTypes = meta.AddManyToMany(this.Domain, this.Type);

            this.ObjectTypeAssignedPluralName = meta.AddUnit<string>(this.ObjectType, "AssignedPluralName");
            this.ObjectTypeDerivedPluralName = meta.AddUnit<string>(this.ObjectType, "DerivedPluralName");
            this.ObjectTypeSingularName = meta.AddUnit<string>(this.ObjectType, "SingularName");

            this.MetaObjectId = meta.AddUnit<Guid>(this.MetaObject, "Id");

            this.RelationEndTypeIsMany = meta.AddUnit<bool>(this.RelationEndType, "IsMany");

            this.RoleTypeAssociationType = meta.AddOneToOne(this.RoleType, this.AssociationType);
            this.RoleTypeAssignedPluralName = meta.AddUnit<string>(this.RoleType, "AssignedPluralName");
            this.RoleTypeDerivedPluralName = meta.AddUnit<string>(this.RoleType, "DerivedPluralName");
            this.RoleTypeObjectType = meta.AddManyToOne(this.RoleType, this.ObjectType);
            this.RoleTypeSingularName = meta.AddUnit<string>(this.RoleType, "SingularName");

            this.WorkspaceTypes = meta.AddManyToMany(this.Workspace, this.Type);

            // Meta
            EmbeddedObject NewInterface(Guid id, string singularName, string? assignedPluralName = null)
            {
                return this.EmbeddedPopulation.Create(this.Interface, v =>
                {
                    v[this.MetaObjectId] = id;
                    v[this.ObjectTypeSingularName] = singularName;
                    v[this.ObjectTypeAssignedPluralName] = assignedPluralName;
                });
            }

            var @object = NewInterface(ID1595A154CEE841FCA88FCE3EEACA8B57, "Object");
        }

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
        /// The embedded meta.
        /// </summary>
        public EmbeddedMeta EmbeddedMeta { get; }

        /// <summary>
        /// The embedded population.
        /// </summary>
        public EmbeddedPopulation EmbeddedPopulation { get; set; }

        /// <summary>
        /// Build a MetaPopulation.
        /// </summary>
        /// <returns></returns>
        public Meta Build()
        {
            return new Meta(this);
        }
    }
}
