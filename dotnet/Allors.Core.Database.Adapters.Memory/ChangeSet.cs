namespace Allors.Core.Database.Adapters.Memory;

using System.Collections.Generic;
using System.Linq;
using Allors.Core.Database.Meta.Handles;

/// <inheritdoc />
public class ChangeSet : IChangeSet
{
    private readonly HashSet<IObject> created;
    private readonly HashSet<IObject> deleted;
    private readonly Dictionary<IObject, ISet<RoleTypeHandle>> roleTypesByAssociation;
    private readonly Dictionary<IObject, ISet<AssociationTypeHandle>> associationTypesByRole;

    private ISet<IObject>? associations;
    private ISet<IObject>? roles;
    private IDictionary<RoleTypeHandle, ISet<IObject>>? associationsByRoleType;
    private IDictionary<AssociationTypeHandle, ISet<IObject>>? rolesByAssociationType;

    internal ChangeSet()
    {
        this.created = [];
        this.deleted = [];
        this.roleTypesByAssociation = [];
        this.associationTypesByRole = [];
    }

    /// <inheritdoc />
    public ISet<IObject> Created => this.created;

    /// <inheritdoc />
    public ISet<IObject> Deleted => this.deleted;

    /// <inheritdoc />
    public IDictionary<IObject, ISet<RoleTypeHandle>> RoleTypesByAssociation => this.roleTypesByAssociation;

    /// <inheritdoc />
    public IDictionary<IObject, ISet<AssociationTypeHandle>> AssociationTypesByRole => this.associationTypesByRole;

    /// <inheritdoc />
    public ISet<IObject> Associations => this.associations ??= new HashSet<IObject>(this.RoleTypesByAssociation.Keys);

    /// <inheritdoc />
    public ISet<IObject> Roles => this.roles ??= new HashSet<IObject>(this.AssociationTypesByRole.Keys);

    /// <inheritdoc />
    public IDictionary<RoleTypeHandle, ISet<IObject>> AssociationsByRoleType => this.associationsByRoleType ??=
        (from kvp in this.RoleTypesByAssociation
         from value in kvp.Value
         group kvp.Key by value)
        .ToDictionary(grp => grp.Key, grp => new HashSet<IObject>(grp) as ISet<IObject>);

    /// <inheritdoc />
    public IDictionary<AssociationTypeHandle, ISet<IObject>> RolesByAssociationType => this.rolesByAssociationType ??=
        (from kvp in this.AssociationTypesByRole
         from value in kvp.Value
         group kvp.Key by value)
        .ToDictionary(grp => grp.Key, grp => new HashSet<IObject>(grp) as ISet<IObject>);

    internal void AddChangedRoleByRoleTypeId(IObject @object, RoleTypeHandle roleTypeHandleId)
    {
        if (!this.roleTypesByAssociation.TryGetValue(@object, out var roleTypes))
        {
            roleTypes = new HashSet<RoleTypeHandle>();
            this.roleTypesByAssociation.Add(@object, roleTypes);
        }

        roleTypes.Add(roleTypeHandleId);
    }

    internal void AddChangedAssociationByAssociationTypeId(IObject @object, AssociationTypeHandle associationTypeHandleId)
    {
        if (!this.associationTypesByRole.TryGetValue(@object, out var associationTypes))
        {
            associationTypes = new HashSet<AssociationTypeHandle>();
            this.associationTypesByRole.Add(@object, associationTypes);
        }

        associationTypes.Add(associationTypeHandleId);
    }
}
