namespace Allors.Core.Database.Meta;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// An association type handle for a decimal role.
/// </summary>
public sealed class DecimalAssociationType : MetaObject, IUnitAssociationType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DecimalAssociationType"/> class.
    /// </summary>
    public DecimalAssociationType(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }
}
