namespace Allors.Core.Database
{
    using System;
    using Allors.Embedded.Meta;

    /// <summary>
    /// Meta Core.
    /// </summary>
    public sealed class CoreMetaMeta
    {
        /// <summary>
        /// Creates a new Core Meta Population.
        /// </summary>
        public CoreMetaMeta()
        {
            this.EmbeddedMeta = new EmbeddedMeta();

            var meta = this.EmbeddedMeta;

            // ObjectTypes
            this.AssociationType = meta.AddClass("AssociationType");
            this.Class = meta.AddClass("Class");
            this.Composite = meta.AddInterface("Composite");
            this.Domain = meta.AddClass("Domain");
            this.Interface = meta.AddClass("Interface");
            this.MetaIdentifiableObject = meta.AddInterface("MetaIdentifiableObject");
            this.MethodType = meta.AddClass("MethodType");
            this.ObjectType = meta.AddInterface("ObjectType");
            this.OperandType = meta.AddInterface("OperandType");
            this.RelationEndType = meta.AddInterface("RelationEndType");
            this.RoleType = meta.AddClass("RoleType");
            this.Unit = meta.AddClass("Unit");
            this.Workspace = meta.AddClass("Workspace");

            // Inheritance
            this.AssociationType.AddDirectSupertype(this.RelationEndType);
            this.Class.AddDirectSupertype(this.Composite);
            this.Composite.AddDirectSupertype(this.ObjectType);
            this.Interface.AddDirectSupertype(this.Composite);
            this.MethodType.AddDirectSupertype(this.OperandType);
            this.ObjectType.AddDirectSupertype(this.MetaIdentifiableObject);
            this.OperandType.AddDirectSupertype(this.MetaIdentifiableObject);
            this.RelationEndType.AddDirectSupertype(this.OperandType);
            this.RoleType.AddDirectSupertype(this.RelationEndType);
            this.Unit.AddDirectSupertype(this.ObjectType);

            // Relations
            this.AssociationTypeComposite = this.EmbeddedMeta.AddManyToOne(this.AssociationType, this.Composite);

            this.CompositeDirectSupertypes = this.EmbeddedMeta.AddManyToMany(this.Composite, this.Interface, "DirectSupertype");

            this.DomainMembers = this.EmbeddedMeta.AddManyToMany(this.Domain, this.MetaIdentifiableObject, "Member");

            this.ObjectTypeAssignedPluralName = this.EmbeddedMeta.AddUnit<string>(this.ObjectType, "AssignedPluralName");
            this.ObjectTypeDerivedPluralName = this.EmbeddedMeta.AddUnit<string>(this.ObjectType, "DerivedPluralName");
            this.ObjectTypeSingularName = this.EmbeddedMeta.AddUnit<string>(this.ObjectType, "SingularName");

            this.MetaIdentifiableObjectId = this.EmbeddedMeta.AddUnit<Guid>(this.MetaIdentifiableObject, "Id");

            this.RoleTypeAssociationType = this.EmbeddedMeta.AddOneToOne(this.RoleType, this.AssociationType);
            this.RoleTypeAssignedPluralName = this.EmbeddedMeta.AddUnit<string>(this.RoleType, "RoleTypeAssignedPluralName");
            this.RoleTypeDerivedPluralName = this.EmbeddedMeta.AddUnit<string>(this.RoleType, "RoleTypeDerivedPluralName");
            this.RoleTypeObjectType = this.EmbeddedMeta.AddManyToOne(this.RoleType, this.ObjectType);
            this.RoleTypeSingularName = this.EmbeddedMeta.AddUnit<string>(this.RoleType, "RoleTypeSingularName");

            this.WorkspaceMembers = this.EmbeddedMeta.AddManyToMany(this.Workspace, this.MetaIdentifiableObject, "Member");
        }

        /// <summary>
        /// The meta of Meta Core.
        /// </summary>
        public EmbeddedMeta EmbeddedMeta { get; }

        /// <summary>
        /// The active end of a relation.
        /// </summary>
        public EmbeddedObjectType AssociationType { get; set; }

