namespace Allors.Core.Database.Engines.Tests;

using System;
using Allors.Core.Database.Engines.Tests.Meta;
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
        foreach (var (_, preact) in this.preActs)
        {
            var database = this.CreateDatabase();
            var transaction = database.CreateTransaction();

            var c1a = transaction.Build(this.Meta.C1());

            preact(transaction);

            c1a[this.Meta.I1AllorsString] = "A string";

            Assert.Equal("A string", c1a[this.Meta.I1AllorsString]);
        }
    }

    protected abstract IDatabase CreateDatabase();
}
