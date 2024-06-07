namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// An inheritance.
/// </summary>
public sealed class Inheritance : MetaObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Inheritance"/> class.
    /// </summary>
    public Inheritance(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }
}
