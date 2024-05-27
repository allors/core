namespace Allors.Core.Meta.Meta;

public interface IMetaCompositeAssociationType : IMetaAssociationType
{
    new IMetaCompositeRoleType RoleType { get; }

    bool IsOne { get; }

    bool IsMany { get; }
}
