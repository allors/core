namespace Allors.Core.Database.Meta
{
    using System;
    using Allors.Embedded.Meta;

    /// <summary>
    /// Core Meta Meta.
    /// </summary>
    public sealed class CoreMetaMeta
    {
        internal CoreMetaMeta()
        {
            this.EmbeddedMeta = new EmbeddedMeta();

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
        }

        /// <summary>
        /// The embedded meta.
        /// </summary>
        public EmbeddedMeta EmbeddedMeta { get; }

        /// <summary>
        /// An association type.
        /// </summary>
        public EmbeddedObjectType AssociationType { get; init; }

        /// <summary>
        /// The composite of an association type.
        /// </summary>
        public EmbeddedManyToOneRoleType AssociationTypeComposite { get; init; }

        /// <summary>
        /// A class.
        /// </summary>
        public EmbeddedObjectType Class { get; init; }

        /// <summary>
        /// A composite.
        /// </summary>
        public EmbeddedObjectType Composite { get; init; }

        /// <summary>
        /// The direct supertypes of a composite.
        /// </summary>
        public EmbeddedManyToManyRoleType CompositeDirectSupertypes { get; init; }

        /// <summary>
        /// A domain.
        /// </summary>
        public EmbeddedObjectType Domain { get; init; }

        /// <summary>
        /// The types of a domain.
        /// </summary>
        public EmbeddedManyToManyRoleType DomainTypes { get; init; }

        /// <summary>
        /// An interface.
        /// </summary>
        public EmbeddedObjectType Interface { get; init; }

        /// <summary>
        /// A meta object.
        /// </summary>
        public EmbeddedObjectType MetaObject { get; init; }

        /// <summary>
        /// The id of a meta object.
        /// </summary>
        public EmbeddedUnitRoleType MetaObjectId { get; init; }

        /// <summary>
        /// A method type.
        /// </summary>
        public EmbeddedObjectType MethodType { get; init; }

        /// <summary>
        /// An object type.
        /// </summary>
        public EmbeddedObjectType ObjectType { get; init; }

        /// <summary>
        /// The assigned plural name of an object type.
        /// </summary>
        public EmbeddedUnitRoleType ObjectTypeAssignedPluralName { get; init; }

        /// <summary>
        /// The derived plural name of an object type.
        /// </summary>
        public EmbeddedUnitRoleType ObjectTypeDerivedPluralName { get; init; }

        /// <summary>
        /// The singular name of an object type.
        /// </summary>
        public EmbeddedUnitRoleType ObjectTypeSingularName { get; init; }

        /// <summary>
        /// An operand type.
        /// </summary>public EmbeddedObjectType OperandType { get; init; }
        public EmbeddedObjectType OperandType { get; init; }

        /// <summary>
        /// A relation end type.
        /// </summary>public EmbeddedObjectType OperandType { get; init; }
        public EmbeddedObjectType RelationEndType { get; init; }

        /// <summary>
        /// The is many of a role type.
        /// </summary>
        public EmbeddedUnitRoleType RelationEndTypeIsMany { get; }

        /// <summary>
        /// A role type.
        /// </summary>
        public EmbeddedObjectType RoleType { get; init; }

        /// <summary>
        /// The association type a role type.
        /// </summary>
        public EmbeddedOneToOneRoleType RoleTypeAssociationType { get; init; }

        /// <summary>
        /// The assigned plural name of a role type.
        /// </summary>
        public EmbeddedUnitRoleType RoleTypeAssignedPluralName { get; init; }

        /// <summary>
        /// The derived plural name of a role type.
        /// </summary>
        public EmbeddedUnitRoleType RoleTypeDerivedPluralName { get; init; }

        /// <summary>
        /// The role type of object type.
        /// </summary>
        public EmbeddedManyToOneRoleType RoleTypeObjectType { get; init; }

        /// <summary>
        /// The singular name of object type.
        /// </summary>
        public EmbeddedUnitRoleType RoleTypeSingularName { get; init; }

        /// <summary>
        /// A type.
        /// </summary>
        public EmbeddedObjectType Type { get; init; }

        /// <summary>
        /// A unit.
        /// </summary>
        public EmbeddedObjectType Unit { get; init; }

        /// <summary>
        /// A workspace.
        /// </summary>
        public EmbeddedObjectType Workspace { get; init; }

        /// <summary>
        /// The types of a workspace.
        /// </summary>
        public EmbeddedManyToManyRoleType WorkspaceTypes { get; init; }

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
    }
}
