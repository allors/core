namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta;
using Allors.Core.Meta.Meta;

/// <summary>
/// An association type handle for a binary role.
/// </summary>
public sealed class BinaryAssociationType : MetaObject, IUnitAssociationType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryAssociationType"/> class.
    /// </summary>
    public BinaryAssociationType(MetaPopulation population, MetaObjectType objectType)
        : base(population, objectType)
    {
    }
}
