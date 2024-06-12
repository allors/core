namespace Allors.Core.Database.Tests.Meta;

using System.Linq;
using Allors.Core.Database.Meta;
using Allors.Core.Database.Meta.Domain;
using Allors.Core.Database.MetaMeta;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;
using Allors.Core.MetaMeta.Diagrams;
using Xunit;

public class CoreMetaTests
{
    [Fact]
    public void Diagram()
    {
        var metaMeta = new MetaMeta();
        CoreMetaMeta.Populate(metaMeta);

        var meta = new Meta(metaMeta);
        CoreMeta.Populate(meta);

        meta.Derive();

        var diagram = new ClassDiagram(metaMeta).Render();

        Assert.NotNull(diagram);
    }

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

        Assert.Equal(10, meta.Objects.Count);

        Assert.Single(domains);
        Assert.Equal(8, units.Length);
        Assert.Single(interfaces);
        Assert.Empty(classes);
        Assert.Empty(associationTypes);
        Assert.Empty(roleTypes);
    }
}
