namespace Allors.Core.Meta.Domain
{
    using System;
    using System.Collections.Generic;
    using Allors.Core.Meta.Meta;

    public class MetaObject(MetaPopulation population, MetaObjectType objectType)
        : IMetaObject
    {
        public MetaPopulation Population { get; } = population;

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
            get => this.Population.GetUnitRole(this, roleType);
            set => this.Population.SetUnitRole(this, roleType, value);
        }

        public IMetaObject? this[IMetaToOneRoleType roleType]
        {
            get => this.Population.GetToOneRole(this, roleType);
            set
            {
                this.Population.SetToOneRole(this, roleType, value);
            }
        }

        public IEnumerable<IMetaObject> this[IMetaToManyRoleType roleType]
        {
            get => this.Population.GetToManyRole(this, roleType) ?? [];
            set => this.Population.SetToManyRole(this, roleType, value);
        }

        public object? this[IMetaAssociationType associationType] => associationType switch
        {
            IMetaOneToAssociationType oneToAssociationType => this[oneToAssociationType],
            IMetaManyToAssociationType manyToAssociationType => this[manyToAssociationType],
            _ => throw new InvalidOperationException(),
        };

        public IMetaObject? this[IMetaOneToAssociationType associationType] => this.Population.GetToOneAssociation(this, associationType);

        public IEnumerable<IMetaObject> this[IMetaManyToAssociationType associationType] => this.Population.GetToManyAssociation(this, associationType) ?? [];

        public void Add(IMetaToManyRoleType roleType, IMetaObject item) => this.Population.AddToManyRole(this, roleType, item);

        public void Add(IMetaToManyRoleType roleType, params IMetaObject[] items) => this.Population.AddToManyRole(this, roleType, items);

        public void Remove(IMetaToManyRoleType roleType, IMetaObject item) => this.Population.RemoveToManyRole(this, roleType, item);

        public void Remove(IMetaToManyRoleType roleType, params IMetaObject[] items) => this.Population.RemoveToManyRole(this, roleType, items);
    }
}
