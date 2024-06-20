namespace Allors.Core.Database.Meta;

using Allors.Core.Database.MetaMeta;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// A method part.
/// </summary>
public sealed class MethodPart : MetaObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MethodPart"/> class.
    /// </summary>
    public MethodPart(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        var m = this.MetaMeta;

        var domain = this[m.MethodPartDomain];
        var composite = this[m.MethodPartComposite];
        var methodType = this[m.MethodTypeMethodParts().AssociationType];
        return $"{methodType}({domain}:{composite})";
    }
}
