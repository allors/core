namespace Allors.Core.Database.Tests.Meta
{
    using Allors.Core.Database.Config;
    using Xunit;

    public class MetaPopulationTests
    {
        [Fact]
        public void Build()
        {
            var meta = new CoreMetaConfig();
            var metaPopulation = meta.Build();

            Assert.NotNull(metaPopulation);
        }
    }
}
