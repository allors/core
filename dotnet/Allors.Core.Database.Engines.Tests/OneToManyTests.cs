namespace Allors.Core.Database.Engines.Tests;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Allors.Core.Database.Engines.Tests.Meta;
using Allors.Core.Database.Meta;
using FluentAssertions;
using MoreLinq;
using Xunit;

public abstract class OneToManyTests : Tests
{
    private readonly Func<(
        OneToManyAssociationType Association,
        OneToManyRoleType Role,
        Func<ITransaction, IObject>[] Builders,
        Func<ITransaction, IObject> FromBuilder,
        Func<ITransaction, IObject> FromAnotherBuilder,
        Func<ITransaction, IObject> ToBuilder,
        Func<ITransaction, IObject> ToAnotherBuilder,
        Func<ITransaction, IObject> To3Builder,
        Func<ITransaction, IObject> To4Builder)>[] fixtures;

    private readonly (string, Action<ITransaction>)[] preActs;

    protected OneToManyTests()
    {
        this.fixtures =
        [
            () =>
            {
                // C1 <-> C1
                Debugger.Log(0, null, $"C1 <-> C1\n");
                var association = this.Meta.C1WhereC1OneToMany();
                var role = this.Meta.C1C1OneToMany();

                return (association, role, [C1Builder], C1Builder, C1Builder, C1Builder, C1Builder, C1Builder, C1Builder);

                IObject C1Builder(ITransaction transaction) => transaction.Build(this.Meta.C1);
            },
        ];

        this.preActs =
        [
            ("Nothing", _ => { }),
            ("Checkpoint", v => v.Checkpoint()),
            ("Checkpoint Checkpoint", v =>
            {
                v.Checkpoint();
                v.Checkpoint();
            }),
            ("Commit", v => v.Commit()),
            ("Commit Commit", v =>
            {
                v.Commit();
                v.Commit();
            }),
            ("Checkpoint Commit", v =>
            {
                v.Checkpoint();
                v.Commit();
            }),
            ("Commit Checkpoint", v =>
            {
                v.Commit();
                v.Checkpoint();
            }),
        ];
    }

