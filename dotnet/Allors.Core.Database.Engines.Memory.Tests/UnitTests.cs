namespace Allors.Core.Database.Engines.Memory.Tests
{
    using Allors.Core.Database.Engines.Tests;
    using Allors.Core.Database.Meta;

    public class UnitTests : Engines.Tests.UnitTests
    {
        protected override IDatabase CreateDatabase() => new Database(new EnginesMeta(new CoreMeta()).CoreMeta);
    }
}
