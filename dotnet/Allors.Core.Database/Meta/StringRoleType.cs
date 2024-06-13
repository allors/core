namespace Allors.Core.Database.Meta;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// A string role type.
/// </summary>
public sealed class StringRoleType : MetaObject, IUnitRoleType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StringRoleType"/> class.
    /// </summary>
    public StringRoleType(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }
}
