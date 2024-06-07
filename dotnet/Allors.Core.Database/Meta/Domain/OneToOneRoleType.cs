namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta;
using Allors.Core.Meta.Meta;

/// <summary>
/// A role type handle with multiplicity one to one.
/// </summary>
public sealed class OneToOneRoleType : MetaObject, IToOneRoleType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OneToOneRoleType"/> class.
    /// </summary>
    public OneToOneRoleType(MetaPopulation population, MetaObjectType objectType)
        : base(population, objectType)
    {
    }

    /// <inheritdoc/>
    public override string ToString() => (string)this["SingularName"]!;
}
