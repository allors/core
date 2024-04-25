namespace Allors.Core.Database.Adapters.Memory;

using System.Collections.Frozen;
using Allors.Core.Database.Meta.Handles;

/// <summary>
/// Contains the immutable state of an object.
/// </summary>
public record Record(ClassHandle ClassHandle, long Id, long Version, FrozenDictionary<RoleTypeHandle, object> RoleByRoleTypeId, FrozenDictionary<AssociationTypeHandle, object> AssociationByAssociationTypeId)
{
}
