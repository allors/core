namespace Allors.Core.Database.Adapters.Memory.Tests
{
    using Allors.Core.Database.Adapters.Tests;
    using Allors.Core.Database.Meta;

    public class ChangesTests : Adapters.Tests.ChangesTests
    {
        protected override IDatabase CreateDatabase() => new Database(new AdaptersMeta(new CoreMeta()).CoreMeta);
    }
}
