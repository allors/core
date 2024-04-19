namespace Allors.Core.Database.Tests.Meta
{
    using Allors.Core.Database.Meta;
    using Allors.Embedded.Meta.Diagrams;
    using Xunit;

    public class CoreMetaTests
    {
        [Fact]
        public void Diagram()
        {
            var meta = new CoreMeta();
            var diagram = new ClassDiagram(meta.EmbeddedMeta).Render();

            Assert.NotNull(diagram);
        }
    }
}
