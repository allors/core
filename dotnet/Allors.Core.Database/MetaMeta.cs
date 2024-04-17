namespace Allors.Core.Database
{
    using System;
    using Allors.Embedded.Meta;

    /// <summary>
    /// Meta Core.
    /// </summary>
    public sealed class MetaMeta
    {
        /// <summary>
        /// Creates a new Core Meta Population.
        /// </summary>
        public MetaMeta()
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
            this.RelationType = meta.AddInterface("RelationType");
            this.RoleType = meta.AddClass("RoleType");
            this.Unit = meta.AddClass("Unit");
            this.Workspace = meta.AddClass("Workspace");

            // Inheritance
            this.AssociationType.AddDirectSupertype(this.RelationEndType);
            this.Class.AddDirectSupertype(this.Composite);
            this.Composite.AddDirectSupertype(this.ObjectType);
            this.Interface.AddDirectSupertype(this.Composite);
            this.MethodType.AddDirectSupertype(this.MetaIdentifiableObject);
            this.MethodType.AddDirectSupertype(this.OperandType);
            this.ObjectType.AddDirectSupertype(this.MetaIdentifiableObject);
            this.RelationType.AddDirectSupertype(this.MetaIdentifiableObject);
            this.RelationEndType.AddDirectSupertype(this.OperandType);
            this.RoleType.AddDirectSupertype(this.RelationEndType);
            this.Unit.AddDirectSupertype(this.ObjectType);

            // Relations
            this.CompositeAssociationTypes = this.EmbeddedMeta.AddManyToMany(this.Composite, this.AssociationType, "AssociationType");
            this.CompositeClasses = this.EmbeddedMeta.AddManyToMany(this.Composite, this.Class, "Class");
            this.CompositeComposites = this.EmbeddedMeta.AddManyToMany(this.Composite, this.Composite, "Composite");

            this.ObjectTypeAssignedPluralName = this.EmbeddedMeta.AddUnit<string>(this.ObjectType, "AssignedPluralName");
            this.ObjectTypeDerivedPluralName = this.EmbeddedMeta.AddUnit<string>(this.ObjectType, "DerivedPluralName");
            this.ObjectTypeSingularName = this.EmbeddedMeta.AddUnit<string>(this.ObjectType, "SingularName");

            this.MetaIdentifiableObjectId = this.EmbeddedMeta.AddUnit<Guid>(this.MetaIdentifiableObject, "Id");
            this.MetaIdentifiableObjectAssignedTag = this.EmbeddedMeta.AddUnit<string>(this.MetaIdentifiableObject, "AssignedTag");
            this.MetaIdentifiableObjectDerivedTag = this.EmbeddedMeta.AddUnit<string>(this.MetaIdentifiableObject, "DerivedTag");

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
        /// The composite's association types.
        /// </summary>
        public EmbeddedManyToManyRoleType CompositeAssociationTypes { get; set; }

        /// <summary>
        /// The composite's classes.
        /// </summary>
        public EmbeddedManyToManyRoleType CompositeClasses { get; set; }

        /// <summary>
        /// The composite's composites.
        /// </summary>
        public EmbeddedManyToManyRoleType CompositeComposites { get; set; }

        /// <summary>
        /// A Domain groups related DomainObjects.
        /// Domains can inherit from other domains.
        /// </summary>
        public EmbeddedObjectType Domain { get; set; }

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
        /// The assigned tag of the MetaIdentifiableObject.
        /// </summary>
        public EmbeddedUnitRoleType MetaIdentifiableObjectAssignedTag { get; set; }

        /// <summary>
        /// The derived tag of the MetaIdentifiableObject.
        /// </summary>
        public EmbeddedUnitRoleType MetaIdentifiableObjectDerivedTag { get; set; }

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
        /// The relation type.
        /// </summary>
        public EmbeddedObjectType RelationType { get; set; }

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
