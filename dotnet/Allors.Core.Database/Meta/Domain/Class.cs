namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// A class.
/// </summary>
public sealed class Class : MetaObject, IComposite
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Class"/> class.
    /// </summary>
    public Class(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }

    /// <inheritdoc/>
    public override string ToString() => (string)this["SingularName"]!;
}