        /// <summary>
        /// A class is not extensible and can only extend interfaces.
        /// </summary>
        public EmbeddedObjectType Class { get; }

        /// <summary>
        /// A Composite can be on either side of a relation.
        /// </summary>
        public EmbeddedObjectType Composite { get; }

        /// <summary>
        /// A Domain groups related DomainObjects.
        /// Domains can inherit from other domains.
        /// </summary>
        public EmbeddedObjectType Domain { get; set; }

        /// <summary>
        /// The composite of the association type.
        /// </summary>
        public EmbeddedManyToOneRoleType AssociationTypeComposite { get; set; }

        /// <summary>
        /// The direct supertypes of the composite.
        /// </summary>
        public EmbeddedManyToManyRoleType CompositeDirectSupertypes { get; set; }

        /// <summary>
        /// The members of the Domain.
        /// </summary>
        public EmbeddedManyToManyRoleType DomainMembers { get; set; }

        /// <summary>
        /// An interface is extensible by other interfaces.
        /// </summary>
        public EmbeddedObjectType Interface { get; }

        /// <summary>
        /// A MetaIdentifiableObject that can be looked up by a unique identifier.
        /// </summary>
        public EmbeddedObjectType MetaIdentifiableObject { get; }

        /// <summary>
        /// The id of the MetaIdentifiableObject.
        /// </summary>
        public EmbeddedUnitRoleType MetaIdentifiableObjectId { get; set; }

        /// <summary>
        /// The association type of the role type.
        /// </summary>
        public EmbeddedOneToOneRoleType RoleTypeAssociationType { get; set; }

        /// <summary>
        /// The assigned plural name of the role type.
        /// </summary>
        public EmbeddedUnitRoleType RoleTypeAssignedPluralName { get; set; }

        /// <summary>
        /// The derived plural name of the role type.
        /// </summary>
        public EmbeddedUnitRoleType RoleTypeDerivedPluralName { get; set; }

        /// <summary>
        /// The object type of the role type.
        /// </summary>
        public EmbeddedManyToOneRoleType RoleTypeObjectType { get; set; }

        /// <summary>
        /// The singular name of the role type.
        /// </summary>
        public EmbeddedUnitRoleType RoleTypeSingularName { get; set; }

        /// <summary>
        /// A MethodType describes a method.
        /// </summary>
        public EmbeddedObjectType MethodType { get; set; }

        /// <summary>
        /// An ObjectType is a shared interface implemented by Unit and Composite types.
        /// </summary>
        public EmbeddedObjectType ObjectType { get; }

        /// <summary>
        /// The assigned plural name of the object type.
        /// </summary>
        public EmbeddedUnitRoleType ObjectTypeAssignedPluralName { get; set; }

        /// <summary>
        /// The derived plural name of the object type.
        /// </summary>
        public EmbeddedUnitRoleType ObjectTypeDerivedPluralName { get; set; }

        /// <summary>
        /// The singular name of the object type.
        /// </summary>
        public EmbeddedUnitRoleType ObjectTypeSingularName { get; set; }

        /// <summary>
        /// An OperandType is a shared interface implemented by Method and Relation types.
        /// </summary>
        public EmbeddedObjectType OperandType { get; set; }

        /// <summary>
        /// A RelationEndType describes either the association or role end point of a relation.
        /// </summary>
        public EmbeddedObjectType RelationEndType { get; set; }

        /// <summary>
        /// The passive end of a relation.
        /// </summary>
        public EmbeddedObjectType RoleType { get; set; }

        /// <summary>
        /// A Unit can only be on the role side of a relation.
        /// </summary>
        public EmbeddedObjectType Unit { get; }

        /// <summary>
        /// A Workspace allows objects to be checked in and out of a database.
        /// </summary>
        public EmbeddedObjectType Workspace { get; set; }

        /// <summary>
        /// The members of the Workspace.
        /// </summary>
        public EmbeddedManyToManyRoleType WorkspaceMembers { get; set; }
    }
}
