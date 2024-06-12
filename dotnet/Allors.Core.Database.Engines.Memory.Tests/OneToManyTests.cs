namespace Allors.Core.Database.Engines.Memory.Tests;

using Allors.Core.Database.Engines.Meta;

public class OneToManyTests : Engines.Tests.OneToManyTests
{
    protected override IDatabase CreateDatabase() => new Database(new EnginesMeta(this.Meta));
}
