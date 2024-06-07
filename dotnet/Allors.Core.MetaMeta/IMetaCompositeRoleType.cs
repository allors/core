namespace Allors.Core.MetaMeta;

public interface IMetaCompositeRoleType : IMetaRoleType
{
    new IMetaCompositeAssociationType AssociationType { get; }

    bool IsOne { get; }

    bool IsMany { get; }
}
