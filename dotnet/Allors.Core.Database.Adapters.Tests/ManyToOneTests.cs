namespace Allors.Core.Database.Adapters.Tests;

using System.Linq;
using Allors.Core.Database.Meta;
using Xunit;

public abstract class ManyToOneTests : Tests
{
    [Fact]
    public void C1_C1many2one()
    {
        var database = this.CreateDatabase();
        var transaction = database.CreateTransaction();

        var m = this.Meta;

        var from1 = transaction.Build(m.C1);
        var from2 = transaction.Build(m.C1);
        var from3 = transaction.Build(m.C1);
        var from4 = transaction.Build(m.C1);
        var to = transaction.Build(m.C1);
        var toAnother = transaction.Build(m.C1);

        // New 0-4-0
        // Get
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from3[m.C1C1ManyToOne]);
        Assert.Null(from3[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);

        // 1-1
        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to; // Twice

        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Equal(from1, to[m.C1sWhereC1ManyToOne].ToArray().ElementAt(0));
        Assert.Equal(from1, to[m.C1sWhereC1ManyToOne].ToArray().ElementAt(0));
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from3[m.C1C1ManyToOne]);
        Assert.Null(from3[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);

        // 1-2
        from2[m.C1C1ManyToOne] = to;
        from2[m.C1C1ManyToOne] = to; // Twice

        Assert.Equal(2, to[m.C1sWhereC1ManyToOne].ToArray().ToArray().Length);
        Assert.Equal(2, to[m.C1sWhereC1ManyToOne].ToArray().ToArray().Length);
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from2[m.C1C1ManyToOne]);
        Assert.Equal(to, from2[m.C1C1ManyToOne]);
        Assert.Null(from3[m.C1C1ManyToOne]);
        Assert.Null(from3[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);

        // 1-3
        from3[m.C1C1ManyToOne] = to;
        from3[m.C1C1ManyToOne] = to; // Twice

        Assert.Equal(3, to[m.C1sWhereC1ManyToOne].ToArray().Length);
        Assert.Equal(3, to[m.C1sWhereC1ManyToOne].ToArray().Length);
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from3, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from3, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from2[m.C1C1ManyToOne]);
        Assert.Equal(to, from2[m.C1C1ManyToOne]);
        Assert.Equal(to, from3[m.C1C1ManyToOne]);
        Assert.Equal(to, from3[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);

        // 1-4
        from4[m.C1C1ManyToOne] = to;
        from4[m.C1C1ManyToOne] = to; // Twice

        Assert.Equal(4, to[m.C1sWhereC1ManyToOne].ToArray().Length);
        Assert.Equal(4, to[m.C1sWhereC1ManyToOne].ToArray().Length);
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from3, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from3, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from4, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from4, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from2[m.C1C1ManyToOne]);
        Assert.Equal(to, from2[m.C1C1ManyToOne]);
        Assert.Equal(to, from3[m.C1C1ManyToOne]);
        Assert.Equal(to, from3[m.C1C1ManyToOne]);
        Assert.Equal(to, from4[m.C1C1ManyToOne]);
        Assert.Equal(to, from4[m.C1C1ManyToOne]);

        // 1-3
        from4[m.C1C1ManyToOne] = null;
        from4[m.C1C1ManyToOne] = null; // Twice

