namespace Allors.Core.MetaMeta;

public interface IMetaCompositeAssociationType : IMetaAssociationType
{
    new IMetaCompositeRoleType RoleType { get; }

    bool IsOne { get; }

    bool IsMany { get; }
}
