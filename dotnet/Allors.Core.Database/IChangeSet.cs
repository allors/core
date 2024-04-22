namespace Allors.Core.Database;

using System.Collections.Generic;
using Allors.Core.Database.Meta;

/// <summary>
/// The change set.
/// </summary>
public interface IChangeSet
{
    /// <summary>
    ///     Gets the created objects.
    /// </summary>
    ISet<IObject> Created { get; }

    /// <summary>
    ///     Gets the deleted objects.
    /// </summary>
    ISet<IObject> Deleted { get; }

    /// <summary>
    ///     Gets the changed associations.
    /// </summary>
    ISet<IObject> Associations { get; }

    /// <summary>
    ///     Gets the changed roles.
    /// </summary>
    ISet<IObject> Roles { get; }

    /// <summary>
    ///     Gets the changed role types by association.
    /// </summary>
    IDictionary<IObject, ISet<RoleType>> RoleTypesByAssociation { get; }

    /// <summary>
    ///     Gets the changed association types by role.
    /// </summary>
    IDictionary<IObject, ISet<AssociationType>> AssociationTypesByRole { get; }

    /// <summary>
    ///     Gets the changed associations by role type.
    /// </summary>
    IDictionary<RoleType, ISet<IObject>> AssociationsByRoleType { get; }

    /// <summary>
    ///     Gets the changed roles by association type.
    /// </summary>
    IDictionary<AssociationType, ISet<IObject>> RolesByAssociationType { get; }
}
