namespace Allors.Core.Database.Engines.Memory.Tests
{
    using Allors.Core.Database.Engines.Tests;
    using Allors.Core.Database.Meta;

    public class SandboxTests : Engines.Tests.SandboxTests
    {
        protected override EnginesMeta Meta { get; } = new(new CoreMeta());

        protected override IDatabase CreateDatabase() => new Database(this.Meta.CoreMeta);
    }
}
