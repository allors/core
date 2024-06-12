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
        CoreMetaMeta.Populate(metaMeta);

        var meta = new Core.Meta.Meta(metaMeta);
        CoreMeta.Populate(meta);
        TestsMeta.Populate(meta);

        meta.Derive();

        this.Meta = meta;
    }

    public Core.Meta.Meta Meta { get; }
}
