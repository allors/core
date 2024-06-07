namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// A boolean role type handle.
/// </summary>
public sealed class BooleanRoleType : MetaObject, IUnitRoleType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BooleanRoleType"/> class.
    /// </summary>
    public BooleanRoleType(MetaPopulation population, MetaObjectType objectType)
        : base(population, objectType)
    {
    }
}
