namespace Allors.Core.MetaMeta;

using System;

public sealed class MetaInheritance
{
    internal MetaInheritance(MetaMeta metaMeta, Guid id, MetaObjectType subtype, MetaObjectType supertype)
    {
        this.MetaMeta = metaMeta;
        this.Id = id;
        this.Subtype = subtype;
        this.Supertype = supertype;
    }

    public MetaMeta MetaMeta { get; }

    public Guid Id { get; set; }

    public MetaObjectType Subtype { get; }

    public MetaObjectType Supertype { get; }
}
