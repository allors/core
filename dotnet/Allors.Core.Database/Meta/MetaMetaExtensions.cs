namespace Allors.Core.Database.Meta;

using Allors.Core.MetaMeta;

/// <summary>
/// MetaMeta Extensions.
/// </summary>
internal static class MetaMetaExtensions
{
    /// <summary>
    /// An association type.
    /// </summary>
    internal static MetaObjectType AssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.AssociationType];

    /// <summary>
    /// The composite of an association type.
    /// </summary>
    internal static MetaManyToOneRoleType AssociationTypeComposite(this MetaMeta @this) => (MetaManyToOneRoleType)@this.RoleTypeById[CoreIds.AssociationTypeComposite];

    /// <summary>
    /// A class.
    /// </summary>
    internal static MetaObjectType Class(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.Class];

    /// <summary>
    /// A composite.
    /// </summary>
    internal static MetaObjectType Composite(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.Composite];

    /// <summary>
    /// Composite association type.
    /// </summary>
    internal static MetaObjectType CompositeAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.CompositeAssociationType];

    /// <summary>
    /// Composite role type.
    /// </summary>
    internal static MetaObjectType CompositeRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.CompositeRoleType];

    /// <summary>
    /// The supertypes of a composite.
    /// </summary>
    internal static MetaManyToManyRoleType CompositeSupertypes(this MetaMeta @this) => (MetaManyToManyRoleType)@this.RoleTypeById[CoreIds.CompositeSupertypes];

    /// <summary>
    /// The direct supertypes of a composite.
    /// </summary>
    internal static MetaManyToManyRoleType CompositeDirectSupertypes(this MetaMeta @this) => (MetaManyToManyRoleType)@this.RoleTypeById[CoreIds.CompositeDirectSupertypes];

    /// <summary>
    /// A domain.
    /// </summary>
    internal static MetaObjectType Domain(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.Domain];

    /// <summary>
    /// The name of a domain.
    /// </summary>
    internal static MetaUnitRoleType DomainName(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreIds.DomainName];

    /// <summary>
    /// The super domains of a domain.
    /// </summary>
    internal static MetaManyToManyRoleType DomainSuperdomains(this MetaMeta @this) => (MetaManyToManyRoleType)@this.RoleTypeById[CoreIds.DomainSuperdomains];

    /// <summary>
    /// The types of a domain.
    /// </summary>
    internal static MetaManyToManyRoleType DomainTypes(this MetaMeta @this) => (MetaManyToManyRoleType)@this.RoleTypeById[CoreIds.DomainTypes];

    /// <summary>
    /// An inheritance.
    /// </summary>
    internal static MetaObjectType Inheritance(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.Inheritance];

    /// <summary>
    /// The subtype of an inheritance.
    /// </summary>
    internal static MetaManyToOneRoleType InheritanceSubtype(this MetaMeta @this) => (MetaManyToOneRoleType)@this.RoleTypeById[CoreIds.InheritanceSubtype];

    /// <summary>
    /// The supertype of an inheritance.
    /// </summary>
    internal static MetaManyToOneRoleType InheritanceSupertype(this MetaMeta @this) => (MetaManyToOneRoleType)@this.RoleTypeById[CoreIds.InheritanceSupertype];

    /// <summary>
    /// An interface.
    /// </summary>
    internal static MetaObjectType Interface(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.Interface];

    /// <summary>
    /// The allors core meta domain.
    /// </summary>
    internal static MetaDomain AllorsCore(this MetaMeta @this) => @this.DomainById[CoreIds.AllorsCore];

    /// <summary>
    /// A boolean.
    /// </summary>
    internal static MetaObjectType Boolean(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.Boolean];

    /// <summary>
    /// An string.
    /// </summary>
    internal static MetaObjectType Integer(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.Integer];

    /// <summary>
    /// An string.
    /// </summary>
    internal static MetaObjectType String(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.String];

    /// <summary>
    /// A unique.
    /// </summary>
    internal static MetaObjectType Unique(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.Unique];

    /// <summary>
    /// Many to association type
    /// </summary>
    internal static MetaObjectType ManyToAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.ManyToAssociationType];

    /// <summary>
    /// Many to many association type.
    /// </summary>
    internal static MetaObjectType ManyToManyAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.ManyToManyAssociationType];

    /// <summary>
    /// Many to many role type.
    /// </summary>
    internal static MetaObjectType ManyToManyRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.ManyToManyRoleType];

    /// <summary>
    /// Many to one association type.
    /// </summary>
    internal static MetaObjectType ManyToOneAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.ManyToOneAssociationType];

    /// <summary>
    /// Many to one role type.
    /// </summary>
    internal static MetaObjectType ManyToOneRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.ManyToOneRoleType];

    /// <summary>
    /// A meta object.
    /// </summary>
    internal static MetaObjectType MetaObject(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.MetaObject];

    /// <summary>
    /// The id of a meta object.
    /// </summary>
    internal static MetaUnitRoleType MetaObjectId(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreIds.MetaObjectId];

    /// <summary>
    /// An object type.
    /// </summary>
    internal static MetaObjectType ObjectType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.ObjectType];

    /// <summary>
    /// The assigned plural name of an object type.
    /// </summary>
    internal static MetaUnitRoleType ObjectTypeAssignedPluralName(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreIds.ObjectTypeAssignedPluralName];

    /// <summary>
    /// The derived plural name of an object type.
    /// </summary>
    internal static MetaUnitRoleType ObjectTypeDerivedPluralName(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreIds.ObjectTypeDerivedPluralName];

    /// <summary>
    /// The singular name of an object type.
    /// </summary>
    internal static MetaUnitRoleType ObjectTypeSingularName(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreIds.ObjectTypeSingularName];

    /// <summary>
    /// One to association type.
    /// </summary>
    internal static MetaObjectType OneToAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.OneToAssociationType];

    /// <summary>
    /// One to many association type.
    /// </summary>
    internal static MetaObjectType OneToManyAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.OneToManyAssociationType];

    /// <summary>
    /// One to many role type.
    /// </summary>
    internal static MetaObjectType OneToManyRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.OneToManyRoleType];

    /// <summary>
    /// One to one association type.
    /// </summary>
    internal static MetaObjectType OneToOneAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.OneToOneAssociationType];

    /// <summary>
    /// One to one role type.
    /// </summary>
    internal static MetaObjectType OneToOneRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.OneToOneRoleType];

    /// <summary>
    /// An operand type.
    /// </summary>
    internal static MetaObjectType OperandType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.OperandType];

    /// <summary>
    /// A relation end type.
    /// </summary>
    internal static MetaObjectType RelationEndType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.RelationEndType];

    /// <summary>
    /// The is many of a role type.
    /// </summary>
    internal static MetaUnitRoleType RelationEndTypeIsMany(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreIds.RelationEndTypeIsMany];

    /// <summary>
    /// A role type.
    /// </summary>
    internal static MetaObjectType RoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.RoleType];

    /// <summary>
    /// The association type a role type.
    /// </summary>
    internal static MetaOneToOneRoleType RoleTypeAssociationType(this MetaMeta @this) => (MetaOneToOneRoleType)@this.RoleTypeById[CoreIds.RoleTypeAssociationType];

    /// <summary>
    /// The assigned plural name of a role type.
    /// </summary>
    internal static MetaUnitRoleType RoleTypeAssignedPluralName(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreIds.RoleTypeAssignedPluralName];

    /// <summary>
    /// The derived plural name of a role type.
    /// </summary>
    internal static MetaUnitRoleType RoleTypeDerivedPluralName(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreIds.RoleTypeDerivedPluralName];

    /// <summary>
    /// The role type of object type.
    /// </summary>
    internal static MetaManyToOneRoleType RoleTypeObjectType(this MetaMeta @this) => (MetaManyToOneRoleType)@this.RoleTypeById[CoreIds.RoleTypeObjectType];

    /// <summary>
    /// The name of role type.
    /// </summary>
    internal static MetaUnitRoleType RoleTypeName(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreIds.RoleTypeName];

    /// <summary>
    /// The singular name of role type.
    /// </summary>
    internal static MetaUnitRoleType RoleTypeSingularName(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreIds.RoleTypeSingularName];

    /// <summary>
    /// The binary association type.
    /// </summary>
    internal static MetaObjectType BinaryAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.BinaryAssociationType];

    /// <summary>
    /// The binary role type.
    /// </summary>
    internal static MetaObjectType BinaryRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.BinaryRoleType];

    /// <summary>
    /// The boolean association type.
    /// </summary>
    internal static MetaObjectType BooleanAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.BooleanAssociationType];

    /// <summary>
    /// The boolean role type.
    /// </summary>
    internal static MetaObjectType BooleanRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.BooleanRoleType];

    /// <summary>
    /// The dateTime association type.
    /// </summary>
    internal static MetaObjectType DateTimeAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.DateTimeAssociationType];

    /// <summary>
    /// The dateTime role type.
    /// </summary>
    internal static MetaObjectType DateTimeRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.DateTimeRoleType];

    /// <summary>
    /// The decimal association type.
    /// </summary>
    internal static MetaObjectType DecimalAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.DecimalAssociationType];

    /// <summary>
    /// The decimal role type.
    /// </summary>
    internal static MetaObjectType DecimalRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.DecimalRoleType];

    /// <summary>
    /// The precision of the decimal role type.
    /// </summary>
    internal static MetaUnitRoleType DecimalRoleTypeAssignedPrecision(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreIds.DecimalRoleTypeAssignedPrecision];

    /// <summary>
    /// The derived precision of the decimal role type.
    /// </summary>
    internal static MetaUnitRoleType DecimalRoleTypeDerivedPrecision(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreIds.DecimalRoleTypeDerivedPrecision];

    /// <summary>
    /// The assigned scale of the decimal role type.
    /// </summary>
    internal static MetaUnitRoleType DecimalRoleTypeAssignedScale(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreIds.DecimalRoleTypeAssignedScale];

    /// <summary>
    /// The derived scale of the decimal role type.
    /// </summary>
    internal static MetaUnitRoleType DecimalRoleTypeDerivedScale(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreIds.DecimalRoleTypeDerivedScale];

    /// <summary>
    /// The float association type.
    /// </summary>
    internal static MetaObjectType FloatAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.FloatAssociationType];

    /// <summary>
    /// The float role type.
    /// </summary>
    internal static MetaObjectType FloatRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.FloatRoleType];

    /// <summary>
    /// The integer association type.
    /// </summary>
    internal static MetaObjectType IntegerAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.IntegerAssociationType];

    /// <summary>
    /// The integer role type.
    /// </summary>
    internal static MetaObjectType IntegerRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.IntegerRoleType];

    /// <summary>
    /// The string association type.
    /// </summary>
    internal static MetaObjectType StringAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.StringAssociationType];

    /// <summary>
    /// The string role type.
    /// </summary>
    internal static MetaObjectType StringRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.StringRoleType];

    /// <summary>
    /// The assigned size of the string role type.
    /// </summary>
    internal static MetaUnitRoleType StringRoleTypeAssignedSize(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreIds.StringRoleTypeAssignedSize];

    /// <summary>
    /// The derived size of the string role type.
    /// </summary>
    internal static MetaUnitRoleType StringRoleTypeDerivedSize(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreIds.StringRoleTypeDerivedSize];

    /// <summary>
    /// The unique association type.
    /// </summary>
    internal static MetaObjectType UniqueAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.UniqueAssociationType];

    /// <summary>
    /// The unique role type.
    /// </summary>
    internal static MetaObjectType UniqueRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.UniqueRoleType];

    /// <summary>
    /// To many role type.
    /// </summary>
    internal static MetaObjectType ToManyRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.ToManyRoleType];

    /// <summary>
    /// To one role type.
    /// </summary>
    internal static MetaObjectType ToOneRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.ToOneRoleType];

    /// <summary>
    /// A type.
    /// </summary>
    internal static MetaObjectType Type(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.Type];

    /// <summary>
    /// A unit.
    /// </summary>
    internal static MetaObjectType Unit(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.Unit];

    /// <summary>
    /// Unit association type.
    /// </summary>
    internal static MetaObjectType UnitAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.UnitAssociationType];

    /// <summary>
    /// Unit role type.
    /// </summary>
    internal static MetaObjectType UnitRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreIds.UnitRoleType];
}
