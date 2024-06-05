namespace Allors.Core.Database.Tests.Meta;

using System.Linq;
using Allors.Core.Database.Meta;
using Allors.Core.Database.Meta.Domain;
using Allors.Core.Meta.Meta.Diagrams;
using Xunit;

public class CoreMetaTests
{
    [Fact]
    public void Diagram()
    {
        var coreMeta = new CoreMeta();
        var diagram = new ClassDiagram(coreMeta.Meta.MetaMeta).Render();

        Assert.NotNull(diagram);
    }

    [Fact]
    public void Build()
    {
        var coreMeta = new CoreMeta();

        var domains = coreMeta.MetaPopulation.Objects.OfType<Domain>().ToArray();
        var units = coreMeta.MetaPopulation.Objects.OfType<Unit>().ToArray();
        var interfaces = coreMeta.MetaPopulation.Objects.OfType<Interface>().ToArray();
        var classes = coreMeta.MetaPopulation.Objects.OfType<Class>().ToArray();
        var associationTypes = coreMeta.MetaPopulation.Objects.OfType<IAssociationType>().ToArray();
        var roleTypes = coreMeta.MetaPopulation.Objects.OfType<IRoleType>().ToArray();

        Assert.Equal(103, coreMeta.MetaPopulation.Objects.Count());

        Assert.Single(domains);
        Assert.Equal(6, units.Length);
        Assert.Equal(17, interfaces.Length);
        Assert.Equal(29, classes.Length);
        Assert.Equal(25, associationTypes.Length);
        Assert.Equal(25, roleTypes.Length);
    }
}
