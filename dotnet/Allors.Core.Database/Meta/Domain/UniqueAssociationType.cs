namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// An association type handle for a unit role.
/// </summary>
public sealed class UniqueAssociationType : MetaObject, IUnitAssociationType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UniqueAssociationType"/> class.
    /// </summary>
    public UniqueAssociationType(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }
}
