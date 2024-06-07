namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta;
using Allors.Core.Meta.Meta;

/// <summary>
/// An association type handle for a boolean role.
/// </summary>
public sealed class BooleanAssociationType : MetaObject, IUnitAssociationType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BooleanAssociationType"/> class.
    /// </summary>
    public BooleanAssociationType(MetaPopulation population, MetaObjectType objectType)
        : base(population, objectType)
    {
    }
}
