namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// A role type handle with multiplicity many to one.
/// </summary>
public sealed class ManyToOneRoleType : MetaObject, IToOneRoleType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ManyToOneRoleType"/> class.
    /// </summary>
    public ManyToOneRoleType(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }
}
