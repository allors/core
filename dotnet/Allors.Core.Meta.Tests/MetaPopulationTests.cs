namespace Allors.Core.Meta.Tests;

using System;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;
using Xunit;

public class MetaPopulationTests
{
    [Fact]
    public void New()
    {
        var metaMeta = new MetaMeta();
        var domain = metaMeta.AddDomain(Guid.NewGuid(), "Domain");
        var @string = metaMeta.AddUnit(domain, Guid.NewGuid(), "String");
        var named = metaMeta.AddInterface(domain, Guid.NewGuid(), "Named");
        var organization = metaMeta.AddClass(domain, Guid.NewGuid(), "Organization", named);
        var person = metaMeta.AddClass(domain, Guid.NewGuid(), "Person", named);
        metaMeta.AddUnitRelation(domain, Guid.NewGuid(), Guid.NewGuid(), named, @string, "Name");
        metaMeta.AddOneToOneRelation(domain, Guid.NewGuid(), Guid.NewGuid(), organization, person, "Owner");

        var meta = new Meta(metaMeta);

        var acme = meta.Build(organization, v =>
        {
            v["Name"] = "Acme";
            v["Owner"] = meta.Build(person, w => w["Name"] = "Jane");
        });

        var jane = (MetaObject)acme["Owner"]!;

        Assert.Equal("Acme", acme["Name"]);
        Assert.Equal("Jane", jane["Name"]);

        Assert.Equal(acme, jane["OrganizationWhereOwner"]);
    }
}
