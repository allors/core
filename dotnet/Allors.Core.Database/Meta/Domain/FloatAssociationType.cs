namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// An association type handle for a float role.
/// </summary>
public sealed class FloatAssociationType : MetaObject, IUnitAssociationType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FloatAssociationType"/> class.
    /// </summary>
    public FloatAssociationType(MetaPopulation population, MetaObjectType objectType)
        : base(population, objectType)
    {
    }
}
