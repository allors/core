namespace Allors.Core.Database.Adapters.Memory;

using System;
using System.Collections.Immutable;
using Allors.Core.Database.Meta;

/// <summary>
/// Contains the immutable state of an object.
/// </summary>
public record State(Class Class, long Id, long Version, ImmutableDictionary<Guid, object> RoleByRoleTypeId)
{
}