    [Fact]
    public void C1C1OneToManies()
    {
        var database = this.CreateDatabase();
        var transaction = database.CreateTransaction();

        var m = this.Meta;

        var from = transaction.Build(m.C1);
        var fromAnother = transaction.Build(m.C1);

        var to1 = transaction.Build(m.C1);
        var to2 = transaction.Build(m.C1);
        var to3 = transaction.Build(m.C1);
        var to4 = transaction.Build(m.C1);

        IObject[] to1Array = [to1];
        IObject[] to2Array = [to2];
        IObject[] to12Array = [to1, to2];

        // To 0-4-0
        // Get
        from[m.C1C1OneToMany].Should().BeEmpty();
        from[m.C1C1OneToMany].Should().BeEmpty();
        to1[m.C1WhereC1OneToMany].Should().BeNull();
        to1[m.C1WhereC1OneToMany].Should().BeNull();
        to2[m.C1WhereC1OneToMany].Should().BeNull();
        to2[m.C1WhereC1OneToMany].Should().BeNull();
        to3[m.C1WhereC1OneToMany].Should().BeNull();
        to3[m.C1WhereC1OneToMany].Should().BeNull();
        to4[m.C1WhereC1OneToMany].Should().BeNull();
        to4[m.C1WhereC1OneToMany].Should().BeNull();

        // 1-1
        from.Add(m.C1C1OneToMany, to1);
        from.Add(m.C1C1OneToMany, to1); // Add Twice

        from[m.C1C1OneToMany].Should().HaveCount(1);
        from[m.C1C1OneToMany].Should().HaveCount(1);
        from[m.C1C1OneToMany].Should().Contain(to1);
        from[m.C1C1OneToMany].Should().Contain(to1);
        to1[m.C1WhereC1OneToMany].Should().Be(from);
        to1[m.C1WhereC1OneToMany].Should().Be(from);
        to2[m.C1WhereC1OneToMany].Should().BeNull();
        to2[m.C1WhereC1OneToMany].Should().BeNull();
        to3[m.C1WhereC1OneToMany].Should().BeNull();
        to3[m.C1WhereC1OneToMany].Should().BeNull();
        to4[m.C1WhereC1OneToMany].Should().BeNull();
        to4[m.C1WhereC1OneToMany].Should().BeNull();

        // 1-2
        from.Add(m.C1C1OneToMany, to2);

        from[m.C1C1OneToMany].Count().Should().Be(2);
        from[m.C1C1OneToMany].Count().Should().Be(2);
        from[m.C1C1OneToMany].Should().Contain(to1);
        from[m.C1C1OneToMany].Should().Contain(to1);
        from[m.C1C1OneToMany].Should().Contain(to2);
        from[m.C1C1OneToMany].Should().Contain(to2);
        to1[m.C1WhereC1OneToMany].Should().Be(from);
        to1[m.C1WhereC1OneToMany].Should().Be(from);
        to2[m.C1WhereC1OneToMany].Should().Be(from);
        to2[m.C1WhereC1OneToMany].Should().Be(from);
        to3[m.C1WhereC1OneToMany].Should().BeNull();
        to3[m.C1WhereC1OneToMany].Should().BeNull();
        to4[m.C1WhereC1OneToMany].Should().BeNull();
        to4[m.C1WhereC1OneToMany].Should().BeNull();

        // 1-3
        from.Add(m.C1C1OneToMany, to3);

        from[m.C1C1OneToMany].Count().Should().Be(3);
        from[m.C1C1OneToMany].Count().Should().Be(3);
        from[m.C1C1OneToMany].Should().Contain(to1);
        from[m.C1C1OneToMany].Should().Contain(to1);
        from[m.C1C1OneToMany].Should().Contain(to2);
        from[m.C1C1OneToMany].Should().Contain(to2);
        from[m.C1C1OneToMany].Should().Contain(to3);
        from[m.C1C1OneToMany].Should().Contain(to3);
        to1[m.C1WhereC1OneToMany].Should().Be(from);
        to1[m.C1WhereC1OneToMany].Should().Be(from);
        to2[m.C1WhereC1OneToMany].Should().Be(from);
        to2[m.C1WhereC1OneToMany].Should().Be(from);
        to3[m.C1WhereC1OneToMany].Should().Be(from);
        to3[m.C1WhereC1OneToMany].Should().Be(from);
        to4[m.C1WhereC1OneToMany].Should().BeNull();
        to4[m.C1WhereC1OneToMany].Should().BeNull();

        // 1-4
        from.Add(m.C1C1OneToMany, to4);

        from[m.C1C1OneToMany].Count().Should().Be(4);
        from[m.C1C1OneToMany].Count().Should().Be(4);
        from[m.C1C1OneToMany].Should().Contain(to1);
        from[m.C1C1OneToMany].Should().Contain(to1);
        from[m.C1C1OneToMany].Should().Contain(to2);
        from[m.C1C1OneToMany].Should().Contain(to2);
        from[m.C1C1OneToMany].Should().Contain(to3);
        from[m.C1C1OneToMany].Should().Contain(to3);
        from[m.C1C1OneToMany].Should().Contain(to4);
        from[m.C1C1OneToMany].Should().Contain(to4);
        to1[m.C1WhereC1OneToMany].Should().Be(from);
        to1[m.C1WhereC1OneToMany].Should().Be(from);
        to2[m.C1WhereC1OneToMany].Should().Be(from);
        to2[m.C1WhereC1OneToMany].Should().Be(from);
        to3[m.C1WhereC1OneToMany].Should().Be(from);
        to3[m.C1WhereC1OneToMany].Should().Be(from);
        to4[m.C1WhereC1OneToMany].Should().Be(from);
        to4[m.C1WhereC1OneToMany].Should().Be(from);

        // 1-3
        from.Remove(m.C1C1OneToMany, to4);

        from[m.C1C1OneToMany].Count().Should().Be(3);
        from[m.C1C1OneToMany].Count().Should().Be(3);
        from[m.C1C1OneToMany].Should().Contain(to1);
        from[m.C1C1OneToMany].Should().Contain(to1);
        from[m.C1C1OneToMany].Should().Contain(to2);
        from[m.C1C1OneToMany].Should().Contain(to2);
        from[m.C1C1OneToMany].Should().Contain(to3);
        from[m.C1C1OneToMany].Should().Contain(to3);
        to1[m.C1WhereC1OneToMany].Should().Be(from);
        to1[m.C1WhereC1OneToMany].Should().Be(from);
        to2[m.C1WhereC1OneToMany].Should().Be(from);
        to2[m.C1WhereC1OneToMany].Should().Be(from);
        to3[m.C1WhereC1OneToMany].Should().Be(from);
        to3[m.C1WhereC1OneToMany].Should().Be(from);
        to4[m.C1WhereC1OneToMany].Should().BeNull();
        to4[m.C1WhereC1OneToMany].Should().BeNull();

        // 1-2
        from.Remove(m.C1C1OneToMany, to3);

        from[m.C1C1OneToMany].Count().Should().Be(2);
        from[m.C1C1OneToMany].Count().Should().Be(2);
        from[m.C1C1OneToMany].Should().Contain(to1);
        from[m.C1C1OneToMany].Should().Contain(to1);
        from[m.C1C1OneToMany].Should().Contain(to2);
        from[m.C1C1OneToMany].Should().Contain(to2);
        to1[m.C1WhereC1OneToMany].Should().Be(from);
        to1[m.C1WhereC1OneToMany].Should().Be(from);
        to2[m.C1WhereC1OneToMany].Should().Be(from);
        to2[m.C1WhereC1OneToMany].Should().Be(from);
        to3[m.C1WhereC1OneToMany].Should().BeNull();
        to3[m.C1WhereC1OneToMany].Should().BeNull();
        to4[m.C1WhereC1OneToMany].Should().BeNull();
        to4[m.C1WhereC1OneToMany].Should().BeNull();

        // 1-1
        from.Remove(m.C1C1OneToMany, to2);
        from.Remove(m.C1C1OneToMany, to2); // Delete Twice

        from[m.C1C1OneToMany].Should().HaveCount(1);
        from[m.C1C1OneToMany].Should().HaveCount(1);
        from[m.C1C1OneToMany].Should().Contain(to1);
        from[m.C1C1OneToMany].Should().Contain(to1);
        to1[m.C1WhereC1OneToMany].Should().Be(from);
        to1[m.C1WhereC1OneToMany].Should().Be(from);
        to2[m.C1WhereC1OneToMany].Should().BeNull();
        to2[m.C1WhereC1OneToMany].Should().BeNull();
        to3[m.C1WhereC1OneToMany].Should().BeNull();
        to3[m.C1WhereC1OneToMany].Should().BeNull();
        to4[m.C1WhereC1OneToMany].Should().BeNull();
        to4[m.C1WhereC1OneToMany].Should().BeNull();

        // 0-0
        from.Remove(m.C1C1OneToMany, to1);
        from.Remove(m.C1C1OneToMany, to1);
        from.Add(m.C1C1OneToMany, to1);
        from[m.C1C1OneToMany] = [];
        from[m.C1C1OneToMany] = [];
        from.Add(m.C1C1OneToMany, to1);
        from[m.C1C1OneToMany] = [];
        from[m.C1C1OneToMany] = [];
        from.Add(m.C1C1OneToMany, to1);
        IObject[] emptyArray = [];
        from[m.C1C1OneToMany] = emptyArray;
        from[m.C1C1OneToMany] = emptyArray;

        from[m.C1C1OneToMany].Should().BeEmpty();
        from[m.C1C1OneToMany].Should().BeEmpty();
        to1[m.C1WhereC1OneToMany].Should().BeNull();
        to1[m.C1WhereC1OneToMany].Should().BeNull();
        to2[m.C1WhereC1OneToMany].Should().BeNull();
        to2[m.C1WhereC1OneToMany].Should().BeNull();
        to3[m.C1WhereC1OneToMany].Should().BeNull();
        to3[m.C1WhereC1OneToMany].Should().BeNull();
        to4[m.C1WhereC1OneToMany].Should().BeNull();
        to4[m.C1WhereC1OneToMany].Should().BeNull();

        // Exist
        from.Exist(m.C1C1OneToMany).Should().BeFalse();
        from.Exist(m.C1C1OneToMany).Should().BeFalse();
        to1.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to1.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to2.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to2.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to3.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to3.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to4.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to4.Exist(m.C1WhereC1OneToMany).Should().BeFalse();

        // 1-1
        from.Add(m.C1C1OneToMany, to1);
        from.Add(m.C1C1OneToMany, to1); // Add Twice

        from.Exist(m.C1C1OneToMany).Should().BeTrue();
        from.Exist(m.C1C1OneToMany).Should().BeTrue();
        to1.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to1.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to2.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to2.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to3.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to3.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to4.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to4.Exist(m.C1WhereC1OneToMany).Should().BeFalse();

        // 1-2
        from.Add(m.C1C1OneToMany, to2);

        from.Exist(m.C1C1OneToMany).Should().BeTrue();
        from.Exist(m.C1C1OneToMany).Should().BeTrue();
        to1.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to1.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to2.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to2.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to3.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to3.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to4.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to4.Exist(m.C1WhereC1OneToMany).Should().BeFalse();

        // 1-3
        from.Add(m.C1C1OneToMany, to3);

        from.Exist(m.C1C1OneToMany).Should().BeTrue();
        from.Exist(m.C1C1OneToMany).Should().BeTrue();
        to1.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to1.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to2.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to2.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to3.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to3.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to4.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to4.Exist(m.C1WhereC1OneToMany).Should().BeFalse();

        // 1-4
        from.Add(m.C1C1OneToMany, to4);

        from.Exist(m.C1C1OneToMany).Should().BeTrue();
        from.Exist(m.C1C1OneToMany).Should().BeTrue();
        to1.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to1.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to2.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to2.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to3.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to3.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to4.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to4.Exist(m.C1WhereC1OneToMany).Should().BeTrue();

        // 1-3
        from.Remove(m.C1C1OneToMany, to4);

        from.Exist(m.C1C1OneToMany).Should().BeTrue();
        from.Exist(m.C1C1OneToMany).Should().BeTrue();
        to1.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to1.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to2.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to2.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to3.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to3.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to4.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to4.Exist(m.C1WhereC1OneToMany).Should().BeFalse();

        // 1-2
        from.Remove(m.C1C1OneToMany, to3);

        from.Exist(m.C1C1OneToMany).Should().BeTrue();
        from.Exist(m.C1C1OneToMany).Should().BeTrue();
        to1.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to1.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to2.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to2.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to3.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to3.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to4.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to4.Exist(m.C1WhereC1OneToMany).Should().BeFalse();

        // 1-1
        from.Remove(m.C1C1OneToMany, to2);
        from.Remove(m.C1C1OneToMany, to2); // Delete Twice

        from.Exist(m.C1C1OneToMany).Should().BeTrue();
        from.Exist(m.C1C1OneToMany).Should().BeTrue();
        to1.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to1.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        to2.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to2.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to3.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to3.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to4.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to4.Exist(m.C1WhereC1OneToMany).Should().BeFalse();

        // 0-0
        from.Remove(m.C1C1OneToMany, to1);
        from.Remove(m.C1C1OneToMany, to1);
        from.Add(m.C1C1OneToMany, to1);
        from[m.C1C1OneToMany] = [];
        from[m.C1C1OneToMany] = [];
        from.Add(m.C1C1OneToMany, to1);
        from[m.C1C1OneToMany] = [];
        from[m.C1C1OneToMany] = [];
        from.Add(m.C1C1OneToMany, to1);
        from[m.C1C1OneToMany] = emptyArray;
        from[m.C1C1OneToMany] = emptyArray;

        from.Exist(m.C1C1OneToMany).Should().BeFalse();
        from.Exist(m.C1C1OneToMany).Should().BeFalse();
        to1.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to1.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to2.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to2.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to3.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to3.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to4.Exist(m.C1WhereC1OneToMany).Should().BeFalse();
        to4.Exist(m.C1WhereC1OneToMany).Should().BeFalse();

        // Multiplicity
        from.Add(m.C1C1OneToMany, to1);
        from.Add(m.C1C1OneToMany, to1);

        from[m.C1C1OneToMany].Should().HaveCount(1);
        from[m.C1C1OneToMany].Should().Contain(to1);
        to1[m.C1WhereC1OneToMany].Should().Be(from);

        // TODO: Replicate to other variants
        fromAnother.Remove(m.C1C1OneToMany, to1);

        from[m.C1C1OneToMany].Should().HaveCount(1);
        from[m.C1C1OneToMany].Should().Contain(to1);
        to1[m.C1WhereC1OneToMany].Should().Be(from);

        from[m.C1C1OneToMany] = [];

        from[m.C1C1OneToMany] = to1Array;
        from[m.C1C1OneToMany] = to1Array;

        from[m.C1C1OneToMany].Should().HaveCount(1);
        from[m.C1C1OneToMany].Should().Contain(to1);
        to1[m.C1WhereC1OneToMany].Should().Be(from);

        from[m.C1C1OneToMany] = [];

        from.Add(m.C1C1OneToMany, to1);
        from.Add(m.C1C1OneToMany, to2);

        from[m.C1C1OneToMany].Count().Should().Be(2);
        from[m.C1C1OneToMany].Should().Contain(to1);
        from[m.C1C1OneToMany].Should().Contain(to2);
        to1[m.C1WhereC1OneToMany].Should().Be(from);
        to2[m.C1WhereC1OneToMany].Should().Be(from);

        from[m.C1C1OneToMany] = [];

        from[m.C1C1OneToMany] = to1Array;
        from[m.C1C1OneToMany] = to2Array;

        from[m.C1C1OneToMany].Should().HaveCount(1);
        from[m.C1C1OneToMany].Should().Contain(to2);
        to1[m.C1WhereC1OneToMany].Should().BeNull();
        to2[m.C1WhereC1OneToMany].Should().Be(from);

        from[m.C1C1OneToMany] = to12Array;

        from[m.C1C1OneToMany].Count().Should().Be(2);
        from[m.C1C1OneToMany].Should().Contain(to1);
        from[m.C1C1OneToMany].Should().Contain(to2);
        to1[m.C1WhereC1OneToMany].Should().Be(from);
        to2[m.C1WhereC1OneToMany].Should().Be(from);

        from[m.C1C1OneToMany] = [];

        from.Add(m.C1C1OneToMany, to1);
        fromAnother.Add(m.C1C1OneToMany, to1);

        from[m.C1C1OneToMany].Should().BeEmpty();
        fromAnother[m.C1C1OneToMany].Should().HaveCount(1);
        fromAnother[m.C1C1OneToMany].Should().Contain(to1);
        to1[m.C1WhereC1OneToMany].Should().Be(fromAnother);

        fromAnother[m.C1C1OneToMany] = [];

        // Replicate to others
        from.Add(m.C1C1OneToMany, to1);
        from.Add(m.C1C1OneToMany, to2);
        fromAnother.Add(m.C1C1OneToMany, to1);

        from[m.C1C1OneToMany].Should().HaveCount(1);
        fromAnother[m.C1C1OneToMany].Should().HaveCount(1);
        from[m.C1C1OneToMany].Should().Contain(to2);
        fromAnother[m.C1C1OneToMany].Should().Contain(to1);
        to2[m.C1WhereC1OneToMany].Should().Be(from);
        to1[m.C1WhereC1OneToMany].Should().Be(fromAnother);

        fromAnother[m.C1C1OneToMany] = [];

        from[m.C1C1OneToMany] = to1Array;

        from[m.C1C1OneToMany].Should().HaveCount(1); // TODO: Add this to others
        fromAnother[m.C1C1OneToMany] = to1Array;

        from[m.C1C1OneToMany].Should().BeEmpty();
        fromAnother[m.C1C1OneToMany].Should().HaveCount(1);
        fromAnother[m.C1C1OneToMany].Should().Contain(to1);
        to1[m.C1WhereC1OneToMany].Should().Be(fromAnother);

        fromAnother[m.C1C1OneToMany] = [];

        from.Add(m.C1C1OneToMany, to1);
        fromAnother.Add(m.C1C1OneToMany, to2);

        from[m.C1C1OneToMany].Should().HaveCount(1);
        from[m.C1C1OneToMany].Should().Contain(to1);
        to1[m.C1WhereC1OneToMany].Should().Be(from);
        fromAnother[m.C1C1OneToMany].Should().HaveCount(1);
        fromAnother[m.C1C1OneToMany].Should().Contain(to2);
        to2[m.C1WhereC1OneToMany].Should().Be(fromAnother);

        from[m.C1C1OneToMany] = [];
        fromAnother[m.C1C1OneToMany] = [];

        from[m.C1C1OneToMany] = to1Array;
        fromAnother[m.C1C1OneToMany] = to2Array;

        from[m.C1C1OneToMany].Should().HaveCount(1);
        from[m.C1C1OneToMany].Should().Contain(to1);
        to1[m.C1WhereC1OneToMany].Should().Be(from);
        fromAnother[m.C1C1OneToMany].Should().HaveCount(1);
        fromAnother[m.C1C1OneToMany].Should().Contain(to2);
        to2[m.C1WhereC1OneToMany].Should().Be(fromAnother);

        from[m.C1C1OneToMany] = [];
        fromAnother[m.C1C1OneToMany] = [];

        // Null & Empty Array
        // Set Empty Array
        from[m.C1C1OneToMany] = [];

        from[m.C1C1OneToMany].Should().BeEmpty();

        // Set Array with only a null
        from[m.C1C1OneToMany] = new IObject[1];

        from[m.C1C1OneToMany].Should().BeEmpty();
        from[m.C1C1OneToMany] = new IObject[1];

        from[m.C1C1OneToMany].Should().BeEmpty();

        // Set Array with a null in the front
        var nullInFront = new IObject[3];
        nullInFront[1] = to1;
        nullInFront[2] = to2;
        from[m.C1C1OneToMany] = nullInFront;

        from[m.C1C1OneToMany].Count().Should().Be(2);
        from[m.C1C1OneToMany].Should().Contain(to1);
        from[m.C1C1OneToMany].Should().Contain(to2);
        from[m.C1C1OneToMany] = nullInFront;

        from[m.C1C1OneToMany].Count().Should().Be(2);
        from[m.C1C1OneToMany].Should().Contain(to1);
        from[m.C1C1OneToMany].Should().Contain(to2);

        // Set Array with a null in the middle
        var nullInTheMiddle = new IObject[3];
        nullInTheMiddle[0] = to1;
        nullInTheMiddle[2] = to2;
        from[m.C1C1OneToMany] = nullInTheMiddle;

        from[m.C1C1OneToMany].Count().Should().Be(2);
        from[m.C1C1OneToMany].Should().Contain(to1);
        from[m.C1C1OneToMany].Should().Contain(to2);
        from[m.C1C1OneToMany] = nullInTheMiddle;

        from[m.C1C1OneToMany].Count().Should().Be(2);
        from[m.C1C1OneToMany].Should().Contain(to1);
        from[m.C1C1OneToMany].Should().Contain(to2);

        // Set Array with a null in the back
        IObject[] nullInBack = new IObject[3];
        nullInBack[1] = to1;
        nullInBack[2] = to2;
        from[m.C1C1OneToMany] = nullInBack;

        from[m.C1C1OneToMany].Count().Should().Be(2);
        from[m.C1C1OneToMany].Should().Contain(to1);
        from[m.C1C1OneToMany].Should().Contain(to2);
        from[m.C1C1OneToMany] = nullInBack;

        from[m.C1C1OneToMany].Count().Should().Be(2);
        from[m.C1C1OneToMany].Should().Contain(to1);
        from[m.C1C1OneToMany].Should().Contain(to2);

        // Remove and Add
        from = transaction.Build(m.C1);
        var to = transaction.Build(m.C1);

        from.Add(m.C1C1OneToMany, to);

        transaction.Commit();

        from.Remove(m.C1C1OneToMany, to);
        from.Add(m.C1C1OneToMany, to);

        transaction.Commit();

        // Add and Remove
        from = transaction.Build(m.C1);
        to1 = transaction.Build(m.C1);
        to2 = transaction.Build(m.C1);

        from.Add(m.C1C1OneToMany, to1);

        transaction.Commit();

        from.Add(m.C1C1OneToMany, to2);
        from.Remove(m.C1C1OneToMany, to2);

        transaction.Commit();

        // New - Middle - To
        from = transaction.Build(m.C1);
        var middle = transaction.Build(m.C1);
        to = transaction.Build(m.C1);

        from.Add(m.C1C1OneToMany, middle);
        middle.Add(m.C1C1OneToMany, to);
        from.Add(m.C1C1OneToMany, to);

        middle.Exist(m.C1WhereC1OneToMany).Should().BeTrue();
        middle.Exist(m.C1C1OneToMany).Should().BeFalse();

        // Very Big Array
        var bigArray = transaction.Build(m.C1, Settings.LargeArraySize).ToArray();
        from[m.C1C1OneToMany] = bigArray;
        var getBigArray = from[m.C1C1OneToMany].ToArray();

        getBigArray.Length.Should().Be(Settings.LargeArraySize);

        var objects = new HashSet<IObject>(getBigArray);
        foreach (var bigArrayObject in bigArray)
        {
            objects.Should().Contain(bigArrayObject);
        }

        from = transaction.Build(m.C1);

        from[m.C1C1OneToMany] = to1Array;
        from[m.C1C1OneToMany] = to1Array;

        from[m.C1C1OneToMany].Should().HaveCount(1);
        from[m.C1C1OneToMany].Should().Contain(to1Array[0]);
        to1Array[0][m.C1WhereC1OneToMany].Should().Be(from);

        // Extent.ToArray()
        from = transaction.Build(m.C1);
        to1 = transaction.Build(m.C1);

        from.Add(m.C1C1OneToMany, to1);

        from[m.C1C1OneToMany].Should().HaveCount(1);
        from[m.C1C1OneToMany].First().Should().Be(to1);

        // Extent<T>.ToArray()
        from = transaction.Build(m.C1);
        to1 = transaction.Build(m.C1);

        from.Add(m.C1C1OneToMany, to1);

        from[m.C1C1OneToMany].ToArray().Should().HaveCount(1);
        from[m.C1C1OneToMany].ElementAt(0).Should().Be(to1);
    }

