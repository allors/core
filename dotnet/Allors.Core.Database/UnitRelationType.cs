namespace Allors.Core.Database
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// A unit relation type.
    /// </summary>
    public sealed record UnitRelationType : IRelationType
    {
        private UnitRelationType(EmbeddedObject embeddedObject)
        {
            this.EmbeddedObject = embeddedObject;
        }

        /// <summary>
        /// The embedded object.
        /// </summary>
        public EmbeddedObject EmbeddedObject { get; }

        /// <summary>
        /// Implicitly cast from EmbeddedObject.
        /// </summary>
        public static implicit operator UnitRelationType(EmbeddedObject embeddedObject)
        {
            return new UnitRelationType(embeddedObject);
        }

        /// <summary>
        /// Implicitly cast to EmbeddedObject.
        /// </summary>
        public static implicit operator EmbeddedObject(UnitRelationType unitRelationType)
        {
            return unitRelationType.EmbeddedObject;
        }
    }
}
