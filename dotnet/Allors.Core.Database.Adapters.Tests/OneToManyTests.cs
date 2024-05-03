namespace Allors.Core.Database.Adapters.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Allors.Core.Database.Meta;
    using Xunit;

    public abstract class OneToManyTests
    {
        [Fact]
        public void ToDo()
        {
            Assert.True(true);
        }

        [Fact]
        public void C1_C1OneToManies()
        {
            var coreMeta = new CoreMeta();
            var adaptersMeta = new AdaptersMeta(coreMeta);

            var database = this.CreateDatabase();
            var transaction = database.CreateTransaction();

            var from = transaction.Build(adaptersMeta.C1);
            var fromAnother = transaction.Build(adaptersMeta.C1);

            var to1 = transaction.Build(adaptersMeta.C1);
            var to2 = transaction.Build(adaptersMeta.C1);
            var to3 = transaction.Build(adaptersMeta.C1);
            var to4 = transaction.Build(adaptersMeta.C1);

            IObject[] to1Array = [to1];
            IObject[] to2Array = [to2];
            IObject[] to12Array = [to1, to2];

            var m = adaptersMeta;

            // To 0-4-0
            // Get
            Assert.Empty(from[m.C1C1OneToManies]);
            Assert.Empty(from[m.C1C1OneToManies]);
            Assert.Null(to1[m.C1WhereC1C1one2many]);
            Assert.Null(to1[m.C1WhereC1C1one2many]);
            Assert.Null(to2[m.C1WhereC1C1one2many]);
            Assert.Null(to2[m.C1WhereC1C1one2many]);
            Assert.Null(to3[m.C1WhereC1C1one2many]);
            Assert.Null(to3[m.C1WhereC1C1one2many]);
            Assert.Null(to4[m.C1WhereC1C1one2many]);
            Assert.Null(to4[m.C1WhereC1C1one2many]);

            // 1-1
            from.Add(m.C1C1OneToManies, to1);
            from.Add(m.C1C1OneToManies, to1); // Add Twice

            Assert.Single(from[m.C1C1OneToManies]);
            Assert.Single(from[m.C1C1OneToManies]);
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Equal(from, to1[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to1[m.C1WhereC1C1one2many]);
            Assert.Null(to2[m.C1WhereC1C1one2many]);
            Assert.Null(to2[m.C1WhereC1C1one2many]);
            Assert.Null(to3[m.C1WhereC1C1one2many]);
            Assert.Null(to3[m.C1WhereC1C1one2many]);
            Assert.Null(to4[m.C1WhereC1C1one2many]);
            Assert.Null(to4[m.C1WhereC1C1one2many]);

            // 1-2
            from.Add(m.C1C1OneToManies, to2);

            Assert.Equal(2, from[m.C1C1OneToManies].Count());
            Assert.Equal(2, from[m.C1C1OneToManies].Count());
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Contains(to2, from[m.C1C1OneToManies]);
            Assert.Contains(to2, from[m.C1C1OneToManies]);
            Assert.Equal(from, to1[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to1[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to2[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to2[m.C1WhereC1C1one2many]);
            Assert.Null(to3[m.C1WhereC1C1one2many]);
            Assert.Null(to3[m.C1WhereC1C1one2many]);
            Assert.Null(to4[m.C1WhereC1C1one2many]);
            Assert.Null(to4[m.C1WhereC1C1one2many]);

            // 1-3
            from.Add(m.C1C1OneToManies, to3);

            Assert.Equal(3, from[m.C1C1OneToManies].Count());
            Assert.Equal(3, from[m.C1C1OneToManies].Count());
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Contains(to2, from[m.C1C1OneToManies]);
            Assert.Contains(to2, from[m.C1C1OneToManies]);
            Assert.Contains(to3, from[m.C1C1OneToManies]);
            Assert.Contains(to3, from[m.C1C1OneToManies]);
            Assert.Equal(from, to1[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to1[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to2[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to2[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to3[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to3[m.C1WhereC1C1one2many]);
            Assert.Null(to4[m.C1WhereC1C1one2many]);
            Assert.Null(to4[m.C1WhereC1C1one2many]);

            // 1-4
            from.Add(m.C1C1OneToManies, to4);

            Assert.Equal(4, from[m.C1C1OneToManies].Count());
            Assert.Equal(4, from[m.C1C1OneToManies].Count());
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Contains(to2, from[m.C1C1OneToManies]);
            Assert.Contains(to2, from[m.C1C1OneToManies]);
            Assert.Contains(to3, from[m.C1C1OneToManies]);
            Assert.Contains(to3, from[m.C1C1OneToManies]);
            Assert.Contains(to4, from[m.C1C1OneToManies]);
            Assert.Contains(to4, from[m.C1C1OneToManies]);
            Assert.Equal(from, to1[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to1[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to2[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to2[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to3[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to3[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to4[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to4[m.C1WhereC1C1one2many]);

            // 1-3
            from.Remove(m.C1C1OneToManies, to4);

            Assert.Equal(3, from[m.C1C1OneToManies].Count());
            Assert.Equal(3, from[m.C1C1OneToManies].Count());
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Contains(to2, from[m.C1C1OneToManies]);
            Assert.Contains(to2, from[m.C1C1OneToManies]);
            Assert.Contains(to3, from[m.C1C1OneToManies]);
            Assert.Contains(to3, from[m.C1C1OneToManies]);
            Assert.Equal(from, to1[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to1[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to2[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to2[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to3[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to3[m.C1WhereC1C1one2many]);
            Assert.Null(to4[m.C1WhereC1C1one2many]);
            Assert.Null(to4[m.C1WhereC1C1one2many]);

            // 1-2
            from.Remove(m.C1C1OneToManies, to3);

            Assert.Equal(2, from[m.C1C1OneToManies].Count());
            Assert.Equal(2, from[m.C1C1OneToManies].Count());
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Contains(to2, from[m.C1C1OneToManies]);
            Assert.Contains(to2, from[m.C1C1OneToManies]);
            Assert.Equal(from, to1[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to1[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to2[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to2[m.C1WhereC1C1one2many]);
            Assert.Null(to3[m.C1WhereC1C1one2many]);
            Assert.Null(to3[m.C1WhereC1C1one2many]);
            Assert.Null(to4[m.C1WhereC1C1one2many]);
            Assert.Null(to4[m.C1WhereC1C1one2many]);

            // 1-1
            from.Remove(m.C1C1OneToManies, to2);
            from.Remove(m.C1C1OneToManies, to2); // Delete Twice

            Assert.Single(from[m.C1C1OneToManies]);
            Assert.Single(from[m.C1C1OneToManies]);
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Equal(from, to1[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to1[m.C1WhereC1C1one2many]);
            Assert.Null(to2[m.C1WhereC1C1one2many]);
            Assert.Null(to2[m.C1WhereC1C1one2many]);
            Assert.Null(to3[m.C1WhereC1C1one2many]);
            Assert.Null(to3[m.C1WhereC1C1one2many]);
            Assert.Null(to4[m.C1WhereC1C1one2many]);
            Assert.Null(to4[m.C1WhereC1C1one2many]);

            // 0-0
            from.Remove(m.C1C1OneToManies, to1);
            from.Remove(m.C1C1OneToManies, to1);
            from.Add(m.C1C1OneToManies, to1);
            from[m.C1C1OneToManies] = [];
            from[m.C1C1OneToManies] = [];
            from.Add(m.C1C1OneToManies, to1);
            from[m.C1C1OneToManies] = [];
            from[m.C1C1OneToManies] = [];
            from.Add(m.C1C1OneToManies, to1);
            IObject[] emptyArray = [];
            from[m.C1C1OneToManies] = emptyArray;
            from[m.C1C1OneToManies] = emptyArray;

            Assert.Empty(from[m.C1C1OneToManies]);
            Assert.Empty(from[m.C1C1OneToManies]);
            Assert.Null(to1[m.C1WhereC1C1one2many]);
            Assert.Null(to1[m.C1WhereC1C1one2many]);
            Assert.Null(to2[m.C1WhereC1C1one2many]);
            Assert.Null(to2[m.C1WhereC1C1one2many]);
            Assert.Null(to3[m.C1WhereC1C1one2many]);
            Assert.Null(to3[m.C1WhereC1C1one2many]);
            Assert.Null(to4[m.C1WhereC1C1one2many]);
            Assert.Null(to4[m.C1WhereC1C1one2many]);

            // Exist
            Assert.False(from.Exist(m.C1C1OneToManies));
            Assert.False(from.Exist(m.C1C1OneToManies));
            Assert.False(to1.Exist(m.C1WhereC1C1one2many));
            Assert.False(to1.Exist(m.C1WhereC1C1one2many));
            Assert.False(to2.Exist(m.C1WhereC1C1one2many));
            Assert.False(to2.Exist(m.C1WhereC1C1one2many));
            Assert.False(to3.Exist(m.C1WhereC1C1one2many));
            Assert.False(to3.Exist(m.C1WhereC1C1one2many));
            Assert.False(to4.Exist(m.C1WhereC1C1one2many));
            Assert.False(to4.Exist(m.C1WhereC1C1one2many));

            // 1-1
            from.Add(m.C1C1OneToManies, to1);
            from.Add(m.C1C1OneToManies, to1); // Add Twice

            Assert.True(from.Exist(m.C1C1OneToManies));
            Assert.True(from.Exist(m.C1C1OneToManies));
            Assert.True(to1.Exist(m.C1WhereC1C1one2many));
            Assert.True(to1.Exist(m.C1WhereC1C1one2many));
            Assert.False(to2.Exist(m.C1WhereC1C1one2many));
            Assert.False(to2.Exist(m.C1WhereC1C1one2many));
            Assert.False(to3.Exist(m.C1WhereC1C1one2many));
            Assert.False(to3.Exist(m.C1WhereC1C1one2many));
            Assert.False(to4.Exist(m.C1WhereC1C1one2many));
            Assert.False(to4.Exist(m.C1WhereC1C1one2many));

            // 1-2
            from.Add(m.C1C1OneToManies, to2);

            Assert.True(from.Exist(m.C1C1OneToManies));
            Assert.True(from.Exist(m.C1C1OneToManies));
            Assert.True(to1.Exist(m.C1WhereC1C1one2many));
            Assert.True(to1.Exist(m.C1WhereC1C1one2many));
            Assert.True(to2.Exist(m.C1WhereC1C1one2many));
            Assert.True(to2.Exist(m.C1WhereC1C1one2many));
            Assert.False(to3.Exist(m.C1WhereC1C1one2many));
            Assert.False(to3.Exist(m.C1WhereC1C1one2many));
            Assert.False(to4.Exist(m.C1WhereC1C1one2many));
            Assert.False(to4.Exist(m.C1WhereC1C1one2many));

            // 1-3
            from.Add(m.C1C1OneToManies, to3);

            Assert.True(from.Exist(m.C1C1OneToManies));
            Assert.True(from.Exist(m.C1C1OneToManies));
            Assert.True(to1.Exist(m.C1WhereC1C1one2many));
            Assert.True(to1.Exist(m.C1WhereC1C1one2many));
            Assert.True(to2.Exist(m.C1WhereC1C1one2many));
            Assert.True(to2.Exist(m.C1WhereC1C1one2many));
            Assert.True(to3.Exist(m.C1WhereC1C1one2many));
            Assert.True(to3.Exist(m.C1WhereC1C1one2many));
            Assert.False(to4.Exist(m.C1WhereC1C1one2many));
            Assert.False(to4.Exist(m.C1WhereC1C1one2many));

            // 1-4
            from.Add(m.C1C1OneToManies, to4);

            Assert.True(from.Exist(m.C1C1OneToManies));
            Assert.True(from.Exist(m.C1C1OneToManies));
            Assert.True(to1.Exist(m.C1WhereC1C1one2many));
            Assert.True(to1.Exist(m.C1WhereC1C1one2many));
            Assert.True(to2.Exist(m.C1WhereC1C1one2many));
            Assert.True(to2.Exist(m.C1WhereC1C1one2many));
            Assert.True(to3.Exist(m.C1WhereC1C1one2many));
            Assert.True(to3.Exist(m.C1WhereC1C1one2many));
            Assert.True(to4.Exist(m.C1WhereC1C1one2many));
            Assert.True(to4.Exist(m.C1WhereC1C1one2many));

            // 1-3
            from.Remove(m.C1C1OneToManies, to4);

            Assert.True(from.Exist(m.C1C1OneToManies));
            Assert.True(from.Exist(m.C1C1OneToManies));
            Assert.True(to1.Exist(m.C1WhereC1C1one2many));
            Assert.True(to1.Exist(m.C1WhereC1C1one2many));
            Assert.True(to2.Exist(m.C1WhereC1C1one2many));
            Assert.True(to2.Exist(m.C1WhereC1C1one2many));
            Assert.True(to3.Exist(m.C1WhereC1C1one2many));
            Assert.True(to3.Exist(m.C1WhereC1C1one2many));
            Assert.False(to4.Exist(m.C1WhereC1C1one2many));
            Assert.False(to4.Exist(m.C1WhereC1C1one2many));

            // 1-2
            from.Remove(m.C1C1OneToManies, to3);

            Assert.True(from.Exist(m.C1C1OneToManies));
            Assert.True(from.Exist(m.C1C1OneToManies));
            Assert.True(to1.Exist(m.C1WhereC1C1one2many));
            Assert.True(to1.Exist(m.C1WhereC1C1one2many));
            Assert.True(to2.Exist(m.C1WhereC1C1one2many));
            Assert.True(to2.Exist(m.C1WhereC1C1one2many));
            Assert.False(to3.Exist(m.C1WhereC1C1one2many));
            Assert.False(to3.Exist(m.C1WhereC1C1one2many));
            Assert.False(to4.Exist(m.C1WhereC1C1one2many));
            Assert.False(to4.Exist(m.C1WhereC1C1one2many));

            // 1-1
            from.Remove(m.C1C1OneToManies, to2);
            from.Remove(m.C1C1OneToManies, to2); // Delete Twice

            Assert.True(from.Exist(m.C1C1OneToManies));
            Assert.True(from.Exist(m.C1C1OneToManies));
            Assert.True(to1.Exist(m.C1WhereC1C1one2many));
            Assert.True(to1.Exist(m.C1WhereC1C1one2many));
            Assert.False(to2.Exist(m.C1WhereC1C1one2many));
            Assert.False(to2.Exist(m.C1WhereC1C1one2many));
            Assert.False(to3.Exist(m.C1WhereC1C1one2many));
            Assert.False(to3.Exist(m.C1WhereC1C1one2many));
            Assert.False(to4.Exist(m.C1WhereC1C1one2many));
            Assert.False(to4.Exist(m.C1WhereC1C1one2many));

            // 0-0
            from.Remove(m.C1C1OneToManies, to1);
            from.Remove(m.C1C1OneToManies, to1);
            from.Add(m.C1C1OneToManies, to1);
            from[m.C1C1OneToManies] = [];
            from[m.C1C1OneToManies] = [];
            from.Add(m.C1C1OneToManies, to1);
            from[m.C1C1OneToManies] = [];
            from[m.C1C1OneToManies] = [];
            from.Add(m.C1C1OneToManies, to1);
            from[m.C1C1OneToManies] = emptyArray;
            from[m.C1C1OneToManies] = emptyArray;

            Assert.False(from.Exist(m.C1C1OneToManies));
            Assert.False(from.Exist(m.C1C1OneToManies));
            Assert.False(to1.Exist(m.C1WhereC1C1one2many));
            Assert.False(to1.Exist(m.C1WhereC1C1one2many));
            Assert.False(to2.Exist(m.C1WhereC1C1one2many));
            Assert.False(to2.Exist(m.C1WhereC1C1one2many));
            Assert.False(to3.Exist(m.C1WhereC1C1one2many));
            Assert.False(to3.Exist(m.C1WhereC1C1one2many));
            Assert.False(to4.Exist(m.C1WhereC1C1one2many));
            Assert.False(to4.Exist(m.C1WhereC1C1one2many));

            // Multiplicity
            from.Add(m.C1C1OneToManies, to1);
            from.Add(m.C1C1OneToManies, to1);

            Assert.Single(from[m.C1C1OneToManies]);
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Equal(from, to1[m.C1WhereC1C1one2many]);

            // TODO: Replicate to other variants
            fromAnother.Remove(m.C1C1OneToManies, to1);

            Assert.Single(from[m.C1C1OneToManies]);
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Equal(from, to1[m.C1WhereC1C1one2many]);

            from[m.C1C1OneToManies] = [];

            from[m.C1C1OneToManies] = to1Array;
            from[m.C1C1OneToManies] = to1Array;

            Assert.Single(from[m.C1C1OneToManies]);
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Equal(from, to1[m.C1WhereC1C1one2many]);

            from[m.C1C1OneToManies] = [];

            from.Add(m.C1C1OneToManies, to1);
            from.Add(m.C1C1OneToManies, to2);

            Assert.Equal(2, from[m.C1C1OneToManies].Count());
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Contains(to2, from[m.C1C1OneToManies]);
            Assert.Equal(from, to1[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to2[m.C1WhereC1C1one2many]);

            from[m.C1C1OneToManies] = [];

            from[m.C1C1OneToManies] = to1Array;
            from[m.C1C1OneToManies] = to2Array;

            Assert.Single(from[m.C1C1OneToManies]);
            Assert.Contains(to2, from[m.C1C1OneToManies]);
            Assert.Null(to1[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to2[m.C1WhereC1C1one2many]);

            from[m.C1C1OneToManies] = to12Array;

            Assert.Equal(2, from[m.C1C1OneToManies].Count());
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Contains(to2, from[m.C1C1OneToManies]);
            Assert.Equal(from, to1[m.C1WhereC1C1one2many]);
            Assert.Equal(from, to2[m.C1WhereC1C1one2many]);

            from[m.C1C1OneToManies] = [];

            from.Add(m.C1C1OneToManies, to1);
            fromAnother.Add(m.C1C1OneToManies, to1);

            Assert.Empty(from[m.C1C1OneToManies]);
            Assert.Single(fromAnother[m.C1C1OneToManies]);
            Assert.Contains(to1, fromAnother[m.C1C1OneToManies]);
            Assert.Equal(fromAnother, to1[m.C1WhereC1C1one2many]);

            fromAnother[m.C1C1OneToManies] = [];

            // Replicate to others
            from.Add(m.C1C1OneToManies, to1);
            from.Add(m.C1C1OneToManies, to2);
            fromAnother.Add(m.C1C1OneToManies, to1);

            Assert.Single(from[m.C1C1OneToManies]);
            Assert.Single(fromAnother[m.C1C1OneToManies]);
            Assert.Contains(to2, from[m.C1C1OneToManies]);
            Assert.Contains(to1, fromAnother[m.C1C1OneToManies]);
            Assert.Equal(from, to2[m.C1WhereC1C1one2many]);
            Assert.Equal(fromAnother, to1[m.C1WhereC1C1one2many]);

            fromAnother[m.C1C1OneToManies] = [];

            from[m.C1C1OneToManies] = to1Array;

            Assert.Single(from[m.C1C1OneToManies]); // TODO: Add this to others
            fromAnother[m.C1C1OneToManies] = to1Array;

            Assert.Empty(from[m.C1C1OneToManies]);
            Assert.Single(fromAnother[m.C1C1OneToManies]);
            Assert.Contains(to1, fromAnother[m.C1C1OneToManies]);
            Assert.Equal(fromAnother, to1[m.C1WhereC1C1one2many]);

            fromAnother[m.C1C1OneToManies] = [];

            from.Add(m.C1C1OneToManies, to1);
            fromAnother.Add(m.C1C1OneToManies, to2);

            Assert.Single(from[m.C1C1OneToManies]);
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Equal(from, to1[m.C1WhereC1C1one2many]);
            Assert.Single(fromAnother[m.C1C1OneToManies]);
            Assert.Contains(to2, fromAnother[m.C1C1OneToManies]);
            Assert.Equal(fromAnother, to2[m.C1WhereC1C1one2many]);

            from[m.C1C1OneToManies] = [];
            fromAnother[m.C1C1OneToManies] = [];

            from[m.C1C1OneToManies] = to1Array;
            fromAnother[m.C1C1OneToManies] = to2Array;

            Assert.Single(from[m.C1C1OneToManies]);
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Equal(from, to1[m.C1WhereC1C1one2many]);
            Assert.Single(fromAnother[m.C1C1OneToManies]);
            Assert.Contains(to2, fromAnother[m.C1C1OneToManies]);
            Assert.Equal(fromAnother, to2[m.C1WhereC1C1one2many]);

            from[m.C1C1OneToManies] = [];
            fromAnother[m.C1C1OneToManies] = [];

            // Null & Empty Array
            // Set Empty Array
            from[m.C1C1OneToManies] = Array.Empty<IObject>();

            Assert.Empty(from[m.C1C1OneToManies]);

            // Set Array with only a null
            from[m.C1C1OneToManies] = new IObject[1];

            Assert.Empty(from[m.C1C1OneToManies]);
            from[m.C1C1OneToManies] = new IObject[1];

            Assert.Empty(from[m.C1C1OneToManies]);

            // Set Array with a null in the front
            var nullInFront = new IObject[3];
            nullInFront[1] = to1;
            nullInFront[2] = to2;
            from[m.C1C1OneToManies] = nullInFront;

            Assert.Equal(2, from[m.C1C1OneToManies].Count());
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Contains(to2, from[m.C1C1OneToManies]);
            from[m.C1C1OneToManies] = nullInFront;

            Assert.Equal(2, from[m.C1C1OneToManies].Count());
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Contains(to2, from[m.C1C1OneToManies]);

            // Set Array with a null in the middle
            var nullInTheMiddle = new IObject[3];
            nullInTheMiddle[0] = to1;
            nullInTheMiddle[2] = to2;
            from[m.C1C1OneToManies] = nullInTheMiddle;

            Assert.Equal(2, from[m.C1C1OneToManies].Count());
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Contains(to2, from[m.C1C1OneToManies]);
            from[m.C1C1OneToManies] = nullInTheMiddle;

            Assert.Equal(2, from[m.C1C1OneToManies].Count());
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Contains(to2, from[m.C1C1OneToManies]);

            // Set Array with a null in the back
            IObject[] nullInBack = new IObject[3];
            nullInBack[1] = to1;
            nullInBack[2] = to2;
            from[m.C1C1OneToManies] = nullInBack;

            Assert.Equal(2, from[m.C1C1OneToManies].Count());
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Contains(to2, from[m.C1C1OneToManies]);
            from[m.C1C1OneToManies] = nullInBack;

            Assert.Equal(2, from[m.C1C1OneToManies].Count());
            Assert.Contains(to1, from[m.C1C1OneToManies]);
            Assert.Contains(to2, from[m.C1C1OneToManies]);

            // Remove and Add
            from = transaction.Build(adaptersMeta.C1);
            var to = transaction.Build(adaptersMeta.C1);

            from.Add(m.C1C1OneToManies, to);

            transaction.Commit();

            from.Remove(m.C1C1OneToManies, to);
            from.Add(m.C1C1OneToManies, to);

            transaction.Commit();

            // Add and Remove
            from = transaction.Build(adaptersMeta.C1);
            to1 = transaction.Build(adaptersMeta.C1);
            to2 = transaction.Build(adaptersMeta.C1);

            from.Add(m.C1C1OneToManies, to1);

            transaction.Commit();

            from.Add(m.C1C1OneToManies, to2);
            from.Remove(m.C1C1OneToManies, to2);

            transaction.Commit();

            // New - Middle - To
            from = transaction.Build(adaptersMeta.C1);
            var middle = transaction.Build(adaptersMeta.C1);
            to = transaction.Build(adaptersMeta.C1);

            from.Add(m.C1C1OneToManies, middle);
            middle.Add(m.C1C1OneToManies, to);
            from.Add(m.C1C1OneToManies, to);

            Assert.True(middle.Exist(m.C1WhereC1C1one2many));
            Assert.False(middle.Exist(m.C1C1OneToManies));

            // Very Big Array
            var bigArray = transaction.Build(m.C1, Settings.LargeArraySize).ToArray();
            from[m.C1C1OneToManies] = bigArray;
            var getBigArray = from[m.C1C1OneToManies].ToArray();

            Assert.Equal(Settings.LargeArraySize, getBigArray.Length);

            var objects = new HashSet<IObject>(getBigArray);
            foreach (var bigArrayObject in bigArray)
            {
                Assert.Contains(bigArrayObject, objects);
            }

            from = transaction.Build(m.C1);

            from[m.C1C1OneToManies] = to1Array;
            from[m.C1C1OneToManies] = to1Array;

            Assert.Single(from[m.C1C1OneToManies]);
            Assert.Contains(to1Array[0], from[m.C1C1OneToManies]);
            Assert.Equal(from, to1Array[0][m.C1WhereC1C1one2many]);

            // Extent.ToArray()
            from = transaction.Build(adaptersMeta.C1);
            to1 = transaction.Build(adaptersMeta.C1);

            from.Add(m.C1C1OneToManies, to1);

            Assert.Single(from[m.C1C1OneToManies]);
            Assert.Equal(to1, from[m.C1C1OneToManies].First());

            // Extent<T>.ToArray()
            from = transaction.Build(adaptersMeta.C1);
            to1 = transaction.Build(adaptersMeta.C1);

            from.Add(m.C1C1OneToManies, to1);

            Assert.Single(from[m.C1C1OneToManies].ToArray());
            Assert.Equal(to1, from[m.C1C1OneToManies].ElementAt(0));
        }

        protected abstract IDatabase CreateDatabase();
    }
}
