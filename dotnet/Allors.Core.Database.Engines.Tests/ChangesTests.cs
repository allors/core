namespace Allors.Core.Database.Engines.Tests;

using System.Linq;
using Allors.Core.Database.Engines.Tests.Extensions;
using Xunit;

public abstract class ChangesTests : Tests
{
    [Fact]
    public void StringRole()
    {
        var database = this.CreateDatabase();
        var transaction = database.CreateTransaction();

        var m = this.Meta;

        var a = transaction.Build(m.C1());
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

        Assert.Empty(associations);
        Assert.Empty(roles);

        a[m.C1AllorsString] = "a changed";
        b[m.C2AllorsString] = "b changed";

        changeSet = transaction.Checkpoint();

        associations = changeSet.Associations;
        roles = changeSet.Roles;

        Assert.Equal(2, associations.Count());
        Assert.Contains(a, associations.ToArray());
        Assert.Contains(b, associations.ToArray());

        Assert.Equal("a changed", a[m.C1AllorsString]);
        Assert.Equal("b changed", b[m.C2AllorsString]);

        Assert.Single(changeSet.GetRoleTypes(a));
        Assert.Equal(m.C1AllorsString, changeSet.GetRoleTypes(a).First());

        Assert.Single(changeSet.GetRoleTypes(b));
        Assert.Equal(m.C2AllorsString, changeSet.GetRoleTypes(b).First());

        Assert.True(associations.Contains(a));
        Assert.True(associations.Contains(b));
        Assert.False(associations.Contains(c));

        Assert.False(roles.Contains(a));
        Assert.False(roles.Contains(b));
        Assert.False(roles.Contains(c));

        a[m.C1AllorsString] = "a changed";
        b[m.C2AllorsString] = "b changed";

        changeSet = transaction.Checkpoint();

        associations = changeSet.Associations;
        roles = changeSet.Roles;

        Assert.Empty(associations);
        Assert.Empty(roles);

        a[m.C1AllorsString] = "a changed again";
        b[m.C2AllorsString] = "b changed again";

        changeSet = transaction.Checkpoint();

        associations = changeSet.Associations;
        roles = changeSet.Roles;

        Assert.Equal(2, associations.Count());
        Assert.True(associations.Contains(a));
        Assert.True(associations.Contains(a));

        Assert.Single(changeSet.GetRoleTypes(a));
        Assert.Equal(m.C1AllorsString, changeSet.GetRoleTypes(a).First());

        Assert.Single(changeSet.GetRoleTypes(b));
        Assert.Equal(m.C2AllorsString, changeSet.GetRoleTypes(b).First());

        Assert.True(associations.Contains(a));
        Assert.True(associations.Contains(b));
        Assert.False(associations.Contains(c));

        Assert.False(roles.Contains(a));
        Assert.False(roles.Contains(b));
        Assert.False(roles.Contains(c));

        changeSet = transaction.Checkpoint();

        associations = changeSet.Associations;
        roles = changeSet.Roles;

        Assert.Empty(associations);
        Assert.Empty(changeSet.GetRoleTypes(a));
        Assert.Empty(changeSet.GetRoleTypes(b));

        Assert.False(associations.Contains(a));
        Assert.False(associations.Contains(b));
        Assert.False(associations.Contains(c));

        Assert.False(roles.Contains(a));
        Assert.False(roles.Contains(b));
        Assert.False(roles.Contains(c));

        a[m.C1AllorsString] = null;
        b[m.C2AllorsString] = null;

        changeSet = transaction.Checkpoint();

        associations = changeSet.Associations;
        roles = changeSet.Roles;

        Assert.Equal(2, associations.Count());
        Assert.True(associations.Contains(a));
        Assert.True(associations.Contains(a));

        Assert.Single(changeSet.GetRoleTypes(a));
        Assert.Equal(m.C1AllorsString, changeSet.GetRoleTypes(a).First());

        Assert.Single(changeSet.GetRoleTypes(b));
        Assert.Equal(m.C2AllorsString, changeSet.GetRoleTypes(b).First());

        Assert.True(associations.Contains(a));
        Assert.True(associations.Contains(b));
        Assert.False(associations.Contains(c));

        Assert.False(roles.Contains(a));
        Assert.False(roles.Contains(b));
        Assert.False(roles.Contains(c));

        changeSet = transaction.Checkpoint();

        associations = changeSet.Associations;
        roles = changeSet.Roles;

        Assert.Empty(associations);
        Assert.Empty(changeSet.GetRoleTypes(a));
        Assert.Empty(changeSet.GetRoleTypes(b));

        Assert.False(associations.Contains(a));
        Assert.False(associations.Contains(b));
        Assert.False(associations.Contains(c));

        Assert.False(roles.Contains(a));
        Assert.False(roles.Contains(b));

        transaction.Rollback();

        changeSet = transaction.Checkpoint();

        associations = changeSet.Associations;
        roles = changeSet.Roles;

        Assert.Empty(associations);
        Assert.Empty(changeSet.GetRoleTypes(a));
        Assert.Empty(changeSet.GetRoleTypes(b));

        Assert.False(associations.Contains(a));
        Assert.False(associations.Contains(b));
        Assert.False(associations.Contains(c));

        Assert.False(roles.Contains(a));
        Assert.False(roles.Contains(b));

        a[m.C1AllorsString] = "a changed";

        transaction.Commit();

        changeSet = transaction.Checkpoint();

        associations = changeSet.Associations;
        roles = changeSet.Roles;

        Assert.Empty(associations);
        Assert.Empty(changeSet.GetRoleTypes(a));
        Assert.Empty(changeSet.GetRoleTypes(b));

        Assert.False(associations.Contains(a));
        Assert.False(associations.Contains(b));
        Assert.False(associations.Contains(c));

        Assert.False(roles.Contains(a));
        Assert.False(roles.Contains(b));

        a[m.C1AllorsString] = null;
        a[m.C1AllorsString] = "a changed";

        transaction.Commit();

        changeSet = transaction.Checkpoint();

        associations = changeSet.Associations;
        roles = changeSet.Roles;

        Assert.Empty(associations);
        Assert.Empty(changeSet.GetRoleTypes(a));
        Assert.Empty(changeSet.GetRoleTypes(b));

        Assert.False(associations.Contains(a));
        Assert.False(associations.Contains(b));
        Assert.False(associations.Contains(c));

        Assert.False(roles.Contains(a));
        Assert.False(roles.Contains(b));
    }

    protected abstract IDatabase CreateDatabase();
}
