namespace Allors.Core.Database.Meta
{
    using System;
    using System.Collections.Generic;
    using Allors.Core.Database.Meta.Handles;
    using Allors.Embedded.Domain;
    using Allors.Embedded.Meta;

    /// <summary>
    /// Meta Core.
    /// </summary>
    public sealed class Meta
    {
        private readonly IDictionary<MetaHandle, EmbeddedObject> metaObjectByMetaHandle;

        private readonly IDictionary<Guid, EmbeddedObject> metaObjectById;

        /// <summary>
        /// Initializes a new instance of the <see cref="Meta"/> class.
        /// </summary>
        public Meta()
        {
            this.metaObjectByMetaHandle = new Dictionary<MetaHandle, EmbeddedObject>();
            this.metaObjectById = new Dictionary<Guid, EmbeddedObject>();

            this.EmbeddedMeta = new EmbeddedMeta();
            this.EmbeddedPopulation = new EmbeddedPopulation();
        }

        /// <summary>
        /// The embedded meta.
        /// </summary>
        public EmbeddedMeta EmbeddedMeta { get; }

        /// <summary>
        /// The embedded population.
        /// </summary>
        public EmbeddedPopulation EmbeddedPopulation { get; set; }

        /// <summary>
        /// Gets meta object by meta handle.
        /// </summary>
        public IEnumerable<MetaHandle> MetaHandles => this.metaObjectByMetaHandle.Keys;

        /// <summary>
        /// Gets meta object by meta handle.
        /// </summary>
        public EmbeddedObject this[MetaHandle metaHandle] => this.metaObjectByMetaHandle[metaHandle];

        /// <summary>
        /// Gets meta object by meta handle.
        /// </summary>
        public EmbeddedObject this[Guid id] => this.metaObjectById[id];

        /// <summary>
        /// Add a new meta object.
        /// </summary>
        public void Add(MetaHandle metaHandle, EmbeddedObject embeddedObject)
        {
            this.metaObjectByMetaHandle.Add(metaHandle, embeddedObject);
            this.metaObjectById.Add(metaHandle.Id, embeddedObject);
        }
    }
}
