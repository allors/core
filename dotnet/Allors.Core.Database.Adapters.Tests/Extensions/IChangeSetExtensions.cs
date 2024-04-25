namespace Allors.Core.Database.Adapters.Tests.Extensions;

using System.Collections.Generic;
using Allors.Core.Database.Meta.Handles;

public static class IChangeSetExtensions
{
    public static ISet<RoleTypeHandle> GetRoleTypes(this IChangeSet @this, IObject association) =>
        @this.RoleTypesByAssociation.TryGetValue(association, out var roleTypes) ? roleTypes : new HashSet<RoleTypeHandle>();

    public static ISet<AssociationTypeHandle> GetAssociationTypes(this IChangeSet @this, IObject role) =>
        @this.AssociationTypesByRole.TryGetValue(role, out var associationTypes) ? associationTypes : new HashSet<AssociationTypeHandle>();
}
