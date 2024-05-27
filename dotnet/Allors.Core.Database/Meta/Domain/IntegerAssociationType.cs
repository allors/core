namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta.Domain;
using Allors.Core.Meta.Meta;

/// <summary>
/// An association type handle for a unit role.
/// </summary>
public sealed class IntegerAssociationType : MetaObject, IUnitAssociationType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IntegerAssociationType"/> class.
    /// </summary>
    public IntegerAssociationType(MetaPopulation population, MetaObjectType objectType)
        : base(population, objectType)
    {
    }
}
