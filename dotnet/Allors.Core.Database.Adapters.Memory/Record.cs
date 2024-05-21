namespace Allors.Core.Database.Adapters.Memory;

using System.Collections.Frozen;
using Allors.Core.Database.Meta.Handles;

/// <summary>
/// Contains the immutable state of an object.
/// </summary>
public record Record(Class ClassHandle, long Id, long Version, FrozenDictionary<IRoleType, object> RoleByRoleTypeId, FrozenDictionary<IAssociationType, object> AssociationByAssociationTypeId)
{
}
