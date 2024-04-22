namespace Allors.Core.Database.Adapters.Memory.Tests
{
    public class OneToOneTests : Adapters.Tests.OneToOneTests
    {
        protected override IDatabase CreateDatabase() => new Database();
    }
}
