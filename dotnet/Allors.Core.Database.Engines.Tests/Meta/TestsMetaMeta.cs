namespace Allors.Core.Database.Engines.Tests.Meta;

using System;
using Allors.Core.Database.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// Populates meta meta with Tests types.
/// </summary>
public static class TestsMetaMeta
{
    internal static void Populate(MetaMeta m)
    {
        var allorsCore = m.AllorsCore();

        var allorsTests = m.AddDomain(new Guid("3d32f751-233a-4ce7-a444-c5a38b7fec22"), "AllorsTests");
        allorsTests.AddDirectSuperdomain(allorsCore);
    }
}
