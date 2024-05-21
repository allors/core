namespace Allors.Core.Database.Adapters.Memory.Tests
{
    using Allors.Core.Database.Adapters.Tests;
    using Allors.Core.Database.Meta;

    public class OneToManyTests : Adapters.Tests.OneToManyTests
    {
        protected override IDatabase CreateDatabase() => new Database(this.Meta.CoreMeta);
    }
}
