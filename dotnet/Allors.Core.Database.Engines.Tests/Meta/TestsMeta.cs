namespace Allors.Core.Database.Engines.Tests.Meta;

using System;
using Allors.Core.Database.Meta;
using Allors.Core.Meta;

/// <summary>
/// Test Meta.
/// </summary>
public static class TestsMeta
{
    internal static readonly Guid AllorsTests = new("90ce782d-67cb-4732-aeb1-ac82939fe6c4");

    internal static readonly Guid C1 = new("94c7c90e-9651-4e92-ba4e-91f0f071bd5b");

    internal static readonly Guid C2 = new("68f144d6-4989-4129-8473-6ee1c2e7efb6");

    internal static readonly Guid C3 = new("7ae7c80e-a07a-4e69-9d0c-194fd1985eb0");

    internal static readonly Guid C4 = new("5f1f11b6-5f7d-4573-9a62-95b65a2b4b93");

    internal static readonly Guid I1 = new("825dc238-836e-427c-bfda-1220d2994de9");

    internal static readonly Guid I2 = new("bdee1cd8-b4fe-4cea-9e19-98feb46b4ccf");

    internal static readonly Guid S1 = new("3b6029ed-6a77-42c1-acd8-4ab5dfb822a0");

    internal static readonly Guid S2 = new("933aa0ec-7ee5-4cdb-9529-087d5b5a4720");

    internal static readonly Guid I12 = new("0fe83247-092f-4450-80d8-695c9d449dad");

    internal static readonly Guid C1C1OneToOne = new("674b8b6e-174f-42cd-beae-3fc64e7190f8");

    internal static readonly Guid C1WhereC1OneToOne = new("e9f0e464-a7cf-4c0b-bf62-b9a2e2471502");

    internal static readonly Guid C1I1OneToOne = new("cc8f234a-b82e-4a3a-bf27-2ff3e817c7d0");

    internal static readonly Guid C1WhereI1OneToOne = new("f410c8fb-6449-4fcc-9bed-c3ea5878045f");

    internal static readonly Guid C1C2OneToOne = new("2eb70c60-e8f0-4825-8cdf-af9f19650f02");

    internal static readonly Guid C1WhereC2OneToOne = new("d6dbd47a-7aad-4afd-9fc5-e0b933756f97");

    internal static readonly Guid C1I2OneToOne = new("fad66164-806e-4484-ac32-9352a10305ce");

    internal static readonly Guid C1WhereI2OneToOne = new("8f2485f9-d59f-4b4d-8bc6-1bc55f4b4394");

    internal static readonly Guid C1S2OneToOne = new("6fc3a7dc-9ec5-4db9-af5c-45d88534bb87");

    internal static readonly Guid C1WhereS2OneToOne = new("7b852e1b-3b78-4175-94d8-ed2f3b620495");

    internal static readonly Guid C2C1OneToOne = new("4f2481bc-0386-4a48-9b4a-3e528d989474");

    internal static readonly Guid C2WhereC1OneToOne = new("cfe6cbc7-1261-48d4-ae0d-194a9b96bdc6");

    internal static readonly Guid C2C2OneToOne = new("421dce6d-a80e-4eb6-9267-6849bdcad903");

    internal static readonly Guid C2WhereC2OneToOne = new("96a94da5-8020-409a-89b1-28a3d3ec154b");

    internal static readonly Guid C1C1ManyToOne = new("5ad418c3-46ec-43f3-939a-f43d5745561b");

    internal static readonly Guid C1sWhereC1ManyToOne = new("c03437c0-b6cb-4d09-bed2-bd7a5198b8e6");

    internal static readonly Guid C1C2ManyToOne = new("02d74903-4c1c-4e1e-8a78-28e5df8b602c");

    internal static readonly Guid C1sWhereC2ManyToOne = new("f89bed84-800c-49df-b619-7be252e1aef1");

    internal static readonly Guid C1I2ManyToOne = new("63707e47-89da-41d1-88e1-7a921a659521");

