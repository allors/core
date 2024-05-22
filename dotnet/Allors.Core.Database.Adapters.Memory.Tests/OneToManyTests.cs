namespace Allors.Core.Database.Adapters.Memory.Tests
{
    public class OneToManyTests : Adapters.Tests.OneToManyTests
    {
        protected override IDatabase CreateDatabase() => new Database(this.Meta.CoreMeta);
    }
}
