namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// An association type handle with multiplicity many to many.
/// </summary>
public sealed class ManyToManyAssociationType : MetaObject, IManyToAssociationType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ManyToManyAssociationType"/> class.
    /// </summary>
    public ManyToManyAssociationType(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }
}