    internal static readonly Guid C1sWhereI2ManyToOne = new("52bc16bf-5084-4b8c-9694-6c6050f3acee");

    internal static readonly Guid C1S2ManyToOne = new("73682f7a-6dad-42ba-bb1f-aa0e08f30ed2");

    internal static readonly Guid C1sWhereS2ManyToOne = new("46222e3b-a57b-461f-bd42-08510f4da988");

    internal static readonly Guid C2C1ManyToOne = new("a8372e3d-3812-4f45-ad56-9834f5496f6a");

    internal static readonly Guid C2sWhereC1ManyToOne = new("d0b297c5-d070-4eaa-8175-0c5ca399b04f");

    internal static readonly Guid C1C1OneToMany = new("43eb1c7b-04f1-401a-b4be-9129659f0acd");

    internal static readonly Guid C1WhereC1OneToMany = new("53952569-bdb2-4fe3-a02d-a4be98332e6e");

    internal static readonly Guid C1C2OneToMany = new("19752730-49ab-469d-b070-9311672e94bc");

    internal static readonly Guid C1WhereC2OneToMany = new("1fe7e253-329c-4228-9102-4a8449996a21");

    internal static readonly Guid C1I2OneToMany = new("40c40d11-39d0-4692-b7b8-39894bc8947b");

    internal static readonly Guid C1WhereI2OneToMany = new("6a39f9ee-0569-466a-8a72-3b3dacb3318d");

    internal static readonly Guid C2C1OneToMany = new("5a960c58-38db-4892-8567-b5b14c03a8c4");

    internal static readonly Guid C2WhereC1OneToMany = new("15409a0d-2e0c-452f-88a5-75e84dc5e4e0");

    internal static readonly Guid C1C1ManyToMany = new("1eafe865-8fa8-4e9d-8e3e-d10f589b6f13");

    internal static readonly Guid C1sWhereC1ManyToMany = new("93ff6155-58c3-42eb-a97c-d7c5d6312683");

    internal static readonly Guid C1C2ManyToMany = new("0d6f964a-6d1a-4660-829d-083f4e2855c0");

    internal static readonly Guid C1sWhereC2ManyToMany = new("4ef23db0-c44c-4540-9eb6-fafc133482d7");

    internal static readonly Guid C1I2ManyToMany = new("ecc25815-e209-4a64-9d63-c530f5308dbd");

    internal static readonly Guid C1sWhereI2ManyToMany = new("57984f2a-2b87-4cae-9b14-77d4d3d061c5");

    internal static readonly Guid C2C1ManyToMany = new("093eead8-9ccd-4a6d-a2f2-f55e4ece016e");

    internal static readonly Guid C2sWhereC1ManyToMany = new("97c34eb8-7020-4d5c-bbad-f8fe15b5592d");

    internal static readonly Guid I1AllorsString = new("0a9ed174-fd82-4e3f-a41b-83edbfb593f6");

    internal static readonly Guid C1AllorsString = new("23254b09-cb4d-43b1-92fc-418ef57c95f1");

    internal static readonly Guid C2AllorsString = new("aa602af7-cd02-439b-b8d1-eb29e07ca353");

    internal static readonly Guid C3AllorsString = new("8a5cefd0-39c2-45da-a15d-9f9d06e148ae");

    internal static readonly Guid C4AllorsString = new("4255196c-2101-47c3-ac1f-178222cfe426");

