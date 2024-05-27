namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta.Domain;
using Allors.Core.Meta.Meta;

/// <summary>
/// A unit role type handle.
/// </summary>
public sealed class StringRoleType : MetaObject, IUnitRoleType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StringRoleType"/> class.
    /// </summary>
    public StringRoleType(MetaPopulation population, MetaObjectType objectType)
        : base(population, objectType)
    {
    }
}
