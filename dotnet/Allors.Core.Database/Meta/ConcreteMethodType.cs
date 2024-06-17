namespace Allors.Core.Database.Meta;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// A concrete method type.
/// </summary>
public sealed class ConcreteMethodType : MetaObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConcreteMethodType"/> class.
    /// </summary>
    public ConcreteMethodType(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }
}
