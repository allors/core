namespace Allors.Core.Database.Adapters.Tests
{
    using Allors.Core.Database.Meta;
    using Xunit;

    public abstract class UnitTestsBase
    {
        [Fact]
        public void String()
        {
            var coreMeta = new CoreMeta();
            var adaptersMeta = new AdaptersMeta(coreMeta);

            var database = this.CreateDatabase();
            var transaction = database.CreateTransaction();

            var c1a = transaction.Build(adaptersMeta.C1);
        }

        protected abstract IDatabase CreateDatabase();
    }
}
