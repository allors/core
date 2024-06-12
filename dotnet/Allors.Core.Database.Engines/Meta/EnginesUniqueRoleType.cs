namespace Allors.Core.Database.Engines.Meta;

using System;
using Allors.Core.Database.Meta;
using Allors.Core.Database.Meta.Domain;
using Allors.Core.Meta;

/// <summary>
/// An engine unit role type.
/// </summary>
public sealed class EnginesUniqueRoleType(EnginesMeta enginesMeta, MetaObject metaObject)
    : EnginesUnitRoleType(enginesMeta, metaObject)
{
    private EnginesUniqueAssociationType? associationType;
    private EnginesUnit? unit;

    /// <inheritdoc/>
    public override EnginesAssociationType AssociationType => this.UniqueAssociationType;

    /// <summary>
    /// The association type.
    /// </summary>
    public EnginesUniqueAssociationType UniqueAssociationType => this.associationType ??=
        (EnginesUniqueAssociationType)this.EnginesMeta[this.MetaObject[this.M.RoleTypeAssociationType]!];

    /// <inheritdoc />
    public override EnginesObjectType ObjectType => this.Unit;

    /// <summary>
    /// The composite.
    /// </summary>
    public EnginesUnit Unit => this.unit ??= this.EnginesMeta[(Unit)this.MetaObject[this.M.RoleTypeObjectType]!];

    /// <summary>
    /// Normalize the value.
    /// </summary>
    public Guid? Normalize(Guid? value)
    {
        return Guid.Empty.Equals(value) ? null : value;
    }
}
