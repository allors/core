namespace Allors.Core.Database.Engines.Meta
{
    using System;
    using Allors.Core.Database.Meta;
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine meta object.
    /// </summary>
    public abstract class EngineMetaObject(EngineMeta engineMeta, MetaObject metaObject)
    {
        private Guid? id;

        /// <summary>
        /// The engines meta.
        /// </summary>
        public EngineMeta EngineMeta { get; } = engineMeta;

        /// <summary>
        /// The core meta meta.
        /// </summary>
        public CoreMetaMeta M => this.EngineMeta.CoreMeta.Meta;

        /// <summary>
        /// The meta object.
        /// </summary>
        public MetaObject MetaObject { get; } = metaObject;

        /// <summary>
        /// The id.
        /// </summary>
        public Guid Id => this.id ??= (Guid)this.MetaObject[this.M.MetaObjectId]!;
    }
}
