namespace Allors.Core.Meta.Tests.Meta;

using System;
using Allors.Core.Meta.Meta;
using Xunit;

public class MetaObjectTypeTests
{
    [Fact]
    public void Supertypes()
    {
        var meta = new MetaMeta();
        var s1 = meta.AddInterface(Guid.NewGuid(), "S1");
        var i1 = meta.AddInterface(Guid.NewGuid(), "I1", s1);
        var c1 = meta.AddClass(Guid.NewGuid(), "C1", i1);

        Assert.Equal(2, c1.Supertypes.Count);
        Assert.Contains(i1, c1.Supertypes);
        Assert.Contains(s1, c1.Supertypes);

        Assert.Single(i1.Supertypes);
        Assert.Contains(s1, i1.Supertypes);

        Assert.Empty(s1.Supertypes);
    }

    [Fact]
    public void IsAssignableFrom()
    {
        var meta = new MetaMeta();
        var s1 = meta.AddInterface(Guid.NewGuid(), "S1");
        var i1 = meta.AddInterface(Guid.NewGuid(), "I1", s1);
        var c1 = meta.AddClass(Guid.NewGuid(), "C1", i1);

        Assert.True(c1.IsAssignableFrom(c1));
        Assert.True(i1.IsAssignableFrom(c1));
        Assert.True(s1.IsAssignableFrom(c1));

        Assert.False(c1.IsAssignableFrom(i1));
        Assert.True(i1.IsAssignableFrom(i1));
        Assert.True(s1.IsAssignableFrom(i1));

        Assert.False(c1.IsAssignableFrom(s1));
        Assert.False(i1.IsAssignableFrom(s1));
        Assert.True(s1.IsAssignableFrom(s1));
    }
}
