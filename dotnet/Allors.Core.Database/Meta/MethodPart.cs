namespace Allors.Core.Database.Meta;

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
}
