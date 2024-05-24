namespace Allors.Core.Database.Engines.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Allors.Core.Database.Meta.Domain;
    using FluentAssertions;
    using MoreLinq;
    using Xunit;

    public abstract class OneToManyTests : Tests
    {
        private readonly Func<(
            OneToManyAssociationType Association,
            OneToManyRoleType Role,
            Func<ITransaction, IObject>[] Builders,
            Func<ITransaction, IObject> FromBuilder,
            Func<ITransaction, IObject> FromAnotherBuilder,
            Func<ITransaction, IObject> ToBuilder,
            Func<ITransaction, IObject> ToAnotherBuilder,
            Func<ITransaction, IObject> To3Builder,
            Func<ITransaction, IObject> To4Builder)>[] fixtures;

        private readonly (string, Action<ITransaction>)[] preActs;

        protected OneToManyTests()
        {
            this.fixtures =
            [
                () =>
                {
                    // C1 <-> C1
                    Debugger.Log(0, null, $"C1 <-> C1\n");
                    var association = this.Meta.C1WhereC1OneToMany;
                    var role = this.Meta.C1C1OneToMany;

                    return (association, role, [C1Builder], C1Builder, C1Builder, C1Builder, C1Builder, C1Builder, C1Builder);

                    IObject C1Builder(ITransaction transaction) => transaction.Build(this.Meta.C1);
                },
            ];

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
        public void C1_C1OneToManies()
        {
            var database = this.CreateDatabase();
            var transaction = database.CreateTransaction();

            var m = this.Meta;

            var from = transaction.Build(m.C1);
            var fromAnother = transaction.Build(m.C1);

            var to1 = transaction.Build(m.C1);
            var to2 = transaction.Build(m.C1);
            var to3 = transaction.Build(m.C1);
            var to4 = transaction.Build(m.C1);

            IObject[] to1Array = [to1];
            IObject[] to2Array = [to2];
            IObject[] to12Array = [to1, to2];

            // To 0-4-0
            // Get
            Assert.Empty(from[m.C1C1OneToMany]);
            Assert.Empty(from[m.C1C1OneToMany]);
            Assert.Null(to1[m.C1WhereC1OneToMany]);
            Assert.Null(to1[m.C1WhereC1OneToMany]);
            Assert.Null(to2[m.C1WhereC1OneToMany]);
            Assert.Null(to2[m.C1WhereC1OneToMany]);
            Assert.Null(to3[m.C1WhereC1OneToMany]);
            Assert.Null(to3[m.C1WhereC1OneToMany]);
            Assert.Null(to4[m.C1WhereC1OneToMany]);
            Assert.Null(to4[m.C1WhereC1OneToMany]);

            // 1-1
            from.Add(m.C1C1OneToMany, to1);
            from.Add(m.C1C1OneToMany, to1); // Add Twice

            Assert.Single(from[m.C1C1OneToMany]);
            Assert.Single(from[m.C1C1OneToMany]);
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Equal(from, to1[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to1[m.C1WhereC1OneToMany]);
            Assert.Null(to2[m.C1WhereC1OneToMany]);
            Assert.Null(to2[m.C1WhereC1OneToMany]);
            Assert.Null(to3[m.C1WhereC1OneToMany]);
            Assert.Null(to3[m.C1WhereC1OneToMany]);
            Assert.Null(to4[m.C1WhereC1OneToMany]);
            Assert.Null(to4[m.C1WhereC1OneToMany]);

            // 1-2
            from.Add(m.C1C1OneToMany, to2);

            Assert.Equal(2, from[m.C1C1OneToMany].Count());
            Assert.Equal(2, from[m.C1C1OneToMany].Count());
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Contains(to2, from[m.C1C1OneToMany]);
            Assert.Contains(to2, from[m.C1C1OneToMany]);
            Assert.Equal(from, to1[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to1[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to2[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to2[m.C1WhereC1OneToMany]);
            Assert.Null(to3[m.C1WhereC1OneToMany]);
            Assert.Null(to3[m.C1WhereC1OneToMany]);
            Assert.Null(to4[m.C1WhereC1OneToMany]);
            Assert.Null(to4[m.C1WhereC1OneToMany]);

            // 1-3
            from.Add(m.C1C1OneToMany, to3);

            Assert.Equal(3, from[m.C1C1OneToMany].Count());
            Assert.Equal(3, from[m.C1C1OneToMany].Count());
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Contains(to2, from[m.C1C1OneToMany]);
            Assert.Contains(to2, from[m.C1C1OneToMany]);
            Assert.Contains(to3, from[m.C1C1OneToMany]);
            Assert.Contains(to3, from[m.C1C1OneToMany]);
            Assert.Equal(from, to1[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to1[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to2[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to2[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to3[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to3[m.C1WhereC1OneToMany]);
            Assert.Null(to4[m.C1WhereC1OneToMany]);
            Assert.Null(to4[m.C1WhereC1OneToMany]);

            // 1-4
            from.Add(m.C1C1OneToMany, to4);

            Assert.Equal(4, from[m.C1C1OneToMany].Count());
            Assert.Equal(4, from[m.C1C1OneToMany].Count());
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Contains(to2, from[m.C1C1OneToMany]);
            Assert.Contains(to2, from[m.C1C1OneToMany]);
            Assert.Contains(to3, from[m.C1C1OneToMany]);
            Assert.Contains(to3, from[m.C1C1OneToMany]);
            Assert.Contains(to4, from[m.C1C1OneToMany]);
            Assert.Contains(to4, from[m.C1C1OneToMany]);
            Assert.Equal(from, to1[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to1[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to2[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to2[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to3[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to3[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to4[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to4[m.C1WhereC1OneToMany]);

            // 1-3
            from.Remove(m.C1C1OneToMany, to4);

            Assert.Equal(3, from[m.C1C1OneToMany].Count());
            Assert.Equal(3, from[m.C1C1OneToMany].Count());
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Contains(to2, from[m.C1C1OneToMany]);
            Assert.Contains(to2, from[m.C1C1OneToMany]);
            Assert.Contains(to3, from[m.C1C1OneToMany]);
            Assert.Contains(to3, from[m.C1C1OneToMany]);
            Assert.Equal(from, to1[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to1[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to2[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to2[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to3[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to3[m.C1WhereC1OneToMany]);
            Assert.Null(to4[m.C1WhereC1OneToMany]);
            Assert.Null(to4[m.C1WhereC1OneToMany]);

            // 1-2
            from.Remove(m.C1C1OneToMany, to3);

            Assert.Equal(2, from[m.C1C1OneToMany].Count());
            Assert.Equal(2, from[m.C1C1OneToMany].Count());
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Contains(to2, from[m.C1C1OneToMany]);
            Assert.Contains(to2, from[m.C1C1OneToMany]);
            Assert.Equal(from, to1[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to1[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to2[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to2[m.C1WhereC1OneToMany]);
            Assert.Null(to3[m.C1WhereC1OneToMany]);
            Assert.Null(to3[m.C1WhereC1OneToMany]);
            Assert.Null(to4[m.C1WhereC1OneToMany]);
            Assert.Null(to4[m.C1WhereC1OneToMany]);

            // 1-1
            from.Remove(m.C1C1OneToMany, to2);
            from.Remove(m.C1C1OneToMany, to2); // Delete Twice

            Assert.Single(from[m.C1C1OneToMany]);
            Assert.Single(from[m.C1C1OneToMany]);
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Equal(from, to1[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to1[m.C1WhereC1OneToMany]);
            Assert.Null(to2[m.C1WhereC1OneToMany]);
            Assert.Null(to2[m.C1WhereC1OneToMany]);
            Assert.Null(to3[m.C1WhereC1OneToMany]);
            Assert.Null(to3[m.C1WhereC1OneToMany]);
            Assert.Null(to4[m.C1WhereC1OneToMany]);
            Assert.Null(to4[m.C1WhereC1OneToMany]);

            // 0-0
            from.Remove(m.C1C1OneToMany, to1);
            from.Remove(m.C1C1OneToMany, to1);
            from.Add(m.C1C1OneToMany, to1);
            from[m.C1C1OneToMany] = [];
            from[m.C1C1OneToMany] = [];
            from.Add(m.C1C1OneToMany, to1);
            from[m.C1C1OneToMany] = [];
            from[m.C1C1OneToMany] = [];
            from.Add(m.C1C1OneToMany, to1);
            IObject[] emptyArray = [];
            from[m.C1C1OneToMany] = emptyArray;
            from[m.C1C1OneToMany] = emptyArray;

            Assert.Empty(from[m.C1C1OneToMany]);
            Assert.Empty(from[m.C1C1OneToMany]);
            Assert.Null(to1[m.C1WhereC1OneToMany]);
            Assert.Null(to1[m.C1WhereC1OneToMany]);
            Assert.Null(to2[m.C1WhereC1OneToMany]);
            Assert.Null(to2[m.C1WhereC1OneToMany]);
            Assert.Null(to3[m.C1WhereC1OneToMany]);
            Assert.Null(to3[m.C1WhereC1OneToMany]);
            Assert.Null(to4[m.C1WhereC1OneToMany]);
            Assert.Null(to4[m.C1WhereC1OneToMany]);

            // Exist
            Assert.False(from.Exist(m.C1C1OneToMany));
            Assert.False(from.Exist(m.C1C1OneToMany));
            Assert.False(to1.Exist(m.C1WhereC1OneToMany));
            Assert.False(to1.Exist(m.C1WhereC1OneToMany));
            Assert.False(to2.Exist(m.C1WhereC1OneToMany));
            Assert.False(to2.Exist(m.C1WhereC1OneToMany));
            Assert.False(to3.Exist(m.C1WhereC1OneToMany));
            Assert.False(to3.Exist(m.C1WhereC1OneToMany));
            Assert.False(to4.Exist(m.C1WhereC1OneToMany));
            Assert.False(to4.Exist(m.C1WhereC1OneToMany));

            // 1-1
            from.Add(m.C1C1OneToMany, to1);
            from.Add(m.C1C1OneToMany, to1); // Add Twice

            Assert.True(from.Exist(m.C1C1OneToMany));
            Assert.True(from.Exist(m.C1C1OneToMany));
            Assert.True(to1.Exist(m.C1WhereC1OneToMany));
            Assert.True(to1.Exist(m.C1WhereC1OneToMany));
            Assert.False(to2.Exist(m.C1WhereC1OneToMany));
            Assert.False(to2.Exist(m.C1WhereC1OneToMany));
            Assert.False(to3.Exist(m.C1WhereC1OneToMany));
            Assert.False(to3.Exist(m.C1WhereC1OneToMany));
            Assert.False(to4.Exist(m.C1WhereC1OneToMany));
            Assert.False(to4.Exist(m.C1WhereC1OneToMany));

            // 1-2
            from.Add(m.C1C1OneToMany, to2);

            Assert.True(from.Exist(m.C1C1OneToMany));
            Assert.True(from.Exist(m.C1C1OneToMany));
            Assert.True(to1.Exist(m.C1WhereC1OneToMany));
            Assert.True(to1.Exist(m.C1WhereC1OneToMany));
            Assert.True(to2.Exist(m.C1WhereC1OneToMany));
            Assert.True(to2.Exist(m.C1WhereC1OneToMany));
            Assert.False(to3.Exist(m.C1WhereC1OneToMany));
            Assert.False(to3.Exist(m.C1WhereC1OneToMany));
            Assert.False(to4.Exist(m.C1WhereC1OneToMany));
            Assert.False(to4.Exist(m.C1WhereC1OneToMany));

            // 1-3
            from.Add(m.C1C1OneToMany, to3);

            Assert.True(from.Exist(m.C1C1OneToMany));
            Assert.True(from.Exist(m.C1C1OneToMany));
            Assert.True(to1.Exist(m.C1WhereC1OneToMany));
            Assert.True(to1.Exist(m.C1WhereC1OneToMany));
            Assert.True(to2.Exist(m.C1WhereC1OneToMany));
            Assert.True(to2.Exist(m.C1WhereC1OneToMany));
            Assert.True(to3.Exist(m.C1WhereC1OneToMany));
            Assert.True(to3.Exist(m.C1WhereC1OneToMany));
            Assert.False(to4.Exist(m.C1WhereC1OneToMany));
            Assert.False(to4.Exist(m.C1WhereC1OneToMany));

            // 1-4
            from.Add(m.C1C1OneToMany, to4);

            Assert.True(from.Exist(m.C1C1OneToMany));
            Assert.True(from.Exist(m.C1C1OneToMany));
            Assert.True(to1.Exist(m.C1WhereC1OneToMany));
            Assert.True(to1.Exist(m.C1WhereC1OneToMany));
            Assert.True(to2.Exist(m.C1WhereC1OneToMany));
            Assert.True(to2.Exist(m.C1WhereC1OneToMany));
            Assert.True(to3.Exist(m.C1WhereC1OneToMany));
            Assert.True(to3.Exist(m.C1WhereC1OneToMany));
            Assert.True(to4.Exist(m.C1WhereC1OneToMany));
            Assert.True(to4.Exist(m.C1WhereC1OneToMany));

            // 1-3
            from.Remove(m.C1C1OneToMany, to4);

            Assert.True(from.Exist(m.C1C1OneToMany));
            Assert.True(from.Exist(m.C1C1OneToMany));
            Assert.True(to1.Exist(m.C1WhereC1OneToMany));
            Assert.True(to1.Exist(m.C1WhereC1OneToMany));
            Assert.True(to2.Exist(m.C1WhereC1OneToMany));
            Assert.True(to2.Exist(m.C1WhereC1OneToMany));
            Assert.True(to3.Exist(m.C1WhereC1OneToMany));
            Assert.True(to3.Exist(m.C1WhereC1OneToMany));
            Assert.False(to4.Exist(m.C1WhereC1OneToMany));
            Assert.False(to4.Exist(m.C1WhereC1OneToMany));

            // 1-2
            from.Remove(m.C1C1OneToMany, to3);

            Assert.True(from.Exist(m.C1C1OneToMany));
            Assert.True(from.Exist(m.C1C1OneToMany));
            Assert.True(to1.Exist(m.C1WhereC1OneToMany));
            Assert.True(to1.Exist(m.C1WhereC1OneToMany));
            Assert.True(to2.Exist(m.C1WhereC1OneToMany));
            Assert.True(to2.Exist(m.C1WhereC1OneToMany));
            Assert.False(to3.Exist(m.C1WhereC1OneToMany));
            Assert.False(to3.Exist(m.C1WhereC1OneToMany));
            Assert.False(to4.Exist(m.C1WhereC1OneToMany));
            Assert.False(to4.Exist(m.C1WhereC1OneToMany));

            // 1-1
            from.Remove(m.C1C1OneToMany, to2);
            from.Remove(m.C1C1OneToMany, to2); // Delete Twice

            Assert.True(from.Exist(m.C1C1OneToMany));
            Assert.True(from.Exist(m.C1C1OneToMany));
            Assert.True(to1.Exist(m.C1WhereC1OneToMany));
            Assert.True(to1.Exist(m.C1WhereC1OneToMany));
            Assert.False(to2.Exist(m.C1WhereC1OneToMany));
            Assert.False(to2.Exist(m.C1WhereC1OneToMany));
            Assert.False(to3.Exist(m.C1WhereC1OneToMany));
            Assert.False(to3.Exist(m.C1WhereC1OneToMany));
            Assert.False(to4.Exist(m.C1WhereC1OneToMany));
            Assert.False(to4.Exist(m.C1WhereC1OneToMany));

            // 0-0
            from.Remove(m.C1C1OneToMany, to1);
            from.Remove(m.C1C1OneToMany, to1);
            from.Add(m.C1C1OneToMany, to1);
            from[m.C1C1OneToMany] = [];
            from[m.C1C1OneToMany] = [];
            from.Add(m.C1C1OneToMany, to1);
            from[m.C1C1OneToMany] = [];
            from[m.C1C1OneToMany] = [];
            from.Add(m.C1C1OneToMany, to1);
            from[m.C1C1OneToMany] = emptyArray;
            from[m.C1C1OneToMany] = emptyArray;

            Assert.False(from.Exist(m.C1C1OneToMany));
            Assert.False(from.Exist(m.C1C1OneToMany));
            Assert.False(to1.Exist(m.C1WhereC1OneToMany));
            Assert.False(to1.Exist(m.C1WhereC1OneToMany));
            Assert.False(to2.Exist(m.C1WhereC1OneToMany));
            Assert.False(to2.Exist(m.C1WhereC1OneToMany));
            Assert.False(to3.Exist(m.C1WhereC1OneToMany));
            Assert.False(to3.Exist(m.C1WhereC1OneToMany));
            Assert.False(to4.Exist(m.C1WhereC1OneToMany));
            Assert.False(to4.Exist(m.C1WhereC1OneToMany));

            // Multiplicity
            from.Add(m.C1C1OneToMany, to1);
            from.Add(m.C1C1OneToMany, to1);

            Assert.Single(from[m.C1C1OneToMany]);
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Equal(from, to1[m.C1WhereC1OneToMany]);

            // TODO: Replicate to other variants
            fromAnother.Remove(m.C1C1OneToMany, to1);

            Assert.Single(from[m.C1C1OneToMany]);
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Equal(from, to1[m.C1WhereC1OneToMany]);

            from[m.C1C1OneToMany] = [];

            from[m.C1C1OneToMany] = to1Array;
            from[m.C1C1OneToMany] = to1Array;

            Assert.Single(from[m.C1C1OneToMany]);
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Equal(from, to1[m.C1WhereC1OneToMany]);

            from[m.C1C1OneToMany] = [];

            from.Add(m.C1C1OneToMany, to1);
            from.Add(m.C1C1OneToMany, to2);

            Assert.Equal(2, from[m.C1C1OneToMany].Count());
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Contains(to2, from[m.C1C1OneToMany]);
            Assert.Equal(from, to1[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to2[m.C1WhereC1OneToMany]);

            from[m.C1C1OneToMany] = [];

            from[m.C1C1OneToMany] = to1Array;
            from[m.C1C1OneToMany] = to2Array;

            Assert.Single(from[m.C1C1OneToMany]);
            Assert.Contains(to2, from[m.C1C1OneToMany]);
            Assert.Null(to1[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to2[m.C1WhereC1OneToMany]);

            from[m.C1C1OneToMany] = to12Array;

            Assert.Equal(2, from[m.C1C1OneToMany].Count());
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Contains(to2, from[m.C1C1OneToMany]);
            Assert.Equal(from, to1[m.C1WhereC1OneToMany]);
            Assert.Equal(from, to2[m.C1WhereC1OneToMany]);

            from[m.C1C1OneToMany] = [];

            from.Add(m.C1C1OneToMany, to1);
            fromAnother.Add(m.C1C1OneToMany, to1);

            Assert.Empty(from[m.C1C1OneToMany]);
            Assert.Single(fromAnother[m.C1C1OneToMany]);
            Assert.Contains(to1, fromAnother[m.C1C1OneToMany]);
            Assert.Equal(fromAnother, to1[m.C1WhereC1OneToMany]);

            fromAnother[m.C1C1OneToMany] = [];

            // Replicate to others
            from.Add(m.C1C1OneToMany, to1);
            from.Add(m.C1C1OneToMany, to2);
            fromAnother.Add(m.C1C1OneToMany, to1);

            Assert.Single(from[m.C1C1OneToMany]);
            Assert.Single(fromAnother[m.C1C1OneToMany]);
            Assert.Contains(to2, from[m.C1C1OneToMany]);
            Assert.Contains(to1, fromAnother[m.C1C1OneToMany]);
            Assert.Equal(from, to2[m.C1WhereC1OneToMany]);
            Assert.Equal(fromAnother, to1[m.C1WhereC1OneToMany]);

            fromAnother[m.C1C1OneToMany] = [];

            from[m.C1C1OneToMany] = to1Array;

            Assert.Single(from[m.C1C1OneToMany]); // TODO: Add this to others
            fromAnother[m.C1C1OneToMany] = to1Array;

            Assert.Empty(from[m.C1C1OneToMany]);
            Assert.Single(fromAnother[m.C1C1OneToMany]);
            Assert.Contains(to1, fromAnother[m.C1C1OneToMany]);
            Assert.Equal(fromAnother, to1[m.C1WhereC1OneToMany]);

            fromAnother[m.C1C1OneToMany] = [];

            from.Add(m.C1C1OneToMany, to1);
            fromAnother.Add(m.C1C1OneToMany, to2);

            Assert.Single(from[m.C1C1OneToMany]);
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Equal(from, to1[m.C1WhereC1OneToMany]);
            Assert.Single(fromAnother[m.C1C1OneToMany]);
            Assert.Contains(to2, fromAnother[m.C1C1OneToMany]);
            Assert.Equal(fromAnother, to2[m.C1WhereC1OneToMany]);

            from[m.C1C1OneToMany] = [];
            fromAnother[m.C1C1OneToMany] = [];

            from[m.C1C1OneToMany] = to1Array;
            fromAnother[m.C1C1OneToMany] = to2Array;

            Assert.Single(from[m.C1C1OneToMany]);
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Equal(from, to1[m.C1WhereC1OneToMany]);
            Assert.Single(fromAnother[m.C1C1OneToMany]);
            Assert.Contains(to2, fromAnother[m.C1C1OneToMany]);
            Assert.Equal(fromAnother, to2[m.C1WhereC1OneToMany]);

            from[m.C1C1OneToMany] = [];
            fromAnother[m.C1C1OneToMany] = [];

            // Null & Empty Array
            // Set Empty Array
            from[m.C1C1OneToMany] = Array.Empty<IObject>();

            Assert.Empty(from[m.C1C1OneToMany]);

            // Set Array with only a null
            from[m.C1C1OneToMany] = new IObject[1];

            Assert.Empty(from[m.C1C1OneToMany]);
            from[m.C1C1OneToMany] = new IObject[1];

            Assert.Empty(from[m.C1C1OneToMany]);

            // Set Array with a null in the front
            var nullInFront = new IObject[3];
            nullInFront[1] = to1;
            nullInFront[2] = to2;
            from[m.C1C1OneToMany] = nullInFront;

            Assert.Equal(2, from[m.C1C1OneToMany].Count());
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Contains(to2, from[m.C1C1OneToMany]);
            from[m.C1C1OneToMany] = nullInFront;

            Assert.Equal(2, from[m.C1C1OneToMany].Count());
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Contains(to2, from[m.C1C1OneToMany]);

            // Set Array with a null in the middle
            var nullInTheMiddle = new IObject[3];
            nullInTheMiddle[0] = to1;
            nullInTheMiddle[2] = to2;
            from[m.C1C1OneToMany] = nullInTheMiddle;

            Assert.Equal(2, from[m.C1C1OneToMany].Count());
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Contains(to2, from[m.C1C1OneToMany]);
            from[m.C1C1OneToMany] = nullInTheMiddle;

            Assert.Equal(2, from[m.C1C1OneToMany].Count());
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Contains(to2, from[m.C1C1OneToMany]);

            // Set Array with a null in the back
            IObject[] nullInBack = new IObject[3];
            nullInBack[1] = to1;
            nullInBack[2] = to2;
            from[m.C1C1OneToMany] = nullInBack;

            Assert.Equal(2, from[m.C1C1OneToMany].Count());
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Contains(to2, from[m.C1C1OneToMany]);
            from[m.C1C1OneToMany] = nullInBack;

            Assert.Equal(2, from[m.C1C1OneToMany].Count());
            Assert.Contains(to1, from[m.C1C1OneToMany]);
            Assert.Contains(to2, from[m.C1C1OneToMany]);

            // Remove and Add
            from = transaction.Build(m.C1);
            var to = transaction.Build(m.C1);

            from.Add(m.C1C1OneToMany, to);

            transaction.Commit();

            from.Remove(m.C1C1OneToMany, to);
            from.Add(m.C1C1OneToMany, to);

            transaction.Commit();

            // Add and Remove
            from = transaction.Build(m.C1);
            to1 = transaction.Build(m.C1);
            to2 = transaction.Build(m.C1);

            from.Add(m.C1C1OneToMany, to1);

            transaction.Commit();

            from.Add(m.C1C1OneToMany, to2);
            from.Remove(m.C1C1OneToMany, to2);

            transaction.Commit();

            // New - Middle - To
            from = transaction.Build(m.C1);
            var middle = transaction.Build(m.C1);
            to = transaction.Build(m.C1);

            from.Add(m.C1C1OneToMany, middle);
            middle.Add(m.C1C1OneToMany, to);
            from.Add(m.C1C1OneToMany, to);

            Assert.True(middle.Exist(m.C1WhereC1OneToMany));
            Assert.False(middle.Exist(m.C1C1OneToMany));

            // Very Big Array
            var bigArray = transaction.Build(m.C1, Settings.LargeArraySize).ToArray();
            from[m.C1C1OneToMany] = bigArray;
            var getBigArray = from[m.C1C1OneToMany].ToArray();

            Assert.Equal(Settings.LargeArraySize, getBigArray.Length);

            var objects = new HashSet<IObject>(getBigArray);
            foreach (var bigArrayObject in bigArray)
            {
                Assert.Contains(bigArrayObject, objects);
            }

            from = transaction.Build(m.C1);

            from[m.C1C1OneToMany] = to1Array;
            from[m.C1C1OneToMany] = to1Array;

            Assert.Single(from[m.C1C1OneToMany]);
            Assert.Contains(to1Array[0], from[m.C1C1OneToMany]);
            Assert.Equal(from, to1Array[0][m.C1WhereC1OneToMany]);

            // Extent.ToArray()
            from = transaction.Build(m.C1);
            to1 = transaction.Build(m.C1);

            from.Add(m.C1C1OneToMany, to1);

            Assert.Single(from[m.C1C1OneToMany]);
            Assert.Equal(to1, from[m.C1C1OneToMany].First());

            // Extent<T>.ToArray()
            from = transaction.Build(m.C1);
            to1 = transaction.Build(m.C1);

            from.Add(m.C1C1OneToMany, to1);

            Assert.Single(from[m.C1C1OneToMany].ToArray());
            Assert.Equal(to1, from[m.C1C1OneToMany].ElementAt(0));
        }

        [Fact]
        public void FromToInitial()
        {
            this.FromTo(
                () =>
                [
                ],
                () =>
                [
                    (association, _, _, to) => to[association].Should().BeNull(),
                    (_, role, from, _) => from[role].Should().BeEmpty()
                ]);
        }

        [Fact]
        public void FromToAdd()
        {
            this.FromTo(
                () =>
                [
                    (_, role, from, to) => from.Add(role, to)
                ],
                () =>
                [
                    (association, _, from, to) => to[association].Should().BeSameAs(from),
                    (_, role, from, to) => from[role].Should().BeEquivalentTo([to])
                ]);
        }

        [Fact]
        public void FromToSet()
        {
            this.FromTo(
                () =>
                [
                    (_, role, from, to) => from[role] = [to]
                ],
                () =>
                [
                    (association, _, from, to) => to[association].Should().BeSameAs(from),
                    (_, role, from, to) => from[role].Should().BeEquivalentTo([to])
                ]);
        }

        [Fact]
        public void FromToSetFromAnotherToSet()
        {
            this.FromFromAnotherTo(
                () =>
                [
                    (_, role, from, _, to) => from[role] = [to],
                    (_, role, _, fromAnother, to) => fromAnother[role] = [to]
                ],
                () =>
                [
                    (association, _, _, fromAnother, to) => to[association].Should().BeSameAs(fromAnother),
                    (_, role, from, _, _) => from[role].Should().BeEmpty(),
                    (_, role, _, fromAnother, to) => fromAnother[role].Should().BeEquivalentTo([to])
                ]);
        }

        [Fact]
        public void CheckRoleIsAssignable()
        {
            foreach (var (_, preact) in this.preActs)
            {
                var database = this.CreateDatabase();
                var transaction = database.CreateTransaction();

                var m = this.Meta;

                var c1a = transaction.Build(m.C1);
                var c1b = transaction.Build(m.C1);

                var c2a = transaction.Build(m.C2);

                // Illegal Role
                preact(transaction);

                c1a.Invoking(v => v.Add(m.C1C2OneToMany, c1b))
                    .Should().Throw<ArgumentException>();

                preact(transaction);

                c1a.Invoking(v => v.Remove(m.C1C2OneToMany, c1b))
                    .Should().Throw<ArgumentException>();

                preact(transaction);

                c1a.Invoking(v => v[m.C1C2OneToMany] = [c1b])
                    .Should().Throw<ArgumentException>();

                preact(transaction);

                c1a.Invoking(v => v.Add(m.C1I2OneToMany, c1b))
                    .Should().Throw<ArgumentException>();

                preact(transaction);

                c1a.Invoking(v => v.Remove(m.C1I2OneToMany, c1b))
                    .Should().Throw<ArgumentException>();

                preact(transaction);

                c1a.Invoking(v => v[m.C1I2OneToMany] = [c1b])
                    .Should().Throw<ArgumentException>();

                preact(transaction);

                c1a.Invoking(v => v.Add(m.C2C1OneToMany, c2a))
                    .Should().Throw<ArgumentException>();

                preact(transaction);

                c1a.Invoking(v => v.Remove(m.C2C1OneToMany, c2a))
                    .Should().Throw<ArgumentException>();

                preact(transaction);

                c1a.Invoking(v => v[m.C2C1OneToMany] = [c2a])
                    .Should().Throw<ArgumentException>();
            }
        }

        protected abstract IDatabase CreateDatabase();

        private void FromTo(
            Func<IEnumerable<Action<OneToManyAssociationType, OneToManyRoleType, IObject, IObject>>> acts,
            Func<IEnumerable<Action<OneToManyAssociationType, OneToManyRoleType, IObject, IObject>>> asserts)
        {
            var assertPermutations = asserts().Permutations().ToArray();

            foreach (var (preactName, preact) in this.preActs)
            {
                Debugger.Log(0, null, $"Preact {preactName}\n");
                foreach (var actRepeats in new[] { 1, 2 })
                {
                    Debugger.Log(0, null, $"Act Repeats {actRepeats}\n");
                    foreach (var assertPermutation in assertPermutations)
                    {
                        foreach (var assertRepeats in new[] { 1, 2 })
                        {
                            Debugger.Log(0, null, $"Assert Repeats {assertRepeats}\n");
                            foreach (var fixture in this.fixtures)
                            {
                                var (association, role, _, fromBuilder, _, toBuilder, _, _, _) = fixture();

                                var database = this.CreateDatabase();
                                var transaction = database.CreateTransaction();

                                var from = fromBuilder(transaction);
                                var to = toBuilder(transaction);

                                foreach (var act in acts())
                                {
                                    for (var actRepeat = 0; actRepeat < actRepeats; actRepeat++)
                                    {
                                        preact(transaction);
                                        act(association, role, from, to);
                                    }
                                }

                                foreach (var assert in assertPermutation)
                                {
                                    for (var assertRepeat = 0; assertRepeat < assertRepeats; assertRepeat++)
                                    {
                                        assert(association, role, from, to);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void FromFromAnotherTo(
            Func<IEnumerable<Action<OneToManyAssociationType, OneToManyRoleType, IObject, IObject, IObject>>> acts,
            Func<IEnumerable<Action<OneToManyAssociationType, OneToManyRoleType, IObject, IObject, IObject>>> asserts)
        {
            var assertPermutations = asserts().Permutations().ToArray();

            foreach (var (preactName, preact) in this.preActs)
            {
                Debugger.Log(0, null, $"Preact {preactName}\n");
                foreach (var actRepeats in new[] { 1, 2 })
                {
                    Debugger.Log(0, null, $"Act Repeats {actRepeats}\n");

                    if (preactName == "Commit" && actRepeats == 2)
                    {
                        Debugger.Break();
                    }

                    foreach (var assertPermutation in assertPermutations)
                    {
                        foreach (var assertRepeats in new[] { 1, 2 })
                        {
                            Debugger.Log(0, null, $"Assert Repeats {assertRepeats}\n");
                            foreach (var fixture in this.fixtures)
                            {
                                var (association, role, _, fromBuilder, fromAnotherBuilder, toBuilder, _, _, _) = fixture();

                                var database = this.CreateDatabase();
                                var transaction = database.CreateTransaction();

                                var from = fromBuilder(transaction);
                                var fromAnother = fromAnotherBuilder(transaction);
                                var to = toBuilder(transaction);

                                foreach (var act in acts())
                                {
                                    for (var actRepeat = 0; actRepeat < actRepeats; actRepeat++)
                                    {
                                        preact(transaction);
                                        act(association, role, from, fromAnother, to);
                                    }
                                }

                                foreach (var assert in assertPermutation)
                                {
                                    for (var assertRepeat = 0; assertRepeat < assertRepeats; assertRepeat++)
                                    {
                                        assert(association, role, from, fromAnother, to);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
