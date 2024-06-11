namespace Allors.Core.Database.Tests.Meta;

using System.Linq;
using Allors.Core.Database.Meta;
using Allors.Core.Database.Meta.Domain;
using Allors.Core.MetaMeta.Diagrams;
using Xunit;

public class CoreMetaTests
{
    [Fact]
    public void Diagram()
    {
        var coreMeta = new CoreMeta();
        var diagram = new ClassDiagram(coreMeta.CoreMetaMeta.MetaMeta).Render();

        Assert.NotNull(diagram);
    }

    [Fact]
    public void Build()
    {
        var coreMeta = new CoreMeta();

        var domains = coreMeta.Meta.Objects.OfType<Domain>().ToArray();
        var units = coreMeta.Meta.Objects.OfType<Unit>().ToArray();
        var interfaces = coreMeta.Meta.Objects.OfType<Interface>().ToArray();
        var classes = coreMeta.Meta.Objects.OfType<Class>().ToArray();
        var associationTypes = coreMeta.Meta.Objects.OfType<IAssociationType>().ToArray();
        var roleTypes = coreMeta.Meta.Objects.OfType<IRoleType>().ToArray();

        Assert.Equal(105, coreMeta.Meta.Objects.Count());

        Assert.Single(domains);
        Assert.Equal(6, units.Length);
        Assert.Equal(17, interfaces.Length);
        Assert.Equal(29, classes.Length);
        Assert.Equal(26, associationTypes.Length);
        Assert.Equal(26, roleTypes.Length);
    }
}
