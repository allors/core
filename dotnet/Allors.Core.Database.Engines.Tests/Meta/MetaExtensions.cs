namespace Allors.Core.Database.Engines.Tests.Meta;

using System;
using System.Linq;
using Allors.Core.Database.Meta;
using Allors.Core.Database.Meta.Domain;
using Allors.Core.Meta;

/// <summary>
/// Meta Extensions.
/// </summary>
public static class MetaExtensions
{
    public static Domain AllorsTests(this Meta @this) => (Domain)@this.Get(TestsIds.AllorsTests);

    public static Class C1(this Meta @this) => (Class)@this.Get(TestsIds.C1);

    public static Class C2(this Meta @this) => (Class)@this.Get(TestsIds.C2);

    public static Class C3(this Meta @this) => (Class)@this.Get(TestsIds.C3);

    public static Class C4(this Meta @this) => (Class)@this.Get(TestsIds.C4);

    public static Interface I1(this Meta @this) => (Interface)@this.Get(TestsIds.I1);

    public static Interface I2(this Meta @this) => (Interface)@this.Get(TestsIds.I2);

    public static Interface S1(this Meta @this) => (Interface)@this.Get(TestsIds.S1);

    public static Interface S2(this Meta @this) => (Interface)@this.Get(TestsIds.S2);

    public static Interface I12(this Meta @this) => (Interface)@this.Get(TestsIds.I12);

    public static OneToOneRoleType C1C1OneToOne(this Meta @this) => (OneToOneRoleType)@this.Get(TestsIds.C1C1OneToOne);

    public static OneToOneAssociationType C1WhereC1OneToOne(this Meta @this) => (OneToOneAssociationType)@this.Get(TestsIds.C1WhereC1OneToOne);

    public static OneToOneRoleType C1I1OneToOne(this Meta @this) => (OneToOneRoleType)@this.Get(TestsIds.C1I1OneToOne);

    public static OneToOneAssociationType C1WhereI1OneToOne(this Meta @this) => (OneToOneAssociationType)@this.Get(TestsIds.C1WhereI1OneToOne);

    public static OneToOneRoleType C1C2OneToOne(this Meta @this) => (OneToOneRoleType)@this.Get(TestsIds.C1C2OneToOne);

    public static OneToOneAssociationType C1WhereC2OneToOne(this Meta @this) => (OneToOneAssociationType)@this.Get(TestsIds.C1WhereC2OneToOne);

    public static OneToOneRoleType C1I2OneToOne(this Meta @this) => (OneToOneRoleType)@this.Get(TestsIds.C1I2OneToOne);

    public static OneToOneAssociationType C1WhereI2OneToOne(this Meta @this) => (OneToOneAssociationType)@this.Get(TestsIds.C1WhereI2OneToOne);

    public static OneToOneRoleType C1S2OneToOne(this Meta @this) => (OneToOneRoleType)@this.Get(TestsIds.C1S2OneToOne);

    public static OneToOneAssociationType C1WhereS2OneToOne(this Meta @this) => (OneToOneAssociationType)@this.Get(TestsIds.C1WhereS2OneToOne);

    public static OneToOneRoleType C2C1OneToOne(this Meta @this) => (OneToOneRoleType)@this.Get(TestsIds.C2C1OneToOne);

    public static OneToOneAssociationType C2WhereC1OneToOne(this Meta @this) => (OneToOneAssociationType)@this.Get(TestsIds.C2WhereC1OneToOne);

    public static OneToOneRoleType C2C2OneToOne(this Meta @this) => (OneToOneRoleType)@this.Get(TestsIds.C2C2OneToOne);

    public static OneToOneAssociationType C2WhereC2OneToOne(this Meta @this) => (OneToOneAssociationType)@this.Get(TestsIds.C2WhereC2OneToOne);

    public static ManyToOneRoleType C1C1ManyToOne(this Meta @this) => (ManyToOneRoleType)@this.Get(TestsIds.C1C1ManyToOne);

    public static ManyToOneAssociationType C1sWhereC1ManyToOne(this Meta @this) => (ManyToOneAssociationType)@this.Get(TestsIds.C1sWhereC1ManyToOne);

    public static ManyToOneRoleType C1C2ManyToOne(this Meta @this) => (ManyToOneRoleType)@this.Get(TestsIds.C1C2ManyToOne);

    public static ManyToOneAssociationType C1sWhereC2ManyToOne(this Meta @this) => (ManyToOneAssociationType)@this.Get(TestsIds.C1sWhereC2ManyToOne);

    public static ManyToOneRoleType C1I2ManyToOne(this Meta @this) => (ManyToOneRoleType)@this.Get(TestsIds.C1I2ManyToOne);

