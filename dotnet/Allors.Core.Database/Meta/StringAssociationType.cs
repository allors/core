namespace Allors.Core.Database.Meta;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// An association type handle for a string role.
/// </summary>
public sealed class StringAssociationType : MetaObject, IUnitAssociationType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StringAssociationType"/> class.
    /// </summary>
    public StringAssociationType(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }
}
