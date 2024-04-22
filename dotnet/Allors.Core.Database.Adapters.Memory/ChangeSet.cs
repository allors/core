namespace Allors.Core.Database.Adapters.Memory;

using System.Collections.Generic;
using System.Linq;
using Allors.Core.Database.Meta;

/// <inheritdoc />
public class ChangeSet : IChangeSet
{
    private ISet<IObject>? associations;
    private ISet<IObject>? roles;
    private IDictionary<RoleType, ISet<IObject>>? associationsByRoleType;
    private IDictionary<AssociationType, ISet<IObject>>? rolesByAssociationType;

    internal ChangeSet(
        ISet<IObject> created,
        ISet<IObject> deleted,
        IDictionary<IObject, ISet<RoleType>> roleTypesByAssociation,
        IDictionary<IObject, ISet<AssociationType>> associationTypesByRole)
    {
        this.Created = created;
        this.Deleted = deleted;
        this.RoleTypesByAssociation = roleTypesByAssociation;
        this.AssociationTypesByRole = associationTypesByRole;
    }

    /// <inheritdoc />
    public ISet<IObject> Created { get; }

    /// <inheritdoc />
    public ISet<IObject> Deleted { get; }

    /// <inheritdoc />
    public IDictionary<IObject, ISet<RoleType>> RoleTypesByAssociation { get; }

    /// <inheritdoc />
    public IDictionary<IObject, ISet<AssociationType>> AssociationTypesByRole { get; }

    /// <inheritdoc />
    public ISet<IObject> Associations => this.associations ??= new HashSet<IObject>(this.RoleTypesByAssociation.Keys);

    /// <inheritdoc />
    public ISet<IObject> Roles => this.roles ??= new HashSet<IObject>(this.AssociationTypesByRole.Keys);

    /// <inheritdoc />
    public IDictionary<RoleType, ISet<IObject>> AssociationsByRoleType => this.associationsByRoleType ??=
        (from kvp in this.RoleTypesByAssociation
         from value in kvp.Value
         group kvp.Key by value)
        .ToDictionary(grp => grp.Key, grp => new HashSet<IObject>(grp) as ISet<IObject>);

    /// <inheritdoc />
    public IDictionary<AssociationType, ISet<IObject>> RolesByAssociationType => this.rolesByAssociationType ??=
        (from kvp in this.AssociationTypesByRole
         from value in kvp.Value
         group kvp.Key by value)
        .ToDictionary(grp => grp.Key, grp => new HashSet<IObject>(grp) as ISet<IObject>);
}
