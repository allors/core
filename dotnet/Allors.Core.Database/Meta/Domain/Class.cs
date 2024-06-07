namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// A class handle.
/// </summary>
public sealed class Class : MetaObject, IComposite
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Class"/> class.
    /// </summary>
    public Class(MetaPopulation population, MetaObjectType objectType)
        : base(population, objectType)
    {
    }

    /// <inheritdoc/>
    public override string ToString() => (string)this["SingularName"]!;
}
