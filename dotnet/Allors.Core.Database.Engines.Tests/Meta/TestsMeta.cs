namespace Allors.Core.Database.Engines.Tests.Meta;

using System;
using System.Linq;
using Allors.Core.Database.Meta;
using Allors.Core.Database.Meta.Domain;
using Allors.Core.Meta;

/// <summary>
/// Test Meta.
/// </summary>
public static class TestsMeta
{
    /// <summary>
    /// Populates meta with Tests types.
    /// </summary>
    public static void Populate(Meta m)
    {
        // Meta Domain
        var metaMeta = m.MetaMeta;
        var byId = m.Objects.ToDictionary(v => (Guid)v[metaMeta.MetaObjectId()]!, v => v);

        var d = (Domain)byId[TestsIds.AllorsTests];

        var i1 = m.AddInterface(d, TestsIds.I1, "I1");
        var i2 = m.AddInterface(d, TestsIds.I2, "I2");
        var i12 = m.AddInterface(d, TestsIds.I12, "I2");
        var s1 = m.AddInterface(d, TestsIds.S1, "S1");
        var s2 = m.AddInterface(d, TestsIds.S2, "S2");

        var c1 = m.AddClass(d, TestsIds.C1, "C1");
        var c2 = m.AddClass(d, TestsIds.C2, "C2");
        var c3 = m.AddClass(d, TestsIds.C3, "C3");
        var c4 = m.AddClass(d, TestsIds.C4, "C4");

        m.AddInheritance(d, new Guid("9b0d35eb-c635-43d4-92fc-3e5352c51272"), i12, m.Object());
        m.AddInheritance(d, new Guid("1823df02-c77a-412e-87f3-36457b6f60ac"), i1, s1, i12);
        m.AddInheritance(d, new Guid("1685a94f-14d6-45e7-98e3-b897959e0328"), i2, s2, i12);
        m.AddInheritance(d, new Guid("e890b0b3-1f02-4c5d-bd05-c23d36cefde9"), c1, i1);
        m.AddInheritance(d, new Guid("11ac4d4b-c835-4ef1-aafc-35a15d3f50ae"), c2, i2);

        m.AddStringRelation(d, new Guid("1CCF5BAC-3ED4-490B-9B09-822E045FB0CE"), TestsIds.I1AllorsString, i1, m.String(), "I1AllorsString");
        m.AddStringRelation(d, new Guid("B9434972-9B14-403F-91D3-EE2A0866A5D4"), TestsIds.C1AllorsString, c1, m.String(), "C1AllorsString");
        m.AddStringRelation(d, new Guid("F86DC318-AEB0-4311-B26D-DC74003281F1"), TestsIds.C2AllorsString, c2, m.String(), "C2AllorsString");
        m.AddStringRelation(d, new Guid("5B9E7A1B-4CA2-45D4-8FEB-829CF2115F33"), TestsIds.C3AllorsString, c3, m.String(), "C3AllorsString");
        m.AddStringRelation(d, new Guid("2B0B3AAE-0F9E-43CF-BF0C-2CD38C3D08F6"), TestsIds.C4AllorsString, c4, m.String(), "C4AllorsString");

        m.AddOneToOneRelation(d, new Guid("3C696258-F21A-4A01-8791-6C978173CC0E"), TestsIds.C1C1OneToOne, c1, c1, "C1OneToOne");
        m.AddOneToOneRelation(d, new Guid("BFB9C78A-55C4-433D-9A19-F848CBE30254"), TestsIds.C1I1OneToOne, c1, i1, "I1OneToOne");
        m.AddOneToOneRelation(d, new Guid("CC911BE0-EA31-4192-9E4F-269A1B2051C9"), TestsIds.C1C2OneToOne, c1, c2, "C2OneToOne");
        m.AddOneToOneRelation(d, new Guid("84A18359-0BA3-4074-B111-CBD00069B657"), TestsIds.C1I2OneToOne, c1, i2, "I2OneToOne");
        m.AddOneToOneRelation(d, new Guid("F8E094DD-3D9E-45DD-869A-F20DFF609F0D"), TestsIds.C1S2OneToOne, c1, s2, "S2OneToOne");
        m.AddOneToOneRelation(d, new Guid("1CDC24F5-9D1D-42AC-AD6F-4FDED79E915E"), TestsIds.C2C1OneToOne, c2, c1, "C1OneToOne");
        m.AddOneToOneRelation(d, new Guid("A69E6BD3-A621-4A39-A271-66D8BB188604"), TestsIds.C2C2OneToOne, c2, c2, "C2OneToOne");

        m.AddManyToOneRelation(d, new Guid("ECC71685-4003-4A85-BFD3-2A90BE7DA2AA"), TestsIds.C1C1ManyToOne, c1, c1, "C1ManyToOne");
        m.AddManyToOneRelation(d, new Guid("111A55E0-84F9-4A55-8004-536CB9773F58"), TestsIds.C1C2ManyToOne, c1, c2, "C2ManyToOne");
        m.AddManyToOneRelation(d, new Guid("AA9BC541-96C4-4EE8-8701-DAC5243C1CB4"), TestsIds.C1I2ManyToOne, c1, i2, "I2ManyToOne");
        m.AddManyToOneRelation(d, new Guid("D46333C0-18F6-4A69-A08B-49994CA9BD21"), TestsIds.C1I2ManyToOne, c1, s2, "I2ManyToOne");
        m.AddManyToOneRelation(d, new Guid("BD7399C4-DBC4-4633-9368-36405F9CF43D"), TestsIds.C2C1ManyToOne, c2, c1, "I2ManyToOne");

        m.AddOneToManyRelation(d, new Guid("3650DE27-3D66-49FB-B471-E05B2D3DFC9F"), TestsIds.C1C1OneToMany, c1, c1, "C1OneToMany");
        m.AddOneToManyRelation(d, new Guid("74BE82C7-1E74-4D72-9D78-38BD9C0E2A38"), TestsIds.C1C2OneToMany, c1, c2, "C2OneToMany");
        m.AddOneToManyRelation(d, new Guid("73849043-6873-4D35-91C4-3D629E2DA3E9"), TestsIds.C1I2OneToMany, c1, i2, "I2OneToMany");
        m.AddOneToManyRelation(d, new Guid("1D063D30-F277-4E04-BDA9-5A3086C9B5B7"), TestsIds.C2C1OneToMany, c2, c1, "C1OneToMany");

        m.AddManyToManyRelation(d, new Guid("8DF1A750-42DC-486E-B1E3-D904BAC254FD"), TestsIds.C1C1ManyToMany, c1, c1, "C1ManyToMany");
        m.AddManyToManyRelation(d, new Guid("86BA3009-2CB2-41A3-BACE-16730853C04E"), TestsIds.C1C2ManyToMany, c1, c2, "C2ManyToMany");
        m.AddManyToManyRelation(d, new Guid("88135E97-2D7A-480D-90BA-AB1DEE5BAE2C"), TestsIds.C1I2ManyToMany, c1, i2, "I2ManyToMany");
        m.AddManyToManyRelation(d, new Guid("135F8E64-A62D-4167-B0ED-2C0A97FCAF2E"), TestsIds.C2C1ManyToMany, c2, c1, "C1ManyToMany");
    }
 }
