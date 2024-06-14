namespace Allors.Core.Database.Meta;

using Allors.Core.Database.MetaMeta;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// A unit.
/// </summary>
public sealed class Interface : MetaObject, IComposite
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Interface"/> class.
    /// </summary>
    public Interface(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }

    /// <inheritdoc/>
    public override string ToString() => (string)this[this.MetaMeta.ObjectTypeSingularName]!;
}
