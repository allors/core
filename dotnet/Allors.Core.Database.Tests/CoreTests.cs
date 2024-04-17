namespace Allors.Core.Database.Tests
{
    using Allors.Embedded.Meta.Diagrams;
    using Xunit;

    public class CoreTests
    {
        [Fact]
        public void Diagram()
        {
            var meta = new Meta();
            var diagram = new ClassDiagram(meta.MetaMeta.EmbeddedMeta).Render();

            Assert.NotNull(diagram);
        }
    }
}
