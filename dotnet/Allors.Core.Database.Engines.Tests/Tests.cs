namespace Allors.Core.Database.Engines.Tests;

using Allors.Core.Database.Engines.Tests.Meta;
using Allors.Core.Database.Meta;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;

public abstract class Tests
{
    protected Tests()
    {
        var metaMeta = new MetaMeta();
        CoreMetaMeta.Populate(metaMeta);
        TestsMetaMeta.Populate(metaMeta);

        var meta = new Core.Meta.Meta(metaMeta);
        meta.Sync();
        CoreMeta.Populate(meta);
        TestsMeta.Populate(meta);

        meta.Derive();

        this.Meta = meta;
    }

    public Core.Meta.Meta Meta { get; }
}
