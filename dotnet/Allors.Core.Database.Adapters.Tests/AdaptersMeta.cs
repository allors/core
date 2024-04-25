namespace Allors.Core.Database.Adapters.Tests
{
    using System;
    using Allors.Core.Database.Meta;
    using Allors.Core.Database.Meta.Handles;
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
            this.C2 = this.NewClass(new Guid("800483A5-E060-4AA3-9070-81110F8D78DF"), "C2");
            this.C3 = this.NewClass(new Guid("3F819AFC-A9BE-4CF7-B049-23463B0ED383"), "C3");
            this.C4 = this.NewClass(new Guid("FBADE605-8CC2-4590-87E4-7CBCDF3F78D6"), "C4");

            (_, this.I1AllorsString) = this.NewUnitRelationEndTypes(new Guid("1CCF5BAC-3ED4-490B-9B09-822E045FB0CE"), new Guid("F6D47ECD-453A-45C7-B681-9DAFB0983867"), this.I1, coreMeta.String, "I1AllorsString");
            (_, this.C1AllorsString) = this.NewUnitRelationEndTypes(new Guid("B9434972-9B14-403F-91D3-EE2A0866A5D4"), new Guid("F8C575CE-286F-45A4-BA90-B6FAFA282D66"), this.C1, coreMeta.String, "C1AllorsString");
            (_, this.C2AllorsString) = this.NewUnitRelationEndTypes(new Guid("F86DC318-AEB0-4311-B26D-DC74003281F1"), new Guid("DB1F153D-65C2-4D05-A69D-601FD92CAD05"), this.C2, coreMeta.String, "C2AllorsString");
            (_, this.C3AllorsString) = this.NewUnitRelationEndTypes(new Guid("5B9E7A1B-4CA2-45D4-8FEB-829CF2115F33"), new Guid("F5EF2461-ED10-496B-A5C1-3AFFB4C29A8E"), this.C3, coreMeta.String, "C3AllorsString");
            (_, this.C4AllorsString) = this.NewUnitRelationEndTypes(new Guid("2B0B3AAE-0F9E-43CF-BF0C-2CD38C3D08F6"), new Guid("50FE6867-2E78-4D37-B3F7-CD04135B1230"), this.C4, coreMeta.String, "C4AllorsString");

            (_, this.C1C1ManyToOne) = this.NewManyToOneRelationEndTypes(new Guid("ECC71685-4003-4A85-BFD3-2A90BE7DA2AA"), new Guid("67912ABF-9A24-47B4-8C22-E2BE15FE94B3"), this.C1, this.C1, "C1ManyToOne");
        }

        public CoreMeta CoreMeta { get; }

        public ClassHandle C1 { get; }

        public ManyToOneRoleTypeHandle C1C1ManyToOne { get; }

        public ClassHandle C2 { get; }

        public ClassHandle C3 { get; }

        public ClassHandle C4 { get; }

        public Interface I1 { get; }

        public UnitRoleTypeHandleHandle I1AllorsString { get; }

        public UnitRoleTypeHandleHandle C1AllorsString { get; }

        public UnitRoleTypeHandleHandle C2AllorsString { get; }

        public UnitRoleTypeHandleHandle C3AllorsString { get; }

        public UnitRoleTypeHandleHandle C4AllorsString { get; }

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
        public UnitHandle NewUnit(Guid id, string singularName, string? assignedPluralName = null) => this.CoreMeta.NewUnit(id, singularName, assignedPluralName);

        /// <summary>
        /// Creates a new interface.
        /// </summary>
        public Interface NewInterface(Guid id, string singularName, string? assignedPluralName = null) => this.CoreMeta.NewInterface(id, singularName, assignedPluralName);

        /// <summary>
        /// Creates a new class.
        /// </summary>
        public ClassHandle NewClass(Guid id, string singularName, string? assignedPluralName = null) => this.CoreMeta.NewClass(id, singularName, assignedPluralName);

        private (UnitAssociationTypeHandleHandle AssociationType, UnitRoleTypeHandleHandle RoleType) NewUnitRelationEndTypes(Guid associationTypeId, Guid roleTypeId, CompositeHandle associationCompositeHandle, UnitHandle unitHandle, string singularName, string? assignedPluralName = null)
            => this.CoreMeta.NewUnitRelationEndTypes(associationTypeId, roleTypeId, associationCompositeHandle, unitHandle, singularName, assignedPluralName);

        private (ManyToOneAssociationTypeHandle AssociationType, ManyToOneRoleTypeHandle RoleType) NewManyToOneRelationEndTypes(Guid associationTypeId, Guid roleTypeId, CompositeHandle associationCompositeHandle, CompositeHandle roleCompositeHandle, string singularName, string? assignedPluralName = null)
            => this.CoreMeta.NewManyToOneRelationEndTypes(associationTypeId, roleTypeId, associationCompositeHandle, roleCompositeHandle, singularName, assignedPluralName);
    }
}
