namespace Allors.Core.Database.Engines.Meta
{
    using System;
    using System.Collections.Frozen;
    using System.Collections.Generic;
    using System.Linq;
    using Allors.Core.Database.Meta;
    using Allors.Core.Database.Meta.Domain;
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine meta.
    /// </summary>
    public sealed class EngineMeta
    {
        private readonly FrozenDictionary<IMetaObject, EngineMetaObject> mapping;

        /// <summary>
        /// Initializes a new instance of the <see cref="EngineMeta"/> class.
        /// </summary>
        public EngineMeta(CoreMeta coreMeta)
        {
            this.CoreMeta = coreMeta;
            var m = this.CoreMeta.Meta;

            this.mapping = this.CoreMeta.MetaPopulation.Objects
                .Select(v =>
                {
                    EngineMetaObject metaObject = v switch
                    {
                        Domain domain => new EngineDomain(this, domain),
                        Workspace workspace => new EngineWorkspace(this, workspace),
                        Class @class => new EngineClass(this, @class),
                        Interface @interface => new EngineInterface(this, @interface),
                        Unit unit => new EngineUnit(this, unit),
                        MethodType methodType => new EngineMethodType(this, methodType),
                        UnitRoleType unitRoleType => new EngineUnitRoleType(this, unitRoleType),
                        OneToOneRoleType oneToOneRoleType => new EngineOneToOneRoleType(this, oneToOneRoleType),
                        ManyToOneRoleType manyToOneRoleType => new EngineManyToOneRoleType(this, manyToOneRoleType),
                        OneToManyRoleType oneToManyRoleType => new EngineOneToManyRoleType(this, oneToManyRoleType),
                        ManyToManyRoleType manyToManyRoleType => new EngineManyToManyRoleType(this, manyToManyRoleType),
                        OneToOneAssociationType oneToOneAssociationType => new EngineOneToOneAssociationType(this, oneToOneAssociationType),
                        OneToManyAssociationType oneToManyAssociationType => new EngineOneToManyAssociationType(this, oneToManyAssociationType),
                        ManyToOneAssociationType manyToOneAssociationType => new EngineManyToOneAssociationType(this, manyToOneAssociationType),
                        ManyToManyAssociationType manyToManyAssociationType => new EngineManyToManyAssociationType(this, manyToManyAssociationType),
                        _ => new EngineGenericMetaObject(this, (MetaObject)v),
                    };

                    return new KeyValuePair<IMetaObject, EngineMetaObject>(v, metaObject);
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
        public EngineMetaObject this[IMetaObject key] => this.mapping[key];

        /// <summary>
        /// Lookup engines role type.
        /// </summary>
        public EngineRoleType this[IRoleType key] => (EngineRoleType)this.mapping[key];

        /// <summary>
        /// Lookup engines unit role type.
        /// </summary>
        public EngineUnitRoleType this[UnitRoleType key] => (EngineUnitRoleType)this.mapping[key];

        /// <summary>
        /// Lookup engines to one role type.
        /// </summary>
        public EngineToOneRoleType this[IToOneRoleType key] => (EngineToOneRoleType)this.mapping[key];

        /// <summary>
        /// Lookup engines to many role type.
        /// </summary>
        public EngineToManyRoleType this[IToManyRoleType key] => (EngineToManyRoleType)this.mapping[key];

        /// <summary>
        /// Lookup engines association type.
        /// </summary>
        public EngineAssociationType this[IAssociationType key] => (EngineAssociationType)this.mapping[key];

        /// <summary>
        /// Lookup engines to one association type.
        /// </summary>
        public EngineOneToAssociationType this[IOneToAssociationType key] => (EngineOneToAssociationType)this.mapping[key];

        /// <summary>
        /// Lookup engines to many association type.
        /// </summary>
        public EngineManyToAssociationType this[IManyToAssociationType key] => (EngineManyToAssociationType)this.mapping[key];
    }
}
