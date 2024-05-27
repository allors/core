namespace Allors.Core.Database.Engines.Meta;

using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using Allors.Core.Database.Meta;
using Allors.Core.Database.Meta.Domain;
using Allors.Core.Meta.Domain;

/// <summary>
/// An engine meta.
/// </summary>
public sealed class EnginesMeta
{
    private readonly FrozenDictionary<IMetaObject, EnginesMetaObject> mapping;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnginesMeta"/> class.
    /// </summary>
    public EnginesMeta(CoreMeta coreMeta)
    {
        this.CoreMeta = coreMeta;
        var m = this.CoreMeta.Meta;

        this.mapping = this.CoreMeta.MetaPopulation.Objects
            .Select(v =>
            {
                EnginesMetaObject metaObject = v switch
                {
                    Domain domain => new EnginesDomain(this, domain),
                    Workspace workspace => new EnginesWorkspace(this, workspace),
                    Class @class => new EnginesClass(this, @class),
                    Interface @interface => new EnginesInterface(this, @interface),
                    Unit unit => new EnginesUnit(this, unit),
                    MethodType methodType => new EnginesMethodType(this, methodType),
                    StringRoleType stringRoleType => new EnginesStringRoleType(this, stringRoleType),
                    OneToOneRoleType oneToOneRoleType => new EnginesOneToOneRoleType(this, oneToOneRoleType),
                    ManyToOneRoleType manyToOneRoleType => new EnginesManyToOneRoleType(this, manyToOneRoleType),
                    OneToManyRoleType oneToManyRoleType => new EnginesOneToManyRoleType(this, oneToManyRoleType),
                    ManyToManyRoleType manyToManyRoleType => new EnginesManyToManyRoleType(this, manyToManyRoleType),
                    StringAssociationType stringAssociationType => new EnginesStringAssociationType(this, stringAssociationType),
                    OneToOneAssociationType oneToOneAssociationType => new EnginesOneToOneAssociationType(this, oneToOneAssociationType),
                    OneToManyAssociationType oneToManyAssociationType => new EnginesOneToManyAssociationType(this, oneToManyAssociationType),
                    ManyToOneAssociationType manyToOneAssociationType => new EnginesManyToOneAssociationType(this, manyToOneAssociationType),
                    ManyToManyAssociationType manyToManyAssociationType => new EnginesManyToManyAssociationType(this, manyToManyAssociationType),
                    _ => new EnginesGenericMetaObject(this, (MetaObject)v),
                };

                return new KeyValuePair<IMetaObject, EnginesMetaObject>(v, metaObject);
            })
            .ToFrozenDictionary();
    }

    /// <summary>
    /// Core meta.
    /// </summary>
    public CoreMeta CoreMeta { get; }

    /// <summary>
    /// Lookup engines meta object.
    /// </summary>
    public EnginesMetaObject this[IMetaObject key] => this.mapping[key];

    /// <summary>
    /// Lookup engines composite.
    /// </summary>
    public EnginesComposite this[IComposite key] => (EnginesComposite)this.mapping[key];

    /// <summary>
    /// Lookup engines unit.
    /// </summary>
    public EnginesUnit this[Unit key] => (EnginesUnit)this.mapping[key];

    /// <summary>
    /// Lookup engines interface.
    /// </summary>
    public EnginesInterface this[Interface key] => (EnginesInterface)this.mapping[key];

    /// <summary>
    /// Lookup engines class.
    /// </summary>
    public EnginesClass this[Class key] => (EnginesClass)this.mapping[key];

    /// <summary>
    /// Lookup engines role type.
    /// </summary>
    public EnginesRoleType this[IRoleType key] => (EnginesRoleType)this.mapping[key];

    /// <summary>
    /// Lookup engines unit role type.
    /// </summary>
    public EnginesStringRoleType this[StringRoleType key] => (EnginesStringRoleType)this.mapping[key];

    /// <summary>
    /// Lookup engines to one role type.
    /// </summary>
    public EnginesToOneRoleType this[IToOneRoleType key] => (EnginesToOneRoleType)this.mapping[key];

    /// <summary>
    /// Lookup engines to many role type.
    /// </summary>
    public EnginesToManyRoleType this[IToManyRoleType key] => (EnginesToManyRoleType)this.mapping[key];

    /// <summary>
    /// Lookup engines association type.
    /// </summary>
    public EnginesAssociationType this[IAssociationType key] => (EnginesAssociationType)this.mapping[key];

    /// <summary>
    /// Lookup engines to one association type.
    /// </summary>
    public EnginesOneToAssociationType this[IOneToAssociationType key] => (EnginesOneToAssociationType)this.mapping[key];

    /// <summary>
    /// Lookup engines to many association type.
    /// </summary>
    public EnginesManyToAssociationType this[IManyToAssociationType key] => (EnginesManyToAssociationType)this.mapping[key];
}
