namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// A binary role type handle.
/// </summary>
public sealed class BinaryRoleType : MetaObject, IUnitRoleType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryRoleType"/> class.
    /// </summary>
    public BinaryRoleType(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }
}
