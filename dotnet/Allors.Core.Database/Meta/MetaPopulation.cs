namespace Allors.Core.Database.Meta
{
    using System;
    using System.Collections.Generic;
    using Allors.Embedded.Domain;

    /// <summary>
    /// The meta population.
    /// </summary>
    public sealed class MetaPopulation
    {
        private readonly Dictionary<Guid, MetaObject> metaObjectById;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaPopulation"/> class.
        /// </summary>
        public MetaPopulation(EmbeddedPopulation embeddedPopulation)
        {
            this.EmbeddedPopulation = embeddedPopulation;
            this.metaObjectById = [];

            foreach (var embeddedObject in embeddedPopulation.Objects)
            {
                MetaObject? metaObject = embeddedObject.ObjectType.Name switch
                {
                    "Class" => new Class(this, embeddedObject),
                    "Interface" => new Interface(this, embeddedObject),
                    "Unit" => new Unit(this, embeddedObject),
                    _ => null,
                };

                if (metaObject != null)
                {
                    this.metaObjectById.Add(metaObject.Id, metaObject);
                }
            }
        }

        /// <summary>
        /// The embedded population.
        /// </summary>
        public EmbeddedPopulation EmbeddedPopulation { get; }

        /// <summary>
        /// Lookup meta object by id.
        /// </summary>
        public IReadOnlyDictionary<Guid, MetaObject> MetaObjectById => this.metaObjectById;
    }
}
