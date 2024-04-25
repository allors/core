namespace Allors.Core.Database.Tests.Meta
{
    using System;
    using Allors.Core.Database.Meta;
    using Xunit;

    public class MetaTests
    {
        [Fact]
        public void MetaObjectById()
        {
            var coreMeta = new CoreMeta();

            var @object = coreMeta[new Guid("8904EE32-CF11-4019-9FD7-FB9631F9ACAC")];
            var @string = coreMeta[new Guid("58BB7632-4724-4F92-869B-B30D7A7BEE9E")];

            Assert.NotNull(@object);
            Assert.NotNull(@string);
        }
    }
}
