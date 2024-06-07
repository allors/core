namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// A unit role type handle.
/// </summary>
public sealed class FloatRoleType : MetaObject, IUnitRoleType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FloatRoleType"/> class.
    /// </summary>
    public FloatRoleType(MetaPopulation population, MetaObjectType objectType)
        : base(population, objectType)
    {
    }
}
