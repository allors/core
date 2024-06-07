namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta;
using Allors.Core.Meta.Meta;

/// <summary>
/// An association type handle with multiplicity one to one.
/// </summary>
public sealed class OneToOneAssociationType : MetaObject, IOneToAssociationType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OneToOneAssociationType"/> class.
    /// </summary>
    public OneToOneAssociationType(MetaPopulation population, MetaObjectType objectType)
        : base(population, objectType)
    {
    }
}
