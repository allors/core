namespace Allors.Core.Meta;

using System;
using System.Collections.Generic;
using Allors.Core.MetaMeta;

public class MetaObject(Meta meta, MetaObjectType objectType)
    : IMetaObject
{
    public Meta Meta { get; } = meta;

    public MetaObjectType ObjectType { get; } = objectType;

    public object? this[string name]
    {
        get
        {
            if (this.ObjectType.RoleTypeByName.TryGetValue(name, out var roleType))
            {
                return this[roleType];
            }

            if (this.ObjectType.AssociationTypeByName.TryGetValue(name, out var associationType))
            {
                return this[associationType];
            }

            throw new ArgumentException("Unknown role or association", name);
        }

        set
        {
            if (!this.ObjectType.RoleTypeByName.TryGetValue(name, out var roleType))
            {
                throw new ArgumentException("Unknown role", name);
            }

            this[roleType] = value;
        }
    }

    public object? this[IMetaRoleType roleType]
    {
        get => roleType switch
        {
            MetaUnitRoleType unitRoleType => this[unitRoleType],
            IMetaToOneRoleType toOneRoleType => this[toOneRoleType],
            IMetaToManyRoleType toManyRoleType => this[toManyRoleType],
            _ => throw new InvalidOperationException(),
        };
        set
        {
            switch (roleType)
            {
                case MetaUnitRoleType unitRoleType:
                    this[unitRoleType] = value;
                    return;

                case IMetaToOneRoleType toOneRoleType:
                    this[toOneRoleType] = (IMetaObject?)value;
                    return;

                case IMetaToManyRoleType toManyRoleType:
                    this[toManyRoleType] = (IEnumerable<IMetaObject>)(value ?? Array.Empty<IMetaObject>());
                    return;

                default:
                    throw new InvalidOperationException();
            }
        }
    }

    public object? this[MetaUnitRoleType roleType]
    {
        get => this.Meta.GetUnitRole(this, roleType);
        set => this.Meta.SetUnitRole(this, roleType, value);
    }

    public IMetaObject? this[IMetaToOneRoleType roleType]
    {
        get => this.Meta.GetToOneRole(this, roleType);
        set => this.Meta.SetToOneRole(this, roleType, value);
    }

    public IEnumerable<IMetaObject> this[IMetaToManyRoleType roleType]
    {
        get => this.Meta.GetToManyRole(this, roleType) ?? [];
        set => this.Meta.SetToManyRole(this, roleType, value);
    }

    public object? this[IMetaAssociationType associationType] => associationType switch
    {
        IMetaOneToAssociationType oneToAssociationType => this[oneToAssociationType],
        IMetaManyToAssociationType manyToAssociationType => this[manyToAssociationType],
        _ => throw new InvalidOperationException(),
    };

    public IMetaObject? this[IMetaOneToAssociationType associationType] => this.Meta.GetToOneAssociation(this, associationType);

    public IEnumerable<IMetaObject> this[IMetaManyToAssociationType associationType] => this.Meta.GetToManyAssociation(this, associationType) ?? [];

    public void Add(IMetaToManyRoleType roleType, IMetaObject item) => this.Meta.AddToManyRole(this, roleType, item);

    public void Remove(IMetaToManyRoleType roleType, IMetaObject item) => this.Meta.RemoveToManyRole(this, roleType, item);
}
