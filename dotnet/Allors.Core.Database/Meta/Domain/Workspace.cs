namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta.Domain;
using Allors.Core.Meta.Meta;

/// <summary>
/// A workspace handle.
/// </summary>
public sealed class Workspace : MetaObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Workspace"/> class.
    /// </summary>
    public Workspace(MetaPopulation population, MetaObjectType objectType)
        : base(population, objectType)
    {
    }
}
