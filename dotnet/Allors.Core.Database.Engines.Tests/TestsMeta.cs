namespace Allors.Core.Database.Engines.Tests;

using System;
using Allors.Core.Database.Meta;
using Allors.Core.Database.Meta.Domain;

/// <summary>
/// MetaMeta for AllorsTests.
/// </summary>
public sealed class TestsMeta
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TestsMeta"/> class.
    /// </summary>
    public TestsMeta(CoreMeta m)
    {
        this.CoreMeta = m;

        this.MetaMeta = new TestsMetaMeta(m.CoreMetaMeta);

        this.AllorsTests = m.AddDomain(this.MetaMeta.AllorsTests.Id, this.MetaMeta.AllorsTests.Name);

        this.I1 = m.AddInterface(this.AllorsTests, new Guid("57F22F6E-640A-4F70-BFD8-B205BC9BAF68"), "I1");
        this.I2 = m.AddInterface(this.AllorsTests, new Guid("E3B0C7A5-172D-41BB-9003-3F83671340D4"), "I2");
        this.I12 = m.AddInterface(this.AllorsTests, new Guid("F7983E4F-CAE4-498F-900E-DAFADCE24F1B"), "I2");
        this.S1 = m.AddInterface(this.AllorsTests, new Guid("1E98E9B0-A749-45E4-8218-968A716D9EA2"), "S1");
        this.S2 = m.AddInterface(this.AllorsTests, new Guid("BE622486-50F2-44C8-9BD8-549A2145CD62"), "S2");

        this.C1 = m.AddClass(this.AllorsTests, new Guid("840A24C6-7E62-4540-87FD-69284FD58935"), "C1");
        this.C2 = m.AddClass(this.AllorsTests, new Guid("800483A5-E060-4AA3-9070-81110F8D78DF"), "C2");
        this.C3 = m.AddClass(this.AllorsTests, new Guid("3F819AFC-A9BE-4CF7-B049-23463B0ED383"), "C3");
        this.C4 = m.AddClass(this.AllorsTests, new Guid("FBADE605-8CC2-4590-87E4-7CBCDF3F78D6"), "C4");

        m.AddInheritance(this.AllorsTests, this.I12, m.Object);
        m.AddInheritance(this.AllorsTests, this.I1, this.S1, this.I12);
        m.AddInheritance(this.AllorsTests, this.I2, this.S2, this.I12);
        m.AddInheritance(this.AllorsTests, this.C1, this.I1);
        m.AddInheritance(this.AllorsTests, this.C2, this.I2);

        /*
        (_, this.I1AllorsBoolean) = m.AddUnitRelation(new Guid("1CCF5BAC-3ED4-490B-9B09-822E045FB0CE"), new Guid("F6D47ECD-453A-45C7-B681-9DAFB0983867"), this.I1, m.Boolean, "I1AllorsBoolean");
        (_, this.C1AllorsBoolean) = m.AddUnitRelation(new Guid("B9434972-9B14-403F-91D3-EE2A0866A5D4"), new Guid("F8C575CE-286F-45A4-BA90-B6FAFA282D66"), this.C1, m.Boolean, "C1AllorsBoolean");
        (_, this.C2AllorsBoolean) = m.AddUnitRelation(new Guid("F86DC318-AEB0-4311-B26D-DC74003281F1"), new Guid("DB1F153D-65C2-4D05-A69D-601FD92CAD05"), this.C2, m.Boolean, "C2AllorsBoolean");
        (_, this.C3AllorsBoolean) = m.AddUnitRelation(new Guid("5B9E7A1B-4CA2-45D4-8FEB-829CF2115F33"), new Guid("F5EF2461-ED10-496B-A5C1-3AFFB4C29A8E"), this.C3, m.Boolean, "C3AllorsBoolean");
        (_, this.C4AllorsBoolean) = m.AddUnitRelation(new Guid("2B0B3AAE-0F9E-43CF-BF0C-2CD38C3D08F6"), new Guid("50FE6867-2E78-4D37-B3F7-CD04135B1230"), this.C4, m.Boolean, "C4AllorsBoolean");

        (_, this.I1AllorsDecimal) = m.AddUnitRelation(new Guid("1CCF5BAC-3ED4-490B-9B09-822E045FB0CE"), new Guid("F6D47ECD-453A-45C7-B681-9DAFB0983867"), this.I1, m.Decimal, "I1AllorsDecimal");
        (_, this.C1AllorsDecimal) = m.AddUnitRelation(new Guid("B9434972-9B14-403F-91D3-EE2A0866A5D4"), new Guid("F8C575CE-286F-45A4-BA90-B6FAFA282D66"), this.C1, m.Decimal, "C1AllorsDecimal");
        (_, this.C2AllorsDecimal) = m.AddUnitRelation(new Guid("F86DC318-AEB0-4311-B26D-DC74003281F1"), new Guid("DB1F153D-65C2-4D05-A69D-601FD92CAD05"), this.C2, m.Decimal, "C2AllorsDecimal");
        (_, this.C3AllorsDecimal) = m.AddUnitRelation(new Guid("5B9E7A1B-4CA2-45D4-8FEB-829CF2115F33"), new Guid("F5EF2461-ED10-496B-A5C1-3AFFB4C29A8E"), this.C3, m.Decimal, "C3AllorsDecimal");
        (_, this.C4AllorsDecimal) = m.AddUnitRelation(new Guid("2B0B3AAE-0F9E-43CF-BF0C-2CD38C3D08F6"), new Guid("50FE6867-2E78-4D37-B3F7-CD04135B1230"), this.C4, m.Decimal, "C4AllorsDecimal");

        (_, this.I1AllorsDouble) = m.AddUnitRelation(new Guid("1CCF5BAC-3ED4-490B-9B09-822E045FB0CE"), new Guid("F6D47ECD-453A-45C7-B681-9DAFB0983867"), this.I1, m.Double, "I1AllorsDouble");
        (_, this.C1AllorsDouble) = m.AddUnitRelation(new Guid("B9434972-9B14-403F-91D3-EE2A0866A5D4"), new Guid("F8C575CE-286F-45A4-BA90-B6FAFA282D66"), this.C1, m.Double, "C1AllorsDouble");
        (_, this.C2AllorsDouble) = m.AddUnitRelation(new Guid("F86DC318-AEB0-4311-B26D-DC74003281F1"), new Guid("DB1F153D-65C2-4D05-A69D-601FD92CAD05"), this.C2, m.Double, "C2AllorsDouble");
        (_, this.C3AllorsDouble) = m.AddUnitRelation(new Guid("5B9E7A1B-4CA2-45D4-8FEB-829CF2115F33"), new Guid("F5EF2461-ED10-496B-A5C1-3AFFB4C29A8E"), this.C3, m.Double, "C3AllorsDouble");
        (_, this.C4AllorsDouble) = m.AddUnitRelation(new Guid("2B0B3AAE-0F9E-43CF-BF0C-2CD38C3D08F6"), new Guid("50FE6867-2E78-4D37-B3F7-CD04135B1230"), this.C4, m.Double, "C4AllorsDouble");

        (_, this.I1AllorsInteger) = m.AddUnitRelation(new Guid("1CCF5BAC-3ED4-490B-9B09-822E045FB0CE"), new Guid("F6D47ECD-453A-45C7-B681-9DAFB0983867"), this.I1, m.Integer, "I1AllorsInteger");
        (_, this.C1AllorsInteger) = m.AddUnitRelation(new Guid("B9434972-9B14-403F-91D3-EE2A0866A5D4"), new Guid("F8C575CE-286F-45A4-BA90-B6FAFA282D66"), this.C1, m.Integer, "C1AllorsInteger");
        (_, this.C2AllorsInteger) = m.AddUnitRelation(new Guid("F86DC318-AEB0-4311-B26D-DC74003281F1"), new Guid("DB1F153D-65C2-4D05-A69D-601FD92CAD05"), this.C2, m.Integer, "C2AllorsInteger");
        (_, this.C3AllorsInteger) = m.AddUnitRelation(new Guid("5B9E7A1B-4CA2-45D4-8FEB-829CF2115F33"), new Guid("F5EF2461-ED10-496B-A5C1-3AFFB4C29A8E"), this.C3, m.Integer, "C3AllorsInteger");
        (_, this.C4AllorsInteger) = m.AddUnitRelation(new Guid("2B0B3AAE-0F9E-43CF-BF0C-2CD38C3D08F6"), new Guid("50FE6867-2E78-4D37-B3F7-CD04135B1230"), this.C4, m.Integer, "C4AllorsInteger");
        */

        (_, this.I1AllorsString) = m.AddStringRelation(this.AllorsTests, new Guid("1CCF5BAC-3ED4-490B-9B09-822E045FB0CE"), new Guid("F6D47ECD-453A-45C7-B681-9DAFB0983867"), this.I1, m.String, "I1AllorsString");
        (_, this.C1AllorsString) = m.AddStringRelation(this.AllorsTests, new Guid("B9434972-9B14-403F-91D3-EE2A0866A5D4"), new Guid("F8C575CE-286F-45A4-BA90-B6FAFA282D66"), this.C1, m.String, "C1AllorsString");
        (_, this.C2AllorsString) = m.AddStringRelation(this.AllorsTests, new Guid("F86DC318-AEB0-4311-B26D-DC74003281F1"), new Guid("DB1F153D-65C2-4D05-A69D-601FD92CAD05"), this.C2, m.String, "C2AllorsString");
        (_, this.C3AllorsString) = m.AddStringRelation(this.AllorsTests, new Guid("5B9E7A1B-4CA2-45D4-8FEB-829CF2115F33"), new Guid("F5EF2461-ED10-496B-A5C1-3AFFB4C29A8E"), this.C3, m.String, "C3AllorsString");
        (_, this.C4AllorsString) = m.AddStringRelation(this.AllorsTests, new Guid("2B0B3AAE-0F9E-43CF-BF0C-2CD38C3D08F6"), new Guid("50FE6867-2E78-4D37-B3F7-CD04135B1230"), this.C4, m.String, "C4AllorsString");

        (this.C1WhereC1OneToOne, this.C1C1OneToOne) = m.AddOneToOneRelation(this.AllorsTests, new Guid("3C696258-F21A-4A01-8791-6C978173CC0E"), new Guid("7497C880-94D3-464A-B15B-402AE4444533"), this.C1, this.C1, "C1OneToOne");
        (this.C1WhereI1OneToOne, this.C1I1OneToOne) = m.AddOneToOneRelation(this.AllorsTests, new Guid("BFB9C78A-55C4-433D-9A19-F848CBE30254"), new Guid("105397E3-150F-4FAD-9D7F-95A497ECF812"), this.C1, this.I1, "I1OneToOne");
        (this.C1WhereC2OneToOne, this.C1C2OneToOne) = m.AddOneToOneRelation(this.AllorsTests, new Guid("CC911BE0-EA31-4192-9E4F-269A1B2051C9"), new Guid("9CE1875E-B8F3-4A0C-A4FC-B3AE439CBE1F"), this.C1, this.C2, "C2OneToOne");
        (this.C1WhereI2OneToOne, this.C1I2OneToOne) = m.AddOneToOneRelation(this.AllorsTests, new Guid("84A18359-0BA3-4074-B111-CBD00069B657"), new Guid("B520451B-7055-49AF-9BA1-3A07CC1BD257"), this.C1, this.I2, "I2OneToOne");
        (this.C1WhereS2OneToOne, this.C1S2OneToOne) = m.AddOneToOneRelation(this.AllorsTests, new Guid("F8E094DD-3D9E-45DD-869A-F20DFF609F0D"), new Guid("E5069995-CF03-4A8A-BADB-7CA85AE85AA4"), this.C1, this.S2, "S2OneToOne");
        (this.C2WhereC1OneToOne, this.C2C1OneToOne) = m.AddOneToOneRelation(this.AllorsTests, new Guid("1CDC24F5-9D1D-42AC-AD6F-4FDED79E915E"), new Guid("5A548C31-1F7E-443E-A0E8-E222572BF5D7"), this.C2, this.C1, "C1OneToOne");
        (this.C2WhereC2OneToOne, this.C2C2OneToOne) = m.AddOneToOneRelation(this.AllorsTests, new Guid("A69E6BD3-A621-4A39-A271-66D8BB188604"), new Guid("4C40A2D1-EA3D-43DD-A0E1-44E162E1CBCB"), this.C2, this.C2, "C2OneToOne");

        (this.C1sWhereC1ManyToOne, this.C1C1ManyToOne) = m.AddManyToOneRelation(this.AllorsTests, new Guid("ECC71685-4003-4A85-BFD3-2A90BE7DA2AA"), new Guid("67912ABF-9A24-47B4-8C22-E2BE15FE94B3"), this.C1, this.C1, "C1ManyToOne");
        (this.C1sWhereC2ManyToOne, this.C1C2ManyToOne) = m.AddManyToOneRelation(this.AllorsTests, new Guid("111A55E0-84F9-4A55-8004-536CB9773F58"), new Guid("2C8334EF-A8CD-4C11-BD4F-E12A9E45F062"), this.C1, this.C2, "C2ManyToOne");
        (this.C1sWhereI2ManyToOne, this.C1I2ManyToOne) = m.AddManyToOneRelation(this.AllorsTests, new Guid("AA9BC541-96C4-4EE8-8701-DAC5243C1CB4"), new Guid("E843ABB9-2A0E-4254-B198-83D4303DA99A"), this.C1, this.I2, "I2ManyToOne");
        (this.C1sWhereS2ManyToOne, this.C1S2ManyToOne) = m.AddManyToOneRelation(this.AllorsTests, new Guid("D46333C0-18F6-4A69-A08B-49994CA9BD21"), new Guid("0EA6BD81-0B24-4108-9BD8-050460252FA9"), this.C1, this.S2, "I2ManyToOne");
        (this.C2sWhereC1ManyToOne, this.C2C1ManyToOne) = m.AddManyToOneRelation(this.AllorsTests, new Guid("BD7399C4-DBC4-4633-9368-36405F9CF43D"), new Guid("C8E3F31B-1DF6-49E4-A990-E580A69AB506"), this.C2, this.C1, "I2ManyToOne");

        (this.C1WhereC1OneToMany, this.C1C1OneToMany) = m.AddOneToManyRelation(this.AllorsTests, new Guid("3650DE27-3D66-49FB-B471-E05B2D3DFC9F"), new Guid("10251B8B-A877-4397-9192-0B09583FB350"), this.C1, this.C1, "C1OneToMany");
        (this.C1WhereC2OneToMany, this.C1C2OneToMany) = m.AddOneToManyRelation(this.AllorsTests, new Guid("74BE82C7-1E74-4D72-9D78-38BD9C0E2A38"), new Guid("D0396485-4000-4061-B4AA-B7319C94E83C"), this.C1, this.C2, "C2OneToMany");
        (this.C1WhereI2OneToMany, this.C1I2OneToMany) = m.AddOneToManyRelation(this.AllorsTests, new Guid("73849043-6873-4D35-91C4-3D629E2DA3E9"), new Guid("BB0CE828-1C4F-42F7-B54C-72994B32BEE8"), this.C1, this.I2, "I2OneToMany");
        (this.C2WhereC1OneToMany, this.C2C1OneToMany) = m.AddOneToManyRelation(this.AllorsTests, new Guid("1D063D30-F277-4E04-BDA9-5A3086C9B5B7"), new Guid("ED195F8A-E0A8-489F-B735-7E3889537669"), this.C2, this.C1, "C1OneToMany");

        (this.C1sWhereC1ManyToMany, this.C1C1ManyToMany) = m.AddManyToManyRelation(this.AllorsTests, new Guid("8DF1A750-42DC-486E-B1E3-D904BAC254FD"), new Guid("FF0EC2D0-8F95-47D1-A0C3-B4B43632E718"), this.C1, this.C1, "C1ManyToMany");
        (this.C1sWhereC2ManyToMany, this.C1C2ManyToMany) = m.AddManyToManyRelation(this.AllorsTests, new Guid("86BA3009-2CB2-41A3-BACE-16730853C04E"), new Guid("5D697E8C-73B5-4D0C-9150-167782FCD817"), this.C1, this.C2, "C2ManyToMany");
        (this.C1sWhereI2ManyToMany, this.C1I2ManyToMany) = m.AddManyToManyRelation(this.AllorsTests, new Guid("88135E97-2D7A-480D-90BA-AB1DEE5BAE2C"), new Guid("0CF41359-9AB9-4EFA-98DF-4D8C513ED853"), this.C1, this.I2, "I2ManyToMany");
        (this.C2sWhereC1ManyToMany, this.C2C1ManyToMany) = m.AddManyToManyRelation(this.AllorsTests, new Guid("135F8E64-A62D-4167-B0ED-2C0A97FCAF2E"), new Guid("20B66206-4404-4B2C-8246-912B54413620"), this.C2, this.C1, "C1ManyToMany");

        this.CoreMeta.Freeze();
    }

    public CoreMeta CoreMeta { get; }

    public TestsMetaMeta MetaMeta { get; }

    public Domain AllorsTests { get; }

    public Class C1 { get; }

    public OneToOneRoleType C1C1OneToOne { get; }

    public OneToOneAssociationType C1WhereC1OneToOne { get; }

    public OneToOneRoleType C1I1OneToOne { get; }

    public OneToOneAssociationType C1WhereI1OneToOne { get; }

    public OneToOneRoleType C1C2OneToOne { get; }

    public OneToOneAssociationType C1WhereC2OneToOne { get; }

    public OneToOneRoleType C1I2OneToOne { get; }

    public OneToOneAssociationType C1WhereI2OneToOne { get; }

    public OneToOneRoleType C1S2OneToOne { get; }

    public OneToOneAssociationType C1WhereS2OneToOne { get; }

    public OneToOneRoleType C2C1OneToOne { get; }

    public OneToOneAssociationType C2WhereC1OneToOne { get; }

    public OneToOneRoleType C2C2OneToOne { get; }

    public OneToOneAssociationType C2WhereC2OneToOne { get; }

    public ManyToOneRoleType C1C1ManyToOne { get; }

    public ManyToOneAssociationType C1sWhereC1ManyToOne { get; }

    public ManyToOneRoleType C1C2ManyToOne { get; }

    public ManyToOneAssociationType C1sWhereC2ManyToOne { get; }

    public ManyToOneRoleType C1I2ManyToOne { get; }

    public ManyToOneAssociationType C1sWhereI2ManyToOne { get; }

    public ManyToOneRoleType C1S2ManyToOne { get; }

    public ManyToOneAssociationType C1sWhereS2ManyToOne { get; }

    public ManyToOneRoleType C2C1ManyToOne { get; }

    public ManyToOneAssociationType C2sWhereC1ManyToOne { get; }

    public OneToManyRoleType C1C1OneToMany { get; }

    public OneToManyAssociationType C1WhereC1OneToMany { get; }

    public OneToManyRoleType C1C2OneToMany { get; }

    public OneToManyAssociationType C1WhereC2OneToMany { get; }

    public OneToManyRoleType C1I2OneToMany { get; }

    public OneToManyAssociationType C1WhereI2OneToMany { get; }

    public OneToManyRoleType C2C1OneToMany { get; }

    public OneToManyAssociationType C2WhereC1OneToMany { get; }

    public ManyToManyRoleType C1C1ManyToMany { get; }

    public ManyToManyAssociationType C1sWhereC1ManyToMany { get; }

    public ManyToManyRoleType C1C2ManyToMany { get; }

    public ManyToManyAssociationType C1sWhereC2ManyToMany { get; }

    public ManyToManyRoleType C1I2ManyToMany { get; }

    public ManyToManyAssociationType C1sWhereI2ManyToMany { get; }

    public ManyToManyRoleType C2C1ManyToMany { get; }

    public ManyToManyAssociationType C2sWhereC1ManyToMany { get; }

    public Class C2 { get; }

    public Class C3 { get; }

    public Class C4 { get; }

    public Interface I1 { get; }

    public Interface I2 { get; }

    public Interface S1 { get; }

    public Interface S2 { get; }

    public Interface I12 { get; }

    /*
    public UnitRoleType I1AllorsBoolean { get; }

    public UnitRoleType C1AllorsBoolean { get; }

    public UnitRoleType C2AllorsBoolean { get; }

    public UnitRoleType C3AllorsBoolean { get; }

    public UnitRoleType C4AllorsBoolean { get; }

    public UnitRoleType I1AllorsDecimal { get; }

    public UnitRoleType C1AllorsDecimal { get; }

    public UnitRoleType C2AllorsDecimal { get; }

    public UnitRoleType C3AllorsDecimal { get; }

    public UnitRoleType C4AllorsDecimal { get; }

    public UnitRoleType I1AllorsDouble { get; }

    public UnitRoleType C1AllorsDouble { get; }

    public UnitRoleType C2AllorsDouble { get; }

    public UnitRoleType C3AllorsDouble { get; }

    public UnitRoleType C4AllorsDouble { get; }

    public UnitRoleType I1AllorsInteger { get; }

    public UnitRoleType C1AllorsInteger { get; }

    public UnitRoleType C2AllorsInteger { get; }

    public UnitRoleType C3AllorsInteger { get; }

    public UnitRoleType C4AllorsInteger { get; }
    */

    public StringRoleType I1AllorsString { get; }

    public StringRoleType C1AllorsString { get; }

    public StringRoleType C2AllorsString { get; }

    public StringRoleType C3AllorsString { get; }

    public StringRoleType C4AllorsString { get; }
}
