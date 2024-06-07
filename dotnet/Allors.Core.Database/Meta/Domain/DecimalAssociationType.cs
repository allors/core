namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// An association type handle for a decimal role.
/// </summary>
public sealed class DecimalAssociationType : MetaObject, IUnitAssociationType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DecimalAssociationType"/> class.
    /// </summary>
    public DecimalAssociationType(MetaPopulation population, MetaObjectType objectType)
        : base(population, objectType)
    {
    }
}
