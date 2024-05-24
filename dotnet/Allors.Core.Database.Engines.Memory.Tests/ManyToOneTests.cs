namespace Allors.Core.Database.Engines.Memory.Tests
{
    public class ManyToOneTests : Engines.Tests.ManyToOneTests
    {
        protected override IDatabase CreateDatabase() => new Database(this.Meta.CoreMeta);
    }
}