    /// <summary>
    /// Populates meta with Tests types.
    /// </summary>
    public static void Populate(Meta m)
    {
        var d = m.AddDomain(AllorsTests, nameof(AllorsTests));

        var i1 = d.AddInterface(I1, nameof(I1));
        var i2 = d.AddInterface(I2, nameof(I2));
        var i12 = d.AddInterface(I12, nameof(I2));
        var s1 = d.AddInterface(S1, nameof(S1));
        var s2 = d.AddInterface(S2, nameof(S2));

        var c1 = d.AddClass(C1, nameof(C1));
        var c2 = d.AddClass(C2, nameof(C2));
        var c3 = d.AddClass(C3, nameof(C3));
        var c4 = d.AddClass(C4, nameof(C4));

        d.AddInheritance(i12, m.Object());
        d.AddInheritance(i1, s1, i12);
        d.AddInheritance(i2, s2, i12);
        d.AddInheritance(c1, i1);
        d.AddInheritance(c2, i2);

        d.AddStringRelation(new Guid("1CCF5BAC-3ED4-490B-9B09-822E045FB0CE"), I1AllorsString, i1, "I1AllorsString");
        d.AddStringRelation(new Guid("B9434972-9B14-403F-91D3-EE2A0866A5D4"), C1AllorsString, c1, "C1AllorsString");
        d.AddStringRelation(new Guid("F86DC318-AEB0-4311-B26D-DC74003281F1"), C2AllorsString, c2, "C2AllorsString");
        d.AddStringRelation(new Guid("5B9E7A1B-4CA2-45D4-8FEB-829CF2115F33"), C3AllorsString, c3, "C3AllorsString");
        d.AddStringRelation(new Guid("2B0B3AAE-0F9E-43CF-BF0C-2CD38C3D08F6"), C4AllorsString, c4, "C4AllorsString");

        d.AddOneToOneRelation(C1WhereC1OneToOne, C1C1OneToOne, c1, c1, "C1OneToOne");
        d.AddOneToOneRelation(C1WhereI1OneToOne, C1I1OneToOne, c1, i1, "I1OneToOne");
        d.AddOneToOneRelation(C1WhereC2OneToOne, C1C2OneToOne, c1, c2, "C2OneToOne");
        d.AddOneToOneRelation(C1WhereI2OneToOne, C1I2OneToOne, c1, i2, "I2OneToOne");
        d.AddOneToOneRelation(C1WhereS2OneToOne, C1S2OneToOne, c1, s2, "S2OneToOne");
        d.AddOneToOneRelation(C2WhereC1OneToOne, C2C1OneToOne, c2, c1, "C1OneToOne");
        d.AddOneToOneRelation(C2WhereC2OneToOne, C2C2OneToOne, c2, c2, "C2OneToOne");

        d.AddManyToOneRelation(C1sWhereC1ManyToOne, C1C1ManyToOne, c1, c1, "C1ManyToOne");
        d.AddManyToOneRelation(C1sWhereC2ManyToOne, C1C2ManyToOne, c1, c2, "C2ManyToOne");
        d.AddManyToOneRelation(C1sWhereI2ManyToOne, C1I2ManyToOne, c1, i2, "I2ManyToOne");
        d.AddManyToOneRelation(C1sWhereS2ManyToOne, C1S2ManyToOne, c1, s2, "S2ManyToOne");
        d.AddManyToOneRelation(C2sWhereC1ManyToOne, C2C1ManyToOne, c2, c1, "C1ManyToOne");

        d.AddOneToManyRelation(C1WhereC1OneToMany, C1C1OneToMany, c1, c1, "C1OneToMany");
        d.AddOneToManyRelation(C1WhereC2OneToMany, C1C2OneToMany, c1, c2, "C2OneToMany");
        d.AddOneToManyRelation(C1WhereI2OneToMany, C1I2OneToMany, c1, i2, "I2OneToMany");
        d.AddOneToManyRelation(C2WhereC1OneToMany, C2C1OneToMany, c2, c1, "C1OneToMany");

        d.AddManyToManyRelation(C1sWhereC1ManyToMany, C1C1ManyToMany, c1, c1, "C1ManyToMany");
        d.AddManyToManyRelation(C1sWhereC2ManyToMany, C1C2ManyToMany, c1, c2, "C2ManyToMany");
        d.AddManyToManyRelation(C1sWhereI2ManyToMany, C1I2ManyToMany, c1, i2, "I2ManyToMany");
        d.AddManyToManyRelation(C2sWhereC1ManyToMany, C2C1ManyToMany, c2, c1, "C1ManyToMany");
    }
}
