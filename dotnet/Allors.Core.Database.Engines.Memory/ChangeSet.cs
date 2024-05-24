namespace Allors.Core.Database.Engines.Memory;

using System.Collections.Generic;
using System.Linq;
using Allors.Core.Database.Meta.Domain;

/// <inheritdoc />
public class ChangeSet : IChangeSet
{
    private readonly HashSet<IObject> created;
    private readonly HashSet<IObject> deleted;
    private readonly Dictionary<IObject, ISet<IRoleType>> roleTypesByAssociation;
    private readonly Dictionary<IObject, ISet<IAssociationType>> associationTypesByRole;

    private ISet<IObject>? associations;
    private ISet<IObject>? roles;
    private IDictionary<IRoleType, ISet<IObject>>? associationsByRoleType;
    private IDictionary<IAssociationType, ISet<IObject>>? rolesByAssociationType;

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
    public IDictionary<IObject, ISet<IRoleType>> RoleTypesByAssociation => this.roleTypesByAssociation;

    /// <inheritdoc />
    public IDictionary<IObject, ISet<IAssociationType>> AssociationTypesByRole => this.associationTypesByRole;

    /// <inheritdoc />
    public ISet<IObject> Associations => this.associations ??= new HashSet<IObject>(this.RoleTypesByAssociation.Keys);

    /// <inheritdoc />
    public ISet<IObject> Roles => this.roles ??= new HashSet<IObject>(this.AssociationTypesByRole.Keys);

    /// <inheritdoc />
    public IDictionary<IRoleType, ISet<IObject>> AssociationsByRoleType => this.associationsByRoleType ??=
        (from kvp in this.RoleTypesByAssociation
         from value in kvp.Value
         group kvp.Key by value)
        .ToDictionary(grp => grp.Key, grp => new HashSet<IObject>(grp) as ISet<IObject>);

    /// <inheritdoc />
    public IDictionary<IAssociationType, ISet<IObject>> RolesByAssociationType => this.rolesByAssociationType ??=
        (from kvp in this.AssociationTypesByRole
         from value in kvp.Value
         group kvp.Key by value)
        .ToDictionary(grp => grp.Key, grp => new HashSet<IObject>(grp) as ISet<IObject>);

    internal void AddChangedRoleByRoleTypeId(IObject @object, IRoleType roleTypeId)
    {
        if (!this.roleTypesByAssociation.TryGetValue(@object, out var roleTypes))
        {
            roleTypes = new HashSet<IRoleType>();
            this.roleTypesByAssociation.Add(@object, roleTypes);
        }

        roleTypes.Add(roleTypeId);
    }

    internal void AddChangedAssociationByAssociationTypeId(IObject @object, IAssociationType associationTypeId)
    {
        if (!this.associationTypesByRole.TryGetValue(@object, out var associationTypes))
        {
            associationTypes = new HashSet<IAssociationType>();
            this.associationTypesByRole.Add(@object, associationTypes);
        }

        associationTypes.Add(associationTypeId);
    }
}
