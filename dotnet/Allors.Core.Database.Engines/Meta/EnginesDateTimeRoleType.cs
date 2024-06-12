namespace Allors.Core.Database.Engines.Meta;

using System;
using Allors.Core.Database.Meta;
using Allors.Core.Database.Meta.Domain;
using Allors.Core.Meta;

/// <summary>
/// An engine unit role type.
/// </summary>
public sealed class EnginesDateTimeRoleType(EnginesMeta enginesMeta, MetaObject metaObject)
    : EnginesUnitRoleType(enginesMeta, metaObject)
{
    private EnginesDateTimeAssociationType? associationType;
    private EnginesUnit? unit;

    /// <inheritdoc/>
    public override EnginesAssociationType AssociationType => this.DateTimeAssociationType;

    /// <summary>
    /// The association type.
    /// </summary>
    public EnginesDateTimeAssociationType DateTimeAssociationType => this.associationType ??=
        (EnginesDateTimeAssociationType)this.EnginesMeta[this.MetaObject[this.M.RoleTypeAssociationType]!];

    /// <inheritdoc />
    public override EnginesObjectType ObjectType => this.Unit;

    /// <summary>
    /// The composite.
    /// </summary>
    public EnginesUnit Unit => this.unit ??= this.EnginesMeta[(Unit)this.MetaObject[this.M.RoleTypeObjectType]!];

    /// <summary>
    /// Normalize the value.
    /// </summary>
    public static DateTime? Normalize(DateTime? value)
    {
        if (value is not { } dateTime || dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue)
        {
            return value;
        }

        dateTime = dateTime.Kind switch
        {
            DateTimeKind.Local => dateTime.ToUniversalTime(),
            DateTimeKind.Unspecified => throw new ArgumentException("DateTime value is of DateTimeKind.Kind Unspecified. \nUnspecified is only allowed for DateTime.MaxValue and DateTime.MinValue, use DateTimeKind.Utc or DateTimeKind.Local instead."),
            _ => dateTime,
        };

        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond, DateTimeKind.Utc);
    }
}
