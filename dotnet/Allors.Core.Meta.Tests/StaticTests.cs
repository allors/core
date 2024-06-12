namespace Allors.Core.Meta.Tests;

using System;
using Allors.Core.Meta.Tests.Domain;
using Allors.Core.MetaMeta;
using Xunit;

public class StaticTests
{
    [Fact]
    public void C1C2OneToOne()
    {
        var metaMeta = new MetaMeta();

        var c1 = metaMeta.AddClass<C1>(Guid.NewGuid());
        var c2 = metaMeta.AddClass<C2>(Guid.NewGuid());
        var c1C2OneToOne = metaMeta.AddOneToOneRelation(Guid.NewGuid(), Guid.NewGuid(), c1, c2, "C2OneToOne");

        var meta = new Meta(metaMeta);

        var c1a = meta.Build<C1>();
        var c1b = meta.Build<C1>();
        var c2a = meta.Build<C2>();

        c1a[c1C2OneToOne] = c2a;

        Assert.Equal(c2a, c1a[c1C2OneToOne]);
        Assert.Null(c1b[c1C2OneToOne]);
        Assert.Equal(c1a, c2a[c1C2OneToOne.AssociationType]);
    }
}
