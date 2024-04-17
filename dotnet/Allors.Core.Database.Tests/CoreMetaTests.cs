namespace Allors.Core.Database.Tests
{
    using Allors.Embedded.Meta.Diagrams;
    using Xunit;

    public class CoreMetaTests
    {
        [Fact]
        public void Diagram()
        {
            var meta = new CoreMeta();
            var diagram = new ClassDiagram(meta.CoreMetaMeta.EmbeddedMeta).Render();

            Assert.NotNull(diagram);
        }
    }
}
