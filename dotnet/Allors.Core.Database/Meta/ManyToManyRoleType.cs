namespace Allors.Core.Database.Meta;

using Allors.Core.Database.MetaMeta;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// A role type handle with multiplicity many to many.
/// </summary>
public sealed class ManyToManyRoleType : MetaObject, IToManyRoleType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ManyToManyRoleType"/> class.
    /// </summary>
    public ManyToManyRoleType(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }

    /// <inheritdoc/>
    public override string ToString() => (string)this[this.MetaMeta.RoleTypeSingularName]!;
}
