namespace Allors.Core.Database.Adapters.Tests
{
    using Allors.Core.Database.Meta;
    using Xunit;

    public abstract class UnitTests
    {
        [Fact]
        public void String()
        {
            var coreMeta = new CoreMeta();
            var adaptersMeta = new AdaptersMeta(coreMeta);

            var database = this.CreateDatabase();
            var transaction = database.CreateTransaction();

            var c1a = transaction.Build(adaptersMeta.C1);

            c1a[adaptersMeta.I1AllorsString] = "A string";

            Assert.Equal("A string", c1a[adaptersMeta.I1AllorsString]);
        }

        protected abstract IDatabase CreateDatabase();
    }
}
