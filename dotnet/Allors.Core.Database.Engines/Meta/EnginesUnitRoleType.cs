namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Database.Meta.Domain;
using Allors.Core.Meta.Domain;

/// <summary>
/// An engine unit role type.
/// </summary>
public sealed class EnginesUnitRoleType(EnginesMeta enginesMeta, MetaObject metaObject)
    : EnginesRoleType(enginesMeta, metaObject)
{
    private EnginesUnitAssociationType? associationType;
    private EnginesUnit? unit;

    /// <inheritdoc/>
    public override EnginesAssociationType AssociationType => this.UnitAssociationType;

    /// <summary>
    /// The association type.
    /// </summary>
    public EnginesUnitAssociationType UnitAssociationType => this.associationType ??=
        (EnginesUnitAssociationType)this.EnginesMeta[this.MetaObject[this.M.RoleTypeAssociationType]!];

    /// <inheritdoc />
    public override EnginesObjectType ObjectType => this.Unit;

    /// <summary>
    /// The composite.
    /// </summary>
    public EnginesUnit Unit => this.unit ??= this.EnginesMeta[(Unit)this.MetaObject[this.M.RoleTypeObjectType]!];
}
