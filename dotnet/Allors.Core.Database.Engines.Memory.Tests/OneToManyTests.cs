namespace Allors.Core.Database.Engines.Memory.Tests
{
    public class OneToManyTests : Engines.Tests.OneToManyTests
    {
        protected override IDatabase CreateDatabase() => new Database(this.Meta.CoreMeta);
    }
}
