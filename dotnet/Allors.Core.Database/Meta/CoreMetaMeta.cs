namespace Allors.Core.Database.Meta
{
    using System;
    using Allors.Core.Meta.Domain;
    using Allors.Core.Meta.Meta;

    /// <summary>
    /// Core Meta Meta.
    /// </summary>
    public sealed class CoreMetaMeta
    {
        internal CoreMetaMeta()
        {
            this.MetaMeta = new MetaMeta();

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
            var metaMeta = this.MetaMeta;

            this.AssociationTypeComposite = metaMeta.AddManyToOne(this.AssociationType, this.Composite);

            this.CompositeDirectSupertypes = metaMeta.AddManyToMany(this.Composite, this.Interface, "DirectSupertype");

            this.DomainTypes = metaMeta.AddManyToMany(this.Domain, this.Type);

            this.ObjectTypeAssignedPluralName = metaMeta.AddUnit<string>(this.ObjectType, "AssignedPluralName");
            this.ObjectTypeDerivedPluralName = metaMeta.AddUnit<string>(this.ObjectType, "DerivedPluralName");
            this.ObjectTypeSingularName = metaMeta.AddUnit<string>(this.ObjectType, "SingularName");

            this.MetaObjectId = metaMeta.AddUnit<Guid>(this.MetaObject, "Id");

            this.RelationEndTypeIsMany = metaMeta.AddUnit<bool>(this.RelationEndType, "IsMany");

            this.RoleTypeAssociationType = metaMeta.AddOneToOne(this.RoleType, this.AssociationType);
            this.RoleTypeAssignedPluralName = metaMeta.AddUnit<string>(this.RoleType, "AssignedPluralName");
            this.RoleTypeDerivedPluralName = metaMeta.AddUnit<string>(this.RoleType, "DerivedPluralName");
            this.RoleTypeObjectType = metaMeta.AddManyToOne(this.RoleType, this.ObjectType);
            this.RoleTypeSingularName = metaMeta.AddUnit<string>(this.RoleType, "SingularName");

            this.WorkspaceTypes = metaMeta.AddManyToMany(this.Workspace, this.Type);
        }

        /// <summary>
        /// The meta meta.
        /// </summary>
        public MetaMeta MetaMeta { get; }

        /// <summary>
        /// An association type.
        /// </summary>
        public MetaObjectType AssociationType { get; init; }

        /// <summary>
        /// The composite of an association type.
        /// </summary>
        public MetaManyToOneRoleType AssociationTypeComposite { get; init; }

        /// <summary>
        /// A class.
        /// </summary>
        public MetaObjectType Class { get; init; }

        /// <summary>
        /// A composite.
        /// </summary>
        public MetaObjectType Composite { get; init; }

        /// <summary>
        /// The direct supertypes of a composite.
        /// </summary>
        public MetaManyToManyRoleType CompositeDirectSupertypes { get; init; }

        /// <summary>
        /// A domain.
        /// </summary>
        public MetaObjectType Domain { get; init; }

        /// <summary>
        /// The types of a domain.
        /// </summary>
        public MetaManyToManyRoleType DomainTypes { get; init; }

        /// <summary>
        /// An interface.
        /// </summary>
        public MetaObjectType Interface { get; init; }

        /// <summary>
        /// A meta object.
        /// </summary>
        public MetaObjectType MetaObject { get; init; }

        /// <summary>
        /// The id of a meta object.
        /// </summary>
        public MetaUnitRoleType MetaObjectId { get; init; }

        /// <summary>
        /// A method type.
        /// </summary>
        public MetaObjectType MethodType { get; init; }

        /// <summary>
        /// An object type.
        /// </summary>
        public MetaObjectType ObjectType { get; init; }

        /// <summary>
        /// The assigned plural name of an object type.
        /// </summary>
        public MetaUnitRoleType ObjectTypeAssignedPluralName { get; init; }

        /// <summary>
        /// The derived plural name of an object type.
        /// </summary>
        public MetaUnitRoleType ObjectTypeDerivedPluralName { get; init; }

        /// <summary>
        /// The singular name of an object type.
        /// </summary>
        public MetaUnitRoleType ObjectTypeSingularName { get; init; }

        /// <summary>
        /// An operand type.
        /// </summary>public MetaObjectType OperandType { get; init; }
        public MetaObjectType OperandType { get; init; }

        /// <summary>
        /// A relation end type.
        /// </summary>public MetaObjectType OperandType { get; init; }
        public MetaObjectType RelationEndType { get; init; }

        /// <summary>
        /// The is many of a role type.
        /// </summary>
        public MetaUnitRoleType RelationEndTypeIsMany { get; }

        /// <summary>
        /// A role type.
        /// </summary>
        public MetaObjectType RoleType { get; init; }

        /// <summary>
        /// The association type a role type.
        /// </summary>
        public MetaOneToOneRoleType RoleTypeAssociationType { get; init; }

        /// <summary>
        /// The assigned plural name of a role type.
        /// </summary>
        public MetaUnitRoleType RoleTypeAssignedPluralName { get; init; }

        /// <summary>
        /// The derived plural name of a role type.
        /// </summary>
        public MetaUnitRoleType RoleTypeDerivedPluralName { get; init; }

        /// <summary>
        /// The role type of object type.
        /// </summary>
        public MetaManyToOneRoleType RoleTypeObjectType { get; init; }

        /// <summary>
        /// The singular name of object type.
        /// </summary>
        public MetaUnitRoleType RoleTypeSingularName { get; init; }

        /// <summary>
        /// A type.
        /// </summary>
        public MetaObjectType Type { get; init; }

        /// <summary>
        /// A unit.
        /// </summary>
        public MetaObjectType Unit { get; init; }

        /// <summary>
        /// A workspace.
        /// </summary>
        public MetaObjectType Workspace { get; init; }

        /// <summary>
        /// The types of a workspace.
        /// </summary>
        public MetaManyToManyRoleType WorkspaceTypes { get; init; }

        /// <summary>
        /// Creates a new meta class.
        /// </summary>
        public MetaObjectType NewMetaClass(string name) => this.MetaMeta.AddClass(name);

        /// <summary>
        /// Creates a new meta interface.
        /// </summary>
        public MetaObjectType NewMetaInterface(string name) => this.MetaMeta.AddInterface(name);

        /// <summary>
        /// Creates a new MetaPopulation
        /// </summary>
        public MetaPopulation CreateMetaPopulation() => new(this.MetaMeta);
    }
}
