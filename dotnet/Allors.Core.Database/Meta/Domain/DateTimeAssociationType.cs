namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// An association type handle for a dateTime role.
/// </summary>
public sealed class DateTimeAssociationType : MetaObject, IUnitAssociationType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DateTimeAssociationType"/> class.
    /// </summary>
    public DateTimeAssociationType(MetaPopulation population, MetaObjectType objectType)
        : base(population, objectType)
    {
    }
}
