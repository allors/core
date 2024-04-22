namespace Allors.Core.Database.Adapters.Tests
{
    using System;
    using Allors.Core.Database.Meta;
    using Allors.Embedded.Meta;

    /// <summary>
    /// Meta for Tests.
    /// </summary>
    public sealed class AdaptersMeta
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdaptersMeta"/> class.
        /// </summary>
        public AdaptersMeta(CoreMeta coreMeta)
        {
            this.CoreMeta = coreMeta;

            this.I1 = this.NewInterface(new Guid("57F22F6E-640A-4F70-BFD8-B205BC9BAF68"), "I1");
            this.C1 = this.NewClass(new Guid("840A24C6-7E62-4540-87FD-69284FD58935"), "C1");

            (_, this.I1String) = this.NewUnitRelationEndTypes(new Guid("1CCF5BAC-3ED4-490B-9B09-822E045FB0CE"), new Guid("F6D47ECD-453A-45C7-B681-9DAFB0983867"), this.I1, coreMeta.String, "I1String");
        }

        public CoreMeta CoreMeta { get; }

        public Class C1 { get; }

        public Interface I1 { get; }

        public UnitRoleType I1String { get; set; }

        /// <summary>
        /// Creates a new meta class.
        /// </summary>
        public EmbeddedObjectType NewMetaClass(string name) => this.CoreMeta.NewMetaClass(name);

        /// <summary>
        /// Creates a new meta interface.
        /// </summary>
        public EmbeddedObjectType NewMetaInterface(string name) => this.CoreMeta.NewMetaInterface(name);

        /// <summary>
        /// Creates a new unit.
        /// </summary>
        public Unit NewUnit(Guid id, string singularName, string? assignedPluralName = null) => this.CoreMeta.NewUnit(id, singularName, assignedPluralName);

        /// <summary>
        /// Creates a new interface.
        /// </summary>
        public Interface NewInterface(Guid id, string singularName, string? assignedPluralName = null) => this.CoreMeta.NewInterface(id, singularName, assignedPluralName);

        /// <summary>
        /// Creates a new class.
        /// </summary>
        public Class NewClass(Guid id, string singularName, string? assignedPluralName = null) => this.CoreMeta.NewClass(id, singularName, assignedPluralName);

        private (UnitAssociationType AssociationType, UnitRoleType RoleType) NewUnitRelationEndTypes(Guid associationTypeId, Guid roleTypeId, Composite associationComposite, Unit roleUnit, string singularName, string? assignedPluralName = null)
            => this.CoreMeta.NewUnitRelationEndTypes(associationTypeId, roleTypeId, associationComposite, roleUnit, singularName, assignedPluralName);
    }
}
