namespace Allors.Core.Meta.Tests;

using System;
using Allors.Core.Meta.Tests.Static;
using Allors.Core.MetaMeta;
using Xunit;

public class StaticTests
{
    [Fact]
    public void C1C2OneToOne()
    {
        var meta = new MetaMeta();
        var domain = meta.AddDomain(Guid.NewGuid(), "Domain");
        var c1 = meta.AddClass<C1>(domain, Guid.NewGuid());
        var c2 = meta.AddClass<C2>(domain, Guid.NewGuid());
        var c1C2OneToOne = meta.AddOneToOneRelation(domain, Guid.NewGuid(), Guid.NewGuid(), c1, c2, "C2OneToOne");

        var population = new MetaPopulation(meta);

        var c1a = population.Build<C1>();
        var c1b = population.Build<C1>();
        var c2a = population.Build<C2>();

        c1a[c1C2OneToOne] = c2a;

        Assert.Equal(c2a, c1a[c1C2OneToOne]);
        Assert.Null(c1b[c1C2OneToOne]);
        Assert.Equal(c1a, c2a[c1C2OneToOne.AssociationType]);
    }
}
