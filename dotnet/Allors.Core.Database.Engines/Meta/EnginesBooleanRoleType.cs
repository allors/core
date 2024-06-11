namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Database.Meta;
using Allors.Core.Database.Meta.Domain;
using Allors.Core.Meta;

/// <summary>
/// An engine unit role type.
/// </summary>
public sealed class EnginesBooleanRoleType(EnginesMeta enginesMeta, MetaObject metaObject)
    : EnginesUnitRoleType(enginesMeta, metaObject)
{
    private EnginesBooleanAssociationType? associationType;
    private EnginesUnit? unit;

    /// <inheritdoc/>
    public override EnginesAssociationType AssociationType => this.BooleanAssociationType;

    /// <summary>
    /// The association type.
    /// </summary>
    public EnginesBooleanAssociationType BooleanAssociationType => this.associationType ??=
        (EnginesBooleanAssociationType)this.EnginesMeta[this.MetaObject[this.M.MetaMeta.RoleTypeAssociationType()]!];

    /// <inheritdoc />
    public override EnginesObjectType ObjectType => this.Unit;

    /// <summary>
    /// The composite.
    /// </summary>
    public EnginesUnit Unit => this.unit ??= this.EnginesMeta[(Unit)this.MetaObject[this.M.MetaMeta.RoleTypeObjectType()]!];
}
