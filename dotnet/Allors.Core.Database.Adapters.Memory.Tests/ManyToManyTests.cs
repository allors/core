namespace Allors.Core.Database.Adapters.Memory.Tests
{
    using Allors.Core.Database.Adapters.Tests;
    using Allors.Core.Database.Meta;

    public class ManyToManyTests : Adapters.Tests.ManyToManyTests
    {
        protected override IDatabase CreateDatabase() => new Database(this.Meta.CoreMeta);
    }
}
