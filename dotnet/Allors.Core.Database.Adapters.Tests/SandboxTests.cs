namespace Allors.Core.Database.Adapters.Tests
{
    using FluentAssertions;
    using Xunit;

    public abstract class SandboxTests
    {
        protected abstract AdaptersMeta Meta { get; }

        [Fact]
        public void OneToOne()
        {
            var database = this.CreateDatabase();
            var transaction = database.CreateTransaction();

            var m = this.Meta;

            var association = m.C1WhereC1C1one2manies;
            var role = m.C1C1OneToManies;

            var from = transaction.Build(m.C1);
            var fromAnother = transaction.Build(m.C1);
            var to = transaction.Build(m.C1);

            from[role] = [to];

            transaction.Commit();

            to[association].Should().BeSameAs(from);
            from[role].Should().BeEquivalentTo([to]);
            fromAnother[role].Should().BeEmpty();

            fromAnother[role] = [to];

            to[association].Should().BeSameAs(fromAnother);
            from[role].Should().BeEmpty();
            fromAnother[role].Should().BeEquivalentTo([to]);
        }

        protected abstract IDatabase CreateDatabase();
    }
}
