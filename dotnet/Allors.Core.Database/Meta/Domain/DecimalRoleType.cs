namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta;
using Allors.Core.Meta.Meta;

/// <summary>
/// A decimal role type handle.
/// </summary>
public sealed class DecimalRoleType : MetaObject, IUnitRoleType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DecimalRoleType"/> class.
    /// </summary>
    public DecimalRoleType(MetaPopulation population, MetaObjectType objectType)
        : base(population, objectType)
    {
    }
}
