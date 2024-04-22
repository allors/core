namespace Allors.Core.Database.Adapters.Memory.Tests
{
    public class ExtentTests : Adapters.Tests.ExtentTests
    {
        protected override IDatabase CreateDatabase() => new Database();
    }
}
