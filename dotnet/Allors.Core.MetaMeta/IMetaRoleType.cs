﻿namespace Allors.Core.MetaMeta;

using System;

public interface IMetaRoleType
{
    MetaMeta MetaMeta { get; }

    Guid Id { get; }

    IMetaAssociationType AssociationType { get; }

    MetaObjectType ObjectType { get; }

    string SingularName { get; }

    string PluralName { get; }

    string Name { get; }

    void Deconstruct(out IMetaAssociationType associationType, out IMetaRoleType roleType);
}
