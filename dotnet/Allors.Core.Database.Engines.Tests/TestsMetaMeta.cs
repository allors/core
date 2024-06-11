namespace Allors.Core.Database.Meta;

using System;
using Allors.Core.MetaMeta;

/// <summary>
/// Core MetaMeta MetaMeta.
/// </summary>
public sealed class TestsMetaMeta
{
    internal TestsMetaMeta(CoreMetaMeta m)
    {
        this.MetaMeta = m.MetaMeta;

        this.AllorsTests = this.MetaMeta.AddDomain(new Guid("3d32f751-233a-4ce7-a444-c5a38b7fec22"), "AllorsTests");
        this.AllorsTests.AddDirectSuperdomain(m.MetaMeta.AllorsCore());
    }

    /// <summary>
    /// The meta meta.
    /// </summary>
    public MetaMeta MetaMeta { get; }

    /// <summary>
    /// The allors tests meta domain.
    /// </summary>
    public MetaDomain AllorsTests { get; init; }
}
