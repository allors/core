namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Database.Meta;
using Allors.Core.Meta;

/// <summary>
/// An engine role type handle with multiplicity many to many.
/// </summary>
public sealed class EnginesManyToManyRoleType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesToManyRoleType(enginesMeta, metaObject)
{
    private EnginesManyToManyAssociationType? associationType;

    /// <inheritdoc/>
    public override EnginesAssociationType AssociationType => this.ManyToManyAssociationType;

    /// <summary>
    /// The association type.
    /// </summary>
    public EnginesManyToManyAssociationType ManyToManyAssociationType => this.associationType ??= (EnginesManyToManyAssociationType)this.EnginesMeta[this.MetaObject[this.M.MetaMeta.RoleTypeAssociationType()]!];
}
