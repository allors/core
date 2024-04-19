namespace Allors.Core.Database.Meta
{
    using System;
    using System.Collections.Generic;
    using Allors.Embedded.Domain;

    /// <summary>
    /// The meta population.
    /// </summary>
    public sealed class Meta
    {
        private readonly Dictionary<Guid, MetaObject> metaObjectById;

        /// <summary>
        /// Initializes a new instance of the <see cref="Meta"/> class.
        /// </summary>
        internal Meta(CoreMeta meta)
        {
            this.EmbeddedPopulation = meta.EmbeddedPopulation;
            this.metaObjectById = [];

            foreach (var embeddedObject in this.EmbeddedPopulation.Objects)
            {
                MetaObject? metaObject = null;
                if (embeddedObject.ObjectType == meta.AssociationType)
                {
                    var roleType = embeddedObject[meta.RoleTypeAssociationType.AssociationType]!;

                    var roleMany = (bool)roleType[meta.RelationEndTypeIsMany]!;
                    var associationMany = (bool)embeddedObject[meta.RelationEndTypeIsMany]!;

                    metaObject = associationMany switch
                    {
                        true when roleMany => new ManyToManyAssociationType(this, embeddedObject),
                        true when !roleMany => new ManyToOneAssociationType(this, embeddedObject),
                        false when roleMany => new OneToManyAssociationType(this, embeddedObject),
                        _ => new OneToOneAssociationType(this, embeddedObject),
                    };
                }
                else if (embeddedObject.ObjectType == meta.Class)
                {
                    metaObject = new Class(this, embeddedObject);
                }
                else if (embeddedObject.ObjectType == meta.Domain)
                {
                    metaObject = new Domain(this, embeddedObject);
                }
                else if (embeddedObject.ObjectType == meta.Interface)
                {
                    metaObject = new Interface(this, embeddedObject);
                }
                else if (embeddedObject.ObjectType == meta.MethodType)
                {
                    metaObject = new MethodType(this, embeddedObject);
                }
                else if (embeddedObject.ObjectType == meta.RoleType)
                {
                    var associationType = embeddedObject[meta.RoleTypeAssociationType]!;

                    var roleMany = (bool)embeddedObject[meta.RelationEndTypeIsMany]!;
                    var associationMany = (bool)associationType[meta.RelationEndTypeIsMany]!;

                    metaObject = associationMany switch
                    {
                        true when roleMany => new ManyToManyRoleType(this, embeddedObject),
                        true when !roleMany => new ManyToOneRoleType(this, embeddedObject),
                        false when roleMany => new OneToManyRoleType(this, embeddedObject),
                        _ => new OneToOneRoleType(this, embeddedObject),
                    };
                }
                else if (embeddedObject.ObjectType == meta.Unit)
                {
                    metaObject = new Unit(this, embeddedObject);
                }
                else if (embeddedObject.ObjectType == meta.Workspace)
                {
                    metaObject = new Workspace(this, embeddedObject);
                }

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
