namespace Allors.Core.Database.Engines.Tests.Extensions;

using System.Collections.Generic;
using Allors.Core.Database.Meta.Domain;

public static class IChangeSetExtensions
{
    public static ISet<IRoleType> GetRoleTypes(this IChangeSet @this, IObject association) =>
        @this.RoleTypesByAssociation.TryGetValue(association, out var roleTypes) ? roleTypes : new HashSet<IRoleType>();

    public static ISet<IAssociationType> GetAssociationTypes(this IChangeSet @this, IObject role) =>
        @this.AssociationTypesByRole.TryGetValue(role, out var associationTypes) ? associationTypes : new HashSet<IAssociationType>();
}
