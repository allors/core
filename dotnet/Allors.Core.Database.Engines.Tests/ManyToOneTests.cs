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

public abstract class ManyToOneTests : Tests
{
    private readonly Func<(
    ManyToOneAssociationType Association,
    ManyToOneRoleType Role,
    Func<ITransaction, IObject>[] Builders,
    Func<ITransaction, IObject> FromBuilder,
    Func<ITransaction, IObject> FromAnotherBuilder,
    Func<ITransaction, IObject> From3Builder,
    Func<ITransaction, IObject> From4Builder,
    Func<ITransaction, IObject> ToBuilder,
    Func<ITransaction, IObject> ToAnotherBuilder)>[] fixtures;

    private readonly (string, Action<ITransaction>)[] preActs;

    protected ManyToOneTests()
    {
        this.fixtures =
        [
            () =>
                {
                    // C1 <-> C1
                    Debugger.Log(0, null, $"C1 <-> C1\n");
                    var association = this.Meta.C1sWhereC1ManyToOne();
                    var role = this.Meta.C1C1ManyToOne();

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
    public void C1C1many2one()
    {
        var database = this.CreateDatabase();
        var transaction = database.CreateTransaction();

        var m = this.Meta;

        var from1 = transaction.Build(m.C1);
        var from2 = transaction.Build(m.C1);
        var from3 = transaction.Build(m.C1);
        var from4 = transaction.Build(m.C1);
        var to = transaction.Build(m.C1);
        var toAnother = transaction.Build(m.C1);

        // New 0-4-0
        // Get
        to[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();
        to[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();
        from1[m.C1C1ManyToOne].Should().BeNull();
        from1[m.C1C1ManyToOne].Should().BeNull();
        from2[m.C1C1ManyToOne].Should().BeNull();
        from2[m.C1C1ManyToOne].Should().BeNull();
        from3[m.C1C1ManyToOne].Should().BeNull();
        from3[m.C1C1ManyToOne].Should().BeNull();
        from4[m.C1C1ManyToOne].Should().BeNull();
        from4[m.C1C1ManyToOne].Should().BeNull();

        // 1-1
        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to; // Twice

        to[m.C1sWhereC1ManyToOne].ToArray().Should().HaveCount(1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().HaveCount(1);
        to[m.C1sWhereC1ManyToOne].ElementAt(0).Should().Be(from1);
        to[m.C1sWhereC1ManyToOne].ElementAt(0).Should().Be(from1);
        from1[m.C1C1ManyToOne].Should().Be(to);
        from1[m.C1C1ManyToOne].Should().Be(to);
        from2[m.C1C1ManyToOne].Should().BeNull();
        from2[m.C1C1ManyToOne].Should().BeNull();
        from3[m.C1C1ManyToOne].Should().BeNull();
        from3[m.C1C1ManyToOne].Should().BeNull();
        from4[m.C1C1ManyToOne].Should().BeNull();
        from4[m.C1C1ManyToOne].Should().BeNull();

        // 1-2
        from2[m.C1C1ManyToOne] = to;
        from2[m.C1C1ManyToOne] = to; // Twice

        to[m.C1sWhereC1ManyToOne].ToArray().Length.Should().Be(2);
        to[m.C1sWhereC1ManyToOne].ToArray().Length.Should().Be(2);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from2);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from2);
        from1[m.C1C1ManyToOne].Should().Be(to);
        from1[m.C1C1ManyToOne].Should().Be(to);
        from2[m.C1C1ManyToOne].Should().Be(to);
        from2[m.C1C1ManyToOne].Should().Be(to);
        from3[m.C1C1ManyToOne].Should().BeNull();
        from3[m.C1C1ManyToOne].Should().BeNull();
        from4[m.C1C1ManyToOne].Should().BeNull();
        from4[m.C1C1ManyToOne].Should().BeNull();

        // 1-3
        from3[m.C1C1ManyToOne] = to;
        from3[m.C1C1ManyToOne] = to; // Twice

        to[m.C1sWhereC1ManyToOne].ToArray().Length.Should().Be(3);
        to[m.C1sWhereC1ManyToOne].ToArray().Length.Should().Be(3);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from2);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from2);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from3);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from3);
        from1[m.C1C1ManyToOne].Should().Be(to);
        from1[m.C1C1ManyToOne].Should().Be(to);
        from2[m.C1C1ManyToOne].Should().Be(to);
        from2[m.C1C1ManyToOne].Should().Be(to);
        from3[m.C1C1ManyToOne].Should().Be(to);
        from3[m.C1C1ManyToOne].Should().Be(to);
        from4[m.C1C1ManyToOne].Should().BeNull();
        from4[m.C1C1ManyToOne].Should().BeNull();

        // 1-4
        from4[m.C1C1ManyToOne] = to;
        from4[m.C1C1ManyToOne] = to; // Twice

        to[m.C1sWhereC1ManyToOne].ToArray().Length.Should().Be(4);
        to[m.C1sWhereC1ManyToOne].ToArray().Length.Should().Be(4);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from2);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from2);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from3);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from3);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from4);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from4);
        from1[m.C1C1ManyToOne].Should().Be(to);
        from1[m.C1C1ManyToOne].Should().Be(to);
        from2[m.C1C1ManyToOne].Should().Be(to);
        from2[m.C1C1ManyToOne].Should().Be(to);
        from3[m.C1C1ManyToOne].Should().Be(to);
        from3[m.C1C1ManyToOne].Should().Be(to);
        from4[m.C1C1ManyToOne].Should().Be(to);
        from4[m.C1C1ManyToOne].Should().Be(to);

        // 1-3
        from4[m.C1C1ManyToOne] = null;
        from4[m.C1C1ManyToOne] = null; // Twice

        to[m.C1sWhereC1ManyToOne].ToArray().Length.Should().Be(3);
        to[m.C1sWhereC1ManyToOne].ToArray().Length.Should().Be(3);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from2);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from2);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from3);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from3);
        from1[m.C1C1ManyToOne].Should().Be(to);
        from1[m.C1C1ManyToOne].Should().Be(to);
        from2[m.C1C1ManyToOne].Should().Be(to);
        from2[m.C1C1ManyToOne].Should().Be(to);
        from3[m.C1C1ManyToOne].Should().Be(to);
        from3[m.C1C1ManyToOne].Should().Be(to);
        from4[m.C1C1ManyToOne].Should().BeNull();
        from4[m.C1C1ManyToOne].Should().BeNull();

        // 1-2
        from3[m.C1C1ManyToOne] = null;
        from3[m.C1C1ManyToOne] = null; // Twice

        to[m.C1sWhereC1ManyToOne].ToArray().Length.Should().Be(2);
        to[m.C1sWhereC1ManyToOne].ToArray().Length.Should().Be(2);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from2);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from2);
        from1[m.C1C1ManyToOne].Should().Be(to);
        from1[m.C1C1ManyToOne].Should().Be(to);
        from2[m.C1C1ManyToOne].Should().Be(to);
        from2[m.C1C1ManyToOne].Should().Be(to);
        from3[m.C1C1ManyToOne].Should().BeNull();
        from3[m.C1C1ManyToOne].Should().BeNull();
        from4[m.C1C1ManyToOne].Should().BeNull();
        from4[m.C1C1ManyToOne].Should().BeNull();

        // 1-1
        from2[m.C1C1ManyToOne] = null;
        from2[m.C1C1ManyToOne] = null; // Twice

        to[m.C1sWhereC1ManyToOne].ToArray().Should().HaveCount(1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().HaveCount(1);
        to[m.C1sWhereC1ManyToOne].ElementAt(0).Should().Be(from1);
        to[m.C1sWhereC1ManyToOne].ElementAt(0).Should().Be(from1);
        from1[m.C1C1ManyToOne].Should().Be(to);
        from1[m.C1C1ManyToOne].Should().Be(to);
        from2[m.C1C1ManyToOne].Should().BeNull();
        from2[m.C1C1ManyToOne].Should().BeNull();
        from3[m.C1C1ManyToOne].Should().BeNull();
        from3[m.C1C1ManyToOne].Should().BeNull();
        from4[m.C1C1ManyToOne].Should().BeNull();
        from4[m.C1C1ManyToOne].Should().BeNull();

        // 0-0
        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null; // Twice

        from1[m.C1C1ManyToOne].Should().BeNull();
        from1[m.C1C1ManyToOne].Should().BeNull();
        from2[m.C1C1ManyToOne].Should().BeNull();
        from2[m.C1C1ManyToOne].Should().BeNull();
        from3[m.C1C1ManyToOne].Should().BeNull();
        from3[m.C1C1ManyToOne].Should().BeNull();
        from4[m.C1C1ManyToOne].Should().BeNull();
        from4[m.C1C1ManyToOne].Should().BeNull();

        // Exist
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();
        from1.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from1.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from2.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from2.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from3.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from3.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from4.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from4.Exist(m.C1C1ManyToOne).Should().BeFalse();

        // 1-1
        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to; // Twice

        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();
        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from2.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from2.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from3.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from3.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from4.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from4.Exist(m.C1C1ManyToOne).Should().BeFalse();

        // 1-2
        from2[m.C1C1ManyToOne] = to;
        from2[m.C1C1ManyToOne] = to; // Twice

        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();
        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from2.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from2.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from3.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from3.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from4.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from4.Exist(m.C1C1ManyToOne).Should().BeFalse();

        // 1-3
        from3[m.C1C1ManyToOne] = to;
        from3[m.C1C1ManyToOne] = to; // Twice

        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();
        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from2.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from2.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from3.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from3.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from4.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from4.Exist(m.C1C1ManyToOne).Should().BeFalse();

        // 1-4
        from4[m.C1C1ManyToOne] = to;
        from4[m.C1C1ManyToOne] = to; // Twice

        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();
        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from2.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from2.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from3.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from3.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from4.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from4.Exist(m.C1C1ManyToOne).Should().BeTrue();

        // 1-3
        from4[m.C1C1ManyToOne] = null;
        from4[m.C1C1ManyToOne] = null; // Twice

        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();
        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from2.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from2.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from3.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from3.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from4.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from4.Exist(m.C1C1ManyToOne).Should().BeFalse();

        // 1-2
        from3[m.C1C1ManyToOne] = null;
        from3[m.C1C1ManyToOne] = null; // Twice

        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();
        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from2.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from2.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from3.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from3.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from4.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from4.Exist(m.C1C1ManyToOne).Should().BeFalse();

        // 1-1
        from2[m.C1C1ManyToOne] = null;
        from2[m.C1C1ManyToOne] = null; // Twice

        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();
        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from2.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from2.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from3.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from3.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from4.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from4.Exist(m.C1C1ManyToOne).Should().BeFalse();

        // 0-0
        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null; // Twice

        to.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();
        from1.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from1.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from2.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from2.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from3.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from3.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from4.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from4.Exist(m.C1C1ManyToOne).Should().BeFalse();

        // Multiplicity
        // Same New / Same To
        // Get
        from1[m.C1C1ManyToOne].Should().BeNull();
        from1[m.C1C1ManyToOne].Should().BeNull();
        to[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();
        to[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to; // Twice

        from1[m.C1C1ManyToOne].Should().Be(to);
        from1[m.C1C1ManyToOne].Should().Be(to);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().HaveCount(1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().HaveCount(1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from1);

        from1[m.C1C1ManyToOne] = null;

        from1[m.C1C1ManyToOne].Should().BeNull();
        from1[m.C1C1ManyToOne].Should().BeNull();
        to[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();
        to[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();

        // Exist
        from1.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from1.Exist(m.C1C1ManyToOne).Should().BeFalse();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to; // Twice

        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();

        from1[m.C1C1ManyToOne] = null;

        from1.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from1.Exist(m.C1C1ManyToOne).Should().BeFalse();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();

        // Same New / Different To
        // Get
        to[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();
        to[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();
        from1[m.C1C1ManyToOne].Should().BeNull();
        from1[m.C1C1ManyToOne].Should().BeNull();
        toAnother[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();
        toAnother[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to; // Twice

        to[m.C1sWhereC1ManyToOne].ToArray().Should().HaveCount(1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().HaveCount(1);
        from1[m.C1C1ManyToOne].Should().Be(to);
        from1[m.C1C1ManyToOne].Should().Be(to);
        toAnother[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();
        toAnother[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from1);

        from1[m.C1C1ManyToOne] = toAnother;
        from1[m.C1C1ManyToOne] = toAnother; // Twice

        to[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();
        to[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();
        from1[m.C1C1ManyToOne].Should().Be(toAnother);
        from1[m.C1C1ManyToOne].Should().Be(toAnother);
        toAnother[m.C1sWhereC1ManyToOne].ToArray().Should().HaveCount(1);
        toAnother[m.C1sWhereC1ManyToOne].ToArray().Should().HaveCount(1);
        toAnother[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from1);
        toAnother[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from1);

        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null; // Twice

        to[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();
        to[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();
        from1[m.C1C1ManyToOne].Should().BeNull();
        from1[m.C1C1ManyToOne].Should().BeNull();
        toAnother[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();
        toAnother[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();

        // Exist
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();
        from1.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from1.Exist(m.C1C1ManyToOne).Should().BeFalse();
        toAnother.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();
        toAnother.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to; // Twice

        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();
        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        toAnother.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();
        toAnother.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();

        from1[m.C1C1ManyToOne] = toAnother;
        from1[m.C1C1ManyToOne] = toAnother; // Twice

        to.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();
        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        toAnother.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();
        toAnother.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();

        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null; // Twice
        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null; // Twice

        to.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();
        from1.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from1.Exist(m.C1C1ManyToOne).Should().BeFalse();
        toAnother.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();
        toAnother.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();

        // Different New / Different To
        // Get
        from1[m.C1C1ManyToOne].Should().BeNull();
        from1[m.C1C1ManyToOne].Should().BeNull();
        to[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();
        to[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();
        from2[m.C1C1ManyToOne].Should().BeNull();
        from2[m.C1C1ManyToOne].Should().BeNull();
        toAnother[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();
        toAnother[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to; // Twice

        from1[m.C1C1ManyToOne].Should().Be(to);
        from1[m.C1C1ManyToOne].Should().Be(to);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().HaveCount(1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().HaveCount(1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from1);
        from2[m.C1C1ManyToOne].Should().BeNull();
        from2[m.C1C1ManyToOne].Should().BeNull();
        toAnother[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();
        toAnother[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();

        from2[m.C1C1ManyToOne] = toAnother;
        from2[m.C1C1ManyToOne] = toAnother; // Twice

        from1[m.C1C1ManyToOne].Should().Be(to);
        from1[m.C1C1ManyToOne].Should().Be(to);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().HaveCount(1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().HaveCount(1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from1);
        from2[m.C1C1ManyToOne].Should().Be(toAnother);
        from2[m.C1C1ManyToOne].Should().Be(toAnother);
        toAnother[m.C1sWhereC1ManyToOne].ToArray().Should().HaveCount(1);
        toAnother[m.C1sWhereC1ManyToOne].ToArray().Should().HaveCount(1);
        toAnother[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from2);
        toAnother[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from2);

        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null; // Twice
        from2[m.C1C1ManyToOne] = null;
        from2[m.C1C1ManyToOne] = null; // Twice

        from1[m.C1C1ManyToOne].Should().BeNull();
        from1[m.C1C1ManyToOne].Should().BeNull();
        to[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();
        to[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();
        from2[m.C1C1ManyToOne].Should().BeNull();
        from2[m.C1C1ManyToOne].Should().BeNull();
        toAnother[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();
        toAnother[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();

        // Exist
        from1.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from1.Exist(m.C1C1ManyToOne).Should().BeFalse();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();
        from2.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from2.Exist(m.C1C1ManyToOne).Should().BeFalse();
        toAnother.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();
        toAnother.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to; // Twice

        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();
        from2.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from2.Exist(m.C1C1ManyToOne).Should().BeFalse();
        toAnother.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();
        toAnother.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();

        from2[m.C1C1ManyToOne] = toAnother;
        from2[m.C1C1ManyToOne] = toAnother; // Twice

        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();
        from2.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from2.Exist(m.C1C1ManyToOne).Should().BeTrue();
        toAnother.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();
        toAnother.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();

        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null; // Twice
        from2[m.C1C1ManyToOne] = null;
        from2[m.C1C1ManyToOne] = null; // Twice

        from1.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from1.Exist(m.C1C1ManyToOne).Should().BeFalse();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();
        from2.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from2.Exist(m.C1C1ManyToOne).Should().BeFalse();
        toAnother.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();
        toAnother.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();

        // Different New / Same To
        // Get
        from1[m.C1C1ManyToOne].Should().BeNull();
        from1[m.C1C1ManyToOne].Should().BeNull();
        from2[m.C1C1ManyToOne].Should().BeNull();
        from2[m.C1C1ManyToOne].Should().BeNull();
        to[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();
        to[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to; // Twice

        from1[m.C1C1ManyToOne].Should().Be(to);
        from1[m.C1C1ManyToOne].Should().Be(to);
        from2[m.C1C1ManyToOne].Should().BeNull();
        from2[m.C1C1ManyToOne].Should().BeNull();
        to[m.C1sWhereC1ManyToOne].ToArray().Should().HaveCount(1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().HaveCount(1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().NotContain(from2);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().NotContain(from2);

        from2[m.C1C1ManyToOne] = to;
        from2[m.C1C1ManyToOne] = to; // Twice

        from1[m.C1C1ManyToOne].Should().Be(to);
        from1[m.C1C1ManyToOne].Should().Be(to);
        from2[m.C1C1ManyToOne].Should().Be(to);
        from2[m.C1C1ManyToOne].Should().Be(to);
        to[m.C1sWhereC1ManyToOne].ToArray().Length.Should().Be(2);
        to[m.C1sWhereC1ManyToOne].ToArray().Length.Should().Be(2);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from1);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from2);
        to[m.C1sWhereC1ManyToOne].ToArray().Should().Contain(from2);

        from1[m.C1C1ManyToOne] = null;
        from2[m.C1C1ManyToOne] = null;

        from1[m.C1C1ManyToOne].Should().BeNull();
        from1[m.C1C1ManyToOne].Should().BeNull();
        from2[m.C1C1ManyToOne].Should().BeNull();
        from2[m.C1C1ManyToOne].Should().BeNull();
        to[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();
        to[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();

        // Exist
        from1.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from1.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from2.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from2.Exist(m.C1C1ManyToOne).Should().BeFalse();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeFalse();

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to; // Twice

        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from2.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from2.Exist(m.C1C1ManyToOne).Should().BeFalse();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();

        from2[m.C1C1ManyToOne] = to;
        from2[m.C1C1ManyToOne] = to; // Twice

        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from1.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from2.Exist(m.C1C1ManyToOne).Should().BeTrue();
        from2.Exist(m.C1C1ManyToOne).Should().BeTrue();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();
        to.Exist(m.C1sWhereC1ManyToOne).Should().BeTrue();

        from1[m.C1C1ManyToOne] = null;
        from2[m.C1C1ManyToOne] = null;

        from1[m.C1C1ManyToOne].Should().BeNull();
        from1[m.C1C1ManyToOne].Should().BeNull();
        from2[m.C1C1ManyToOne].Should().BeNull();
        from2[m.C1C1ManyToOne].Should().BeNull();
        to[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();
        to[m.C1sWhereC1ManyToOne].ToArray().Should().BeEmpty();

        // Null & Empty Array
        // Set Null
        // Get
        from1[m.C1C1ManyToOne].Should().BeNull();
        from1[m.C1C1ManyToOne].Should().BeNull();

        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null; // Twice

        from1[m.C1C1ManyToOne].Should().BeNull();
        from1[m.C1C1ManyToOne].Should().BeNull();

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to; // Twice
        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null; // Twice

        from1[m.C1C1ManyToOne].Should().BeNull();
        from1[m.C1C1ManyToOne].Should().BeNull();

        // Exist
        from1.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from1.Exist(m.C1C1ManyToOne).Should().BeFalse();

        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null; // Twice

        from1.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from1.Exist(m.C1C1ManyToOne).Should().BeFalse();

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to; // Twice
        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null; // Twice

        from1.Exist(m.C1C1ManyToOne).Should().BeFalse();
        from1.Exist(m.C1C1ManyToOne).Should().BeFalse();
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
                (association, _, _, to) => to[association].Should().BeEmpty(),
                (_, role, from, _) => from[role].Should().BeNull()
            ]);
    }

    [Fact]
    public void FromToSet()
    {
        this.FromTo(
            () =>
            [
                (_, role, from, to) => from[role] = to
            ],
            () =>
            [
                (association, _, from, to) => to[association].Should().BeEquivalentTo([from]),
                (_, role, from, to) => from[role].Should().BeSameAs(to)
            ]);
    }

    [Fact]
    public void FromToSetFromAnotherToSet()
    {
        this.FromToToAnother(
            () =>
            [
                (_, role, from, to, toAnother) => from[role] = to,
                (_, role, from, to, toAnother) => from[role] = toAnother
            ],
            () =>
            [
                (association, _, from, to, toAnother) => to[association].Should().BeEmpty(),
                (association, _, from, to, toAnother) => toAnother[association].Should().BeEquivalentTo([from]),
                (_, role, from, to, toAnother) => from[role].Should().BeSameAs(toAnother),
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

            c1a.Invoking(v => v[m.C1C2ManyToOne] = c1b)
                .Should().Throw<ArgumentException>();

            preact(transaction);

            c1a.Invoking(v => v[m.C1I2ManyToOne] = c1b)
                .Should().Throw<ArgumentException>();

            preact(transaction);

            c1a.Invoking(v => v[m.C1S2ManyToOne] = c1b)
                .Should().Throw<ArgumentException>();

            preact(transaction);

            c1a.Invoking(v => v[m.C2C1ManyToOne] = c2a)
                .Should().Throw<ArgumentException>();
        }
    }

    protected abstract IDatabase CreateDatabase();

    private void FromTo(
       Func<IEnumerable<Action<ManyToOneAssociationType, ManyToOneRoleType, IObject, IObject>>> acts,
       Func<IEnumerable<Action<ManyToOneAssociationType, ManyToOneRoleType, IObject, IObject>>> asserts)
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

    private void FromToToAnother(
       Func<IEnumerable<Action<ManyToOneAssociationType, ManyToOneRoleType, IObject, IObject, IObject>>> acts,
       Func<IEnumerable<Action<ManyToOneAssociationType, ManyToOneRoleType, IObject, IObject, IObject>>> asserts)
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
                            var (association, role, _, fromBuilder, _, toBuilder, toAnotherBuilder, _, _) = fixture();

                            var database = this.CreateDatabase();
                            var transaction = database.CreateTransaction();

                            var from = fromBuilder(transaction);
                            var to = toBuilder(transaction);
                            var toAnother = toAnotherBuilder(transaction);

                            foreach (var act in acts())
                            {
                                for (var actRepeat = 0; actRepeat < actRepeats; actRepeat++)
                                {
                                    preact(transaction);
                                    act(association, role, from, to, toAnother);
                                }
                            }

                            foreach (var assert in assertPermutation)
                            {
                                for (var assertRepeat = 0; assertRepeat < assertRepeats; assertRepeat++)
                                {
                                    assert(association, role, from, to, toAnother);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
