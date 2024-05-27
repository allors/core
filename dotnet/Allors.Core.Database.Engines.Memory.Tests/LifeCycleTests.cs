namespace Allors.Core.Database.Engines.Memory.Tests;

using Allors.Core.Database.Engines.Meta;

public class LifeCycleTests : Engines.Tests.LifeCycleTests
{
    protected override IDatabase CreateDatabase() => new Database(new EnginesMeta(this.Meta.CoreMeta));
}
