namespace Allors.Core.Database.Tests.Meta;

using System.Linq;
using Allors.Core.Database.Meta;
using Allors.Core.Database.Meta.Domain;
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

        Assert.Equal(149, meta.Objects.Count);

        Assert.Single(domains);
        Assert.Equal(6, units.Length);
        Assert.Equal(17, interfaces.Length);
        Assert.Equal(29, classes.Length);
        Assert.Equal(26, associationTypes.Length);
        Assert.Equal(26, roleTypes.Length);
    }
}
