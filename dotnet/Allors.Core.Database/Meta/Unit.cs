namespace Allors.Core.Database.Meta;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// A unit.
/// </summary>
public sealed class Unit : MetaObject, IObjectType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Unit"/> class.
    /// </summary>
    public Unit(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }

    /// <inheritdoc/>
    public override string ToString() => (string)this["SingularName"]!;
}
