namespace Allors.Core.Database.Engines.Memory.Tests;

using Allors.Core.Database.Engines.Meta;

public class ExtentTests : Engines.Tests.ExtentTests
{
    protected override IDatabase CreateDatabase() => new Database(new EnginesMeta(this.Meta.CoreMeta));
}
