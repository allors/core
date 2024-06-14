namespace Allors.Core.Meta.Tests;

using System;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;
using FluentAssertions;
using Xunit;

public class MetaPopulationTests
{
    [Fact]
    public void New()
    {
        var metaMeta = new MetaMeta();

        var @string = metaMeta.AddUnit(Guid.NewGuid(), "String");
        var named = metaMeta.AddInterface(Guid.NewGuid(), "Named");
        var organization = metaMeta.AddClass(Guid.NewGuid(), "Organization", named);
        var person = metaMeta.AddClass(Guid.NewGuid(), "Person", named);
        metaMeta.AddUnitRelation(Guid.NewGuid(), Guid.NewGuid(), named, @string, "Name");
        metaMeta.AddOneToOneRelation(Guid.NewGuid(), Guid.NewGuid(), organization, person, "Owner");

        var meta = new Meta(metaMeta);

        var acme = meta.Build(organization, v =>
        {
            v["Name"] = "Acme";
            v["Owner"] = meta.Build(person, w => w["Name"] = "Jane");
        });

        var jane = (MetaObject)acme["Owner"]!;

        acme["Name"].Should().Be("Acme");
        jane["Name"].Should().Be("Jane");

        jane["OrganizationWhereOwner"].Should().Be(acme);
    }
}
