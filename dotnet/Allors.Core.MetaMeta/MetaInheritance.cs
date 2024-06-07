namespace Allors.Core.MetaMeta;

using System;

public sealed class MetaInheritance
{
    internal MetaInheritance(MetaDomain domain, Guid id, MetaObjectType subtype, MetaObjectType supertype)
    {
        this.Meta = domain.Meta;
        this.Domain = domain;
        this.Id = id;
        this.Subtype = subtype;
        this.Supertype = supertype;
    }

    public MetaMeta Meta { get; }

    public MetaDomain Domain { get; }

    public Guid Id { get; set; }

    public MetaObjectType Subtype { get; }

    public MetaObjectType Supertype { get; }
}
