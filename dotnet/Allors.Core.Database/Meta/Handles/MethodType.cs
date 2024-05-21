namespace Allors.Core.Database.Meta.Handles
{
    using Allors.Core.Meta.Domain;
    using Allors.Core.Meta.Meta;

    /// <summary>
    /// A method type handle.
    /// </summary>
    public sealed class MethodType : MetaObject, IOperandType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodType"/> class.
        /// </summary>
        public MethodType(MetaPopulation population, MetaObjectType objectType)
            : base(population, objectType)
        {
        }
    }
}
