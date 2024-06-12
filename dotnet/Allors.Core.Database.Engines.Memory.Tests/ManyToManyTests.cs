namespace Allors.Core.Database.Engines.Memory.Tests;

using Allors.Core.Database.Engines.Meta;

public class ManyToManyTests : Engines.Tests.ManyToManyTests
{
    protected override IDatabase CreateDatabase() => new Database(new EnginesMeta(this.Meta));
}