        Assert.Equal(3, to[m.C1sWhereC1ManyToOne].ToArray().Length);
        Assert.Equal(3, to[m.C1sWhereC1ManyToOne].ToArray().Length);
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from3, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from3, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from2[m.C1C1ManyToOne]);
        Assert.Equal(to, from2[m.C1C1ManyToOne]);
        Assert.Equal(to, from3[m.C1C1ManyToOne]);
        Assert.Equal(to, from3[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);

        // 1-2
        from3[m.C1C1ManyToOne] = null;
        from3[m.C1C1ManyToOne] = null; // Twice

        Assert.Equal(2, to[m.C1sWhereC1ManyToOne].ToArray().Length);
        Assert.Equal(2, to[m.C1sWhereC1ManyToOne].ToArray().Length);
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from2[m.C1C1ManyToOne]);
        Assert.Equal(to, from2[m.C1C1ManyToOne]);
        Assert.Null(from3[m.C1C1ManyToOne]);
        Assert.Null(from3[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);

        // 1-1
        from2[m.C1C1ManyToOne] = null;
        from2[m.C1C1ManyToOne] = null; // Twice

        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Equal(from1, to[m.C1sWhereC1ManyToOne].ToArray().ElementAt(0));
        Assert.Equal(from1, to[m.C1sWhereC1ManyToOne].ToArray().ElementAt(0));
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from3[m.C1C1ManyToOne]);
        Assert.Null(from3[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);

        // 0-0
        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null; // Twice

        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from3[m.C1C1ManyToOne]);
        Assert.Null(from3[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);

        // Exist
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));

        // 1-1
        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to; // Twice

        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));

        // 1-2
        from2[m.C1C1ManyToOne] = to;
        from2[m.C1C1ManyToOne] = to; // Twice

        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));

        // 1-3
        from3[m.C1C1ManyToOne] = to;
        from3[m.C1C1ManyToOne] = to; // Twice

        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.True(from3.Exist(m.C1C1ManyToOne));
        Assert.True(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));

        // 1-4
        from4[m.C1C1ManyToOne] = to;
        from4[m.C1C1ManyToOne] = to; // Twice

        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.True(from3.Exist(m.C1C1ManyToOne));
        Assert.True(from3.Exist(m.C1C1ManyToOne));
        Assert.True(from4.Exist(m.C1C1ManyToOne));
        Assert.True(from4.Exist(m.C1C1ManyToOne));

        // 1-3
        from4[m.C1C1ManyToOne] = null;
        from4[m.C1C1ManyToOne] = null; // Twice

        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.True(from3.Exist(m.C1C1ManyToOne));
        Assert.True(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));

        // 1-2
        from3[m.C1C1ManyToOne] = null;
        from3[m.C1C1ManyToOne] = null; // Twice

        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));

        // 1-1
        from2[m.C1C1ManyToOne] = null;
        from2[m.C1C1ManyToOne] = null; // Twice

        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));

        // 0-0
        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null; // Twice

        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));

        // Multiplicity
        // Same New / Same To
        // Get
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to; // Twice

        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());

        from1[m.C1C1ManyToOne] = null;

        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());

        // Exist
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to; // Twice

        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));

        from1[m.C1C1ManyToOne] = null;

        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));

        // Same New / Different To
        // Get
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Empty(toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(toAnother[m.C1sWhereC1ManyToOne].ToArray());

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to; // Twice

        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Empty(toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());

        from1[m.C1C1ManyToOne] = toAnother;
        from1[m.C1C1ManyToOne] = toAnother; // Twice

        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Equal(toAnother, from1[m.C1C1ManyToOne]);
        Assert.Equal(toAnother, from1[m.C1C1ManyToOne]);
        Assert.Single(toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Single(toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, toAnother[m.C1sWhereC1ManyToOne].ToArray());

        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null; // Twice

        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Empty(toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(toAnother[m.C1sWhereC1ManyToOne].ToArray());

        // Exist
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(toAnother.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(toAnother.Exist(m.C1sWhereC1ManyToOne));

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to; // Twice

        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.False(toAnother.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(toAnother.Exist(m.C1sWhereC1ManyToOne));

        from1[m.C1C1ManyToOne] = toAnother;
        from1[m.C1C1ManyToOne] = toAnother; // Twice

        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(toAnother.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(toAnother.Exist(m.C1sWhereC1ManyToOne));

        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null; // Twice
        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null; // Twice

        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(toAnother.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(toAnother.Exist(m.C1sWhereC1ManyToOne));

        // Different New / Different To
        // Get
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Empty(toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(toAnother[m.C1sWhereC1ManyToOne].ToArray());

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to; // Twice

        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Empty(toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(toAnother[m.C1sWhereC1ManyToOne].ToArray());

        from2[m.C1C1ManyToOne] = toAnother;
        from2[m.C1C1ManyToOne] = toAnother; // Twice

        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Equal(toAnother, from2[m.C1C1ManyToOne]);
        Assert.Equal(toAnother, from2[m.C1C1ManyToOne]);
        Assert.Single(toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Single(toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, toAnother[m.C1sWhereC1ManyToOne].ToArray());

        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null; // Twice
        from2[m.C1C1ManyToOne] = null;
        from2[m.C1C1ManyToOne] = null; // Twice

        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Empty(toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(toAnother[m.C1sWhereC1ManyToOne].ToArray());

        // Exist
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(toAnother.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(toAnother.Exist(m.C1sWhereC1ManyToOne));

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to; // Twice

        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(toAnother.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(toAnother.Exist(m.C1sWhereC1ManyToOne));

        from2[m.C1C1ManyToOne] = toAnother;
        from2[m.C1C1ManyToOne] = toAnother; // Twice

        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.True(toAnother.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(toAnother.Exist(m.C1sWhereC1ManyToOne));

        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null; // Twice
        from2[m.C1C1ManyToOne] = null;
        from2[m.C1C1ManyToOne] = null; // Twice

        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(toAnother.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(toAnother.Exist(m.C1sWhereC1ManyToOne));

        // Different New / Same To
        // Get
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to; // Twice

        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.DoesNotContain(from2, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.DoesNotContain(from2, to[m.C1sWhereC1ManyToOne].ToArray());

        from2[m.C1C1ManyToOne] = to;
        from2[m.C1C1ManyToOne] = to; // Twice

        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from2[m.C1C1ManyToOne]);
        Assert.Equal(to, from2[m.C1C1ManyToOne]);
        Assert.Equal(2, to[m.C1sWhereC1ManyToOne].ToArray().Length);
        Assert.Equal(2, to[m.C1sWhereC1ManyToOne].ToArray().Length);
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, to[m.C1sWhereC1ManyToOne].ToArray());

        from1[m.C1C1ManyToOne] = null;
        from2[m.C1C1ManyToOne] = null;

        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());

        // Exist
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to; // Twice

        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));

        from2[m.C1C1ManyToOne] = to;
        from2[m.C1C1ManyToOne] = to; // Twice

        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));

        from1[m.C1C1ManyToOne] = null;
        from2[m.C1C1ManyToOne] = null;

        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());

        // Null & Empty Array
        // Set Null
        // Get
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);

        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null; // Twice

        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to; // Twice
        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null; // Twice

        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);

        // Exist
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));

        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null; // Twice

        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to; // Twice
        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null; // Twice

        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
    }

    [Fact]
    public void C1_C1many2one_2()
    {
        var database = this.CreateDatabase();
        var transaction = database.CreateTransaction();

        var m = this.Meta;

        var from1 = transaction.Build(m.C1);
        var from2 = transaction.Build(m.C1);
        var from3 = transaction.Build(m.C1);
        var from4 = transaction.Build(m.C1);
        var to = transaction.Build(m.C1);
        var toAnother = transaction.Build(m.C1);

        // New 0-4-0
        // Get
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from3[m.C1C1ManyToOne]);
        Assert.Null(from3[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);

        // 1-1
        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to;

        // Twice
        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Equal(from1, to[m.C1sWhereC1ManyToOne].ToArray().ElementAt(0));
        Assert.Equal(from1, to[m.C1sWhereC1ManyToOne].ToArray().ElementAt(0));
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from3[m.C1C1ManyToOne]);
        Assert.Null(from3[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);

        // 1-2
        from2[m.C1C1ManyToOne] = to;
        from2[m.C1C1ManyToOne] = to;

        // Twice
        Assert.Equal(2, to[m.C1sWhereC1ManyToOne].ToArray().Length);
        Assert.Equal(2, to[m.C1sWhereC1ManyToOne].ToArray().Length);
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from2[m.C1C1ManyToOne]);
        Assert.Equal(to, from2[m.C1C1ManyToOne]);
        Assert.Null(from3[m.C1C1ManyToOne]);
        Assert.Null(from3[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);

        // 1-3
        from3[m.C1C1ManyToOne] = to;
        from3[m.C1C1ManyToOne] = to;

        // Twice
        Assert.Equal(3, to[m.C1sWhereC1ManyToOne].ToArray().Length);
        Assert.Equal(3, to[m.C1sWhereC1ManyToOne].ToArray().Length);
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from3, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from3, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from2[m.C1C1ManyToOne]);
        Assert.Equal(to, from2[m.C1C1ManyToOne]);
        Assert.Equal(to, from3[m.C1C1ManyToOne]);
        Assert.Equal(to, from3[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);

        // 1-4
        from4[m.C1C1ManyToOne] = to;
        from4[m.C1C1ManyToOne] = to;

        // Twice
        Assert.Equal(4, to[m.C1sWhereC1ManyToOne].ToArray().Length);
        Assert.Equal(4, to[m.C1sWhereC1ManyToOne].ToArray().Length);
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from3, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from3, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from4, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from4, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from2[m.C1C1ManyToOne]);
        Assert.Equal(to, from2[m.C1C1ManyToOne]);
        Assert.Equal(to, from3[m.C1C1ManyToOne]);
        Assert.Equal(to, from3[m.C1C1ManyToOne]);
        Assert.Equal(to, from4[m.C1C1ManyToOne]);
        Assert.Equal(to, from4[m.C1C1ManyToOne]);

        // 1-3
        from4[m.C1C1ManyToOne] = null;
        from4[m.C1C1ManyToOne] = null;

        // Twice
        Assert.Equal(3, to[m.C1sWhereC1ManyToOne].ToArray().Length);
        Assert.Equal(3, to[m.C1sWhereC1ManyToOne].ToArray().Length);
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from3, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from3, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from2[m.C1C1ManyToOne]);
        Assert.Equal(to, from2[m.C1C1ManyToOne]);
        Assert.Equal(to, from3[m.C1C1ManyToOne]);
        Assert.Equal(to, from3[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);

        // 1-2
        from3[m.C1C1ManyToOne] = null;
        from3[m.C1C1ManyToOne] = null;

        // Twice
        Assert.Equal(2, to[m.C1sWhereC1ManyToOne].ToArray().Length);
        Assert.Equal(2, to[m.C1sWhereC1ManyToOne].ToArray().Length);
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from2[m.C1C1ManyToOne]);
        Assert.Equal(to, from2[m.C1C1ManyToOne]);
        Assert.Null(from3[m.C1C1ManyToOne]);
        Assert.Null(from3[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);

        // 1-1
        from2[m.C1C1ManyToOne] = null;
        from2[m.C1C1ManyToOne] = null;

        // Twice
        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Equal(from1, to[m.C1sWhereC1ManyToOne].ToArray().ElementAt(0));
        Assert.Equal(from1, to[m.C1sWhereC1ManyToOne].ToArray().ElementAt(0));
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from3[m.C1C1ManyToOne]);
        Assert.Null(from3[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);

        // 0-0
        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null;

        // Twice
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from3[m.C1C1ManyToOne]);
        Assert.Null(from3[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);
        Assert.Null(from4[m.C1C1ManyToOne]);

        // Exist
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));

        // 1-1
        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to;

        // Twice
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));

        // 1-2
        from2[m.C1C1ManyToOne] = to;
        from2[m.C1C1ManyToOne] = to;

        // Twice
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));

        // 1-3
        from3[m.C1C1ManyToOne] = to;
        from3[m.C1C1ManyToOne] = to;

        // Twice
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.True(from3.Exist(m.C1C1ManyToOne));
        Assert.True(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));

        // 1-4
        from4[m.C1C1ManyToOne] = to;
        from4[m.C1C1ManyToOne] = to;

        // Twice
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.True(from3.Exist(m.C1C1ManyToOne));
        Assert.True(from3.Exist(m.C1C1ManyToOne));
        Assert.True(from4.Exist(m.C1C1ManyToOne));
        Assert.True(from4.Exist(m.C1C1ManyToOne));

        // 1-3
        from4[m.C1C1ManyToOne] = null;
        from4[m.C1C1ManyToOne] = null;

        // Twice
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.True(from3.Exist(m.C1C1ManyToOne));
        Assert.True(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));

        // 1-2
        from3[m.C1C1ManyToOne] = null;
        from3[m.C1C1ManyToOne] = null;

        // Twice
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));

        // 1-1
        from2[m.C1C1ManyToOne] = null;
        from2[m.C1C1ManyToOne] = null;

        // Twice
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));

        // 0-0
        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null;

        // Twice
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from3.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));
        Assert.False(from4.Exist(m.C1C1ManyToOne));

        // Multiplicity
        // Same New / Same To
        // Get
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to;

        // Twice
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());

        from1[m.C1C1ManyToOne] = null;

        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());

        // Exist
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to;

        // Twice
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));

        from1[m.C1C1ManyToOne] = null;

        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));

        // Same New / Different To
        // Get
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Empty(toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(toAnother[m.C1sWhereC1ManyToOne].ToArray());

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to;

        // Twice
        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Empty(toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());

        from1[m.C1C1ManyToOne] = toAnother;
        from1[m.C1C1ManyToOne] = toAnother;

        // Twice
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Equal(toAnother, from1[m.C1C1ManyToOne]);
        Assert.Equal(toAnother, from1[m.C1C1ManyToOne]);
        Assert.Single(toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Single(toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, toAnother[m.C1sWhereC1ManyToOne].ToArray());

        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null;

        // Twice
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Empty(toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(toAnother[m.C1sWhereC1ManyToOne].ToArray());

        // Exist
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(toAnother.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(toAnother.Exist(m.C1sWhereC1ManyToOne));

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to;

        // Twice
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.False(toAnother.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(toAnother.Exist(m.C1sWhereC1ManyToOne));

        from1[m.C1C1ManyToOne] = toAnother;
        from1[m.C1C1ManyToOne] = toAnother;

        // Twice
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(toAnother.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(toAnother.Exist(m.C1sWhereC1ManyToOne));

        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null;

        // Twice
        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null;

        // Twice
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(toAnother.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(toAnother.Exist(m.C1sWhereC1ManyToOne));

        // Different New / Different To
        // Get
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Empty(toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(toAnother[m.C1sWhereC1ManyToOne].ToArray());

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to;

        // Twice
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());

        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Empty(toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(toAnother[m.C1sWhereC1ManyToOne].ToArray());

        from2[m.C1C1ManyToOne] = toAnother;
        from2[m.C1C1ManyToOne] = toAnother;

        // Twice
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Equal(toAnother, from2[m.C1C1ManyToOne]);
        Assert.Equal(toAnother, from2[m.C1C1ManyToOne]);
        Assert.Single(toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Single(toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, toAnother[m.C1sWhereC1ManyToOne].ToArray());

        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null;

        // Twice
        from2[m.C1C1ManyToOne] = null;
        from2[m.C1C1ManyToOne] = null;

        // Twice
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Empty(toAnother[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(toAnother[m.C1sWhereC1ManyToOne].ToArray());

        // Exist
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(toAnother.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(toAnother.Exist(m.C1sWhereC1ManyToOne));

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to;

        // Twice
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(toAnother.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(toAnother.Exist(m.C1sWhereC1ManyToOne));

        from2[m.C1C1ManyToOne] = toAnother;
        from2[m.C1C1ManyToOne] = toAnother;

        // Twice
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.True(toAnother.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(toAnother.Exist(m.C1sWhereC1ManyToOne));

        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null;

        // Twice
        from2[m.C1C1ManyToOne] = null;
        from2[m.C1C1ManyToOne] = null;

        // Twice
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(toAnother.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(toAnother.Exist(m.C1sWhereC1ManyToOne));

        // Different New / Same To
        // Get
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to;

        // Twice
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Single(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.DoesNotContain(from2, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.DoesNotContain(from2, to[m.C1sWhereC1ManyToOne].ToArray());

        from2[m.C1C1ManyToOne] = to;
        from2[m.C1C1ManyToOne] = to;

        // Twice
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from1[m.C1C1ManyToOne]);
        Assert.Equal(to, from2[m.C1C1ManyToOne]);
        Assert.Equal(to, from2[m.C1C1ManyToOne]);
        Assert.Equal(2, to[m.C1sWhereC1ManyToOne].ToArray().Length);
        Assert.Equal(2, to[m.C1sWhereC1ManyToOne].ToArray().Length);
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from1, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Contains(from2, to[m.C1sWhereC1ManyToOne].ToArray());

        from1[m.C1C1ManyToOne] = null;
        from2[m.C1C1ManyToOne] = null;

        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());

        // Exist
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.False(to.Exist(m.C1sWhereC1ManyToOne));

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to;

        // Twice
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.False(from2.Exist(m.C1C1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));

        from2[m.C1C1ManyToOne] = to;
        from2[m.C1C1ManyToOne] = to;

        // Twice
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from1.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.True(from2.Exist(m.C1C1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));
        Assert.True(to.Exist(m.C1sWhereC1ManyToOne));

        from1[m.C1C1ManyToOne] = null;
        from2[m.C1C1ManyToOne] = null;

        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Null(from2[m.C1C1ManyToOne]);
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());
        Assert.Empty(to[m.C1sWhereC1ManyToOne].ToArray());

        // Null & Empty Array
        // Set Null
        // Get
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);

        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null;

        // Twice
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to;

        // Twice
        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null;

        // Twice
        Assert.Null(from1[m.C1C1ManyToOne]);
        Assert.Null(from1[m.C1C1ManyToOne]);

        // Exist
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));

        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null;

        // Twice
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));

        from1[m.C1C1ManyToOne] = to;
        from1[m.C1C1ManyToOne] = to;

        // Twice
        from1[m.C1C1ManyToOne] = null;
        from1[m.C1C1ManyToOne] = null;

        // Twice
        Assert.False(from1.Exist(m.C1C1ManyToOne));
        Assert.False(from1.Exist(m.C1C1ManyToOne));
    }

    protected abstract IDatabase CreateDatabase();
}
