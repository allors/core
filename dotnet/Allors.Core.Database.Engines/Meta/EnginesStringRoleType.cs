namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Database.Meta.Domain;
using Allors.Core.Meta.Domain;

/// <summary>
/// An engine unit role type.
/// </summary>
public sealed class EnginesStringRoleType(EnginesMeta enginesMeta, MetaObject metaObject)
    : EnginesUnitRoleType(enginesMeta, metaObject)
{
    private EnginesStringAssociationType? associationType;
    private EnginesUnit? unit;

    /// <inheritdoc/>
    public override EnginesAssociationType AssociationType => this.StringAssociationType;

    /// <summary>
    /// The association type.
    /// </summary>
    public EnginesStringAssociationType StringAssociationType => this.associationType ??=
        (EnginesStringAssociationType)this.EnginesMeta[this.MetaObject[this.M.RoleTypeAssociationType]!];

    /// <inheritdoc />
    public override EnginesObjectType ObjectType => this.Unit;

    /// <summary>
    /// The composite.
    /// </summary>
    public EnginesUnit Unit => this.unit ??= this.EnginesMeta[(Unit)this.MetaObject[this.M.RoleTypeObjectType]!];
}
