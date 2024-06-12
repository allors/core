namespace Allors.Core.Database.Tests.Meta;

using System;
using Allors.Core.Database.Meta;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;
using FluentAssertions;
using Xunit;

public class CompositeTests
{
    [Fact]
    public void Supertypes()
    {
        var metaMeta = new MetaMeta();
        CoreMetaMeta.Populate(metaMeta);

        var meta = new Meta(metaMeta);
        CoreMeta.Populate(meta);

        meta.Derive();

        var domain = meta.AddDomain(Guid.NewGuid(), "MyDomain");

        var s1 = meta.AddInterface(domain, Guid.NewGuid(), "S1");
        var i1 = meta.AddInterface(domain, Guid.NewGuid(), "I1");
        var c1 = meta.AddInterface(domain, Guid.NewGuid(), "C1");

        meta.AddInheritance(domain, Guid.NewGuid(), i1, s1);
        meta.AddInheritance(domain, Guid.NewGuid(), c1, i1);

        meta.Derive();

        s1[metaMeta.CompositeSupertypes()].Should().BeEmpty();
        i1[metaMeta.CompositeSupertypes()].Should().BeEquivalentTo([s1]);
        c1[metaMeta.CompositeSupertypes()].Should().BeEquivalentTo([i1, s1]);
    }
}
