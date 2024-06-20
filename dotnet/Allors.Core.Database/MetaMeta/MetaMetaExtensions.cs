namespace Allors.Core.Database.MetaMeta;

using Allors.Core.MetaMeta;

/// <summary>
/// MetaMeta Extensions.
/// </summary>
public static class MetaMetaExtensions
{
    /// <summary>
    /// An association type.
    /// </summary>
    public static MetaObjectType AssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.AssociationType];

    /// <summary>
    /// The composite of an association type.
    /// </summary>
    public static MetaManyToOneRoleType AssociationTypeComposite(this MetaMeta @this) => (MetaManyToOneRoleType)@this.RoleTypeById[CoreMetaMeta.AssociationTypeComposite];

    /// <summary>
    /// A class.
    /// </summary>
    public static MetaObjectType Class(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.Class];

    /// <summary>
    /// A composite.
    /// </summary>
    public static MetaObjectType Composite(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.Composite];

    /// <summary>
    /// Composite association type.
    /// </summary>
    public static MetaObjectType CompositeAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.CompositeAssociationType];

    /// <summary>
    /// The concretes (subclasses in case of an interface, the class iteself in case of a class) of a composite.
    /// </summary>
    public static MetaManyToManyRoleType CompositeConcretes(this MetaMeta @this) => (MetaManyToManyRoleType)@this.RoleTypeById[CoreMetaMeta.CompositeConcretes];

    /// <summary>
    /// The direct supertypes of a composite.
    /// </summary>
    public static MetaManyToManyRoleType CompositeDirectSupertypes(this MetaMeta @this) => (MetaManyToManyRoleType)@this.RoleTypeById[CoreMetaMeta.CompositeDirectSupertypes];

    /// <summary>
    /// The method types of a composite.
    /// </summary>
    public static MetaOneToManyRoleType CompositeMethodTypes(this MetaMeta @this) => (MetaOneToManyRoleType)@this.RoleTypeById[CoreMetaMeta.CompositeMethodTypes];

    /// <summary>
    /// Composite role type.
    /// </summary>
    public static MetaObjectType CompositeRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.CompositeRoleType];

    /// <summary>
    /// The supertypes of a composite.
    /// </summary>
    public static MetaManyToManyRoleType CompositeSupertypes(this MetaMeta @this) => (MetaManyToManyRoleType)@this.RoleTypeById[CoreMetaMeta.CompositeSupertypes];

    /// <summary>
    /// A concrete method type.
    /// </summary>
    public static MetaObjectType ConcreteMethodType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.ConcreteMethodType];

    /// <summary>
    /// A class of a concrete method type.
    /// </summary>
    public static MetaManyToOneRoleType ConcreteMethodTypeClass(this MetaMeta @this) => (MetaManyToOneRoleType)@this.RoleTypeById[CoreMetaMeta.ConcreteMethodTypeClass];

    /// <summary>
    /// A method type of a concrete method type.
    /// </summary>
    public static MetaManyToOneRoleType ConcreteMethodTypeMethodType(this MetaMeta @this) => (MetaManyToOneRoleType)@this.RoleTypeById[CoreMetaMeta.MethodTypeConcreteMethodTypes];

    /// <summary>
    /// The action of the concrete method type.
    /// </summary>
    public static MetaUnitRoleType ConcreteMethodTypeActions(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreMetaMeta.ConcreteMethodTypeActions];

    /// <summary>
    /// A method type of a concrete method type.
    /// </summary>
    public static MetaManyToManyRoleType ConcreteMethodTypeMethodParts(this MetaMeta @this) => (MetaManyToManyRoleType)@this.RoleTypeById[CoreMetaMeta.ConcreteMethodTypeMethodParts];

    /// <summary>
    /// A domain.
    /// </summary>
    public static MetaObjectType Domain(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.Domain];

    /// <summary>
    /// The name of a domain.
    /// </summary>
    public static MetaUnitRoleType DomainName(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreMetaMeta.DomainName];

    /// <summary>
    /// The super domains of a domain.
    /// </summary>
    public static MetaManyToManyRoleType DomainSuperdomains(this MetaMeta @this) => (MetaManyToManyRoleType)@this.RoleTypeById[CoreMetaMeta.DomainSuperdomains];

    /// <summary>
    /// The types of a domain.
    /// </summary>
    public static MetaManyToManyRoleType DomainTypes(this MetaMeta @this) => (MetaManyToManyRoleType)@this.RoleTypeById[CoreMetaMeta.DomainTypes];

    /// <summary>
    /// An inheritance.
    /// </summary>
    public static MetaObjectType Inheritance(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.Inheritance];

    /// <summary>
    /// The subtype of an inheritance.
    /// </summary>
    public static MetaManyToOneRoleType InheritanceSubtype(this MetaMeta @this) => (MetaManyToOneRoleType)@this.RoleTypeById[CoreMetaMeta.InheritanceSubtype];

    /// <summary>
    /// The supertype of an inheritance.
    /// </summary>
    public static MetaManyToOneRoleType InheritanceSupertype(this MetaMeta @this) => (MetaManyToOneRoleType)@this.RoleTypeById[CoreMetaMeta.InheritanceSupertype];

    /// <summary>
    /// An interface.
    /// </summary>
    public static MetaObjectType Interface(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.Interface];

    /// <summary>
    /// A boolean.
    /// </summary>
    public static MetaObjectType Boolean(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.Boolean];

    /// <summary>
    /// An string.
    /// </summary>
    public static MetaObjectType Integer(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.Integer];

    /// <summary>
    /// An string.
    /// </summary>
    public static MetaObjectType String(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.String];

    /// <summary>
    /// A unique.
    /// </summary>
    public static MetaObjectType Unique(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.Unique];

    /// <summary>
    /// Many to association type
    /// </summary>
    public static MetaObjectType ManyToAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.ManyToAssociationType];

    /// <summary>
    /// Many to many association type.
    /// </summary>
    public static MetaObjectType ManyToManyAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.ManyToManyAssociationType];

    /// <summary>
    /// Many to many role type.
    /// </summary>
    public static MetaObjectType ManyToManyRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.ManyToManyRoleType];

    /// <summary>
    /// Many to one association type.
    /// </summary>
    public static MetaObjectType ManyToOneAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.ManyToOneAssociationType];

    /// <summary>
    /// Many to one role type.
    /// </summary>
    public static MetaObjectType ManyToOneRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.ManyToOneRoleType];

    /// <summary>
    /// A meta object.
    /// </summary>
    public static MetaObjectType MetaObject(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.MetaIdentifiableObject];

    /// <summary>
    /// The id of a meta object.
    /// </summary>
    public static MetaUnitRoleType MetaObjectId(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreMetaMeta.MetaObjectId];

    /// <summary>
    /// An object type.
    /// </summary>
    public static MetaObjectType ObjectType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.ObjectType];

    /// <summary>
    /// The assigned plural name of an object type.
    /// </summary>
    public static MetaUnitRoleType ObjectTypeAssignedPluralName(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreMetaMeta.ObjectTypeAssignedPluralName];

    /// <summary>
    /// The derived plural name of an object type.
    /// </summary>
    public static MetaUnitRoleType ObjectTypeDerivedPluralName(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreMetaMeta.ObjectTypeDerivedPluralName];

    /// <summary>
    /// The singular name of an object type.
    /// </summary>
    public static MetaUnitRoleType ObjectTypeSingularName(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreMetaMeta.ObjectTypeSingularName];

    /// <summary>
    /// One to association type.
    /// </summary>
    public static MetaObjectType OneToAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.OneToAssociationType];

    /// <summary>
    /// One to many association type.
    /// </summary>
    public static MetaObjectType OneToManyAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.OneToManyAssociationType];

    /// <summary>
    /// One to many role type.
    /// </summary>
    public static MetaObjectType OneToManyRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.OneToManyRoleType];

    /// <summary>
    /// One to one association type.
    /// </summary>
    public static MetaObjectType OneToOneAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.OneToOneAssociationType];

    /// <summary>
    /// One to one role type.
    /// </summary>
    public static MetaObjectType OneToOneRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.OneToOneRoleType];

    /// <summary>
    /// An operand type.
    /// </summary>
    public static MetaObjectType OperandType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.OperandType];

    /// <summary>
    /// A relation end type.
    /// </summary>
    public static MetaObjectType RelationEndType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.RelationEndType];

    /// <summary>
    /// The is many of a role type.
    /// </summary>
    public static MetaUnitRoleType RelationEndTypeIsMany(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreMetaMeta.RelationEndTypeIsMany];

    /// <summary>
    /// A role type.
    /// </summary>
    public static MetaObjectType RoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.RoleType];

    /// <summary>
    /// The association type a role type.
    /// </summary>
    public static MetaOneToOneRoleType RoleTypeAssociationType(this MetaMeta @this) => (MetaOneToOneRoleType)@this.RoleTypeById[CoreMetaMeta.RoleTypeAssociationType];

    /// <summary>
    /// The assigned plural name of a role type.
    /// </summary>
    public static MetaUnitRoleType RoleTypeAssignedPluralName(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreMetaMeta.RoleTypeAssignedPluralName];

    /// <summary>
    /// The derived plural name of a role type.
    /// </summary>
    public static MetaUnitRoleType RoleTypeDerivedPluralName(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreMetaMeta.RoleTypeDerivedPluralName];

    /// <summary>
    /// The role type of object type.
    /// </summary>
    public static MetaManyToOneRoleType RoleTypeObjectType(this MetaMeta @this) => (MetaManyToOneRoleType)@this.RoleTypeById[CoreMetaMeta.RoleTypeObjectType];

    /// <summary>
    /// The name of role type.
    /// </summary>
    public static MetaUnitRoleType RoleTypeName(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreMetaMeta.RoleTypeName];

    /// <summary>
    /// The singular name of role type.
    /// </summary>
    public static MetaUnitRoleType RoleTypeSingularName(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreMetaMeta.RoleTypeSingularName];

    /// <summary>
    /// The binary association type.
    /// </summary>
    public static MetaObjectType BinaryAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.BinaryAssociationType];

    /// <summary>
    /// The binary role type.
    /// </summary>
    public static MetaObjectType BinaryRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.BinaryRoleType];

    /// <summary>
    /// The boolean association type.
    /// </summary>
    public static MetaObjectType BooleanAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.BooleanAssociationType];

    /// <summary>
    /// The boolean role type.
    /// </summary>
    public static MetaObjectType BooleanRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.BooleanRoleType];

    /// <summary>
    /// The dateTime association type.
    /// </summary>
    public static MetaObjectType DateTimeAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.DateTimeAssociationType];

    /// <summary>
    /// The dateTime role type.
    /// </summary>
    public static MetaObjectType DateTimeRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.DateTimeRoleType];

    /// <summary>
    /// The decimal association type.
    /// </summary>
    public static MetaObjectType DecimalAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.DecimalAssociationType];

    /// <summary>
    /// The decimal role type.
    /// </summary>
    public static MetaObjectType DecimalRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.DecimalRoleType];

    /// <summary>
    /// The precision of the decimal role type.
    /// </summary>
    public static MetaUnitRoleType DecimalRoleTypeAssignedPrecision(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreMetaMeta.DecimalRoleTypeAssignedPrecision];

    /// <summary>
    /// The derived precision of the decimal role type.
    /// </summary>
    public static MetaUnitRoleType DecimalRoleTypeDerivedPrecision(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreMetaMeta.DecimalRoleTypeDerivedPrecision];

    /// <summary>
    /// The assigned scale of the decimal role type.
    /// </summary>
    public static MetaUnitRoleType DecimalRoleTypeAssignedScale(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreMetaMeta.DecimalRoleTypeAssignedScale];

    /// <summary>
    /// The derived scale of the decimal role type.
    /// </summary>
    public static MetaUnitRoleType DecimalRoleTypeDerivedScale(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreMetaMeta.DecimalRoleTypeDerivedScale];

    /// <summary>
    /// The float association type.
    /// </summary>
    public static MetaObjectType FloatAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.FloatAssociationType];

    /// <summary>
    /// The float role type.
    /// </summary>
    public static MetaObjectType FloatRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.FloatRoleType];

    /// <summary>
    /// The integer association type.
    /// </summary>
    public static MetaObjectType IntegerAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.IntegerAssociationType];

    /// <summary>
    /// The integer role type.
    /// </summary>
    public static MetaObjectType IntegerRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.IntegerRoleType];

    /// <summary>
    /// The string association type.
    /// </summary>
    public static MetaObjectType StringAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.StringAssociationType];

    /// <summary>
    /// The string role type.
    /// </summary>
    public static MetaObjectType StringRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.StringRoleType];

    /// <summary>
    /// The assigned size of the string role type.
    /// </summary>
    public static MetaUnitRoleType StringRoleTypeAssignedSize(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreMetaMeta.StringRoleTypeAssignedSize];

    /// <summary>
    /// The derived size of the string role type.
    /// </summary>
    public static MetaUnitRoleType StringRoleTypeDerivedSize(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreMetaMeta.StringRoleTypeDerivedSize];

    /// <summary>
    /// The unique association type.
    /// </summary>
    public static MetaObjectType UniqueAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.UniqueAssociationType];

    /// <summary>
    /// The unique role type.
    /// </summary>
    public static MetaObjectType UniqueRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.UniqueRoleType];

    /// <summary>
    /// To many role type.
    /// </summary>
    public static MetaObjectType ToManyRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.ToManyRoleType];

    /// <summary>
    /// To one role type.
    /// </summary>
    public static MetaObjectType ToOneRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.ToOneRoleType];

    /// <summary>
    /// A type.
    /// </summary>
    public static MetaObjectType Type(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.Type];

    /// <summary>
    /// A unit.
    /// </summary>
    public static MetaObjectType Unit(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.Unit];

    /// <summary>
    /// Unit association type.
    /// </summary>
    public static MetaObjectType UnitAssociationType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.UnitAssociationType];

    /// <summary>
    /// Unit role type.
    /// </summary>
    public static MetaObjectType UnitRoleType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.UnitRoleType];

    /// <summary>
    /// MethodPart type.
    /// </summary>
    public static MetaObjectType MethodType(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.MethodType];

    /// <summary>
    /// The name of a method type.
    /// </summary>
    public static MetaUnitRoleType MethodTypeName(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreMetaMeta.MethodTypeName];

    /// <summary>
    /// The method parts of a method type.
    /// </summary>
    public static MetaOneToManyRoleType MethodTypeConcreteMethodTypes(this MetaMeta @this) => (MetaOneToManyRoleType)@this.RoleTypeById[CoreMetaMeta.MethodTypeConcreteMethodTypes];

    /// <summary>
    /// The method parts of a method type.
    /// </summary>
    public static MetaOneToManyRoleType MethodTypeMethodParts(this MetaMeta @this) => (MetaOneToManyRoleType)@this.RoleTypeById[CoreMetaMeta.MethodTypeMethodParts];

    /// <summary>
    /// MethodPart part.
    /// </summary>
    public static MetaObjectType MethodPart(this MetaMeta @this) => @this.ObjectTypeById[CoreMetaMeta.MethodPart];

    /// <summary>
    /// The domain of a method part.
    /// </summary>
    public static MetaManyToOneRoleType MethodPartDomain(this MetaMeta @this) => (MetaManyToOneRoleType)@this.RoleTypeById[CoreMetaMeta.MethodPartDomain];

    /// <summary>
    /// The composite of a method part.
    /// </summary>
    public static MetaManyToOneRoleType MethodPartComposite(this MetaMeta @this) => (MetaManyToOneRoleType)@this.RoleTypeById[CoreMetaMeta.MethodPartComposite];

    /// <summary>
    /// The action of a method part.
    /// </summary>
    public static MetaUnitRoleType MethodPartAction(this MetaMeta @this) => (MetaUnitRoleType)@this.RoleTypeById[CoreMetaMeta.MethodPartAction];
}
