namespace Allors.Core.Database.Engines.Memory;

using System.Collections.Frozen;
using Allors.Core.Database.Engines.Meta;

/// <summary>
/// Contains the immutable state of an object.
/// </summary>
public record Record(EnginesClass Class, long Id, long Version, FrozenDictionary<EnginesRoleType, object> RoleByRoleTypeId, FrozenDictionary<EnginesAssociationType, object> AssociationByAssociationTypeId);
