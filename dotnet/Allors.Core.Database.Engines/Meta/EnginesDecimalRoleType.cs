namespace Allors.Core.Database.Engines.Meta;

using System;
using System.Data.SqlTypes;
using Allors.Core.Database.Meta.Domain;
using Allors.Core.Meta.Domain;

/// <summary>
/// An engine unit role type.
/// </summary>
public sealed class EnginesDecimalRoleType(EnginesMeta enginesMeta, MetaObject metaObject)
    : EnginesUnitRoleType(enginesMeta, metaObject)
{
    private EnginesDecimalAssociationType? associationType;
    private EnginesUnit? unit;
    private int? precision;
    private int? scale;

    /// <inheritdoc/>
    public override EnginesAssociationType AssociationType => this.DecimalAssociationType;

    /// <summary>
    /// The association type.
    /// </summary>
    public EnginesDecimalAssociationType DecimalAssociationType => this.associationType ??=
        (EnginesDecimalAssociationType)this.EnginesMeta[this.MetaObject[this.M.RoleTypeAssociationType]!];

    /// <inheritdoc />
    public override EnginesObjectType ObjectType => this.Unit;

    /// <summary>
    /// The composite.
    /// </summary>
    public EnginesUnit Unit => this.unit ??= this.EnginesMeta[(Unit)this.MetaObject[this.M.RoleTypeObjectType]!];

    /// <summary>
    /// The precision.
    /// </summary>
    public int Precision => this.precision ??= (int)this.MetaObject[this.M.DecimalRoleTypeDerivedPrecision]!;

    /// <summary>
    /// The size.
    /// </summary>
    public int Scale => this.scale ??= (int)this.MetaObject[this.M.DecimalRoleTypeDerivedScale]!;

    /// <summary>
    /// Normalize the value.
    /// </summary>
    public decimal? Normalize(decimal? value)
    {
        if (value is not { } @decimal)
        {
            return value;
        }

        SqlDecimal sqlDecimal = @decimal;

        if (sqlDecimal.Precision > this.Precision)
        {
            throw new ArgumentException("Precision of " + this.Name + " is too great (" + sqlDecimal.Precision + ">" + this.Scale + ").");
        }

        if (sqlDecimal.Scale > this.Scale)
        {
            throw new ArgumentException("Scale of " + this.Name + " is too great (" + sqlDecimal.Scale + ">" + this.Scale + ").");
        }

        return value;
    }
}
