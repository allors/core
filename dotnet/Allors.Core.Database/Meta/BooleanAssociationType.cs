namespace Allors.Core.Database.Meta;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// An association type handle for a boolean role.
/// </summary>
public sealed class BooleanAssociationType : MetaObject, IUnitAssociationType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BooleanAssociationType"/> class.
    /// </summary>
    public BooleanAssociationType(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }
}
