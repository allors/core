namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Database.MetaMeta;
using Allors.Core.Meta;

/// <summary>
/// An engine role type handle with multiplicity one to one.
/// </summary>
public sealed class EnginesOneToOneRoleType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesToOneRoleType(enginesMeta, metaObject)
{
    private EnginesOneToOneAssociationType? associationType;

    /// <inheritdoc/>
    public override EnginesAssociationType AssociationType => this.OneToOneAssociationType;

    /// <summary>
    /// The association type.
    /// </summary>
    public EnginesOneToOneAssociationType OneToOneAssociationType => this.associationType ??= (EnginesOneToOneAssociationType)this.EnginesMeta[this.MetaObject[this.M.RoleTypeAssociationType]!];
}
