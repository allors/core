namespace Allors.Core.Database.Tests.Meta;

using System.Linq;
using Allors.Core.Database.Meta;
using Allors.Core.Database.MetaMeta;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;
using FluentAssertions;
using Xunit;

public class CoreMetaTests
{
    [Fact]
    public void Build()
    {
        var metaMeta = new MetaMeta();
        CoreMetaMeta.Populate(metaMeta);

        var meta = new Meta(metaMeta);
        CoreMeta.Populate(meta);

        meta.Derive();

        var domains = meta.Objects.OfType<Domain>().ToArray();
        var units = meta.Objects.OfType<Unit>().ToArray();
        var interfaces = meta.Objects.OfType<Interface>().ToArray();
        var classes = meta.Objects.OfType<Class>().ToArray();
        var associationTypes = meta.Objects.OfType<IAssociationType>().ToArray();
        var roleTypes = meta.Objects.OfType<IRoleType>().ToArray();
        var methodTypes = meta.Objects.OfType<MethodType>().ToArray();
        var methodParts = meta.Objects.OfType<MethodPart>().ToArray();

        meta.Objects.Count.Should().Be(15);

        domains.Should().HaveCount(1);
        units.Should().HaveCount(8);
        interfaces.Should().HaveCount(1);
        classes.Should().BeEmpty();
        associationTypes.Should().BeEmpty();
        roleTypes.Should().BeEmpty();
        methodTypes.Should().HaveCount(4);
        methodParts.Should().HaveCount(1);
    }
}
