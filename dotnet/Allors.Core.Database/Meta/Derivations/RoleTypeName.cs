namespace Allors.Core.Database.Meta.Derivations;

using System.Linq;
using Allors.Core.Meta;

/// <summary>
/// Derive the name of the role type.
/// </summary>
public sealed class RoleTypeName(Meta meta) : IMetaDerivation
{
    /// <inheritdoc/>
    public void Derive(MetaChangeSet changeSet)
    {
        var m = meta.MetaMeta;

        var singularNames = changeSet.ChangedRoles(m.RoleTypeSingularName());
        var derivedPluralNames = changeSet.ChangedRoles(m.RoleTypeDerivedPluralName());
        var newRoleTypes = changeSet.NewObjects.Where(v => m.RoleType().IsAssignableFrom(v.ObjectType)).ToArray();

        if (singularNames.Any() || derivedPluralNames.Any() || newRoleTypes.Length != 0)
        {
            return;
        }

        var roleTypes = singularNames.Union(derivedPluralNames).Select(v => v.Key).Union(newRoleTypes).Distinct();

        // TODO: Optimize
        foreach (var roleType in roleTypes)
        {
            if (m.UnitRoleType().IsAssignableFrom(roleType.ObjectType) || m.ToOneRoleType().IsAssignableFrom(roleType.ObjectType))
            {
                roleType[m.RoleTypeName()] = roleType[m.RoleTypeSingularName()];
            }
            else
            {
                roleType[m.RoleTypeName()] = roleType[m.RoleTypeDerivedPluralName()];
            }
        }
    }
}
