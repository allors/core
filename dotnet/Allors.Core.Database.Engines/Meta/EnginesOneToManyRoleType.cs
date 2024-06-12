namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Database.Meta;
using Allors.Core.Meta;

/// <summary>
/// An engine role type handle with multiplicity one to many.
/// </summary>
public sealed class EnginesOneToManyRoleType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesToManyRoleType(enginesMeta, metaObject)
{
    private EnginesOneToManyAssociationType? associationType;

    /// <inheritdoc/>
    public override EnginesAssociationType AssociationType => this.OneToManyAssociationType;

    /// <summary>
    /// The association type.
    /// </summary>
    public EnginesOneToManyAssociationType OneToManyAssociationType => this.associationType ??= (EnginesOneToManyAssociationType)this.EnginesMeta[this.MetaObject[this.M.RoleTypeAssociationType]!];
}