    [Fact]
    public void FromToInitial()
    {
        this.FromTo(
            () =>
            [
            ],
            () =>
            [
                (association, _, _, to) => to[association].Should().BeNull(),
                (_, role, from, _) => from[role].Should().BeEmpty()
            ]);
    }

    [Fact]
    public void FromToAdd()
    {
        this.FromTo(
            () =>
            [
                (_, role, from, to) => from.Add(role, to)
            ],
            () =>
            [
                (association, _, from, to) => to[association].Should().BeSameAs(from),
                (_, role, from, to) => from[role].Should().BeEquivalentTo([to])
            ]);
    }

    [Fact]
    public void FromToSet()
    {
        this.FromTo(
            () =>
            [
                (_, role, from, to) => from[role] = [to]
            ],
            () =>
            [
                (association, _, from, to) => to[association].Should().BeSameAs(from),
                (_, role, from, to) => from[role].Should().BeEquivalentTo([to])
            ]);
    }

    [Fact]
    public void FromToSetFromAnotherToSet()
    {
        this.FromFromAnotherTo(
            () =>
            [
                (_, role, from, _, to) => from[role] = [to],
                (_, role, _, fromAnother, to) => fromAnother[role] = [to]
            ],
            () =>
            [
                (association, _, _, fromAnother, to) => to[association].Should().BeSameAs(fromAnother),
                (_, role, from, _, _) => from[role].Should().BeEmpty(),
                (_, role, _, fromAnother, to) => fromAnother[role].Should().BeEquivalentTo([to])
            ]);
    }

