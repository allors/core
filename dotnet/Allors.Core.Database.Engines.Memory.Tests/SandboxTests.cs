namespace Allors.Core.Database.Engines.Memory.Tests;

using Allors.Core.Database.Engines.Meta;

public class SandboxTests : Engines.Tests.SandboxTests
{
    protected override IDatabase CreateDatabase() => new Database(new EnginesMeta(this.Meta));
}
