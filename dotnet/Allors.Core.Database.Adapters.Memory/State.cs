namespace Allors.Core.Database.Adapters.Memory;

using System;
using System.Collections.Immutable;

/// <summary>
/// Contains the immutable state of an object.
/// </summary>
public record State(ImmutableDictionary<Guid, object> RoleByRoleTypeId, long Version);