    [Fact]
    public void CheckRoleIsAssignable()
    {
        foreach (var (_, preact) in this.preActs)
        {
            var database = this.CreateDatabase();
            var transaction = database.CreateTransaction();

            var m = this.Meta;

            var c1a = transaction.Build(m.C1);
            var c1b = transaction.Build(m.C1);

            var c2a = transaction.Build(m.C2);

            // Illegal Role
            preact(transaction);

            c1a.Invoking(v => v.Add(m.C1C2OneToMany, c1b))
                .Should().Throw<ArgumentException>();

            preact(transaction);

            c1a.Invoking(v => v.Remove(m.C1C2OneToMany, c1b))
                .Should().Throw<ArgumentException>();

            preact(transaction);

            c1a.Invoking(v => v[m.C1C2OneToMany] = [c1b])
                .Should().Throw<ArgumentException>();

            preact(transaction);

            c1a.Invoking(v => v.Add(m.C1I2OneToMany, c1b))
                .Should().Throw<ArgumentException>();

            preact(transaction);

            c1a.Invoking(v => v.Remove(m.C1I2OneToMany, c1b))
                .Should().Throw<ArgumentException>();

            preact(transaction);

            c1a.Invoking(v => v[m.C1I2OneToMany] = [c1b])
                .Should().Throw<ArgumentException>();

            preact(transaction);

            c1a.Invoking(v => v.Add(m.C2C1OneToMany, c2a))
                .Should().Throw<ArgumentException>();

            preact(transaction);

            c1a.Invoking(v => v.Remove(m.C2C1OneToMany, c2a))
                .Should().Throw<ArgumentException>();

            preact(transaction);

            c1a.Invoking(v => v[m.C2C1OneToMany] = [c2a])
                .Should().Throw<ArgumentException>();
        }
    }

