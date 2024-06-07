namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta;
using Allors.Core.Meta.Meta;

/// <summary>
/// An association type handle with multiplicity many to one.
/// </summary>
public sealed class ManyToOneAssociationType : MetaObject, IManyToAssociationType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ManyToOneAssociationType"/> class.
    /// </summary>
    public ManyToOneAssociationType(MetaPopulation population, MetaObjectType objectType)
        : base(population, objectType)
    {
    }
}
