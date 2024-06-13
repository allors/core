namespace Allors.Core.Database.Meta;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// A role type handle with multiplicity one to one.
/// </summary>
public sealed class OneToOneRoleType : MetaObject, IToOneRoleType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OneToOneRoleType"/> class.
    /// </summary>
    public OneToOneRoleType(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }

    /// <inheritdoc/>
    public override string ToString() => (string)this["SingularName"]!;
}
