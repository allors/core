namespace Allors.Core.Database.Engines.Memory.Tests
{
    public class ManyToManyTests : Engines.Tests.ManyToManyTests
    {
        protected override IDatabase CreateDatabase() => new Database(this.Meta.CoreMeta);
    }
}
