namespace Allors.Core.Database.Engines.Memory.Tests;

using Allors.Core.Database.Engines.Meta;

public class UnitTests : Engines.Tests.UnitTests
{
    protected override IDatabase CreateDatabase() => new Database(new EnginesMeta(this.Meta.CoreMeta));
}
