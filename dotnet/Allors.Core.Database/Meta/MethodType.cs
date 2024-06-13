namespace Allors.Core.Database.Meta;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// A mehtod type.
/// </summary>
public sealed class MethodType : MetaObject, IComposite
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MethodType"/> class.
    /// </summary>
    public MethodType(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }

    /// <inheritdoc/>
    public override string ToString() => (string)this["Name"]!;
}
