namespace Allors.Core.Meta.Tests.Domain;

using Allors.Core.Meta.Domain;
using Allors.Core.Meta.Meta;
using Xunit;

public class MetaPopulationTests
{
    [Fact]
    public void New()
    {
        var meta = new MetaMeta();
        var named = meta.AddInterface("Named");
        var organization = meta.AddClass("Organization", named);
        var person = meta.AddClass("Person", named);
        meta.AddUnit<string>(named, "Name");
        meta.AddOneToOne(organization, person, "Owner");

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
