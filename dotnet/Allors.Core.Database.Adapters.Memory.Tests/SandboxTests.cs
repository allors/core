namespace Allors.Core.Database.Adapters.Memory.Tests
{
    using Allors.Core.Database.Adapters.Tests;
    using Allors.Core.Database.Meta;

    public class SandboxTests : Adapters.Tests.SandboxTests
    {
        protected override AdaptersMeta Meta { get; } = new(new CoreMeta());

        protected override IDatabase CreateDatabase() => new Database(this.Meta.CoreMeta);
    }
}
