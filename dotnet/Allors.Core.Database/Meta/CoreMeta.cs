namespace Allors.Core.Database.Meta;

using System;
using System.Linq;
using Allors.Core.Database.Meta.Domain;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// Core MetaMeta.
/// </summary>
public sealed class CoreMeta
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CoreMeta"/> class.
    /// </summary>
    public CoreMeta()
    {
        this.IsFrozen = false;
        this.CoreMetaMeta = new CoreMetaMeta();

        this.MetaMeta = this.CoreMetaMeta.MetaMeta;
        this.Meta = this.CoreMetaMeta.CreateMetaPopulation();

        foreach (var (id, domain) in this.MetaMeta.DomainById)
        {
            this.Meta.AddDomain(id, domain.Name);
        }

        foreach (var (id, objectType) in this.MetaMeta.ObjectTypeById)
        {
            var domain = (Domain.Domain)this[objectType.Domain.Id];

            switch (objectType.Kind)
            {
                case MetaObjectTypeKind.Unit:
                    this.Meta.AddUnit(domain, id, objectType.Name);
                    break;
                case MetaObjectTypeKind.Interface:
                    this.Meta.AddInterface(domain, id, objectType.Name);
                    break;
                case MetaObjectTypeKind.Class:
                    this.Meta.AddClass(domain, id, objectType.Name);
                    break;
            }
        }

        foreach (var (id, inheritance) in this.MetaMeta.InheritanceById)
        {
            var domain = (Domain.Domain)this[inheritance.Domain.Id];
            var subtype = (IComposite)this[inheritance.Subtype.Id];
            var supertype = (Interface)this[inheritance.Supertype.Id];
            this.Meta.AddInheritance(domain, id, subtype, supertype);
        }

        foreach (var (id, inheritance) in this.MetaMeta.InheritanceById)
        {
            var domain = (Domain.Domain)this[inheritance.Domain.Id];
            var subtype = (IComposite)this[inheritance.Subtype.Id];
            var supertype = (Interface)this[inheritance.Supertype.Id];
            this.Meta.AddInheritance(domain, id, subtype, supertype);
        }

        foreach (var (roleTypeId, roleType) in this.MetaMeta.RoleTypeById)
        {
            var domain = (Domain.Domain)this[roleType.Domain.Id];

            if (roleType is MetaUnitRoleType unitRoleType)
            {
                var unitAssociationType = unitRoleType.AssociationType;
                var associationObjectType = (IComposite)this[unitAssociationType.ObjectType.Id];
                var roleObjectType = (Unit)this[roleType.ObjectType.Id];

                if (unitRoleType.ObjectType.Id == CoreIds.Boolean)
                {
                    this.Meta.AddBooleanRelation(domain, unitAssociationType.Id, roleTypeId, associationObjectType, roleObjectType, unitRoleType.SingularName, unitRoleType.PluralName);
                }
                else if (unitRoleType.ObjectType.Id == CoreIds.Integer)
                {
                    this.Meta.AddIntegerRelation(domain, unitAssociationType.Id, roleTypeId, associationObjectType, roleObjectType, unitRoleType.SingularName, unitRoleType.PluralName);
                }
                else if (unitRoleType.ObjectType.Id == CoreIds.String)
                {
                    this.Meta.AddStringRelation(domain, unitAssociationType.Id, roleTypeId, associationObjectType, roleObjectType, unitRoleType.SingularName, unitRoleType.PluralName);
                }
                else if (unitRoleType.ObjectType.Id == CoreIds.Unique)
                {
                    this.Meta.AddUniqueRelation(domain, unitAssociationType.Id, roleTypeId, associationObjectType, roleObjectType, unitRoleType.SingularName, unitRoleType.PluralName);
                }
            }
            else if (roleType is MetaOneToOneRoleType oneToOneRoleType)
            {
                var oneToOneAssociationType = oneToOneRoleType.AssociationType;
                var associationObjectType = (IComposite)this[oneToOneAssociationType.ObjectType.Id];
                var roleObjectType = (IComposite)this[roleType.ObjectType.Id];

                this.Meta.AddOneToOneRelation(domain, oneToOneAssociationType.Id, roleTypeId, associationObjectType, roleObjectType, oneToOneRoleType.SingularName, oneToOneRoleType.PluralName);
            }
            else if (roleType is MetaOneToManyRoleType oneToManyRoleType)
            {
                var oneToOneAssociationType = oneToManyRoleType.AssociationType;
                var associationObjectType = (IComposite)this[oneToOneAssociationType.ObjectType.Id];
                var roleObjectType = (IComposite)this[roleType.ObjectType.Id];

                this.Meta.AddOneToManyRelation(domain, oneToOneAssociationType.Id, roleTypeId, associationObjectType, roleObjectType, oneToManyRoleType.SingularName, oneToManyRoleType.PluralName);
            }
            else if (roleType is MetaManyToOneRoleType manyToOneRoleType)
            {
                var oneToOneAssociationType = manyToOneRoleType.AssociationType;
                var associationObjectType = (IComposite)this[oneToOneAssociationType.ObjectType.Id];
                var roleObjectType = (IComposite)this[roleType.ObjectType.Id];

                this.Meta.AddManyToOneRelation(domain, oneToOneAssociationType.Id, roleTypeId, associationObjectType, roleObjectType, manyToOneRoleType.SingularName, manyToOneRoleType.PluralName);
            }
            else if (roleType is MetaManyToManyRoleType manyToManyRoleType)
            {
                var oneToOneAssociationType = manyToManyRoleType.AssociationType;
                var associationObjectType = (IComposite)this[oneToOneAssociationType.ObjectType.Id];
                var roleObjectType = (IComposite)this[roleType.ObjectType.Id];

                this.Meta.AddManyToManyRelation(domain, oneToOneAssociationType.Id, roleTypeId, associationObjectType, roleObjectType, manyToManyRoleType.SingularName, manyToManyRoleType.PluralName);
            }

            var d = (Domain.Domain)this[CoreIds.AllorsCore];

            var @class = (Class)this[CoreIds.Class];

            this.Meta.AddUnit(d, CoreIds.Decimal, "Decimal");
            this.Meta.AddUnit(d, CoreIds.Float, "Float");

            // Domain
            // Composites
            var @object = this.Meta.AddInterface(d, CoreIds.Object, "Object");

            // Relations
            this.Meta.AddManyToOneRelation(d, new Guid("9d2e16ae-8ce0-4b5b-9f39-284320de2452"), CoreIds.ObjectClass, @object, @class);
        }
    }

    /// <summary>
    /// MetaMeta MetaMeta.
    /// </summary>
    public CoreMetaMeta CoreMetaMeta { get; }

    /// <summary>
    /// Is this meta frozen.
    /// </summary>
    public bool IsFrozen { get; private set; }

    /// <summary>
    /// The meta meta.
    /// </summary>
    public MetaMeta MetaMeta { get; init; }

    /// <summary>
    /// The meta.
    /// </summary>
    public Meta Meta { get; init; }

    /// <summary>
    /// Looks up a meta object by id
    /// </summary>
    public IMetaObject this[Guid id]
    {
        get
        {
            // TODO: Add optimizaiton after freeze
            return this.Meta.Objects.First(v => ((Guid)v[this.CoreMetaMeta.MetaMeta.MetaObjectId()]!) == id);
        }
    }

    /// <summary>
    /// Freezes meta.
    /// </summary>
    public void Freeze()
    {
        if (!this.IsFrozen)
        {
            this.IsFrozen = true;
            this.Meta.Derive();

            // TODO: Add freeze to Allors.MetaMeta
            // this.MetaMeta.Freeze();
            // this.MetaMeta.Freeze();
        }
    }
}
