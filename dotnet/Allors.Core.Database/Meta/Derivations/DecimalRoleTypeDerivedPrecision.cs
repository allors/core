namespace Allors.Core.Database.Meta.Derivations;

using System.Linq;
using Allors.Core.Meta.Domain;

/// <summary>
/// Derive the size of the string role.
/// </summary>
public sealed class DecimalRoleTypeDerivedPrecision(CoreMetaMeta m) : IMetaDerivation
{
    private const int DefaultPrecision = 19;

    /// <inheritdoc/>
    public void Derive(MetaChangeSet changeSet)
    {
        var assignedPrecisions = changeSet.ChangedRoles(m.DecimalRoleTypeAssignedPrecision);
        var newDecimalRoleTypes = changeSet.NewObjects.Where(v => v.ObjectType.Equals(m.DecimalRoleType)).ToArray();

        if (assignedPrecisions.Any() || newDecimalRoleTypes.Length != 0)
        {
            return;
        }

        var decimalRoleTypes = assignedPrecisions.Select(v => v.Key).Union(newDecimalRoleTypes).Distinct();

        // TODO: Optimize
        foreach (var decimalRoleType in decimalRoleTypes)
        {
            var assignedPrecision = (int?)decimalRoleType[m.DecimalRoleTypeAssignedPrecision];
            decimalRoleType[m.DecimalRoleTypeDerivedPrecision] = assignedPrecision ?? DefaultPrecision;
        }
    }
}
