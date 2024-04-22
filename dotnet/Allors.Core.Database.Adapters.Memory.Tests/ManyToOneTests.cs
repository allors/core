namespace Allors.Core.Database.Adapters.Memory.Tests
{
    public class ManyToOneTests : Adapters.Tests.ManyToOneTests
    {
        protected override IDatabase CreateDatabase() => new Database();
    }
}
