namespace Allors.Core.Meta.Tests.Domain;

using System;
using Allors.Core.Meta.Domain;
using Allors.Core.Meta.Meta;
using Xunit;

public class MetaPopulationTests
{
    [Fact]
    public void New()
    {
        var meta = new MetaMeta();
        var domain = meta.AddDomain(Guid.NewGuid(), "Domain");
        var @string = meta.AddUnit(domain, Guid.NewGuid(), "String");
        var named = meta.AddInterface(domain, Guid.NewGuid(), "Named");
        var organization = meta.AddClass(domain, Guid.NewGuid(), "Organization", named);
        var person = meta.AddClass(domain, Guid.NewGuid(), "Person", named);
        meta.AddUnitRelation(domain, Guid.NewGuid(), Guid.NewGuid(), named, @string, "Name");
        meta.AddOneToOneRelation(domain, Guid.NewGuid(), Guid.NewGuid(), organization, person, "Owner");

        var population = new MetaPopulation(meta);

        var acme = population.Build(organization, v =>
        {
            v["Name"] = "Acme";
            v["Owner"] = population.Build(person, w => w["Name"] = "Jane");
        });

        var jane = (MetaObject)acme["Owner"]!;

        Assert.Equal("Acme", acme["Name"]);
        Assert.Equal("Jane", jane["Name"]);

        Assert.Equal(acme, jane["OrganizationWhereOwner"]);
    }
}
