namespace Allors.Core.Database.Engines.Tests;

using Allors.Core.Database.Meta;

public abstract class Tests
{
    protected Tests()
    {
        var coreMeta = new CoreMeta();
        this.Meta = new TestsMeta(coreMeta);
    }

    public TestsMeta Meta { get; }
}
