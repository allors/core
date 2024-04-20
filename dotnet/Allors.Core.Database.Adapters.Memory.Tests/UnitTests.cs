namespace Allors.Core.Database.Adapters.Memory.Tests
{
    public class UnitTests : Adapters.Tests.UnitTestsBase
    {
        protected override IDatabase CreateDatabase() => new Database();
    }
}
