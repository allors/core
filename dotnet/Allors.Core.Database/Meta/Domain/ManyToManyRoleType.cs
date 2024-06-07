namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// A role type handle with multiplicity many to many.
/// </summary>
public sealed class ManyToManyRoleType : MetaObject, IToManyRoleType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ManyToManyRoleType"/> class.
    /// </summary>
    public ManyToManyRoleType(MetaPopulation population, MetaObjectType objectType)
        : base(population, objectType)
    {
    }
}
