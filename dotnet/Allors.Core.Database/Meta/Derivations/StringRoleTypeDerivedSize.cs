namespace Allors.Core.Database.Meta.Derivations;

using System.Linq;
using Allors.Core.Meta;

/// <summary>
/// Derive the size of the string role.
/// </summary>
public sealed class StringRoleTypeDerivedSize(Meta meta) : IMetaDerivation
{
    private const int DefaultSize = 256;

    /// <inheritdoc/>
    public void Derive(MetaChangeSet changeSet)
    {
        var m = meta.MetaMeta;

        var assignedSizes = changeSet.ChangedRoles(m.StringRoleTypeAssignedSize());
        var newStringRoleTypes = changeSet.NewObjects.Where(v => v.ObjectType.Equals(m.StringRoleType())).ToArray();

        if (assignedSizes.Any() || newStringRoleTypes.Length != 0)
        {
            var stringRoleTypes = assignedSizes.Select(v => v.Key).Union(newStringRoleTypes).Distinct();

            // TODO: Optimize
            foreach (var stringRoleType in stringRoleTypes)
            {
                var assignedSize = (int?)stringRoleType[m.StringRoleTypeAssignedSize];
                stringRoleType[m.StringRoleTypeDerivedSize] = assignedSize ?? DefaultSize;
            }
        }
    }
}
