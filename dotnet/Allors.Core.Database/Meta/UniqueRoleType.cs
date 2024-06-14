namespace Allors.Core.Database.Meta;

using Allors.Core.Database.MetaMeta;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// A unit role type.
/// </summary>
public sealed class UniqueRoleType : MetaObject, IUnitRoleType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UniqueRoleType"/> class.
    /// </summary>
    public UniqueRoleType(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }

    /// <inheritdoc/>
    public override string ToString() => (string)this[this.MetaMeta.RoleTypeSingularName]!;
}
