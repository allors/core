namespace Allors.Core.Meta.Meta;

public interface IMetaCompositeRoleType : IMetaRoleType
{
    new IMetaCompositeAssociationType AssociationType { get; }

    bool IsOne { get; }

    bool IsMany { get; }
}
