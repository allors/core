namespace Allors.Core.Database.Tests.Meta;

using System;
using Allors.Core.Database.Meta;
using Allors.Core.Database.MetaMeta;
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
        var meta = new Meta(metaMeta);

        CoreMetaMeta.Populate(metaMeta);
        CoreMeta.Configure(meta);
        CoreMeta.Populate(meta);

        meta.Derive();

        var domain = meta.AddDomain(Guid.NewGuid(), "MyDomain");

        var s1 = domain.AddInterface(Guid.NewGuid(), "S1");
        var i1 = domain.AddInterface(Guid.NewGuid(), "I1");
        var c1 = domain.AddInterface(Guid.NewGuid(), "C1");

        domain.AddInheritance(i1, s1);
        domain.AddInheritance(c1, i1);

        meta.Derive();

        s1[metaMeta.CompositeSupertypes].Should().BeEmpty();
        i1[metaMeta.CompositeSupertypes].Should().BeEquivalentTo([s1]);
        c1[metaMeta.CompositeSupertypes].Should().BeEquivalentTo([i1, s1]);
    }
}
