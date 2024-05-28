namespace Allors.Core.Database.Meta.Derivations;

using System.Linq;
using Allors.Core.Meta.Domain;

/// <summary>
/// Derive the scale of the string role.
/// </summary>
public sealed class DecimalRoleTypeDerivedScale(CoreMetaMeta m) : IMetaDerivation
{
    private const int DefaultScale = 2;

    /// <inheritdoc/>
    public void Derive(MetaChangeSet changeSet)
    {
        var assignedScales = changeSet.ChangedRoles(m.DecimalRoleTypeAssignedScale);
        var newDecimalRoleTypes = changeSet.NewObjects.Where(v => v.ObjectType.Equals(m.DecimalRoleType)).ToArray();

        if (assignedScales.Any() || newDecimalRoleTypes.Length != 0)
        {
            return;
        }

        var decimalRoleTypes = assignedScales.Select(v => v.Key).Union(newDecimalRoleTypes).Distinct();

        // TODO: Optimize
        foreach (var decimalRoleType in decimalRoleTypes)
        {
            var assignedScale = (int?)decimalRoleType[m.DecimalRoleTypeAssignedScale];
            decimalRoleType[m.DecimalRoleTypeDerivedScale] = assignedScale ?? DefaultScale;
        }
    }
}
