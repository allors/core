namespace Allors.Core.MetaMeta;

using System;
using System.Collections.Generic;
using System.Linq;

public sealed class MetaDomain
{
    private readonly HashSet<MetaDomain> directSuperdomains;

    private readonly Dictionary<Guid, MetaObjectType> objectTypeById;
    private readonly Dictionary<Guid, IMetaAssociationType> associationTypeById;
    private readonly Dictionary<Guid, IMetaRoleType> roleTypeById;
    private readonly Dictionary<Guid, MetaInheritance> inheritanceById;

    private HashSet<MetaDomain>? derivedSuperdomains;

    public MetaDomain(MetaMeta meta, Guid id, string name)
    {
        this.Meta = meta;
        this.Id = id;
        this.Name = name;
        this.directSuperdomains = [];

        this.objectTypeById = [];
        this.associationTypeById = [];
        this.roleTypeById = [];
        this.inheritanceById = [];

        this.Meta.ResetDerivations();
    }

    public IReadOnlySet<MetaDomain> DirectSuperdomains => this.directSuperdomains;

    public IReadOnlySet<MetaDomain> Superdomains
    {
        get
        {
            if (this.derivedSuperdomains != null)
            {
                return this.derivedSuperdomains;
            }

            this.derivedSuperdomains = [];
            this.AddSuperdomains(this.derivedSuperdomains);
            return this.derivedSuperdomains;
        }
    }

    public IReadOnlyDictionary<Guid, MetaObjectType> ObjectTypeById => this.objectTypeById;

    public IReadOnlyDictionary<Guid, IMetaAssociationType> AssociationTypeById => this.associationTypeById;

    public IReadOnlyDictionary<Guid, IMetaRoleType> RoleTypeById => this.roleTypeById;

    public MetaMeta Meta { get; }

    public string Name { get; }

    public Guid Id { get; }

    public void AddDirectSuperdomain(MetaDomain directSuperdomain)
    {
        this.directSuperdomains.Add(directSuperdomain);
        this.Meta.ResetDerivations();
    }

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

    private void AddSuperdomains(HashSet<MetaDomain> newDerivedSuperdomains)
    {
        foreach (var superdomain in this.directSuperdomains.Where(superdomain => !newDerivedSuperdomains.Contains(superdomain)))
        {
            newDerivedSuperdomains.Add(superdomain);
            superdomain.AddSuperdomains(newDerivedSuperdomains);
        }
    }
}
