namespace Allors.Core.Database.Adapters.Memory.Tests
{
    public class ChangesTests : Adapters.Tests.ChangesTests
    {
        protected override IDatabase CreateDatabase() => new Database();
    }
}
