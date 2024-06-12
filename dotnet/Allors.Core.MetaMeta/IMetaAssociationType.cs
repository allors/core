namespace Allors.Core.MetaMeta;

using System;

public interface IMetaAssociationType
{
    MetaMeta MetaMeta { get; }

    Guid Id { get; }

    MetaObjectType ObjectType { get; }

    IMetaRoleType RoleType { get; }

    string SingularName { get; }

    string PluralName { get; }

    string Name { get; }
}
