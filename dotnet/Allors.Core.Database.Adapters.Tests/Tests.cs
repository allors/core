namespace Allors.Core.Database.Adapters.Tests
{
    using System;
    using Allors.Core.Database.Meta;

    public abstract class Tests
    {
        protected Tests()
        {
            var coreMeta = new CoreMeta();
            this.Meta = new AdaptersMeta(coreMeta);
        }

        public AdaptersMeta Meta { get; }

        public string[][] AssertOrders { get; } = [
            ["A", "R"],
            ["R", "A"],
            ["A", "A", "R", "R"],
            ["A", "R", "A", "R"],
            ["R", "A", "R", "A"],
            ["R", "R", "A", "A"],
        ];

        protected static void Asserts(int assertRepeat, string[] assertOrder, Action associationAssert, Action roleAssert)
        {
            for (var i = 0; i < assertRepeat; i++)
            {
                foreach (var assert in assertOrder)
                {
                    switch (assert)
                    {
                        case "A":
                            associationAssert();
                            break;
                        case "R":
                            roleAssert();
                            break;
                    }
                }
            }
        }
    }
}
