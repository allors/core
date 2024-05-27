namespace Allors.Core.Meta.Domain;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Allors.Core.Meta.Meta;

public sealed class MetaChangeSet(
    IReadOnlyDictionary<IMetaRoleType, Dictionary<IMetaObject, object?>> roleByAssociationByRoleType,
    IReadOnlyDictionary<IMetaCompositeAssociationType, Dictionary<IMetaObject, object?>> associationByRoleByAssociationType)
{
    private static readonly IReadOnlyDictionary<IMetaObject, object?> Empty = ReadOnlyDictionary<IMetaObject, object?>.Empty;

    public bool HasChanges =>
        roleByAssociationByRoleType.Any(v => v.Value.Count > 0) ||
        associationByRoleByAssociationType.Any(v => v.Value.Count > 0);

    public IReadOnlyDictionary<IMetaObject, object?> ChangedRoles(MetaObjectType objectType, string name)
    {
        var roleType = objectType.RoleTypeByName[name];
        return this.ChangedRoles(roleType);
    }

    public IReadOnlyDictionary<IMetaObject, object?> ChangedRoles(IMetaRoleType roleType)
    {
        roleByAssociationByRoleType.TryGetValue(roleType, out var changedRelations);
        return changedRelations ?? Empty;
    }
}
