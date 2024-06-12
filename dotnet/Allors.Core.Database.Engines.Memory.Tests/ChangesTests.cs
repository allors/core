namespace Allors.Core.Database.Engines.Memory.Tests;

using Allors.Core.Database.Engines.Meta;

public class ChangesTests : Engines.Tests.ChangesTests
{
    protected override IDatabase CreateDatabase() => new Database(new EnginesMeta(this.Meta));
}
