namespace Allors.Core.Database.Adapters.Memory.Tests
{
    public class UnitHandleTests : Adapters.Tests.UnitHandleTests
    {
        protected override IDatabase CreateDatabase() => new Database();
    }
}
