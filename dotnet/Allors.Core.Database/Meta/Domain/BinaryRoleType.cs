namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta;
using Allors.Core.Meta.Meta;

/// <summary>
/// A binary role type handle.
/// </summary>
public sealed class BinaryRoleType : MetaObject, IUnitRoleType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryRoleType"/> class.
    /// </summary>
    public BinaryRoleType(MetaPopulation population, MetaObjectType objectType)
        : base(population, objectType)
    {
    }
}
