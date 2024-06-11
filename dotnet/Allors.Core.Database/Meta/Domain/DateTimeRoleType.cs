namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// A dateTime role type.
/// </summary>
public sealed class DateTimeRoleType : MetaObject, IUnitRoleType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DateTimeRoleType"/> class.
    /// </summary>
    public DateTimeRoleType(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }
}
