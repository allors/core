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
            var diagram = new ClassDiagram(coreMeta.Meta.EmbeddedMeta).Render();

            Assert.NotNull(diagram);
        }

        [Fact]
        public void Build()
        {
            var coreMeta = new CoreMeta();
            var meta = coreMeta.Meta;

            var units = meta.MetaHandles.OfType<UnitHandle>().ToArray();
            var interfaces = meta.MetaHandles.OfType<Interface>().ToArray();
            var classes = meta.MetaHandles.OfType<ClassHandle>().ToArray();

            Assert.Equal(2, meta.MetaHandles.Count());
            Assert.Single(units);
            Assert.Single(interfaces);
            Assert.Empty(classes);
        }
    }
}
