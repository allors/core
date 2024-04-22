namespace Allors.Core.Database.Adapters.Tests.Extensions;

using System.Collections.Generic;
using Allors.Core.Database.Meta;

public static class IChangeSetExtensions
{
    public static ISet<RoleType> GetRoleTypes(this IChangeSet @this, IObject association) =>
        @this.RoleTypesByAssociation.TryGetValue(association, out var roleTypes) ? roleTypes : new HashSet<RoleType>();

    public static ISet<AssociationType> GetAssociationTypes(this IChangeSet @this, IObject role) =>
        @this.AssociationTypesByRole.TryGetValue(role, out var associationTypes) ? associationTypes : new HashSet<AssociationType>();
}
