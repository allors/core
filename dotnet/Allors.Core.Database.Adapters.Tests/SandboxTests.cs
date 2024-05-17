namespace Allors.Core.Database.Adapters.Tests
{
    using Allors.Core.Database.Meta;
    using Xunit;

    public abstract class SandboxTests
    {
        [Fact]
        public void OneToOne()
        {
            var coreMeta = new CoreMeta();
            var adaptersMeta = new AdaptersMeta(coreMeta);

            var database = this.CreateDatabase();
            var transaction = database.CreateTransaction();

            var m = adaptersMeta;

            var association = m.C1WhereC1OneToOne;
            var role = m.C1C1OneToOne;

            var from = transaction.Build(m.C1);
            var fromAnother = transaction.Build(m.C1);
            var to = transaction.Build(m.C1);

            from[role] = to;

            transaction.Commit();

            fromAnother[role] = to;

            transaction.Commit();

            Assert.Equal(fromAnother, to[association]);
            Assert.Null(from[role]);
            Assert.Equal(to, fromAnother[role]);
        }

        protected abstract IDatabase CreateDatabase();
    }
}
