namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Database.Meta;
using Allors.Core.Database.Meta.Domain;
using Allors.Core.Meta;

/// <summary>
/// An engine unit role type.
/// </summary>
public sealed class EnginesBinaryRoleType(EnginesMeta enginesMeta, MetaObject metaObject)
    : EnginesUnitRoleType(enginesMeta, metaObject)
{
    private EnginesBinaryAssociationType? associationType;
    private EnginesUnit? unit;

    /// <inheritdoc/>
    public override EnginesAssociationType AssociationType => this.BinaryAssociationType;

    /// <summary>
    /// The association type.
    /// </summary>
    public EnginesBinaryAssociationType BinaryAssociationType => this.associationType ??=
        (EnginesBinaryAssociationType)this.EnginesMeta[this.MetaObject[this.M.RoleTypeAssociationType()]!];

    /// <inheritdoc />
    public override EnginesObjectType ObjectType => this.Unit;

    /// <summary>
    /// The composite.
    /// </summary>
    public EnginesUnit Unit => this.unit ??= this.EnginesMeta[(Unit)this.MetaObject[this.M.RoleTypeObjectType()]!];
}
