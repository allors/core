namespace Allors.Core.Database.Tests
{
    using Xunit;

    public class CoreTests
    {
        [Fact]
        public void Construction()
        {
            var meta = new Meta();

            Assert.NotNull(meta.MetaMeta);
        }
    }
}
