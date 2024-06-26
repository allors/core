﻿namespace Allors.Core.Database.Meta.Derivations;

using System.Linq;
using Allors.Core.Database.MetaMeta;
using Allors.Core.Meta;

/// <summary>
/// Derive the size of the string role.
/// </summary>
public sealed class DecimalRoleTypeDerivedPrecision(Meta meta) : IMetaDerivation
{
    private const int DefaultPrecision = 19;

    /// <inheritdoc/>
    public void Derive(MetaChangeSet changeSet)
    {
        var m = meta.MetaMeta;

        var assignedPrecisions = changeSet.ChangedRoles(m.DecimalRoleTypeAssignedPrecision());
        var newDecimalRoleTypes = changeSet.NewObjects.Where(v => v.ObjectType.Equals(m.DecimalRoleType())).ToArray();

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
