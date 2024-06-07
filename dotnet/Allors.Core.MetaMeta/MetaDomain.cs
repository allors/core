namespace Allors.Core.MetaMeta;

using System;
using System.Collections.Generic;

public sealed class MetaDomain
{
    private readonly Dictionary<Guid, MetaObjectType> objectTypeById;
    private readonly Dictionary<Guid, IMetaAssociationType> associationTypeById;
    private readonly Dictionary<Guid, IMetaRoleType> roleTypeById;
    private readonly Dictionary<Guid, MetaInheritance> inheritanceById;

    public MetaDomain(MetaMeta meta, Guid id, string name)
    {
        this.Meta = meta;
        this.Id = id;
        this.Name = name;

        this.objectTypeById = [];
        this.associationTypeById = [];
        this.roleTypeById = [];
        this.inheritanceById = [];
    }

    public IReadOnlyDictionary<Guid, MetaObjectType> ObjectTypeById => this.objectTypeById;

    public IReadOnlyDictionary<Guid, IMetaAssociationType> AssociationTypeById => this.associationTypeById;

    public IReadOnlyDictionary<Guid, IMetaRoleType> RoleTypeById => this.roleTypeById;

    public MetaMeta Meta { get; }

    public string Name { get; }

    public Guid Id { get; }

    internal void Add(MetaObjectType objectType)
    {
        this.objectTypeById.Add(objectType.Id, objectType);
    }

    internal void Add(IMetaRoleType roleType)
    {
        this.roleTypeById.Add(roleType.Id, roleType);
        this.associationTypeById.Add(roleType.AssociationType.Id, roleType.AssociationType);
    }

    internal void Add(MetaInheritance inheritance)
    {
        this.inheritanceById.Add(inheritance.Id, inheritance);
    }
}
