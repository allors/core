namespace Allors.Core.Database.Tests.Meta
{
    using System.Linq;
    using Allors.Core.Database.Meta;
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
            var meta = coreMeta.Build();

            Assert.NotNull(meta);

            var metaObjects = meta.MetaObjectById.Values.ToArray();
            var units = metaObjects.OfType<Unit>().ToArray();
            var interfaces = metaObjects.OfType<Interface>().ToArray();
            var classes = metaObjects.OfType<Class>().ToArray();

            Assert.Single(metaObjects);
            Assert.Empty(units);
            Assert.Single(interfaces);
            Assert.Empty(classes);
        }
    }
}
