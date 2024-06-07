namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// An association type handle for a unit role.
/// </summary>
public sealed class IntegerAssociationType : MetaObject, IUnitAssociationType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IntegerAssociationType"/> class.
    /// </summary>
    public IntegerAssociationType(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }
}
