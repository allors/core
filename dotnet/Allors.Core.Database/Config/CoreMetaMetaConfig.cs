namespace Allors.Core.Database.Config
{
    using System;
    using Allors.Embedded.Meta;

    /// <summary>
    /// Meta Core.
    /// </summary>
    public sealed class CoreMetaMetaConfig : IMetaMetaConfig
    {
        /// <summary>
        /// Creates a new Core Meta Population.
        /// </summary>
        public CoreMetaMetaConfig()
        {
            this.EmbeddedMeta = new EmbeddedMeta();

            var meta = this.EmbeddedMeta;

            // ObjectTypes
            var associationType = meta.AddClass("AssociationType");
            var @class = meta.AddClass("Class");
            var composite = meta.AddInterface("Composite");
            var domain = meta.AddClass("Domain");
            var @interface = meta.AddClass("Interface");
            var metaObject = meta.AddInterface("MetaObject");
            var methodType = meta.AddClass("MethodType");
            var objectType = meta.AddInterface("ObjectType");
            var operandType = meta.AddInterface("OperandType");
            var relationEndType = meta.AddInterface("RelationEndType");
            var roleType = meta.AddClass("RoleType");
            var type = meta.AddInterface("Type");
            var unit = meta.AddClass("Unit");
            var workspace = meta.AddClass("Workspace");

            // Inheritance
            associationType.AddDirectSupertype(relationEndType);
            @class.AddDirectSupertype(composite);
            composite.AddDirectSupertype(objectType);
            domain.AddDirectSupertype(metaObject);
            @interface.AddDirectSupertype(composite);
            methodType.AddDirectSupertype(operandType);
            objectType.AddDirectSupertype(type);
            operandType.AddDirectSupertype(type);
            relationEndType.AddDirectSupertype(operandType);
            roleType.AddDirectSupertype(relationEndType);
            type.AddDirectSupertype(metaObject);
            unit.AddDirectSupertype(objectType);
            workspace.AddDirectSupertype(metaObject);

            // Relations
            this.EmbeddedMeta.AddManyToOne(associationType, composite);

            this.EmbeddedMeta.AddManyToMany(composite, @interface, "DirectSupertype");

            this.EmbeddedMeta.AddManyToMany(domain, type);

            this.EmbeddedMeta.AddUnit<string>(objectType, "AssignedPluralName");
            this.EmbeddedMeta.AddUnit<string>(objectType, "DerivedPluralName");
            this.EmbeddedMeta.AddUnit<string>(objectType, "SingularName");

            this.EmbeddedMeta.AddUnit<Guid>(metaObject, "Id");

            this.EmbeddedMeta.AddOneToOne(roleType, associationType);
            this.EmbeddedMeta.AddUnit<string>(roleType, "AssignedPluralName");
            this.EmbeddedMeta.AddUnit<string>(roleType, "DerivedPluralName");
            this.EmbeddedMeta.AddManyToOne(roleType, objectType);
            this.EmbeddedMeta.AddUnit<string>(roleType, "SingularName");

            this.EmbeddedMeta.AddManyToMany(workspace, type);
        }

        /// <summary>
        /// The meta of Meta Core.
        /// </summary>
        public EmbeddedMeta EmbeddedMeta { get; }
    }
}
