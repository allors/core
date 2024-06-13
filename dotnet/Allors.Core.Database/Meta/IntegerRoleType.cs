namespace Allors.Core.Database.Meta;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// A unit role type.
/// </summary>
public sealed class IntegerRoleType : MetaObject, IUnitRoleType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IntegerRoleType"/> class.
    /// </summary>
    public IntegerRoleType(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }
}
