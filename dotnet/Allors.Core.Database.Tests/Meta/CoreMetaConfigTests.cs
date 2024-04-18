namespace Allors.Core.Database.Tests.Meta
{
    using Allors.Core.Database.Config;
    using Allors.Embedded.Meta.Diagrams;
    using Xunit;

    public class CoreMetaConfigTests
    {
        [Fact]
        public void Diagram()
        {
            var meta = new CoreMetaConfig();
            var diagram = new ClassDiagram(meta.CoreMetaMetaConfig.EmbeddedMeta).Render();

            Assert.NotNull(diagram);
        }
    }
}
