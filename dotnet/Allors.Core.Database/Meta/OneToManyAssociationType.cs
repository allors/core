namespace Allors.Core.Database.Meta;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// An association type handle with multiplicity one to many.
/// </summary>
public sealed class OneToManyAssociationType : MetaObject, IOneToAssociationType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OneToManyAssociationType"/> class.
    /// </summary>
    public OneToManyAssociationType(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }
}
