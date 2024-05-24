namespace Allors.Core.Database.Engines.Memory;

using System.Collections.Frozen;
using Allors.Core.Database.Engines.Meta;

/// <summary>
/// Contains the immutable state of an object.
/// </summary>
public record Record(EngineClass Class, long Id, long Version, FrozenDictionary<EngineRoleType, object> RoleByRoleTypeId, FrozenDictionary<EngineAssociationType, object> AssociationByAssociationTypeId)
{
}
