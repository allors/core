namespace Allors.Core.Meta;

using System;
using System.Collections.Generic;
using Allors.Core.MetaMeta;

public interface IMetaObject
{
    Meta Meta { get; }

    MetaObjectType ObjectType { get; }

    object? this[string name] { get; set; }

    object? this[Func<string> name] { get; set; }

    object? this[IMetaRoleType roleType] { get; set; }

    object? this[Func<IMetaRoleType> roleType] { get; set; }

    object? this[MetaUnitRoleType roleType] { get; set; }

    object? this[Func<MetaUnitRoleType> roleType] { get; set; }

    IMetaObject? this[IMetaToOneRoleType roleType] { get; set; }

    IMetaObject? this[Func<IMetaToOneRoleType> roleType] { get; set; }

    IEnumerable<IMetaObject> this[IMetaToManyRoleType roleType] { get; set; }

    IEnumerable<IMetaObject> this[Func<IMetaToManyRoleType> roleType] { get; set; }

    object? this[IMetaAssociationType associationType] { get; }

    object? this[Func<IMetaAssociationType> associationType] { get; }

    IMetaObject? this[IMetaOneToAssociationType associationType] { get; }

    IMetaObject? this[Func<IMetaOneToAssociationType> associationType] { get; }

    IEnumerable<IMetaObject> this[IMetaManyToAssociationType associationType] { get; }

    IEnumerable<IMetaObject> this[Func<IMetaManyToAssociationType> associationType] { get; }

    void Add(IMetaToManyRoleType roleType, IMetaObject item);

    void Add(Func<IMetaToManyRoleType> roleType, IMetaObject item);

    void Remove(IMetaToManyRoleType roleType, IMetaObject item);

    void Remove(Func<IMetaToManyRoleType> roleType, IMetaObject item);
}
