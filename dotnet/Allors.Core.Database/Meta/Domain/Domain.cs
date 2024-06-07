namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// A domain handle.
/// </summary>
public class Domain : MetaObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Domain"/> class.
    /// </summary>
    public Domain(MetaPopulation population, MetaObjectType objectType)
        : base(population, objectType)
    {
    }

    /// <inheritdoc/>
    public override string ToString() => (string)this["Name"]!;
}
