namespace Allors.Core.Database.Engines.Tests;

using System.Linq;
using Allors.Core.Database.Engines.Tests.Extensions;
using Allors.Core.Database.Engines.Tests.Meta;
using FluentAssertions;
using Xunit;

public abstract class ChangesTests : Tests
{
    [Fact]
    public void StringRole()
    {
        var database = this.CreateDatabase();
        var transaction = database.CreateTransaction();

        var m = this.Meta;

        var a = transaction.Build(m.C1);
        var c = transaction.Build(m.C3);

        transaction.Commit();

        a = transaction.Instantiate(a.Id)!;
        var b = transaction.Build(m.C2);
        transaction.Instantiate(c.Id);

        a[m.C1AllorsString] = null;
        b[m.C2AllorsString] = null;

        var changeSet = transaction.Checkpoint();

        var associations = changeSet.Associations;
        var roles = changeSet.Roles;

        associations.Should().BeEmpty();
        roles.Should().BeEmpty();

        a[m.C1AllorsString] = "a changed";
        b[m.C2AllorsString] = "b changed";

        changeSet = transaction.Checkpoint();

        associations = changeSet.Associations;
        roles = changeSet.Roles;

        associations.Count.Should().Be(2);
        associations.ToArray().Should().Contain(a);
        associations.ToArray().Should().Contain(b);

        a[m.C1AllorsString].Should().Be("a changed");
        b[m.C2AllorsString].Should().Be("b changed");

        changeSet.GetRoleTypes(a).Should().HaveCount(1);
        changeSet.GetRoleTypes(a).First().Should().Be(m.C1AllorsString());

        changeSet.GetRoleTypes(b).Should().HaveCount(1);
        changeSet.GetRoleTypes(b).First().Should().Be(m.C2AllorsString());

        associations.Contains(a).Should().BeTrue();
        associations.Contains(b).Should().BeTrue();
        associations.Contains(c).Should().BeFalse();

        roles.Contains(a).Should().BeFalse();
        roles.Contains(b).Should().BeFalse();
        roles.Contains(c).Should().BeFalse();

        a[m.C1AllorsString] = "a changed";
        b[m.C2AllorsString] = "b changed";

        changeSet = transaction.Checkpoint();

        associations = changeSet.Associations;
        roles = changeSet.Roles;

        associations.Should().BeEmpty();
        roles.Should().BeEmpty();

        a[m.C1AllorsString] = "a changed again";
        b[m.C2AllorsString] = "b changed again";

        changeSet = transaction.Checkpoint();

        associations = changeSet.Associations;
        roles = changeSet.Roles;

        associations.Count.Should().Be(2);
        associations.Contains(a).Should().BeTrue();
        associations.Contains(a).Should().BeTrue();

        changeSet.GetRoleTypes(a).Should().HaveCount(1);
        changeSet.GetRoleTypes(a).First().Should().Be(m.C1AllorsString());

        changeSet.GetRoleTypes(b).Should().HaveCount(1);
        changeSet.GetRoleTypes(b).First().Should().Be(m.C2AllorsString());

        associations.Contains(a).Should().BeTrue();
        associations.Contains(b).Should().BeTrue();
        associations.Contains(c).Should().BeFalse();

        roles.Contains(a).Should().BeFalse();
        roles.Contains(b).Should().BeFalse();
        roles.Contains(c).Should().BeFalse();

        changeSet = transaction.Checkpoint();

        associations = changeSet.Associations;
        roles = changeSet.Roles;

        associations.Should().BeEmpty();
        changeSet.GetRoleTypes(a).Should().BeEmpty();
        changeSet.GetRoleTypes(b).Should().BeEmpty();

        associations.Contains(a).Should().BeFalse();
        associations.Contains(b).Should().BeFalse();
        associations.Contains(c).Should().BeFalse();

        roles.Contains(a).Should().BeFalse();
        roles.Contains(b).Should().BeFalse();
        roles.Contains(c).Should().BeFalse();

        a[m.C1AllorsString] = null;
        b[m.C2AllorsString] = null;

        changeSet = transaction.Checkpoint();

        associations = changeSet.Associations;
        roles = changeSet.Roles;

        associations.Count.Should().Be(2);
        associations.Contains(a).Should().BeTrue();
        associations.Contains(a).Should().BeTrue();

        changeSet.GetRoleTypes(a).Should().HaveCount(1);
        changeSet.GetRoleTypes(a).First().Should().Be(m.C1AllorsString());

        changeSet.GetRoleTypes(b).Should().HaveCount(1);
        changeSet.GetRoleTypes(b).First().Should().Be(m.C2AllorsString());

        associations.Contains(a).Should().BeTrue();
        associations.Contains(b).Should().BeTrue();
        associations.Contains(c).Should().BeFalse();

        roles.Contains(a).Should().BeFalse();
        roles.Contains(b).Should().BeFalse();
        roles.Contains(c).Should().BeFalse();

        changeSet = transaction.Checkpoint();

        associations = changeSet.Associations;
        roles = changeSet.Roles;

        associations.Should().BeEmpty();
        changeSet.GetRoleTypes(a).Should().BeEmpty();
        changeSet.GetRoleTypes(b).Should().BeEmpty();

        associations.Contains(a).Should().BeFalse();
        associations.Contains(b).Should().BeFalse();
        associations.Contains(c).Should().BeFalse();

        roles.Contains(a).Should().BeFalse();
        roles.Contains(b).Should().BeFalse();

        transaction.Rollback();

        changeSet = transaction.Checkpoint();

        associations = changeSet.Associations;
        roles = changeSet.Roles;

        associations.Should().BeEmpty();
        changeSet.GetRoleTypes(a).Should().BeEmpty();
        changeSet.GetRoleTypes(b).Should().BeEmpty();

        associations.Contains(a).Should().BeFalse();
        associations.Contains(b).Should().BeFalse();
        associations.Contains(c).Should().BeFalse();

        roles.Contains(a).Should().BeFalse();
        roles.Contains(b).Should().BeFalse();

        a[m.C1AllorsString] = "a changed";

        transaction.Commit();

        changeSet = transaction.Checkpoint();

        associations = changeSet.Associations;
        roles = changeSet.Roles;

        associations.Should().BeEmpty();
        changeSet.GetRoleTypes(a).Should().BeEmpty();
        changeSet.GetRoleTypes(b).Should().BeEmpty();

        associations.Contains(a).Should().BeFalse();
        associations.Contains(b).Should().BeFalse();
        associations.Contains(c).Should().BeFalse();

        roles.Contains(a).Should().BeFalse();
        roles.Contains(b).Should().BeFalse();

        a[m.C1AllorsString] = null;
        a[m.C1AllorsString] = "a changed";

        transaction.Commit();

        changeSet = transaction.Checkpoint();

        associations = changeSet.Associations;
        roles = changeSet.Roles;

        associations.Should().BeEmpty();
        changeSet.GetRoleTypes(a).Should().BeEmpty();
        changeSet.GetRoleTypes(b).Should().BeEmpty();

        associations.Contains(a).Should().BeFalse();
        associations.Contains(b).Should().BeFalse();
        associations.Contains(c).Should().BeFalse();

        roles.Contains(a).Should().BeFalse();
        roles.Contains(b).Should().BeFalse();
    }

    protected abstract IDatabase CreateDatabase();
}
