namespace Allors.Core.Database.Adapters.Tests;

public abstract class ManyToOneTests
{
    /*
    [Fact]
    public void C1_C1many2one()
    {
        var coreMeta = new CoreMeta();
        var adaptersMeta = new AdaptersMeta(coreMeta);

        var database = this.CreateDatabase();
        var transaction = database.CreateTransaction();

        var from1 = transaction.Build(adaptersMeta.C1);
        var from2 = transaction.Build(adaptersMeta.C1);
        var from3 = transaction.Build(adaptersMeta.C1);
        var from4 = transaction.Build(adaptersMeta.C1);
        var to = transaction.Build(adaptersMeta.C1);
        var toAnother = transaction.Build(adaptersMeta.C1);

        // New 0-4-0
        // Get
        Assert.Empty(to.C1sWhereC1C1many2one);
        Assert.Empty(to.C1sWhereC1C1many2one);
        Assert.Null(from1.C1C1many2one);
        Assert.Null(from1.C1C1many2one);
        Assert.Null(from2.C1C1many2one);
        Assert.Null(from2.C1C1many2one);
        Assert.Null(from3.C1C1many2one);
        Assert.Null(from3.C1C1many2one);
        Assert.Null(from4.C1C1many2one);
        Assert.Null(from4.C1C1many2one);

        // 1-1
        from1.C1C1many2one = to;
        from1.C1C1many2one = to; // Twice

        Assert.Single(to.C1sWhereC1C1many2one);
        Assert.Single(to.C1sWhereC1C1many2one);
        Assert.Equal(from1, to.C1sWhereC1C1many2one.ElementAt(0));
        Assert.Equal(from1, to.C1sWhereC1C1many2one.ElementAt(0));
        Assert.Equal(to, from1.C1C1many2one);
        Assert.Equal(to, from1.C1C1many2one);
        Assert.Null(from2.C1C1many2one);
        Assert.Null(from2.C1C1many2one);
        Assert.Null(from3.C1C1many2one);
        Assert.Null(from3.C1C1many2one);
        Assert.Null(from4.C1C1many2one);
        Assert.Null(from4.C1C1many2one);

        // 1-2
        from2.C1C1many2one = to;
        from2.C1C1many2one = to; // Twice

        Assert.Equal(2, to.C1sWhereC1C1many2one.Count());
        Assert.Equal(2, to.C1sWhereC1C1many2one.Count());
        Assert.Contains(from1, to.C1sWhereC1C1many2one);
        Assert.Contains(from1, to.C1sWhereC1C1many2one);
        Assert.Contains(from2, to.C1sWhereC1C1many2one);
        Assert.Contains(from2, to.C1sWhereC1C1many2one);
        Assert.Equal(to, from1.C1C1many2one);
        Assert.Equal(to, from1.C1C1many2one);
        Assert.Equal(to, from2.C1C1many2one);
        Assert.Equal(to, from2.C1C1many2one);
        Assert.Null(from3.C1C1many2one);
        Assert.Null(from3.C1C1many2one);
        Assert.Null(from4.C1C1many2one);
        Assert.Null(from4.C1C1many2one);

        // 1-3
        from3.C1C1many2one = to;
        from3.C1C1many2one = to; // Twice

        Assert.Equal(3, to.C1sWhereC1C1many2one.Count());
        Assert.Equal(3, to.C1sWhereC1C1many2one.Count());
        Assert.Contains(from1, to.C1sWhereC1C1many2one);
        Assert.Contains(from1, to.C1sWhereC1C1many2one);
        Assert.Contains(from2, to.C1sWhereC1C1many2one);
        Assert.Contains(from2, to.C1sWhereC1C1many2one);
        Assert.Contains(from3, to.C1sWhereC1C1many2one);
        Assert.Contains(from3, to.C1sWhereC1C1many2one);
        Assert.Equal(to, from1.C1C1many2one);
        Assert.Equal(to, from1.C1C1many2one);
        Assert.Equal(to, from2.C1C1many2one);
        Assert.Equal(to, from2.C1C1many2one);
        Assert.Equal(to, from3.C1C1many2one);
        Assert.Equal(to, from3.C1C1many2one);
        Assert.Null(from4.C1C1many2one);
        Assert.Null(from4.C1C1many2one);

        // 1-4
        from4.C1C1many2one = to;
        from4.C1C1many2one = to; // Twice

        Assert.Equal(4, to.C1sWhereC1C1many2one.Count());
        Assert.Equal(4, to.C1sWhereC1C1many2one.Count());
        Assert.Contains(from1, to.C1sWhereC1C1many2one);
        Assert.Contains(from1, to.C1sWhereC1C1many2one);
        Assert.Contains(from2, to.C1sWhereC1C1many2one);
        Assert.Contains(from2, to.C1sWhereC1C1many2one);
        Assert.Contains(from3, to.C1sWhereC1C1many2one);
        Assert.Contains(from3, to.C1sWhereC1C1many2one);
        Assert.Contains(from4, to.C1sWhereC1C1many2one);
        Assert.Contains(from4, to.C1sWhereC1C1many2one);
        Assert.Equal(to, from1.C1C1many2one);
        Assert.Equal(to, from1.C1C1many2one);
        Assert.Equal(to, from2.C1C1many2one);
        Assert.Equal(to, from2.C1C1many2one);
        Assert.Equal(to, from3.C1C1many2one);
        Assert.Equal(to, from3.C1C1many2one);
        Assert.Equal(to, from4.C1C1many2one);
        Assert.Equal(to, from4.C1C1many2one);

        // 1-3
        from4.RemoveC1C1many2one();
        from4.RemoveC1C1many2one(); // Twice

        Assert.Equal(3, to.C1sWhereC1C1many2one.Count());
        Assert.Equal(3, to.C1sWhereC1C1many2one.Count());
        Assert.Contains(from1, to.C1sWhereC1C1many2one);
        Assert.Contains(from1, to.C1sWhereC1C1many2one);
        Assert.Contains(from2, to.C1sWhereC1C1many2one);
        Assert.Contains(from2, to.C1sWhereC1C1many2one);
        Assert.Contains(from3, to.C1sWhereC1C1many2one);
        Assert.Contains(from3, to.C1sWhereC1C1many2one);
        Assert.Equal(to, from1.C1C1many2one);
        Assert.Equal(to, from1.C1C1many2one);
        Assert.Equal(to, from2.C1C1many2one);
        Assert.Equal(to, from2.C1C1many2one);
        Assert.Equal(to, from3.C1C1many2one);
        Assert.Equal(to, from3.C1C1many2one);
        Assert.Null(from4.C1C1many2one);
        Assert.Null(from4.C1C1many2one);

        // 1-2
        from3.RemoveC1C1many2one();
        from3.RemoveC1C1many2one(); // Twice

        Assert.Equal(2, to.C1sWhereC1C1many2one.Count());
        Assert.Equal(2, to.C1sWhereC1C1many2one.Count());
        Assert.Contains(from1, to.C1sWhereC1C1many2one);
        Assert.Contains(from1, to.C1sWhereC1C1many2one);
        Assert.Contains(from2, to.C1sWhereC1C1many2one);
        Assert.Contains(from2, to.C1sWhereC1C1many2one);
        Assert.Equal(to, from1.C1C1many2one);
        Assert.Equal(to, from1.C1C1many2one);
        Assert.Equal(to, from2.C1C1many2one);
        Assert.Equal(to, from2.C1C1many2one);
        Assert.Null(from3.C1C1many2one);
        Assert.Null(from3.C1C1many2one);
        Assert.Null(from4.C1C1many2one);
        Assert.Null(from4.C1C1many2one);

        // 1-1
        from2.RemoveC1C1many2one();
        from2.RemoveC1C1many2one(); // Twice

        Assert.Single(to.C1sWhereC1C1many2one);
        Assert.Single(to.C1sWhereC1C1many2one);
        Assert.Equal(from1, to.C1sWhereC1C1many2one.ElementAt(0));
        Assert.Equal(from1, to.C1sWhereC1C1many2one.ElementAt(0));
        Assert.Equal(to, from1.C1C1many2one);
        Assert.Equal(to, from1.C1C1many2one);
        Assert.Null(from2.C1C1many2one);
        Assert.Null(from2.C1C1many2one);
        Assert.Null(from3.C1C1many2one);
        Assert.Null(from3.C1C1many2one);
        Assert.Null(from4.C1C1many2one);
        Assert.Null(from4.C1C1many2one);

        // 0-0
        from1.RemoveC1C1many2one();
        from1.RemoveC1C1many2one(); // Twice

        Assert.Null(from1.C1C1many2one);
        Assert.Null(from1.C1C1many2one);
        Assert.Null(from2.C1C1many2one);
        Assert.Null(from2.C1C1many2one);
        Assert.Null(from3.C1C1many2one);
        Assert.Null(from3.C1C1many2one);
        Assert.Null(from4.C1C1many2one);
        Assert.Null(from4.C1C1many2one);

        // Exist
        Assert.False(to.ExistC1sWhereC1C1many2one);
        Assert.False(to.ExistC1sWhereC1C1many2one);
        Assert.False(from1.ExistC1C1many2one);
        Assert.False(from1.ExistC1C1many2one);
        Assert.False(from2.ExistC1C1many2one);
        Assert.False(from2.ExistC1C1many2one);
        Assert.False(from3.ExistC1C1many2one);
        Assert.False(from3.ExistC1C1many2one);
        Assert.False(from4.ExistC1C1many2one);
        Assert.False(from4.ExistC1C1many2one);

        // 1-1
        from1.C1C1many2one = to;
        from1.C1C1many2one = to; // Twice

        Assert.True(to.ExistC1sWhereC1C1many2one);
        Assert.True(to.ExistC1sWhereC1C1many2one);
        Assert.True(from1.ExistC1C1many2one);
        Assert.True(from1.ExistC1C1many2one);
        Assert.False(from2.ExistC1C1many2one);
        Assert.False(from2.ExistC1C1many2one);
        Assert.False(from3.ExistC1C1many2one);
        Assert.False(from3.ExistC1C1many2one);
        Assert.False(from4.ExistC1C1many2one);
        Assert.False(from4.ExistC1C1many2one);

        // 1-2
        from2.C1C1many2one = to;
        from2.C1C1many2one = to; // Twice

        Assert.True(to.ExistC1sWhereC1C1many2one);
        Assert.True(to.ExistC1sWhereC1C1many2one);
        Assert.True(from1.ExistC1C1many2one);
        Assert.True(from1.ExistC1C1many2one);
        Assert.True(from2.ExistC1C1many2one);
        Assert.True(from2.ExistC1C1many2one);
        Assert.False(from3.ExistC1C1many2one);
        Assert.False(from3.ExistC1C1many2one);
        Assert.False(from4.ExistC1C1many2one);
        Assert.False(from4.ExistC1C1many2one);

        // 1-3
        from3.C1C1many2one = to;
        from3.C1C1many2one = to; // Twice

        Assert.True(to.ExistC1sWhereC1C1many2one);
        Assert.True(to.ExistC1sWhereC1C1many2one);
        Assert.True(from1.ExistC1C1many2one);
        Assert.True(from1.ExistC1C1many2one);
        Assert.True(from2.ExistC1C1many2one);
        Assert.True(from2.ExistC1C1many2one);
        Assert.True(from3.ExistC1C1many2one);
        Assert.True(from3.ExistC1C1many2one);
        Assert.False(from4.ExistC1C1many2one);
        Assert.False(from4.ExistC1C1many2one);

        // 1-4
        from4.C1C1many2one = to;
        from4.C1C1many2one = to; // Twice

        Assert.True(to.ExistC1sWhereC1C1many2one);
        Assert.True(to.ExistC1sWhereC1C1many2one);
        Assert.True(from1.ExistC1C1many2one);
        Assert.True(from1.ExistC1C1many2one);
        Assert.True(from2.ExistC1C1many2one);
        Assert.True(from2.ExistC1C1many2one);
        Assert.True(from3.ExistC1C1many2one);
        Assert.True(from3.ExistC1C1many2one);
        Assert.True(from4.ExistC1C1many2one);
        Assert.True(from4.ExistC1C1many2one);

        // 1-3
        from4.RemoveC1C1many2one();
        from4.RemoveC1C1many2one(); // Twice

        Assert.True(to.ExistC1sWhereC1C1many2one);
        Assert.True(to.ExistC1sWhereC1C1many2one);
        Assert.True(from1.ExistC1C1many2one);
        Assert.True(from1.ExistC1C1many2one);
        Assert.True(from2.ExistC1C1many2one);
        Assert.True(from2.ExistC1C1many2one);
        Assert.True(from3.ExistC1C1many2one);
        Assert.True(from3.ExistC1C1many2one);
        Assert.False(from4.ExistC1C1many2one);
        Assert.False(from4.ExistC1C1many2one);

        // 1-2
        from3.RemoveC1C1many2one();
        from3.RemoveC1C1many2one(); // Twice

        Assert.True(to.ExistC1sWhereC1C1many2one);
        Assert.True(to.ExistC1sWhereC1C1many2one);
        Assert.True(from1.ExistC1C1many2one);
        Assert.True(from1.ExistC1C1many2one);
        Assert.True(from2.ExistC1C1many2one);
        Assert.True(from2.ExistC1C1many2one);
        Assert.False(from3.ExistC1C1many2one);
        Assert.False(from3.ExistC1C1many2one);
        Assert.False(from4.ExistC1C1many2one);
        Assert.False(from4.ExistC1C1many2one);

        // 1-1
        from2.RemoveC1C1many2one();
        from2.RemoveC1C1many2one(); // Twice

        Assert.True(to.ExistC1sWhereC1C1many2one);
        Assert.True(to.ExistC1sWhereC1C1many2one);
        Assert.True(from1.ExistC1C1many2one);
        Assert.True(from1.ExistC1C1many2one);
        Assert.False(from2.ExistC1C1many2one);
        Assert.False(from2.ExistC1C1many2one);
        Assert.False(from3.ExistC1C1many2one);
        Assert.False(from3.ExistC1C1many2one);
        Assert.False(from4.ExistC1C1many2one);
        Assert.False(from4.ExistC1C1many2one);

        // 0-0
        from1.RemoveC1C1many2one();
        from1.RemoveC1C1many2one(); // Twice

        Assert.False(to.ExistC1sWhereC1C1many2one);
        Assert.False(to.ExistC1sWhereC1C1many2one);
        Assert.False(from1.ExistC1C1many2one);
        Assert.False(from1.ExistC1C1many2one);
        Assert.False(from2.ExistC1C1many2one);
        Assert.False(from2.ExistC1C1many2one);
        Assert.False(from3.ExistC1C1many2one);
        Assert.False(from3.ExistC1C1many2one);
        Assert.False(from4.ExistC1C1many2one);
        Assert.False(from4.ExistC1C1many2one);

        // Multiplicity
        // Same New / Same To
        // Get
        Assert.Null(from1.C1C1many2one);
        Assert.Null(from1.C1C1many2one);
        Assert.Empty(to.C1sWhereC1C1many2one);
        Assert.Empty(to.C1sWhereC1C1many2one);
        from1.C1C1many2one = to;
        from1.C1C1many2one = to; // Twice

        Assert.Equal(to, from1.C1C1many2one);
        Assert.Equal(to, from1.C1C1many2one);
        Assert.Single(to.C1sWhereC1C1many2one);
        Assert.Single(to.C1sWhereC1C1many2one);
        Assert.Contains(from1, to.C1sWhereC1C1many2one);
        Assert.Contains(from1, to.C1sWhereC1C1many2one);
        from1.RemoveC1C1many2one();

        Assert.Null(from1.C1C1many2one);
        Assert.Null(from1.C1C1many2one);
        Assert.Empty(to.C1sWhereC1C1many2one);
        Assert.Empty(to.C1sWhereC1C1many2one);

        // Exist
        Assert.False(from1.ExistC1C1many2one);
        Assert.False(from1.ExistC1C1many2one);
        Assert.False(to.ExistC1sWhereC1C1many2one);
        Assert.False(to.ExistC1sWhereC1C1many2one);
        from1.C1C1many2one = to;
        from1.C1C1many2one = to; // Twice

        Assert.True(from1.ExistC1C1many2one);
        Assert.True(from1.ExistC1C1many2one);
        Assert.True(to.ExistC1sWhereC1C1many2one);
        Assert.True(to.ExistC1sWhereC1C1many2one);
        from1.RemoveC1C1many2one();

        Assert.False(from1.ExistC1C1many2one);
        Assert.False(from1.ExistC1C1many2one);
        Assert.False(to.ExistC1sWhereC1C1many2one);
        Assert.False(to.ExistC1sWhereC1C1many2one);

        // Same New / Different To
        // Get
        Assert.Empty(to.C1sWhereC1C1many2one);
        Assert.Empty(to.C1sWhereC1C1many2one);
        Assert.Null(from1.C1C1many2one);
        Assert.Null(from1.C1C1many2one);
        Assert.Empty(toAnother.C1sWhereC1C1many2one);
        Assert.Empty(toAnother.C1sWhereC1C1many2one);
        from1.C1C1many2one = to;
        from1.C1C1many2one = to; // Twice

        Assert.Single(to.C1sWhereC1C1many2one);
        Assert.Single(to.C1sWhereC1C1many2one);
        Assert.Equal(to, from1.C1C1many2one);
        Assert.Equal(to, from1.C1C1many2one);
        Assert.Empty(toAnother.C1sWhereC1C1many2one);
        Assert.Empty(toAnother.C1sWhereC1C1many2one);
        Assert.Contains(from1, to.C1sWhereC1C1many2one);
        Assert.Contains(from1, to.C1sWhereC1C1many2one);
        from1.C1C1many2one = toAnother;
        from1.C1C1many2one = toAnother; // Twice

        Assert.Empty(to.C1sWhereC1C1many2one);
        Assert.Empty(to.C1sWhereC1C1many2one);
        Assert.Equal(toAnother, from1.C1C1many2one);
        Assert.Equal(toAnother, from1.C1C1many2one);
        Assert.Single(toAnother.C1sWhereC1C1many2one);
        Assert.Single(toAnother.C1sWhereC1C1many2one);
        Assert.Contains(from1, toAnother.C1sWhereC1C1many2one);
        Assert.Contains(from1, toAnother.C1sWhereC1C1many2one);
        from1.C1C1many2one = null;
        from1.C1C1many2one = null; // Twice

        Assert.Empty(to.C1sWhereC1C1many2one);
        Assert.Empty(to.C1sWhereC1C1many2one);
        Assert.Null(from1.C1C1many2one);
        Assert.Null(from1.C1C1many2one);
        Assert.Empty(toAnother.C1sWhereC1C1many2one);
        Assert.Empty(toAnother.C1sWhereC1C1many2one);

        // Exist
        Assert.False(to.ExistC1sWhereC1C1many2one);
        Assert.False(to.ExistC1sWhereC1C1many2one);
        Assert.False(from1.ExistC1C1many2one);
        Assert.False(from1.ExistC1C1many2one);
        Assert.False(toAnother.ExistC1sWhereC1C1many2one);
        Assert.False(toAnother.ExistC1sWhereC1C1many2one);
        from1.C1C1many2one = to;
        from1.C1C1many2one = to; // Twice

        Assert.True(to.ExistC1sWhereC1C1many2one);
        Assert.True(to.ExistC1sWhereC1C1many2one);
        Assert.True(from1.ExistC1C1many2one);
        Assert.True(from1.ExistC1C1many2one);
        Assert.False(toAnother.ExistC1sWhereC1C1many2one);
        Assert.False(toAnother.ExistC1sWhereC1C1many2one);
        from1.C1C1many2one = toAnother;
        from1.C1C1many2one = toAnother; // Twice

        Assert.False(to.ExistC1sWhereC1C1many2one);
        Assert.False(to.ExistC1sWhereC1C1many2one);
        Assert.True(from1.ExistC1C1many2one);
        Assert.True(from1.ExistC1C1many2one);
        Assert.True(toAnother.ExistC1sWhereC1C1many2one);
        Assert.True(toAnother.ExistC1sWhereC1C1many2one);
        from1.C1C1many2one = null;
        from1.C1C1many2one = null; // Twice
        from1.C1C1many2one = null;
        from1.C1C1many2one = null; // Twice

        Assert.False(to.ExistC1sWhereC1C1many2one);
        Assert.False(to.ExistC1sWhereC1C1many2one);
        Assert.False(from1.ExistC1C1many2one);
        Assert.False(from1.ExistC1C1many2one);
        Assert.False(toAnother.ExistC1sWhereC1C1many2one);
        Assert.False(toAnother.ExistC1sWhereC1C1many2one);

        // Different New / Different To
        // Get
        Assert.Null(from1.C1C1many2one);
        Assert.Null(from1.C1C1many2one);
        Assert.Empty(to.C1sWhereC1C1many2one);
        Assert.Empty(to.C1sWhereC1C1many2one);
        Assert.Null(from2.C1C1many2one);
        Assert.Null(from2.C1C1many2one);
        Assert.Empty(toAnother.C1sWhereC1C1many2one);
        Assert.Empty(toAnother.C1sWhereC1C1many2one);
        from1.C1C1many2one = to;
        from1.C1C1many2one = to; // Twice

        Assert.Equal(to, from1.C1C1many2one);
        Assert.Equal(to, from1.C1C1many2one);
        Assert.Single(to.C1sWhereC1C1many2one);
        Assert.Single(to.C1sWhereC1C1many2one);
        Assert.Contains(from1, to.C1sWhereC1C1many2one);
        Assert.Contains(from1, to.C1sWhereC1C1many2one);
        Assert.Null(from2.C1C1many2one);
        Assert.Null(from2.C1C1many2one);
        Assert.Empty(toAnother.C1sWhereC1C1many2one);
        Assert.Empty(toAnother.C1sWhereC1C1many2one);
        from2.C1C1many2one = toAnother;
        from2.C1C1many2one = toAnother; // Twice

        Assert.Equal(to, from1.C1C1many2one);
        Assert.Equal(to, from1.C1C1many2one);
        Assert.Single(to.C1sWhereC1C1many2one);
        Assert.Single(to.C1sWhereC1C1many2one);
        Assert.Contains(from1, to.C1sWhereC1C1many2one);
        Assert.Contains(from1, to.C1sWhereC1C1many2one);
        Assert.Equal(toAnother, from2.C1C1many2one);
        Assert.Equal(toAnother, from2.C1C1many2one);
        Assert.Single(toAnother.C1sWhereC1C1many2one);
        Assert.Single(toAnother.C1sWhereC1C1many2one);
        Assert.Contains(from2, toAnother.C1sWhereC1C1many2one);
        Assert.Contains(from2, toAnother.C1sWhereC1C1many2one);
        from1.C1C1many2one = null;
        from1.C1C1many2one = null; // Twice
        from2.C1C1many2one = null;
        from2.C1C1many2one = null; // Twice

        Assert.Null(from1.C1C1many2one);
        Assert.Null(from1.C1C1many2one);
        Assert.Empty(to.C1sWhereC1C1many2one);
        Assert.Empty(to.C1sWhereC1C1many2one);
        Assert.Null(from2.C1C1many2one);
        Assert.Null(from2.C1C1many2one);
        Assert.Empty(toAnother.C1sWhereC1C1many2one);
        Assert.Empty(toAnother.C1sWhereC1C1many2one);

        // Exist
        Assert.False(from1.ExistC1C1many2one);
        Assert.False(from1.ExistC1C1many2one);
        Assert.False(to.ExistC1sWhereC1C1many2one);
        Assert.False(to.ExistC1sWhereC1C1many2one);
        Assert.False(from2.ExistC1C1many2one);
        Assert.False(from2.ExistC1C1many2one);
        Assert.False(toAnother.ExistC1sWhereC1C1many2one);
        Assert.False(toAnother.ExistC1sWhereC1C1many2one);
        from1.C1C1many2one = to;
        from1.C1C1many2one = to; // Twice

        Assert.True(from1.ExistC1C1many2one);
        Assert.True(from1.ExistC1C1many2one);
        Assert.True(to.ExistC1sWhereC1C1many2one);
        Assert.True(to.ExistC1sWhereC1C1many2one);
        Assert.False(from2.ExistC1C1many2one);
        Assert.False(from2.ExistC1C1many2one);
        Assert.False(toAnother.ExistC1sWhereC1C1many2one);
        Assert.False(toAnother.ExistC1sWhereC1C1many2one);
        from2.C1C1many2one = toAnother;
        from2.C1C1many2one = toAnother; // Twice

        Assert.True(from1.ExistC1C1many2one);
        Assert.True(from1.ExistC1C1many2one);
        Assert.True(to.ExistC1sWhereC1C1many2one);
        Assert.True(to.ExistC1sWhereC1C1many2one);
        Assert.True(from2.ExistC1C1many2one);
        Assert.True(from2.ExistC1C1many2one);
        Assert.True(toAnother.ExistC1sWhereC1C1many2one);
        Assert.True(toAnother.ExistC1sWhereC1C1many2one);
        from1.C1C1many2one = null;
        from1.C1C1many2one = null; // Twice
        from2.C1C1many2one = null;
        from2.C1C1many2one = null; // Twice

        Assert.False(from1.ExistC1C1many2one);
        Assert.False(from1.ExistC1C1many2one);
        Assert.False(to.ExistC1sWhereC1C1many2one);
        Assert.False(to.ExistC1sWhereC1C1many2one);
        Assert.False(from2.ExistC1C1many2one);
        Assert.False(from2.ExistC1C1many2one);
        Assert.False(toAnother.ExistC1sWhereC1C1many2one);
        Assert.False(toAnother.ExistC1sWhereC1C1many2one);

        // Different New / Same To
        // Get
        Assert.Null(from1.C1C1many2one);
        Assert.Null(from1.C1C1many2one);
        Assert.Null(from2.C1C1many2one);
        Assert.Null(from2.C1C1many2one);
        Assert.Empty(to.C1sWhereC1C1many2one);
        Assert.Empty(to.C1sWhereC1C1many2one);
        from1.C1C1many2one = to;
        from1.C1C1many2one = to; // Twice

        Assert.Equal(to, from1.C1C1many2one);
        Assert.Equal(to, from1.C1C1many2one);
        Assert.Null(from2.C1C1many2one);
        Assert.Null(from2.C1C1many2one);
        Assert.Single(to.C1sWhereC1C1many2one);
        Assert.Single(to.C1sWhereC1C1many2one);
        Assert.Contains(from1, to.C1sWhereC1C1many2one);
        Assert.Contains(from1, to.C1sWhereC1C1many2one);
        Assert.DoesNotContain(from2, to.C1sWhereC1C1many2one);
        Assert.DoesNotContain(from2, to.C1sWhereC1C1many2one);
        from2.C1C1many2one = to;
        from2.C1C1many2one = to; // Twice

        Assert.Equal(to, from1.C1C1many2one);
        Assert.Equal(to, from1.C1C1many2one);
        Assert.Equal(to, from2.C1C1many2one);
        Assert.Equal(to, from2.C1C1many2one);
        Assert.Equal(2, to.C1sWhereC1C1many2one.Count());
        Assert.Equal(2, to.C1sWhereC1C1many2one.Count());
        Assert.Contains(from1, to.C1sWhereC1C1many2one);
        Assert.Contains(from1, to.C1sWhereC1C1many2one);
        Assert.Contains(from2, to.C1sWhereC1C1many2one);
        Assert.Contains(from2, to.C1sWhereC1C1many2one);
        from1.RemoveC1C1many2one();
        from2.RemoveC1C1many2one();

        Assert.Null(from1.C1C1many2one);
        Assert.Null(from1.C1C1many2one);
        Assert.Null(from2.C1C1many2one);
        Assert.Null(from2.C1C1many2one);
        Assert.Empty(to.C1sWhereC1C1many2one);
        Assert.Empty(to.C1sWhereC1C1many2one);

        // Exist
        Assert.False(from1.ExistC1C1many2one);
        Assert.False(from1.ExistC1C1many2one);
        Assert.False(from2.ExistC1C1many2one);
        Assert.False(from2.ExistC1C1many2one);
        Assert.False(to.ExistC1sWhereC1C1many2one);
        Assert.False(to.ExistC1sWhereC1C1many2one);
        from1.C1C1many2one = to;
        from1.C1C1many2one = to; // Twice

        Assert.True(from1.ExistC1C1many2one);
        Assert.True(from1.ExistC1C1many2one);
        Assert.False(from2.ExistC1C1many2one);
        Assert.False(from2.ExistC1C1many2one);
        Assert.True(to.ExistC1sWhereC1C1many2one);
        Assert.True(to.ExistC1sWhereC1C1many2one);
        from2.C1C1many2one = to;
        from2.C1C1many2one = to; // Twice

        Assert.True(from1.ExistC1C1many2one);
        Assert.True(from1.ExistC1C1many2one);
        Assert.True(from2.ExistC1C1many2one);
        Assert.True(from2.ExistC1C1many2one);
        Assert.True(to.ExistC1sWhereC1C1many2one);
        Assert.True(to.ExistC1sWhereC1C1many2one);
        from1.RemoveC1C1many2one();
        from2.RemoveC1C1many2one();

        Assert.Null(from1.C1C1many2one);
        Assert.Null(from1.C1C1many2one);
        Assert.Null(from2.C1C1many2one);
        Assert.Null(from2.C1C1many2one);
        Assert.Empty(to.C1sWhereC1C1many2one);
        Assert.Empty(to.C1sWhereC1C1many2one);

        // Null & Empty Array
        // Set Null
        // Get
        Assert.Null(from1.C1C1many2one);
        Assert.Null(from1.C1C1many2one);
        from1.C1C1many2one = null;
        from1.C1C1many2one = null; // Twice

        Assert.Null(from1.C1C1many2one);
        Assert.Null(from1.C1C1many2one);
        from1.C1C1many2one = to;
        from1.C1C1many2one = to; // Twice
        from1.C1C1many2one = null;
        from1.C1C1many2one = null; // Twice

        Assert.Null(from1.C1C1many2one);
        Assert.Null(from1.C1C1many2one);

        // Exist
        Assert.False(from1.ExistC1C1many2one);
        Assert.False(from1.ExistC1C1many2one);
        from1.C1C1many2one = null;
        from1.C1C1many2one = null; // Twice

        Assert.False(from1.ExistC1C1many2one);
        Assert.False(from1.ExistC1C1many2one);
        from1.C1C1many2one = to;
        from1.C1C1many2one = to; // Twice
        from1.C1C1many2one = null;
        from1.C1C1many2one = null; // Twice

        Assert.False(from1.ExistC1C1many2one);
        Assert.False(from1.ExistC1C1many2one);
    }


        [Fact]
        public void C1_C1many2one_2()
        {
            var from1 = this.Transaction.Build<C1>();

            var from2 = this.Transaction.Build<C1>();

            var from3 = this.Transaction.Build<C1>();

            var from4 = this.Transaction.Build<C1>();

            var to = this.Transaction.Build<C1>();

            var toAnother = this.Transaction.Build<C1>();


            // New 0-4-0
            // Get
            Assert.Empty(to.C1sWhereC1C1many2one);

            Assert.Empty(to.C1sWhereC1C1many2one);

            Assert.Null(from1.C1C1many2one);

            Assert.Null(from1.C1C1many2one);

            Assert.Null(from2.C1C1many2one);

            Assert.Null(from2.C1C1many2one);

            Assert.Null(from3.C1C1many2one);

            Assert.Null(from3.C1C1many2one);

            Assert.Null(from4.C1C1many2one);

            Assert.Null(from4.C1C1many2one);


            // 1-1
            from1.C1C1many2one = to;

            from1.C1C1many2one = to;
            // Twice
            Assert.Single(to.C1sWhereC1C1many2one);

            Assert.Single(to.C1sWhereC1C1many2one);

            Assert.Equal(from1, to.C1sWhereC1C1many2one.ElementAt(0));

            Assert.Equal(from1, to.C1sWhereC1C1many2one.ElementAt(0));

            Assert.Equal(to, from1.C1C1many2one);

            Assert.Equal(to, from1.C1C1many2one);

            Assert.Null(from2.C1C1many2one);

            Assert.Null(from2.C1C1many2one);

            Assert.Null(from3.C1C1many2one);

            Assert.Null(from3.C1C1many2one);

            Assert.Null(from4.C1C1many2one);

            Assert.Null(from4.C1C1many2one);


            // 1-2
            from2.C1C1many2one = to;

            from2.C1C1many2one = to;
            // Twice
            Assert.Equal(2, to.C1sWhereC1C1many2one.Count());

            Assert.Equal(2, to.C1sWhereC1C1many2one.Count());

            Assert.Contains(from1, to.C1sWhereC1C1many2one);

            Assert.Contains(from1, to.C1sWhereC1C1many2one);

            Assert.Contains(from2, to.C1sWhereC1C1many2one);

            Assert.Contains(from2, to.C1sWhereC1C1many2one);

            Assert.Equal(to, from1.C1C1many2one);

            Assert.Equal(to, from1.C1C1many2one);

            Assert.Equal(to, from2.C1C1many2one);

            Assert.Equal(to, from2.C1C1many2one);

            Assert.Null(from3.C1C1many2one);

            Assert.Null(from3.C1C1many2one);

            Assert.Null(from4.C1C1many2one);

            Assert.Null(from4.C1C1many2one);


            // 1-3
            from3.C1C1many2one = to;

            from3.C1C1many2one = to;
            // Twice
            Assert.Equal(3, to.C1sWhereC1C1many2one.Count());

            Assert.Equal(3, to.C1sWhereC1C1many2one.Count());

            Assert.Contains(from1, to.C1sWhereC1C1many2one);

            Assert.Contains(from1, to.C1sWhereC1C1many2one);

            Assert.Contains(from2, to.C1sWhereC1C1many2one);

            Assert.Contains(from2, to.C1sWhereC1C1many2one);

            Assert.Contains(from3, to.C1sWhereC1C1many2one);

            Assert.Contains(from3, to.C1sWhereC1C1many2one);

            Assert.Equal(to, from1.C1C1many2one);

            Assert.Equal(to, from1.C1C1many2one);

            Assert.Equal(to, from2.C1C1many2one);

            Assert.Equal(to, from2.C1C1many2one);

            Assert.Equal(to, from3.C1C1many2one);

            Assert.Equal(to, from3.C1C1many2one);

            Assert.Null(from4.C1C1many2one);

            Assert.Null(from4.C1C1many2one);


            // 1-4
            from4.C1C1many2one = to;

            from4.C1C1many2one = to;
            // Twice
            Assert.Equal(4, to.C1sWhereC1C1many2one.Count());

            Assert.Equal(4, to.C1sWhereC1C1many2one.Count());

            Assert.Contains(from1, to.C1sWhereC1C1many2one);

            Assert.Contains(from1, to.C1sWhereC1C1many2one);

            Assert.Contains(from2, to.C1sWhereC1C1many2one);

            Assert.Contains(from2, to.C1sWhereC1C1many2one);

            Assert.Contains(from3, to.C1sWhereC1C1many2one);

            Assert.Contains(from3, to.C1sWhereC1C1many2one);

            Assert.Contains(from4, to.C1sWhereC1C1many2one);

            Assert.Contains(from4, to.C1sWhereC1C1many2one);

            Assert.Equal(to, from1.C1C1many2one);

            Assert.Equal(to, from1.C1C1many2one);

            Assert.Equal(to, from2.C1C1many2one);

            Assert.Equal(to, from2.C1C1many2one);

            Assert.Equal(to, from3.C1C1many2one);

            Assert.Equal(to, from3.C1C1many2one);

            Assert.Equal(to, from4.C1C1many2one);

            Assert.Equal(to, from4.C1C1many2one);


            // 1-3
            from4.RemoveC1C1many2one();

            from4.RemoveC1C1many2one();
            // Twice
            Assert.Equal(3, to.C1sWhereC1C1many2one.Count());

            Assert.Equal(3, to.C1sWhereC1C1many2one.Count());

            Assert.Contains(from1, to.C1sWhereC1C1many2one);

            Assert.Contains(from1, to.C1sWhereC1C1many2one);

            Assert.Contains(from2, to.C1sWhereC1C1many2one);

            Assert.Contains(from2, to.C1sWhereC1C1many2one);

            Assert.Contains(from3, to.C1sWhereC1C1many2one);

            Assert.Contains(from3, to.C1sWhereC1C1many2one);

            Assert.Equal(to, from1.C1C1many2one);

            Assert.Equal(to, from1.C1C1many2one);

            Assert.Equal(to, from2.C1C1many2one);

            Assert.Equal(to, from2.C1C1many2one);

            Assert.Equal(to, from3.C1C1many2one);

            Assert.Equal(to, from3.C1C1many2one);

            Assert.Null(from4.C1C1many2one);

            Assert.Null(from4.C1C1many2one);


            // 1-2
            from3.RemoveC1C1many2one();

            from3.RemoveC1C1many2one();
            // Twice
            Assert.Equal(2, to.C1sWhereC1C1many2one.Count());

            Assert.Equal(2, to.C1sWhereC1C1many2one.Count());

            Assert.Contains(from1, to.C1sWhereC1C1many2one);

            Assert.Contains(from1, to.C1sWhereC1C1many2one);

            Assert.Contains(from2, to.C1sWhereC1C1many2one);

            Assert.Contains(from2, to.C1sWhereC1C1many2one);

            Assert.Equal(to, from1.C1C1many2one);

            Assert.Equal(to, from1.C1C1many2one);

            Assert.Equal(to, from2.C1C1many2one);

            Assert.Equal(to, from2.C1C1many2one);

            Assert.Null(from3.C1C1many2one);

            Assert.Null(from3.C1C1many2one);

            Assert.Null(from4.C1C1many2one);

            Assert.Null(from4.C1C1many2one);


            // 1-1
            from2.RemoveC1C1many2one();

            from2.RemoveC1C1many2one();
            // Twice
            Assert.Single(to.C1sWhereC1C1many2one);

            Assert.Single(to.C1sWhereC1C1many2one);

            Assert.Equal(from1, to.C1sWhereC1C1many2one.ElementAt(0));

            Assert.Equal(from1, to.C1sWhereC1C1many2one.ElementAt(0));

            Assert.Equal(to, from1.C1C1many2one);

            Assert.Equal(to, from1.C1C1many2one);

            Assert.Null(from2.C1C1many2one);

            Assert.Null(from2.C1C1many2one);

            Assert.Null(from3.C1C1many2one);

            Assert.Null(from3.C1C1many2one);

            Assert.Null(from4.C1C1many2one);

            Assert.Null(from4.C1C1many2one);


            // 0-0
            from1.RemoveC1C1many2one();

            from1.RemoveC1C1many2one();
            // Twice
            Assert.Null(from1.C1C1many2one);

            Assert.Null(from1.C1C1many2one);

            Assert.Null(from2.C1C1many2one);

            Assert.Null(from2.C1C1many2one);

            Assert.Null(from3.C1C1many2one);

            Assert.Null(from3.C1C1many2one);

            Assert.Null(from4.C1C1many2one);

            Assert.Null(from4.C1C1many2one);


            // Exist
            Assert.False(to.ExistC1sWhereC1C1many2one);

            Assert.False(to.ExistC1sWhereC1C1many2one);

            Assert.False(from1.ExistC1C1many2one);

            Assert.False(from1.ExistC1C1many2one);

            Assert.False(from2.ExistC1C1many2one);

            Assert.False(from2.ExistC1C1many2one);

            Assert.False(from3.ExistC1C1many2one);

            Assert.False(from3.ExistC1C1many2one);

            Assert.False(from4.ExistC1C1many2one);

            Assert.False(from4.ExistC1C1many2one);


            // 1-1
            from1.C1C1many2one = to;

            from1.C1C1many2one = to;
            // Twice
            Assert.True(to.ExistC1sWhereC1C1many2one);

            Assert.True(to.ExistC1sWhereC1C1many2one);

            Assert.True(from1.ExistC1C1many2one);

            Assert.True(from1.ExistC1C1many2one);

            Assert.False(from2.ExistC1C1many2one);

            Assert.False(from2.ExistC1C1many2one);

            Assert.False(from3.ExistC1C1many2one);

            Assert.False(from3.ExistC1C1many2one);

            Assert.False(from4.ExistC1C1many2one);

            Assert.False(from4.ExistC1C1many2one);


            // 1-2
            from2.C1C1many2one = to;

            from2.C1C1many2one = to;
            // Twice
            Assert.True(to.ExistC1sWhereC1C1many2one);

            Assert.True(to.ExistC1sWhereC1C1many2one);

            Assert.True(from1.ExistC1C1many2one);

            Assert.True(from1.ExistC1C1many2one);

            Assert.True(from2.ExistC1C1many2one);

            Assert.True(from2.ExistC1C1many2one);

            Assert.False(from3.ExistC1C1many2one);

            Assert.False(from3.ExistC1C1many2one);

            Assert.False(from4.ExistC1C1many2one);

            Assert.False(from4.ExistC1C1many2one);


            // 1-3
            from3.C1C1many2one = to;

            from3.C1C1many2one = to;
            // Twice
            Assert.True(to.ExistC1sWhereC1C1many2one);

            Assert.True(to.ExistC1sWhereC1C1many2one);

            Assert.True(from1.ExistC1C1many2one);

            Assert.True(from1.ExistC1C1many2one);

            Assert.True(from2.ExistC1C1many2one);

            Assert.True(from2.ExistC1C1many2one);

            Assert.True(from3.ExistC1C1many2one);

            Assert.True(from3.ExistC1C1many2one);

            Assert.False(from4.ExistC1C1many2one);

            Assert.False(from4.ExistC1C1many2one);


            // 1-4
            from4.C1C1many2one = to;

            from4.C1C1many2one = to;
            // Twice
            Assert.True(to.ExistC1sWhereC1C1many2one);

            Assert.True(to.ExistC1sWhereC1C1many2one);

            Assert.True(from1.ExistC1C1many2one);

            Assert.True(from1.ExistC1C1many2one);

            Assert.True(from2.ExistC1C1many2one);

            Assert.True(from2.ExistC1C1many2one);

            Assert.True(from3.ExistC1C1many2one);

            Assert.True(from3.ExistC1C1many2one);

            Assert.True(from4.ExistC1C1many2one);

            Assert.True(from4.ExistC1C1many2one);


            // 1-3
            from4.RemoveC1C1many2one();

            from4.RemoveC1C1many2one();
            // Twice
            Assert.True(to.ExistC1sWhereC1C1many2one);

            Assert.True(to.ExistC1sWhereC1C1many2one);

            Assert.True(from1.ExistC1C1many2one);

            Assert.True(from1.ExistC1C1many2one);

            Assert.True(from2.ExistC1C1many2one);

            Assert.True(from2.ExistC1C1many2one);

            Assert.True(from3.ExistC1C1many2one);

            Assert.True(from3.ExistC1C1many2one);

            Assert.False(from4.ExistC1C1many2one);

            Assert.False(from4.ExistC1C1many2one);


            // 1-2
            from3.RemoveC1C1many2one();

            from3.RemoveC1C1many2one();
            // Twice
            Assert.True(to.ExistC1sWhereC1C1many2one);

            Assert.True(to.ExistC1sWhereC1C1many2one);

            Assert.True(from1.ExistC1C1many2one);

            Assert.True(from1.ExistC1C1many2one);

            Assert.True(from2.ExistC1C1many2one);

            Assert.True(from2.ExistC1C1many2one);

            Assert.False(from3.ExistC1C1many2one);

            Assert.False(from3.ExistC1C1many2one);

            Assert.False(from4.ExistC1C1many2one);

            Assert.False(from4.ExistC1C1many2one);


            // 1-1
            from2.RemoveC1C1many2one();

            from2.RemoveC1C1many2one();
            // Twice
            Assert.True(to.ExistC1sWhereC1C1many2one);

            Assert.True(to.ExistC1sWhereC1C1many2one);

            Assert.True(from1.ExistC1C1many2one);

            Assert.True(from1.ExistC1C1many2one);

            Assert.False(from2.ExistC1C1many2one);

            Assert.False(from2.ExistC1C1many2one);

            Assert.False(from3.ExistC1C1many2one);

            Assert.False(from3.ExistC1C1many2one);

            Assert.False(from4.ExistC1C1many2one);

            Assert.False(from4.ExistC1C1many2one);


            // 0-0
            from1.RemoveC1C1many2one();

            from1.RemoveC1C1many2one();
            // Twice
            Assert.False(to.ExistC1sWhereC1C1many2one);

            Assert.False(to.ExistC1sWhereC1C1many2one);

            Assert.False(from1.ExistC1C1many2one);

            Assert.False(from1.ExistC1C1many2one);

            Assert.False(from2.ExistC1C1many2one);

            Assert.False(from2.ExistC1C1many2one);

            Assert.False(from3.ExistC1C1many2one);

            Assert.False(from3.ExistC1C1many2one);

            Assert.False(from4.ExistC1C1many2one);

            Assert.False(from4.ExistC1C1many2one);


            // Multiplicity
            // Same New / Same To
            // Get
            Assert.Null(from1.C1C1many2one);

            Assert.Null(from1.C1C1many2one);

            Assert.Empty(to.C1sWhereC1C1many2one);

            Assert.Empty(to.C1sWhereC1C1many2one);

            from1.C1C1many2one = to;

            from1.C1C1many2one = to;
            // Twice
            Assert.Equal(to, from1.C1C1many2one);

            Assert.Equal(to, from1.C1C1many2one);

            Assert.Single(to.C1sWhereC1C1many2one);

            Assert.Single(to.C1sWhereC1C1many2one);

            Assert.Contains(from1, to.C1sWhereC1C1many2one);

            Assert.Contains(from1, to.C1sWhereC1C1many2one);

            from1.RemoveC1C1many2one();

            Assert.Null(from1.C1C1many2one);

            Assert.Null(from1.C1C1many2one);

            Assert.Empty(to.C1sWhereC1C1many2one);

            Assert.Empty(to.C1sWhereC1C1many2one);


            // Exist
            Assert.False(from1.ExistC1C1many2one);

            Assert.False(from1.ExistC1C1many2one);

            Assert.False(to.ExistC1sWhereC1C1many2one);

            Assert.False(to.ExistC1sWhereC1C1many2one);

            from1.C1C1many2one = to;

            from1.C1C1many2one = to;
            // Twice
            Assert.True(from1.ExistC1C1many2one);

            Assert.True(from1.ExistC1C1many2one);

            Assert.True(to.ExistC1sWhereC1C1many2one);

            Assert.True(to.ExistC1sWhereC1C1many2one);

            from1.RemoveC1C1many2one();

            Assert.False(from1.ExistC1C1many2one);

            Assert.False(from1.ExistC1C1many2one);

            Assert.False(to.ExistC1sWhereC1C1many2one);

            Assert.False(to.ExistC1sWhereC1C1many2one);


            // Same New / Different To
            // Get
            Assert.Empty(to.C1sWhereC1C1many2one);

            Assert.Empty(to.C1sWhereC1C1many2one);

            Assert.Null(from1.C1C1many2one);

            Assert.Null(from1.C1C1many2one);

            Assert.Empty(toAnother.C1sWhereC1C1many2one);

            Assert.Empty(toAnother.C1sWhereC1C1many2one);

            from1.C1C1many2one = to;

            from1.C1C1many2one = to;
            // Twice
            Assert.Single(to.C1sWhereC1C1many2one);

            Assert.Single(to.C1sWhereC1C1many2one);

            Assert.Equal(to, from1.C1C1many2one);

            Assert.Equal(to, from1.C1C1many2one);

            Assert.Empty(toAnother.C1sWhereC1C1many2one);

            Assert.Empty(toAnother.C1sWhereC1C1many2one);

            Assert.Contains(from1, to.C1sWhereC1C1many2one);

            Assert.Contains(from1, to.C1sWhereC1C1many2one);

            from1.C1C1many2one = toAnother;

            from1.C1C1many2one = toAnother;
            // Twice
            Assert.Empty(to.C1sWhereC1C1many2one);

            Assert.Empty(to.C1sWhereC1C1many2one);

            Assert.Equal(toAnother, from1.C1C1many2one);

            Assert.Equal(toAnother, from1.C1C1many2one);

            Assert.Single(toAnother.C1sWhereC1C1many2one);

            Assert.Single(toAnother.C1sWhereC1C1many2one);

            Assert.Contains(from1, toAnother.C1sWhereC1C1many2one);

            Assert.Contains(from1, toAnother.C1sWhereC1C1many2one);

            from1.C1C1many2one = null;

            from1.C1C1many2one = null;
            // Twice
            Assert.Empty(to.C1sWhereC1C1many2one);

            Assert.Empty(to.C1sWhereC1C1many2one);

            Assert.Null(from1.C1C1many2one);

            Assert.Null(from1.C1C1many2one);

            Assert.Empty(toAnother.C1sWhereC1C1many2one);

            Assert.Empty(toAnother.C1sWhereC1C1many2one);


            // Exist
            Assert.False(to.ExistC1sWhereC1C1many2one);

            Assert.False(to.ExistC1sWhereC1C1many2one);

            Assert.False(from1.ExistC1C1many2one);

            Assert.False(from1.ExistC1C1many2one);

            Assert.False(toAnother.ExistC1sWhereC1C1many2one);

            Assert.False(toAnother.ExistC1sWhereC1C1many2one);

            from1.C1C1many2one = to;

            from1.C1C1many2one = to;
            // Twice
            Assert.True(to.ExistC1sWhereC1C1many2one);

            Assert.True(to.ExistC1sWhereC1C1many2one);

            Assert.True(from1.ExistC1C1many2one);

            Assert.True(from1.ExistC1C1many2one);

            Assert.False(toAnother.ExistC1sWhereC1C1many2one);

            Assert.False(toAnother.ExistC1sWhereC1C1many2one);

            from1.C1C1many2one = toAnother;

            from1.C1C1many2one = toAnother;
            // Twice
            Assert.False(to.ExistC1sWhereC1C1many2one);

            Assert.False(to.ExistC1sWhereC1C1many2one);

            Assert.True(from1.ExistC1C1many2one);

            Assert.True(from1.ExistC1C1many2one);

            Assert.True(toAnother.ExistC1sWhereC1C1many2one);

            Assert.True(toAnother.ExistC1sWhereC1C1many2one);

            from1.C1C1many2one = null;

            from1.C1C1many2one = null;
            // Twice
            from1.C1C1many2one = null;

            from1.C1C1many2one = null;
            // Twice
            Assert.False(to.ExistC1sWhereC1C1many2one);

            Assert.False(to.ExistC1sWhereC1C1many2one);

            Assert.False(from1.ExistC1C1many2one);

            Assert.False(from1.ExistC1C1many2one);

            Assert.False(toAnother.ExistC1sWhereC1C1many2one);

            Assert.False(toAnother.ExistC1sWhereC1C1many2one);


            // Different New / Different To
            // Get
            Assert.Null(from1.C1C1many2one);

            Assert.Null(from1.C1C1many2one);

            Assert.Empty(to.C1sWhereC1C1many2one);

            Assert.Empty(to.C1sWhereC1C1many2one);

            Assert.Null(from2.C1C1many2one);

            Assert.Null(from2.C1C1many2one);

            Assert.Empty(toAnother.C1sWhereC1C1many2one);

            Assert.Empty(toAnother.C1sWhereC1C1many2one);

            from1.C1C1many2one = to;

            from1.C1C1many2one = to;
            // Twice
            Assert.Equal(to, from1.C1C1many2one);

            Assert.Equal(to, from1.C1C1many2one);

            Assert.Single(to.C1sWhereC1C1many2one);

            Assert.Single(to.C1sWhereC1C1many2one);

            Assert.Contains(from1, to.C1sWhereC1C1many2one);

            Assert.Contains(from1, to.C1sWhereC1C1many2one);

            Assert.Null(from2.C1C1many2one);

            Assert.Null(from2.C1C1many2one);

            Assert.Empty(toAnother.C1sWhereC1C1many2one);

            Assert.Empty(toAnother.C1sWhereC1C1many2one);

            from2.C1C1many2one = toAnother;

            from2.C1C1many2one = toAnother;
            // Twice
            Assert.Equal(to, from1.C1C1many2one);

            Assert.Equal(to, from1.C1C1many2one);

            Assert.Single(to.C1sWhereC1C1many2one);

            Assert.Single(to.C1sWhereC1C1many2one);

            Assert.Contains(from1, to.C1sWhereC1C1many2one);

            Assert.Contains(from1, to.C1sWhereC1C1many2one);

            Assert.Equal(toAnother, from2.C1C1many2one);

            Assert.Equal(toAnother, from2.C1C1many2one);

            Assert.Single(toAnother.C1sWhereC1C1many2one);

            Assert.Single(toAnother.C1sWhereC1C1many2one);

            Assert.Contains(from2, toAnother.C1sWhereC1C1many2one);

            Assert.Contains(from2, toAnother.C1sWhereC1C1many2one);

            from1.C1C1many2one = null;

            from1.C1C1many2one = null;
            // Twice
            from2.C1C1many2one = null;

            from2.C1C1many2one = null;
            // Twice
            Assert.Null(from1.C1C1many2one);

            Assert.Null(from1.C1C1many2one);

            Assert.Empty(to.C1sWhereC1C1many2one);

            Assert.Empty(to.C1sWhereC1C1many2one);

            Assert.Null(from2.C1C1many2one);

            Assert.Null(from2.C1C1many2one);

            Assert.Empty(toAnother.C1sWhereC1C1many2one);

            Assert.Empty(toAnother.C1sWhereC1C1many2one);


            // Exist
            Assert.False(from1.ExistC1C1many2one);

            Assert.False(from1.ExistC1C1many2one);

            Assert.False(to.ExistC1sWhereC1C1many2one);

            Assert.False(to.ExistC1sWhereC1C1many2one);

            Assert.False(from2.ExistC1C1many2one);

            Assert.False(from2.ExistC1C1many2one);

            Assert.False(toAnother.ExistC1sWhereC1C1many2one);

            Assert.False(toAnother.ExistC1sWhereC1C1many2one);

            from1.C1C1many2one = to;

            from1.C1C1many2one = to;
            // Twice
            Assert.True(from1.ExistC1C1many2one);

            Assert.True(from1.ExistC1C1many2one);

            Assert.True(to.ExistC1sWhereC1C1many2one);

            Assert.True(to.ExistC1sWhereC1C1many2one);

            Assert.False(from2.ExistC1C1many2one);

            Assert.False(from2.ExistC1C1many2one);

            Assert.False(toAnother.ExistC1sWhereC1C1many2one);

            Assert.False(toAnother.ExistC1sWhereC1C1many2one);

            from2.C1C1many2one = toAnother;

            from2.C1C1many2one = toAnother;
            // Twice
            Assert.True(from1.ExistC1C1many2one);

            Assert.True(from1.ExistC1C1many2one);

            Assert.True(to.ExistC1sWhereC1C1many2one);

            Assert.True(to.ExistC1sWhereC1C1many2one);

            Assert.True(from2.ExistC1C1many2one);

            Assert.True(from2.ExistC1C1many2one);

            Assert.True(toAnother.ExistC1sWhereC1C1many2one);

            Assert.True(toAnother.ExistC1sWhereC1C1many2one);

            from1.C1C1many2one = null;

            from1.C1C1many2one = null;
            // Twice
            from2.C1C1many2one = null;

            from2.C1C1many2one = null;
            // Twice
            Assert.False(from1.ExistC1C1many2one);

            Assert.False(from1.ExistC1C1many2one);

            Assert.False(to.ExistC1sWhereC1C1many2one);

            Assert.False(to.ExistC1sWhereC1C1many2one);

            Assert.False(from2.ExistC1C1many2one);

            Assert.False(from2.ExistC1C1many2one);

            Assert.False(toAnother.ExistC1sWhereC1C1many2one);

            Assert.False(toAnother.ExistC1sWhereC1C1many2one);


            // Different New / Same To
            // Get
            Assert.Null(from1.C1C1many2one);

            Assert.Null(from1.C1C1many2one);

            Assert.Null(from2.C1C1many2one);

            Assert.Null(from2.C1C1many2one);

            Assert.Empty(to.C1sWhereC1C1many2one);

            Assert.Empty(to.C1sWhereC1C1many2one);

            from1.C1C1many2one = to;

            from1.C1C1many2one = to;
            // Twice
            Assert.Equal(to, from1.C1C1many2one);

            Assert.Equal(to, from1.C1C1many2one);

            Assert.Null(from2.C1C1many2one);

            Assert.Null(from2.C1C1many2one);

            Assert.Single(to.C1sWhereC1C1many2one);

            Assert.Single(to.C1sWhereC1C1many2one);

            Assert.Contains(from1, to.C1sWhereC1C1many2one);

            Assert.Contains(from1, to.C1sWhereC1C1many2one);

            Assert.DoesNotContain(from2, to.C1sWhereC1C1many2one);

            Assert.DoesNotContain(from2, to.C1sWhereC1C1many2one);

            from2.C1C1many2one = to;

            from2.C1C1many2one = to;
            // Twice
            Assert.Equal(to, from1.C1C1many2one);

            Assert.Equal(to, from1.C1C1many2one);

            Assert.Equal(to, from2.C1C1many2one);

            Assert.Equal(to, from2.C1C1many2one);

            Assert.Equal(2, to.C1sWhereC1C1many2one.Count());

            Assert.Equal(2, to.C1sWhereC1C1many2one.Count());

            Assert.Contains(from1, to.C1sWhereC1C1many2one);

            Assert.Contains(from1, to.C1sWhereC1C1many2one);

            Assert.Contains(from2, to.C1sWhereC1C1many2one);

            Assert.Contains(from2, to.C1sWhereC1C1many2one);

            from1.RemoveC1C1many2one();

            from2.RemoveC1C1many2one();

            Assert.Null(from1.C1C1many2one);

            Assert.Null(from1.C1C1many2one);

            Assert.Null(from2.C1C1many2one);

            Assert.Null(from2.C1C1many2one);

            Assert.Empty(to.C1sWhereC1C1many2one);

            Assert.Empty(to.C1sWhereC1C1many2one);


            // Exist
            Assert.False(from1.ExistC1C1many2one);

            Assert.False(from1.ExistC1C1many2one);

            Assert.False(from2.ExistC1C1many2one);

            Assert.False(from2.ExistC1C1many2one);

            Assert.False(to.ExistC1sWhereC1C1many2one);

            Assert.False(to.ExistC1sWhereC1C1many2one);

            from1.C1C1many2one = to;

            from1.C1C1many2one = to;
            // Twice
            Assert.True(from1.ExistC1C1many2one);

            Assert.True(from1.ExistC1C1many2one);

            Assert.False(from2.ExistC1C1many2one);

            Assert.False(from2.ExistC1C1many2one);

            Assert.True(to.ExistC1sWhereC1C1many2one);

            Assert.True(to.ExistC1sWhereC1C1many2one);

            from2.C1C1many2one = to;

            from2.C1C1many2one = to;
            // Twice
            Assert.True(from1.ExistC1C1many2one);

            Assert.True(from1.ExistC1C1many2one);

            Assert.True(from2.ExistC1C1many2one);

            Assert.True(from2.ExistC1C1many2one);

            Assert.True(to.ExistC1sWhereC1C1many2one);

            Assert.True(to.ExistC1sWhereC1C1many2one);

            from1.RemoveC1C1many2one();

            from2.RemoveC1C1many2one();

            Assert.Null(from1.C1C1many2one);

            Assert.Null(from1.C1C1many2one);

            Assert.Null(from2.C1C1many2one);

            Assert.Null(from2.C1C1many2one);

            Assert.Empty(to.C1sWhereC1C1many2one);

            Assert.Empty(to.C1sWhereC1C1many2one);


            // Null & Empty Array
            // Set Null
            // Get
            Assert.Null(from1.C1C1many2one);

            Assert.Null(from1.C1C1many2one);

            from1.C1C1many2one = null;

            from1.C1C1many2one = null;
            // Twice
            Assert.Null(from1.C1C1many2one);

            Assert.Null(from1.C1C1many2one);

            from1.C1C1many2one = to;

            from1.C1C1many2one = to;
            // Twice
            from1.C1C1many2one = null;

            from1.C1C1many2one = null;
            // Twice
            Assert.Null(from1.C1C1many2one);

            Assert.Null(from1.C1C1many2one);


            // Exist
            Assert.False(from1.ExistC1C1many2one);

            Assert.False(from1.ExistC1C1many2one);

            from1.C1C1many2one = null;

            from1.C1C1many2one = null;
            // Twice
            Assert.False(from1.ExistC1C1many2one);

            Assert.False(from1.ExistC1C1many2one);

            from1.C1C1many2one = to;

            from1.C1C1many2one = to;
            // Twice
            from1.C1C1many2one = null;

            from1.C1C1many2one = null;
            // Twice
            Assert.False(from1.ExistC1C1many2one);

            Assert.False(from1.ExistC1C1many2one);
        }
    */

    protected abstract IDatabase CreateDatabase();
}
