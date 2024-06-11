namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// A decimal role type.
/// </summary>
public sealed class DecimalRoleType : MetaObject, IUnitRoleType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DecimalRoleType"/> class.
    /// </summary>
    public DecimalRoleType(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }
}
