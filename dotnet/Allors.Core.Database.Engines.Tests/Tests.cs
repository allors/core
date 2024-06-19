namespace Allors.Core.Database.Engines.Tests;

using Allors.Core.Database.Engines.Tests.Meta;
using Allors.Core.Database.Meta;
using Allors.Core.Database.MetaMeta;
using Allors.Core.MetaMeta;

public abstract class Tests
{
    protected Tests()
    {
        var metaMeta = new MetaMeta();
        var meta = new Core.Meta.Meta(metaMeta);

        CoreMetaMeta.Populate(metaMeta);
        CoreMeta.Configure(meta);
        CoreMeta.Populate(meta);

        TestsMeta.Populate(meta);

        meta.Derive();

        this.Meta = meta;
    }

    public Core.Meta.Meta Meta { get; }
}
