namespace Allors.Core.Database.Adapters.Memory;

using System.Collections.Frozen;
using Allors.Core.Database.Meta;

/// <summary>
/// Contains the immutable state of an object.
/// </summary>
public record Record(Class Class, long Id, long Version, FrozenDictionary<RoleType, object> RoleByRoleTypeId)
{
}
