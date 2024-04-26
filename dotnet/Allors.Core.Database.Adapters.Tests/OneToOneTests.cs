namespace Allors.Core.Database.Adapters.Tests
{
    using Allors.Core.Database.Meta;
    using Xunit;

    public abstract class OneToOneTests
    {
        [Fact]
        public void ToDo()
        {
            Assert.True(true);
        }

        [Fact]
        public void C1_C1OneToOne()
        {
            var coreMeta = new CoreMeta();
            var adaptersMeta = new AdaptersMeta(coreMeta);

            var database = this.CreateDatabase();
            var transaction = database.CreateTransaction();

            var from = transaction.Build(adaptersMeta.C1);
            var fromAnother = transaction.Build(adaptersMeta.C1);
            var to = transaction.Build(adaptersMeta.C1);
            var toAnother = transaction.Build(adaptersMeta.C1);

            var m = adaptersMeta;

            // To 1 and back
            // Get
            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);

            // 1-1
            from[m.C1C1OneToOne] = to;
            from[m.C1C1OneToOne] = to;

            Assert.Equal(to, from[m.C1C1OneToOne]);
            Assert.Equal(to, from[m.C1C1OneToOne]);
            Assert.Equal(from, to[m.C1WhereC1OneToOne]);
            Assert.Equal(from, to[m.C1WhereC1OneToOne]);

            // 0-0
            from[m.C1C1OneToOne] = null;
            from[m.C1C1OneToOne] = null;
            from[m.C1C1OneToOne] = to;
            from[m.C1C1OneToOne] = null;
            from[m.C1C1OneToOne] = null;

            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);

            // Exist
            Assert.False(from.Exist(m.C1C1OneToOne));
            Assert.False(from.Exist(m.C1C1OneToOne));
            Assert.False(to.Exist(m.C1WhereC1OneToOne));
            Assert.False(to.Exist(m.C1WhereC1OneToOne));

            // 1-1
            from[m.C1C1OneToOne] = to;
            from[m.C1C1OneToOne] = to;

            Assert.True(from.Exist(m.C1C1OneToOne));
            Assert.True(from.Exist(m.C1C1OneToOne));
            Assert.True(to.Exist(m.C1WhereC1OneToOne));
            Assert.True(to.Exist(m.C1WhereC1OneToOne));

            // 0-0
            from[m.C1C1OneToOne] = null;
            from[m.C1C1OneToOne] = null;
            from[m.C1C1OneToOne] = to;
            from[m.C1C1OneToOne] = to;
            from[m.C1C1OneToOne] = null;
            from[m.C1C1OneToOne] = null;

            Assert.False(from.Exist(m.C1C1OneToOne));
            Assert.False(from.Exist(m.C1C1OneToOne));
            Assert.False(to.Exist(m.C1WhereC1OneToOne));
            Assert.False(to.Exist(m.C1WhereC1OneToOne));

            // Multiplicity
            // Same New / Same To
            // Get
            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);

            from[m.C1C1OneToOne] = to;
            from[m.C1C1OneToOne] = to;

            Assert.Equal(to, from[m.C1C1OneToOne]);
            Assert.Equal(to, from[m.C1C1OneToOne]);
            Assert.Equal(from, to[m.C1WhereC1OneToOne]);
            Assert.Equal(from, to[m.C1WhereC1OneToOne]);

            from[m.C1C1OneToOne] = null;
            from[m.C1C1OneToOne] = null;

            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);

            // Exist
            Assert.False(from.Exist(m.C1C1OneToOne));
            Assert.False(from.Exist(m.C1C1OneToOne));
            Assert.False(to.Exist(m.C1WhereC1OneToOne));
            Assert.False(to.Exist(m.C1WhereC1OneToOne));

            from[m.C1C1OneToOne] = to;
            from[m.C1C1OneToOne] = to;

            Assert.True(from.Exist(m.C1C1OneToOne));
            Assert.True(from.Exist(m.C1C1OneToOne));
            Assert.True(to.Exist(m.C1WhereC1OneToOne));
            Assert.True(to.Exist(m.C1WhereC1OneToOne));

            from[m.C1C1OneToOne] = null;
            from[m.C1C1OneToOne] = null;

            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);

            // Same New / Different To
            // Get
            Assert.Null(to[m.C1WhereC1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);
            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(toAnother[m.C1WhereC1OneToOne]);
            Assert.Null(toAnother[m.C1WhereC1OneToOne]);

            from[m.C1C1OneToOne] = to;
            from[m.C1C1OneToOne] = to;

            Assert.Equal(from, to[m.C1WhereC1OneToOne]);
            Assert.Equal(from, to[m.C1WhereC1OneToOne]);
            Assert.Equal(to, from[m.C1C1OneToOne]);
            Assert.Equal(to, from[m.C1C1OneToOne]);
            Assert.Null(toAnother[m.C1WhereC1OneToOne]);
            Assert.Null(toAnother[m.C1WhereC1OneToOne]);

            from[m.C1C1OneToOne] = toAnother;
            from[m.C1C1OneToOne] = toAnother;

            Assert.Null(to[m.C1WhereC1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);
            Assert.Equal(toAnother, from[m.C1C1OneToOne]);
            Assert.Equal(toAnother, from[m.C1C1OneToOne]);
            Assert.Equal(from, toAnother[m.C1WhereC1OneToOne]);
            Assert.Equal(from, toAnother[m.C1WhereC1OneToOne]);

            from[m.C1C1OneToOne] = null;
            from[m.C1C1OneToOne] = null;

            Assert.Null(to[m.C1WhereC1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);
            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(toAnother[m.C1WhereC1OneToOne]);
            Assert.Null(toAnother[m.C1WhereC1OneToOne]);

            // Exist
            Assert.False(to.Exist(m.C1WhereC1OneToOne));
            Assert.False(to.Exist(m.C1WhereC1OneToOne));
            Assert.False(from.Exist(m.C1C1OneToOne));
            Assert.False(from.Exist(m.C1C1OneToOne));
            Assert.False(toAnother.Exist(m.C1WhereC1OneToOne));
            Assert.False(toAnother.Exist(m.C1WhereC1OneToOne));

            from[m.C1C1OneToOne] = to;
            from[m.C1C1OneToOne] = to;

            Assert.True(to.Exist(m.C1WhereC1OneToOne));
            Assert.True(to.Exist(m.C1WhereC1OneToOne));
            Assert.True(from.Exist(m.C1C1OneToOne));
            Assert.True(from.Exist(m.C1C1OneToOne));
            Assert.False(toAnother.Exist(m.C1WhereC1OneToOne));
            Assert.False(toAnother.Exist(m.C1WhereC1OneToOne));

            from[m.C1C1OneToOne] = toAnother;
            from[m.C1C1OneToOne] = toAnother;

            Assert.False(to.Exist(m.C1WhereC1OneToOne));
            Assert.False(to.Exist(m.C1WhereC1OneToOne));
            Assert.True(from.Exist(m.C1C1OneToOne));
            Assert.True(from.Exist(m.C1C1OneToOne));
            Assert.True(toAnother.Exist(m.C1WhereC1OneToOne));
            Assert.True(toAnother.Exist(m.C1WhereC1OneToOne));

            from[m.C1C1OneToOne] = null;
            from[m.C1C1OneToOne] = null;

            Assert.False(to.Exist(m.C1WhereC1OneToOne));
            Assert.False(to.Exist(m.C1WhereC1OneToOne));
            Assert.False(from.Exist(m.C1C1OneToOne));
            Assert.False(from.Exist(m.C1C1OneToOne));
            Assert.False(toAnother.Exist(m.C1WhereC1OneToOne));
            Assert.False(toAnother.Exist(m.C1WhereC1OneToOne));

            // Different New / Different To
            // Get
            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);
            Assert.Null(fromAnother[m.C1C1OneToOne]);
            Assert.Null(fromAnother[m.C1C1OneToOne]);
            Assert.Null(toAnother[m.C1WhereC1OneToOne]);
            Assert.Null(toAnother[m.C1WhereC1OneToOne]);

            from[m.C1C1OneToOne] = to;
            from[m.C1C1OneToOne] = to;

            Assert.Equal(to, from[m.C1C1OneToOne]);
            Assert.Equal(to, from[m.C1C1OneToOne]);
            Assert.Equal(from, to[m.C1WhereC1OneToOne]);
            Assert.Equal(from, to[m.C1WhereC1OneToOne]);
            Assert.Null(fromAnother[m.C1C1OneToOne]);
            Assert.Null(fromAnother[m.C1C1OneToOne]);
            Assert.Null(toAnother[m.C1WhereC1OneToOne]);
            Assert.Null(toAnother[m.C1WhereC1OneToOne]);

            fromAnother[m.C1C1OneToOne] = toAnother;

            Assert.Equal(to, from[m.C1C1OneToOne]);
            Assert.Equal(to, from[m.C1C1OneToOne]);
            Assert.Equal(from, to[m.C1WhereC1OneToOne]);
            Assert.Equal(from, to[m.C1WhereC1OneToOne]);
            Assert.Equal(toAnother, fromAnother[m.C1C1OneToOne]);
            Assert.Equal(toAnother, fromAnother[m.C1C1OneToOne]);
            Assert.Equal(fromAnother, toAnother[m.C1WhereC1OneToOne]);
            Assert.Equal(fromAnother, toAnother[m.C1WhereC1OneToOne]);

            from[m.C1C1OneToOne] = null;
            from[m.C1C1OneToOne] = null;

            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);
            Assert.Equal(toAnother, fromAnother[m.C1C1OneToOne]);
            Assert.Equal(toAnother, fromAnother[m.C1C1OneToOne]);
            Assert.Equal(fromAnother, toAnother[m.C1WhereC1OneToOne]);
            Assert.Equal(fromAnother, toAnother[m.C1WhereC1OneToOne]);

            fromAnother[m.C1C1OneToOne] = null;
            fromAnother[m.C1C1OneToOne] = null;

            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);
            Assert.Null(fromAnother[m.C1C1OneToOne]);
            Assert.Null(fromAnother[m.C1C1OneToOne]);
            Assert.Null(toAnother[m.C1WhereC1OneToOne]);
            Assert.Null(toAnother[m.C1WhereC1OneToOne]);

            // Exist
            Assert.False(from.Exist(m.C1C1OneToOne));
            Assert.False(from.Exist(m.C1C1OneToOne));
            Assert.False(to.Exist(m.C1WhereC1OneToOne));
            Assert.False(to.Exist(m.C1WhereC1OneToOne));
            Assert.False(fromAnother.Exist(m.C1C1OneToOne));
            Assert.False(fromAnother.Exist(m.C1C1OneToOne));
            Assert.False(toAnother.Exist(m.C1WhereC1OneToOne));
            Assert.False(toAnother.Exist(m.C1WhereC1OneToOne));

            from[m.C1C1OneToOne] = to;
            from[m.C1C1OneToOne] = to;

            Assert.True(from.Exist(m.C1C1OneToOne));
            Assert.True(from.Exist(m.C1C1OneToOne));
            Assert.True(to.Exist(m.C1WhereC1OneToOne));
            Assert.True(to.Exist(m.C1WhereC1OneToOne));
            Assert.False(fromAnother.Exist(m.C1C1OneToOne));
            Assert.False(fromAnother.Exist(m.C1C1OneToOne));
            Assert.False(toAnother.Exist(m.C1WhereC1OneToOne));
            Assert.False(toAnother.Exist(m.C1WhereC1OneToOne));

            fromAnother[m.C1C1OneToOne] = toAnother;
            fromAnother[m.C1C1OneToOne] = toAnother;

            Assert.True(from.Exist(m.C1C1OneToOne));
            Assert.True(from.Exist(m.C1C1OneToOne));
            Assert.True(to.Exist(m.C1WhereC1OneToOne));
            Assert.True(to.Exist(m.C1WhereC1OneToOne));
            Assert.True(fromAnother.Exist(m.C1C1OneToOne));
            Assert.True(fromAnother.Exist(m.C1C1OneToOne));
            Assert.True(toAnother.Exist(m.C1WhereC1OneToOne));
            Assert.True(toAnother.Exist(m.C1WhereC1OneToOne));

            from[m.C1C1OneToOne] = null;
            from[m.C1C1OneToOne] = null;

            Assert.False(from.Exist(m.C1C1OneToOne));
            Assert.False(from.Exist(m.C1C1OneToOne));
            Assert.False(to.Exist(m.C1WhereC1OneToOne));
            Assert.False(to.Exist(m.C1WhereC1OneToOne));
            Assert.True(fromAnother.Exist(m.C1C1OneToOne));
            Assert.True(fromAnother.Exist(m.C1C1OneToOne));
            Assert.True(toAnother.Exist(m.C1WhereC1OneToOne));
            Assert.True(toAnother.Exist(m.C1WhereC1OneToOne));

            fromAnother[m.C1C1OneToOne] = null;
            fromAnother[m.C1C1OneToOne] = null;

            Assert.False(from.Exist(m.C1C1OneToOne));
            Assert.False(from.Exist(m.C1C1OneToOne));
            Assert.False(to.Exist(m.C1WhereC1OneToOne));
            Assert.False(to.Exist(m.C1WhereC1OneToOne));
            Assert.False(fromAnother.Exist(m.C1C1OneToOne));
            Assert.False(fromAnother.Exist(m.C1C1OneToOne));
            Assert.False(toAnother.Exist(m.C1WhereC1OneToOne));
            Assert.False(toAnother.Exist(m.C1WhereC1OneToOne));

            // Different New / Same To
            // Get
            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(fromAnother[m.C1C1OneToOne]);
            Assert.Null(fromAnother[m.C1C1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);

            from[m.C1C1OneToOne] = to;
            from[m.C1C1OneToOne] = to;

            Assert.Equal(to, from[m.C1C1OneToOne]);
            Assert.Equal(to, from[m.C1C1OneToOne]);
            Assert.Null(fromAnother[m.C1C1OneToOne]);
            Assert.Null(fromAnother[m.C1C1OneToOne]);
            Assert.Equal(from, to[m.C1WhereC1OneToOne]);
            Assert.Equal(from, to[m.C1WhereC1OneToOne]);

            fromAnother[m.C1C1OneToOne] = to;
            fromAnother[m.C1C1OneToOne] = to;

            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Equal(to, fromAnother[m.C1C1OneToOne]);
            Assert.Equal(to, fromAnother[m.C1C1OneToOne]);
            Assert.Equal(fromAnother, to[m.C1WhereC1OneToOne]);
            Assert.Equal(fromAnother, to[m.C1WhereC1OneToOne]);

            fromAnother[m.C1C1OneToOne] = null;
            fromAnother[m.C1C1OneToOne] = null;

            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(fromAnother[m.C1C1OneToOne]);
            Assert.Null(fromAnother[m.C1C1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);

            from[m.C1C1OneToOne] = to;

            Assert.Equal(to, from[m.C1C1OneToOne]);
            fromAnother[m.C1C1OneToOne] = to;

            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Equal(to, fromAnother[m.C1C1OneToOne]);
            fromAnother[m.C1C1OneToOne] = null;

            // Exist
            Assert.False(from.Exist(m.C1C1OneToOne));
            Assert.False(from.Exist(m.C1C1OneToOne));
            Assert.False(fromAnother.Exist(m.C1C1OneToOne));
            Assert.False(fromAnother.Exist(m.C1C1OneToOne));
            Assert.False(to.Exist(m.C1WhereC1OneToOne));
            Assert.False(to.Exist(m.C1WhereC1OneToOne));

            from[m.C1C1OneToOne] = to;
            from[m.C1C1OneToOne] = to;

            Assert.True(from.Exist(m.C1C1OneToOne));
            Assert.True(from.Exist(m.C1C1OneToOne));
            Assert.False(fromAnother.Exist(m.C1C1OneToOne));
            Assert.False(fromAnother.Exist(m.C1C1OneToOne));
            Assert.True(to.Exist(m.C1WhereC1OneToOne));
            Assert.True(to.Exist(m.C1WhereC1OneToOne));

            fromAnother[m.C1C1OneToOne] = to;
            fromAnother[m.C1C1OneToOne] = to;

            Assert.False(from.Exist(m.C1C1OneToOne));
            Assert.False(from.Exist(m.C1C1OneToOne));
            Assert.True(fromAnother.Exist(m.C1C1OneToOne));
            Assert.True(fromAnother.Exist(m.C1C1OneToOne));
            Assert.True(to.Exist(m.C1WhereC1OneToOne));
            Assert.True(to.Exist(m.C1WhereC1OneToOne));

            fromAnother[m.C1C1OneToOne] = null;
            fromAnother[m.C1C1OneToOne] = null;

            Assert.False(from.Exist(m.C1C1OneToOne));
            Assert.False(from.Exist(m.C1C1OneToOne));
            Assert.False(fromAnother.Exist(m.C1C1OneToOne));
            Assert.False(fromAnother.Exist(m.C1C1OneToOne));
            Assert.False(to.Exist(m.C1WhereC1OneToOne));
            Assert.False(to.Exist(m.C1WhereC1OneToOne));

            // Null
            // Set Null
            // Get
            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(from[m.C1C1OneToOne]);

            from[m.C1C1OneToOne] = null;
            from[m.C1C1OneToOne] = null;

            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(from[m.C1C1OneToOne]);

            from[m.C1C1OneToOne] = to;
            from[m.C1C1OneToOne] = to;

            Assert.Equal(to, from[m.C1C1OneToOne]);
            Assert.Equal(to, from[m.C1C1OneToOne]);

            from[m.C1C1OneToOne] = null;
            from[m.C1C1OneToOne] = null;

            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(from[m.C1C1OneToOne]);

            // Get
            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(from[m.C1C1OneToOne]);

            from[m.C1C1OneToOne] = null;
            from[m.C1C1OneToOne] = null;

            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(from[m.C1C1OneToOne]);

            from[m.C1C1OneToOne] = to;
            from[m.C1C1OneToOne] = to;

            Assert.Equal(to, from[m.C1C1OneToOne]);
            Assert.Equal(to, from[m.C1C1OneToOne]);

            from[m.C1C1OneToOne] = null;
            from[m.C1C1OneToOne] = null;

            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(from[m.C1C1OneToOne]);

            from = transaction.Build(adaptersMeta.C1);
            to = transaction.Build(adaptersMeta.C1);

            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);

            // 1-1
            from[m.C1C1OneToOne] = to;
            from[m.C1C1OneToOne] = to;

            Assert.Equal(to, from[m.C1C1OneToOne]);
            Assert.Equal(to, from[m.C1C1OneToOne]);
            Assert.Equal(from, to[m.C1WhereC1OneToOne]);
            Assert.Equal(from, to[m.C1WhereC1OneToOne]);

            // 0-0
            from[m.C1C1OneToOne] = null;
            from[m.C1C1OneToOne] = null;

            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(from[m.C1C1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);
            Assert.Null(to[m.C1WhereC1OneToOne]);

            // New - Middle - To
            from = transaction.Build(adaptersMeta.C1);
            var middle = transaction.Build(adaptersMeta.C1);
            to = transaction.Build(adaptersMeta.C1);

            from[m.C1C1OneToOne] = middle;
            middle[m.C1C1OneToOne] = to;
            from[m.C1C1OneToOne] = to;

            Assert.Null(middle[m.C1WhereC1OneToOne]);
            Assert.Null(middle[m.C1C1OneToOne]);
        }

        protected abstract IDatabase CreateDatabase();
    }
}
