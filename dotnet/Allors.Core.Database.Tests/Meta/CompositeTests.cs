namespace Allors.Core.Database.Tests.Meta;

using System;
using Allors.Core.Database.Meta;
using FluentAssertions;
using Xunit;

public class CompositeTests
{
    [Fact]
    public void Supertypes()
    {
        var m = new CoreMeta();

        var s1 = m.AddInterface(Guid.NewGuid(), "S1");
        var i1 = m.AddInterface(Guid.NewGuid(), "I1");
        var c1 = m.AddInterface(Guid.NewGuid(), "C1");

        m.AddDirectSupertype(i1, s1);
        m.AddDirectSupertype(c1, i1);

        m.Freeze();

        s1[m.Meta.CompositeSupertypes].Should().BeEmpty();
        i1[m.Meta.CompositeSupertypes].Should().BeEquivalentTo([s1]);
        c1[m.Meta.CompositeSupertypes].Should().BeEquivalentTo([i1, s1]);
    }
}
