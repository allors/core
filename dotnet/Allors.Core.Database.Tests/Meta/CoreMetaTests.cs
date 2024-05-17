namespace Allors.Core.Database.Tests.Meta
{
    using System.Linq;
    using Allors.Core.Database.Meta;
    using Allors.Core.Database.Meta.Handles;
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

            var units = coreMeta.MetaHandles.OfType<UnitHandle>().ToArray();
            var interfaces = coreMeta.MetaHandles.OfType<InterfaceHandle>().ToArray();
            var classes = coreMeta.MetaHandles.OfType<ClassHandle>().ToArray();

            Assert.Equal(2, coreMeta.MetaHandles.Count());
            Assert.Single(units);
            Assert.Single(interfaces);
            Assert.Empty(classes);
        }
    }
}
