namespace Allors.Core.Database.Tests.Meta
{
    using System.Linq;
    using Allors.Core.Database.Meta;
    using Allors.Core.Database.Meta.Handles;
    using Allors.Embedded.Meta.Diagrams;
    using Xunit;

    public class CoreMetaTests
    {
        [Fact]
        public void Diagram()
        {
            var coreMeta = new CoreMeta();
            var diagram = new ClassDiagram(coreMeta.EmbeddedMeta).Render();

            Assert.NotNull(diagram);
        }

        [Fact]
        public void Build()
        {
            var coreMeta = new CoreMeta();

            var units = coreMeta.MetaHandles.OfType<UnitHandle>().ToArray();
            var interfaces = coreMeta.MetaHandles.OfType<Interface>().ToArray();
            var classes = coreMeta.MetaHandles.OfType<ClassHandle>().ToArray();

            Assert.Equal(2, coreMeta.MetaHandles.Count());
            Assert.Single(units);
            Assert.Single(interfaces);
            Assert.Empty(classes);
        }
    }
}
