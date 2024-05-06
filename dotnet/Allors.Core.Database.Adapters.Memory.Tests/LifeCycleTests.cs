namespace Allors.Core.Database.Adapters.Memory.Tests
{
    using Allors.Core.Database.Adapters.Tests;
    using Allors.Core.Database.Meta;

    public class LifeCycleTests : Adapters.Tests.LifeCycleTests
    {
        protected override IDatabase CreateDatabase() => new Database(new AdaptersMeta(new CoreMeta()).CoreMeta);
    }
}
