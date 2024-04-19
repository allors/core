namespace Allors.Custom.Database.Meta
{
    using System;
    using Allors.Core.Database.Meta;
    using Allors.Embedded.Domain;

    /// <summary>
    /// Meta for Tests.
    /// </summary>
    public sealed class TestsMeta
    {
        /// <summary>
        /// The id for I1.
        /// </summary>
        public static readonly Guid I1 = new("3578BB40-D85C-4A09-91E8-34614F436DB9");

        /// <summary>
        /// The id for C1.
        /// </summary>
        public static readonly Guid C1 = new("E1EBBF92-AB5C-45DB-89B8-20A534DFDF6F");

        /// <summary>
        /// Initializes a new instance of the <see cref="TestsMeta"/> class.
        /// </summary>
        public TestsMeta()
        {
            this.CoreMeta = new CoreMeta();

            var embeddedPopulation = this.CoreMeta.EmbeddedPopulation;

            var i1 = NewInterface(I1, "I1");
            var c1 = NewClass(C1, "C1");

            EmbeddedObject NewInterface(Guid id, string singularName, string? assignedPluralName = null)
            {
                return embeddedPopulation.Create(this.CoreMeta.Interface, v =>
                {
                    v[this.CoreMeta.MetaObjectId] = id;
                    v[this.CoreMeta.ObjectTypeSingularName] = singularName;
                    v[this.CoreMeta.ObjectTypeAssignedPluralName] = assignedPluralName;
                });
            }

            EmbeddedObject NewClass(Guid id, string singularName, string? assignedPluralName = null)
            {
                return embeddedPopulation.Create(this.CoreMeta.Class, v =>
                {
                    v[this.CoreMeta.MetaObjectId] = id;
                    v[this.CoreMeta.ObjectTypeSingularName] = singularName;
                    v[this.CoreMeta.ObjectTypeAssignedPluralName] = assignedPluralName;
                });
            }
        }

        public CoreMeta CoreMeta { get; set; }
    }
}