    public static ManyToOneAssociationType C1sWhereI2ManyToOne(this Meta @this) => (ManyToOneAssociationType)@this.Get(TestsIds.C1sWhereI2ManyToOne);

    public static ManyToOneRoleType C1S2ManyToOne(this Meta @this) => (ManyToOneRoleType)@this.Get(TestsIds.C1S2ManyToOne);

    public static ManyToOneAssociationType C1sWhereS2ManyToOne(this Meta @this) => (ManyToOneAssociationType)@this.Get(TestsIds.C1sWhereS2ManyToOne);

    public static ManyToOneRoleType C2C1ManyToOne(this Meta @this) => (ManyToOneRoleType)@this.Get(TestsIds.C2C1ManyToOne);

    public static ManyToOneAssociationType C2sWhereC1ManyToOne(this Meta @this) => (ManyToOneAssociationType)@this.Get(TestsIds.C2sWhereC1ManyToOne);

    public static OneToManyRoleType C1C1OneToMany(this Meta @this) => (OneToManyRoleType)@this.Get(TestsIds.C1C1OneToMany);

    public static OneToManyAssociationType C1WhereC1OneToMany(this Meta @this) => (OneToManyAssociationType)@this.Get(TestsIds.C1WhereC1OneToMany);

    public static OneToManyRoleType C1C2OneToMany(this Meta @this) => (OneToManyRoleType)@this.Get(TestsIds.C1C2OneToMany);

    public static OneToManyAssociationType C1WhereC2OneToMany(this Meta @this) => (OneToManyAssociationType)@this.Get(TestsIds.C1WhereC2OneToMany);

    public static OneToManyRoleType C1I2OneToMany(this Meta @this) => (OneToManyRoleType)@this.Get(TestsIds.C1I2OneToMany);

    public static OneToManyAssociationType C1WhereI2OneToMany(this Meta @this) => (OneToManyAssociationType)@this.Get(TestsIds.C1WhereI2OneToMany);

    public static OneToManyRoleType C2C1OneToMany(this Meta @this) => (OneToManyRoleType)@this.Get(TestsIds.C2C1OneToMany);

    public static OneToManyAssociationType C2WhereC1OneToMany(this Meta @this) => (OneToManyAssociationType)@this.Get(TestsIds.C2WhereC1OneToMany);

    public static ManyToManyRoleType C1C1ManyToMany(this Meta @this) => (ManyToManyRoleType)@this.Get(TestsIds.C1C1ManyToMany);

    public static ManyToManyAssociationType C1sWhereC1ManyToMany(this Meta @this) => (ManyToManyAssociationType)@this.Get(TestsIds.C1sWhereC1ManyToMany);

    public static ManyToManyRoleType C1C2ManyToMany(this Meta @this) => (ManyToManyRoleType)@this.Get(TestsIds.C1C2ManyToMany);

    public static ManyToManyAssociationType C1sWhereC2ManyToMany(this Meta @this) => (ManyToManyAssociationType)@this.Get(TestsIds.C1sWhereC2ManyToMany);

    public static ManyToManyRoleType C1I2ManyToMany(this Meta @this) => (ManyToManyRoleType)@this.Get(TestsIds.C1I2ManyToMany);

    public static ManyToManyAssociationType C1sWhereI2ManyToMany(this Meta @this) => (ManyToManyAssociationType)@this.Get(TestsIds.C1sWhereI2ManyToMany);

    public static ManyToManyRoleType C2C1ManyToMany(this Meta @this) => (ManyToManyRoleType)@this.Get(TestsIds.C2C1ManyToMany);

    public static ManyToManyAssociationType C2sWhereC1ManyToMany(this Meta @this) => (ManyToManyAssociationType)@this.Get(TestsIds.C2sWhereC1ManyToMany);

    public static StringRoleType I1AllorsString(this Meta @this) => (StringRoleType)@this.Get(TestsIds.I1AllorsString);

    public static StringRoleType C1AllorsString(this Meta @this) => (StringRoleType)@this.Get(TestsIds.C1AllorsString);

    public static StringRoleType C2AllorsString(this Meta @this) => (StringRoleType)@this.Get(TestsIds.C2AllorsString);

    public static StringRoleType C3AllorsString(this Meta @this) => (StringRoleType)@this.Get(TestsIds.C3AllorsString);

    public static StringRoleType C4AllorsString(this Meta @this) => (StringRoleType)@this.Get(TestsIds.C4AllorsString);

    private static IMetaObject Get(this Meta @this, Guid id) => @this.Objects.First(v => ((Guid)v[@this.MetaMeta.MetaObjectId()]!) == id);
}
