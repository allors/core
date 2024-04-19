namespace Allors.Core.Database.Tests.Meta
{
    using Allors.Core.Database.Meta;
    using Xunit;

    public class MetaPopulationTests
    {
        [Fact]
        public void Build()
        {
            var meta = new CoreMeta();
            var metaPopulation = new MetaPopulation(meta.EmbeddedPopulation);

            Assert.NotNull(metaPopulation);
        }
    }
}
