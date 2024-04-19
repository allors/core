namespace Allors.Core.Database.Tests.Meta
{
    using Allors.Core.Database.Meta;
    using Xunit;

    public class MetaTests
    {
        [Fact]
        public void MetaObjectById()
        {
            var coreMeta = new CoreMeta();
            var meta = coreMeta.Build();

            var @object = meta.MetaObjectById[CoreMeta.ID1595A154CEE841FCA88FCE3EEACA8B57];

            Assert.NotNull(@object);
        }
    }
}