    protected abstract IDatabase CreateDatabase();

    private void FromTo(
        Func<IEnumerable<Action<OneToManyAssociationType, OneToManyRoleType, IObject, IObject>>> acts,
        Func<IEnumerable<Action<OneToManyAssociationType, OneToManyRoleType, IObject, IObject>>> asserts)
    {
        var assertPermutations = asserts().Permutations().ToArray();

        foreach (var (preactName, preact) in this.preActs)
        {
            Debugger.Log(0, null, $"Preact {preactName}\n");
            foreach (var actRepeats in new[] { 1, 2 })
            {
                Debugger.Log(0, null, $"Act Repeats {actRepeats}\n");
                foreach (var assertPermutation in assertPermutations)
                {
                    foreach (var assertRepeats in new[] { 1, 2 })
                    {
                        Debugger.Log(0, null, $"Assert Repeats {assertRepeats}\n");
                        foreach (var fixture in this.fixtures)
                        {
                            var (association, role, _, fromBuilder, _, toBuilder, _, _, _) = fixture();

                            var database = this.CreateDatabase();
                            var transaction = database.CreateTransaction();

                            var from = fromBuilder(transaction);
                            var to = toBuilder(transaction);

                            foreach (var act in acts())
                            {
                                for (var actRepeat = 0; actRepeat < actRepeats; actRepeat++)
                                {
                                    preact(transaction);
                                    act(association, role, from, to);
                                }
                            }

                            foreach (var assert in assertPermutation)
                            {
                                for (var assertRepeat = 0; assertRepeat < assertRepeats; assertRepeat++)
                                {
                                    assert(association, role, from, to);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void FromFromAnotherTo(
        Func<IEnumerable<Action<OneToManyAssociationType, OneToManyRoleType, IObject, IObject, IObject>>> acts,
        Func<IEnumerable<Action<OneToManyAssociationType, OneToManyRoleType, IObject, IObject, IObject>>> asserts)
    {
        var assertPermutations = asserts().Permutations().ToArray();

        foreach (var (preactName, preact) in this.preActs)
        {
            Debugger.Log(0, null, $"Preact {preactName}\n");
            foreach (var actRepeats in new[] { 1, 2 })
            {
                Debugger.Log(0, null, $"Act Repeats {actRepeats}\n");

                if (preactName == "Commit" && actRepeats == 2)
                {
                    Debugger.Break();
                }

                foreach (var assertPermutation in assertPermutations)
                {
                    foreach (var assertRepeats in new[] { 1, 2 })
                    {
                        Debugger.Log(0, null, $"Assert Repeats {assertRepeats}\n");
                        foreach (var fixture in this.fixtures)
                        {
                            var (association, role, _, fromBuilder, fromAnotherBuilder, toBuilder, _, _, _) = fixture();

                            var database = this.CreateDatabase();
                            var transaction = database.CreateTransaction();

                            var from = fromBuilder(transaction);
                            var fromAnother = fromAnotherBuilder(transaction);
                            var to = toBuilder(transaction);

                            foreach (var act in acts())
                            {
                                for (var actRepeat = 0; actRepeat < actRepeats; actRepeat++)
                                {
                                    preact(transaction);
                                    act(association, role, from, fromAnother, to);
                                }
                            }

                            foreach (var assert in assertPermutation)
                            {
                                for (var assertRepeat = 0; assertRepeat < assertRepeats; assertRepeat++)
                                {
                                    assert(association, role, from, fromAnother, to);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
