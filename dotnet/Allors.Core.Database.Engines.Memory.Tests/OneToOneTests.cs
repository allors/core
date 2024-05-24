namespace Allors.Core.Database.Engines.Memory.Tests
{
    public class OneToOneTests : Engines.Tests.OneToOneTests
    {
        protected override IDatabase CreateDatabase() => new Database(this.Meta.CoreMeta);
    }
}
