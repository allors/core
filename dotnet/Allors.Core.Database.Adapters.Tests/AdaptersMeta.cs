namespace Allors.Core.Database.Adapters.Tests
{
    using System;
    using Allors.Core.Database.Meta;
    using Allors.Core.Database.Meta.Handles;

    /// <summary>
    /// Meta for Tests.
    /// </summary>
    public sealed class AdaptersMeta
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdaptersMeta"/> class.
        /// </summary>
        public AdaptersMeta(CoreMeta m)
        {
            this.CoreMeta = m;

            this.I1 = m.NewInterface(new Guid("57F22F6E-640A-4F70-BFD8-B205BC9BAF68"), "I1", null);
            this.I2 = m.NewInterface(new Guid("E3B0C7A5-172D-41BB-9003-3F83671340D4"), "I2", null);
            this.I12 = m.NewInterface(new Guid("F7983E4F-CAE4-498F-900E-DAFADCE24F1B"), "I2", null);

            this.C1 = m.NewClass(new Guid("840A24C6-7E62-4540-87FD-69284FD58935"), "C1", null);
            this.C2 = m.NewClass(new Guid("800483A5-E060-4AA3-9070-81110F8D78DF"), "C2", null);
            this.C3 = m.NewClass(new Guid("3F819AFC-A9BE-4CF7-B049-23463B0ED383"), "C3", null);
            this.C4 = m.NewClass(new Guid("FBADE605-8CC2-4590-87E4-7CBCDF3F78D6"), "C4", null);

            m.AddDirectSupertype(this.I12, m.Object);

            m.AddDirectSupertype(this.I1, this.I12);
            m.AddDirectSupertype(this.I2, this.I12);

            m.AddDirectSupertype(this.C1, this.I1);
            m.AddDirectSupertype(this.C2, this.I2);

            (_, this.I1AllorsString) = m.NewUnitRelation(new Guid("1CCF5BAC-3ED4-490B-9B09-822E045FB0CE"), new Guid("F6D47ECD-453A-45C7-B681-9DAFB0983867"), this.I1, m.String, "I1AllorsString", null);
            (_, this.C1AllorsString) = m.NewUnitRelation(new Guid("B9434972-9B14-403F-91D3-EE2A0866A5D4"), new Guid("F8C575CE-286F-45A4-BA90-B6FAFA282D66"), this.C1, m.String, "C1AllorsString", null);
            (_, this.C2AllorsString) = m.NewUnitRelation(new Guid("F86DC318-AEB0-4311-B26D-DC74003281F1"), new Guid("DB1F153D-65C2-4D05-A69D-601FD92CAD05"), this.C2, m.String, "C2AllorsString", null);
            (_, this.C3AllorsString) = m.NewUnitRelation(new Guid("5B9E7A1B-4CA2-45D4-8FEB-829CF2115F33"), new Guid("F5EF2461-ED10-496B-A5C1-3AFFB4C29A8E"), this.C3, m.String, "C3AllorsString", null);
            (_, this.C4AllorsString) = m.NewUnitRelation(new Guid("2B0B3AAE-0F9E-43CF-BF0C-2CD38C3D08F6"), new Guid("50FE6867-2E78-4D37-B3F7-CD04135B1230"), this.C4, m.String, "C4AllorsString", null);

            (this.C1WhereC1OneToOne, this.C1C1OneToOne) = m.NewOneToOneRelation(new Guid("3C696258-F21A-4A01-8791-6C978173CC0E"), new Guid("7497C880-94D3-464A-B15B-402AE4444533"), this.C1, this.C1, "C1OneToOne", null);
            (this.C1WhereI1OneToOne, this.C1I1OneToOne) = m.NewOneToOneRelation(new Guid("BFB9C78A-55C4-433D-9A19-F848CBE30254"), new Guid("105397E3-150F-4FAD-9D7F-95A497ECF812"), this.C1, this.I1, "I1OneToOne", null);
            (this.C1WhereC2OneToOne, this.C1C2OneToOne) = m.NewOneToOneRelation(new Guid("CC911BE0-EA31-4192-9E4F-269A1B2051C9"), new Guid("9CE1875E-B8F3-4A0C-A4FC-B3AE439CBE1F"), this.C1, this.C2, "C2OneToOne", null);
            (this.C1WhereI2OneToOne, this.C1I2OneToOne) = m.NewOneToOneRelation(new Guid("84A18359-0BA3-4074-B111-CBD00069B657"), new Guid("B520451B-7055-49AF-9BA1-3A07CC1BD257"), this.C1, this.I2, "I2OneToOne", null);
            (this.C1sWhereC1ManyToOne, this.C1C1ManyToOne) = m.NewManyToOneRelation(new Guid("ECC71685-4003-4A85-BFD3-2A90BE7DA2AA"), new Guid("67912ABF-9A24-47B4-8C22-E2BE15FE94B3"), this.C1, this.C1, "C1ManyToOne", null);
            (this.C1WhereC1C1one2many, this.C1C1OneToManies) = m.NewOneToManyRelation(new Guid("3650DE27-3D66-49FB-B471-E05B2D3DFC9F"), new Guid("10251B8B-A877-4397-9192-0B09583FB350"), this.C1, this.C1, "C1OneToMany", null);
        }

        public CoreMeta CoreMeta { get; }

        public Class C1 { get; }

        public OneToOneRoleType C1C1OneToOne { get; }

        public OneToOneAssociationType C1WhereC1OneToOne { get; }

        public OneToOneRoleType C1I1OneToOne { get; }

        public OneToOneAssociationType C1WhereI1OneToOne { get; }

        public OneToOneRoleType C1C2OneToOne { get; }

        public OneToOneAssociationType C1WhereC2OneToOne { get; }

        public OneToOneRoleType C1I2OneToOne { get; }

        public OneToOneAssociationType C1WhereI2OneToOne { get; }

        public ManyToOneRoleType C1C1ManyToOne { get; }

        public ManyToOneAssociationType C1sWhereC1ManyToOne { get; }

        public OneToManyRoleType C1C1OneToManies { get; }

        public OneToManyAssociationType C1WhereC1C1one2many { get; }

        public Class C2 { get; }

        public Class C3 { get; }

        public Class C4 { get; }

        public Interface I1 { get; }

        public Interface I2 { get; }

        public Interface I12 { get; }

        public UnitRoleType I1AllorsString { get; }

        public UnitRoleType C1AllorsString { get; }

        public UnitRoleType C2AllorsString { get; }

        public UnitRoleType C3AllorsString { get; }

        public UnitRoleType C4AllorsString { get; }
    }
}
