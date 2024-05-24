namespace Allors.Core.Database.Engines.Tests
{
    using System;
    using Allors.Core.Database.Meta;
    using FluentAssertions;
    using Xunit;

    public abstract class UnitTests : Tests
    {
        private readonly (string, Action<ITransaction>)[] preActs;

        protected UnitTests()
        {
            this.preActs =
            [
                ("Nothing", _ => { }),
                ("Checkpoint", v => v.Checkpoint()),
                ("Checkpoint Checkpoint", v =>
                {
                    v.Checkpoint();
                    v.Checkpoint();
                }),
                ("Commit", v => v.Commit()),
                ("Commit Commit", v =>
                {
                    v.Commit();
                    v.Commit();
                }),
                ("Checkpoint Commit", v =>
                {
                    v.Checkpoint();
                    v.Commit();
                }),
                ("Commit Checkpoint", v =>
                {
                    v.Commit();
                    v.Checkpoint();
                }),
            ];
        }

        [Fact]
        public void String()
        {
            var coreMeta = new CoreMeta();
            var enginesMeta = new EnginesMeta(coreMeta);

            var database = this.CreateDatabase();
            var transaction = database.CreateTransaction();

            var c1a = transaction.Build(enginesMeta.C1);

            c1a[enginesMeta.I1AllorsString] = "A string";

            Assert.Equal("A string", c1a[enginesMeta.I1AllorsString]);
        }

        [Fact]
        public void CheckIsLegalValue()
        {
            foreach (var (_, preact) in this.preActs)
            {
                var database = this.CreateDatabase();
                var transaction = database.CreateTransaction();

                var m = this.Meta;

                var c1a = transaction.Build(m.C1);
                var c1b = transaction.Build(m.C1);

                // Illegal Role
                preact(transaction);

                c1a.Invoking(v => v[m.C1AllorsBoolean] = "Oops")
                    .Should().Throw<ArgumentException>();

                preact(transaction);

                c1a.Invoking(v => v[m.C1AllorsBoolean] = c1b)
                    .Should().Throw<ArgumentException>();

                preact(transaction);

                c1a.Invoking(v => v[m.C1AllorsDecimal] = "Oops")
                    .Should().Throw<ArgumentException>();

                preact(transaction);

                c1a.Invoking(v => v[m.C1AllorsDecimal] = c1b)
                    .Should().Throw<ArgumentException>();

                preact(transaction);

                c1a.Invoking(v => v[m.C1AllorsDouble] = 0.01m)
                    .Should().Throw<ArgumentException>();

                preact(transaction);

                c1a.Invoking(v => v[m.C1AllorsDouble] = "Oops")
                    .Should().Throw<ArgumentException>();

                preact(transaction);

                c1a.Invoking(v => v[m.C1AllorsDouble] = c1b)
                    .Should().Throw<ArgumentException>();

                preact(transaction);

                c1a.Invoking(v => v[m.C1AllorsInteger] = 0L)
                    .Should().Throw<ArgumentException>();

                preact(transaction);

                c1a.Invoking(v => v[m.C1AllorsInteger] = "Oops")
                    .Should().Throw<ArgumentException>();

                preact(transaction);

                c1a.Invoking(v => v[m.C1AllorsInteger] = c1b)
                    .Should().Throw<ArgumentException>();

                preact(transaction);

                c1a.Invoking(v => v[m.C1AllorsString] = 0)
                    .Should().Throw<ArgumentException>();

                preact(transaction);

                c1a.Invoking(v => v[m.C1AllorsString] = c1b)
                    .Should().Throw<ArgumentException>();
            }
        }

        protected abstract IDatabase CreateDatabase();
    }
}
