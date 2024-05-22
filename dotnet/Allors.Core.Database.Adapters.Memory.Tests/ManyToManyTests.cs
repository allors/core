namespace Allors.Core.Database.Adapters.Memory.Tests
{
    public class ManyToManyTests : Adapters.Tests.ManyToManyTests
    {
        protected override IDatabase CreateDatabase() => new Database(this.Meta.CoreMeta);
    }
}
