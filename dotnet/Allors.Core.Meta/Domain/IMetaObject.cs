namespace Allors.Core.Meta.Domain;

using System.Collections.Generic;
using Allors.Core.Meta.Meta;

public interface IMetaObject
{
    MetaPopulation Population { get; }

    MetaObjectType ObjectType { get; }

    object? this[string name] { get; set; }

    object? this[IMetaRoleType roleType] { get; set; }

    object? this[MetaUnitRoleType roleType] { get; set; }

    IMetaObject? this[IMetaToOneRoleType roleType] { get; set; }

    IEnumerable<IMetaObject> this[IMetaToManyRoleType roleType] { get; set; }

    object? this[IMetaAssociationType associationType] { get; }

    IMetaObject? this[IMetaOneToAssociationType associationType] { get; }

    IEnumerable<IMetaObject> this[IMetaManyToAssociationType associationType] { get; }

    void Add(IMetaToManyRoleType roleType, IMetaObject item);

    void Remove(IMetaToManyRoleType roleType, IMetaObject item);
}
