namespace Allors.Core.Database.Meta;

using System;
using System.Linq;
using Allors.Core.Database.Meta.Domain;
using Allors.Core.Meta;

/// <summary>
/// Meta Extensions.
/// </summary>
public static class MetaExtensions
{
    /// <summary>
    /// An association type.
    /// </summary>
    public static IAssociationType AssociationType(this Meta @this) => (IAssociationType)@this.Get(CoreIds.AssociationType);

    /// <summary>
    /// The composite of an association type.
    /// </summary>
    public static ManyToOneRoleType AssociationTypeComposite(this Meta @this) => (ManyToOneRoleType)@this.Get(CoreIds.AssociationTypeComposite);

    /// <summary>
    /// A class.
    /// </summary>
    public static IObjectType Class(this Meta @this) => (IObjectType)@this.Get(CoreIds.Class);

    /// <summary>
    /// A composite.
    /// </summary>
    public static IObjectType Composite(this Meta @this) => (IObjectType)@this.Get(CoreIds.Composite);

    /// <summary>
    /// Composite association type.
    /// </summary>
    public static IObjectType CompositeAssociationType(this Meta @this) => (IObjectType)@this.Get(CoreIds.CompositeAssociationType);

    /// <summary>
    /// Composite role type.
    /// </summary>
    public static IObjectType CompositeRoleType(this Meta @this) => (IObjectType)@this.Get(CoreIds.CompositeRoleType);

    /// <summary>
    /// The supertypes of a composite.
    /// </summary>
    public static ManyToManyRoleType CompositeSupertypes(this Meta @this) => (ManyToManyRoleType)@this.Get(CoreIds.CompositeSupertypes);

    /// <summary>
    /// The direct supertypes of a composite.
    /// </summary>
    public static ManyToManyRoleType CompositeDirectSupertypes(this Meta @this) => (ManyToManyRoleType)@this.Get(CoreIds.CompositeDirectSupertypes);

    /// <summary>
    /// A domain.
    /// </summary>
    public static IObjectType Domain(this Meta @this) => (IObjectType)@this.Get(CoreIds.Domain);

    /// <summary>
    /// The name of a domain.
    /// </summary>
    public static IUnitRoleType DomainName(this Meta @this) => (IUnitRoleType)@this.Get(CoreIds.DomainName);

    /// <summary>
    /// The super domains of a domain.
    /// </summary>
    public static ManyToManyRoleType DomainSuperdomains(this Meta @this) => (ManyToManyRoleType)@this.Get(CoreIds.DomainSuperdomains);

    /// <summary>
    /// The types of a domain.
    /// </summary>
    public static ManyToManyRoleType DomainTypes(this Meta @this) => (ManyToManyRoleType)@this.Get(CoreIds.DomainTypes);

    /// <summary>
    /// An inheritance.
    /// </summary>
    public static IObjectType Inheritance(this Meta @this) => (IObjectType)@this.Get(CoreIds.Inheritance);

    /// <summary>
    /// The subtype of an inheritance.
    /// </summary>
    public static ManyToOneRoleType InheritanceSubtype(this Meta @this) => (ManyToOneRoleType)@this.Get(CoreIds.InheritanceSubtype);

    /// <summary>
    /// The supertype of an inheritance.
    /// </summary>
    public static ManyToOneRoleType InheritanceSupertype(this Meta @this) => (ManyToOneRoleType)@this.Get(CoreIds.InheritanceSupertype);

    /// <summary>
    /// An interface.
    /// </summary>
    public static IObjectType Interface(this Meta @this) => (IObjectType)@this.Get(CoreIds.Interface);

    /// <summary>
    /// The allors core meta domain.
    /// </summary>
    public static Domain.Domain AllorsCore(this Meta @this) => (Domain.Domain)@this.Get(CoreIds.AllorsCore);

    /// <summary>
    /// A boolean.
    /// </summary>
    public static IObjectType Boolean(this Meta @this) => (IObjectType)@this.Get(CoreIds.Boolean);

    /// <summary>
    /// An string.
    /// </summary>
    public static IObjectType Integer(this Meta @this) => (IObjectType)@this.Get(CoreIds.Integer);

    /// <summary>
    /// An string.
    /// </summary>
    public static IObjectType String(this Meta @this) => (IObjectType)@this.Get(CoreIds.String);

    /// <summary>
    /// A unique.
    /// </summary>
    public static IObjectType Unique(this Meta @this) => (IObjectType)@this.Get(CoreIds.Unique);

    /// <summary>
    /// Many to association type
    /// </summary>
    public static IObjectType ManyToAssociationType(this Meta @this) => (IObjectType)@this.Get(CoreIds.ManyToAssociationType);

    /// <summary>
    /// Many to many association type.
    /// </summary>
    public static IObjectType ManyToManyAssociationType(this Meta @this) => (IObjectType)@this.Get(CoreIds.ManyToManyAssociationType);

    /// <summary>
    /// Many to many role type.
    /// </summary>
    public static IObjectType ManyToManyRoleType(this Meta @this) => (IObjectType)@this.Get(CoreIds.ManyToManyRoleType);

    /// <summary>
    /// Many to one association type.
    /// </summary>
    public static IObjectType ManyToOneAssociationType(this Meta @this) => (IObjectType)@this.Get(CoreIds.ManyToOneAssociationType);

    /// <summary>
    /// Many to one role type.
    /// </summary>
    public static IObjectType ManyToOneRoleType(this Meta @this) => (IObjectType)@this.Get(CoreIds.ManyToOneRoleType);

    /// <summary>
    /// A meta object.
    /// </summary>
    public static IObjectType Object(this Meta @this) => (IObjectType)@this.Get(CoreIds.MetaObject);

    /// <summary>
    /// The id of a meta object.
    /// </summary>
    public static IUnitRoleType ObjectId(this Meta @this) => (IUnitRoleType)@this.Get(CoreIds.MetaObjectId);

    /// <summary>
    /// An object type.
    /// </summary>
    public static IObjectType ObjectType(this Meta @this) => (IObjectType)@this.Get(CoreIds.ObjectType);

    /// <summary>
    /// The assigned plural name of an object type.
    /// </summary>
    public static IUnitRoleType ObjectTypeAssignedPluralName(this Meta @this) => (IUnitRoleType)@this.Get(CoreIds.ObjectTypeAssignedPluralName);

    /// <summary>
    /// The derived plural name of an object type.
    /// </summary>
    public static IUnitRoleType ObjectTypeDerivedPluralName(this Meta @this) => (IUnitRoleType)@this.Get(CoreIds.ObjectTypeDerivedPluralName);

    /// <summary>
    /// The singular name of an object type.
    /// </summary>
    public static IUnitRoleType ObjectTypeSingularName(this Meta @this) => (IUnitRoleType)@this.Get(CoreIds.ObjectTypeSingularName);

    /// <summary>
    /// One to association type.
    /// </summary>
    public static IObjectType OneToAssociationType(this Meta @this) => (IObjectType)@this.Get(CoreIds.OneToAssociationType);

    /// <summary>
    /// One to many association type.
    /// </summary>
    public static IObjectType OneToManyAssociationType(this Meta @this) => (IObjectType)@this.Get(CoreIds.OneToManyAssociationType);

    /// <summary>
    /// One to many role type.
    /// </summary>
    public static IObjectType OneToManyRoleType(this Meta @this) => (IObjectType)@this.Get(CoreIds.OneToManyRoleType);

    /// <summary>
    /// One to one association type.
    /// </summary>
    public static IObjectType OneToOneAssociationType(this Meta @this) => (IObjectType)@this.Get(CoreIds.OneToOneAssociationType);

    /// <summary>
    /// One to one role type.
    /// </summary>
    public static IObjectType OneToOneRoleType(this Meta @this) => (IObjectType)@this.Get(CoreIds.OneToOneRoleType);

    /// <summary>
    /// An operand type.
    /// </summary>
    public static IObjectType OperandType(this Meta @this) => (IObjectType)@this.Get(CoreIds.OperandType);

    /// <summary>
    /// A relation end type.
    /// </summary>
    public static IObjectType RelationEndType(this Meta @this) => (IObjectType)@this.Get(CoreIds.RelationEndType);

    /// <summary>
    /// The is many of a role type.
    /// </summary>
    public static IUnitRoleType RelationEndTypeIsMany(this Meta @this) => (IUnitRoleType)@this.Get(CoreIds.RelationEndTypeIsMany);

    /// <summary>
    /// A role type.
    /// </summary>
    public static IObjectType RoleType(this Meta @this) => (IObjectType)@this.Get(CoreIds.RoleType);

    /// <summary>
    /// The association type a role type.
    /// </summary>
    public static OneToOneRoleType RoleTypeAssociationType(this Meta @this) => (OneToOneRoleType)@this.Get(CoreIds.RoleTypeAssociationType);

    /// <summary>
    /// The assigned plural name of a role type.
    /// </summary>
    public static IUnitRoleType RoleTypeAssignedPluralName(this Meta @this) => (IUnitRoleType)@this.Get(CoreIds.RoleTypeAssignedPluralName);

    /// <summary>
    /// The derived plural name of a role type.
    /// </summary>
    public static IUnitRoleType RoleTypeDerivedPluralName(this Meta @this) => (IUnitRoleType)@this.Get(CoreIds.RoleTypeDerivedPluralName);

    /// <summary>
    /// The role type of object type.
    /// </summary>
    public static ManyToOneRoleType RoleTypeObjectType(this Meta @this) => (ManyToOneRoleType)@this.Get(CoreIds.RoleTypeObjectType);

    /// <summary>
    /// The name of role type.
    /// </summary>
    public static IUnitRoleType RoleTypeName(this Meta @this) => (IUnitRoleType)@this.Get(CoreIds.RoleTypeName);

    /// <summary>
    /// The singular name of role type.
    /// </summary>
    public static IUnitRoleType RoleTypeSingularName(this Meta @this) => (IUnitRoleType)@this.Get(CoreIds.RoleTypeSingularName);

    /// <summary>
    /// The binary association type.
    /// </summary>
    public static IObjectType BinaryAssociationType(this Meta @this) => (IObjectType)@this.Get(CoreIds.BinaryAssociationType);

    /// <summary>
    /// The binary role type.
    /// </summary>
    public static IObjectType BinaryRoleType(this Meta @this) => (IObjectType)@this.Get(CoreIds.BinaryRoleType);

    /// <summary>
    /// The boolean association type.
    /// </summary>
    public static IObjectType BooleanAssociationType(this Meta @this) => (IObjectType)@this.Get(CoreIds.BooleanAssociationType);

    /// <summary>
    /// The boolean role type.
    /// </summary>
    public static IObjectType BooleanRoleType(this Meta @this) => (IObjectType)@this.Get(CoreIds.BooleanRoleType);

    /// <summary>
    /// The dateTime association type.
    /// </summary>
    public static IObjectType DateTimeAssociationType(this Meta @this) => (IObjectType)@this.Get(CoreIds.DateTimeAssociationType);

    /// <summary>
    /// The dateTime role type.
    /// </summary>
    public static IObjectType DateTimeRoleType(this Meta @this) => (IObjectType)@this.Get(CoreIds.DateTimeRoleType);

    /// <summary>
    /// The decimal association type.
    /// </summary>
    public static IObjectType DecimalAssociationType(this Meta @this) => (IObjectType)@this.Get(CoreIds.DecimalAssociationType);

    /// <summary>
    /// The decimal role type.
    /// </summary>
    public static IObjectType DecimalRoleType(this Meta @this) => (IObjectType)@this.Get(CoreIds.DecimalRoleType);

    /// <summary>
    /// The precision of the decimal role type.
    /// </summary>
    public static IUnitRoleType DecimalRoleTypeAssignedPrecision(this Meta @this) => (IUnitRoleType)@this.Get(CoreIds.DecimalRoleTypeAssignedPrecision);

    /// <summary>
    /// The derived precision of the decimal role type.
    /// </summary>
    public static IUnitRoleType DecimalRoleTypeDerivedPrecision(this Meta @this) => (IUnitRoleType)@this.Get(CoreIds.DecimalRoleTypeDerivedPrecision);

    /// <summary>
    /// The assigned scale of the decimal role type.
    /// </summary>
    public static IUnitRoleType DecimalRoleTypeAssignedScale(this Meta @this) => (IUnitRoleType)@this.Get(CoreIds.DecimalRoleTypeAssignedScale);

    /// <summary>
    /// The derived scale of the decimal role type.
    /// </summary>
    public static IUnitRoleType DecimalRoleTypeDerivedScale(this Meta @this) => (IUnitRoleType)@this.Get(CoreIds.DecimalRoleTypeDerivedScale);

    /// <summary>
    /// The float association type.
    /// </summary>
    public static IObjectType FloatAssociationType(this Meta @this) => (IObjectType)@this.Get(CoreIds.FloatAssociationType);

    /// <summary>
    /// The float role type.
    /// </summary>
    public static IObjectType FloatRoleType(this Meta @this) => (IObjectType)@this.Get(CoreIds.FloatRoleType);

    /// <summary>
    /// The integer association type.
    /// </summary>
    public static IObjectType IntegerAssociationType(this Meta @this) => (IObjectType)@this.Get(CoreIds.IntegerAssociationType);

    /// <summary>
    /// The integer role type.
    /// </summary>
    public static IObjectType IntegerRoleType(this Meta @this) => (IObjectType)@this.Get(CoreIds.IntegerRoleType);

    /// <summary>
    /// The string association type.
    /// </summary>
    public static IObjectType StringAssociationType(this Meta @this) => (IObjectType)@this.Get(CoreIds.StringAssociationType);

    /// <summary>
    /// The string role type.
    /// </summary>
    public static IObjectType StringRoleType(this Meta @this) => (IObjectType)@this.Get(CoreIds.StringRoleType);

    /// <summary>
    /// The assigned size of the string role type.
    /// </summary>
    public static IUnitRoleType StringRoleTypeAssignedSize(this Meta @this) => (IUnitRoleType)@this.Get(CoreIds.StringRoleTypeAssignedSize);

    /// <summary>
    /// The derived size of the string role type.
    /// </summary>
    public static IUnitRoleType StringRoleTypeDerivedSize(this Meta @this) => (IUnitRoleType)@this.Get(CoreIds.StringRoleTypeDerivedSize);

    /// <summary>
    /// The unique association type.
    /// </summary>
    public static IObjectType UniqueAssociationType(this Meta @this) => (IObjectType)@this.Get(CoreIds.UniqueAssociationType);

    /// <summary>
    /// The unique role type.
    /// </summary>
    public static IObjectType UniqueRoleType(this Meta @this) => (IObjectType)@this.Get(CoreIds.UniqueRoleType);

    /// <summary>
    /// To many role type.
    /// </summary>
    public static IObjectType ToManyRoleType(this Meta @this) => (IObjectType)@this.Get(CoreIds.ToManyRoleType);

    /// <summary>
    /// To one role type.
    /// </summary>
    public static IObjectType ToOneRoleType(this Meta @this) => (IObjectType)@this.Get(CoreIds.ToOneRoleType);

    /// <summary>
    /// A type.
    /// </summary>
    public static IObjectType Type(this Meta @this) => (IObjectType)@this.Get(CoreIds.Type);

    /// <summary>
    /// A unit.
    /// </summary>
    public static IObjectType Unit(this Meta @this) => (IObjectType)@this.Get(CoreIds.Unit);

    /// <summary>
    /// Unit association type.
    /// </summary>
    public static IObjectType UnitAssociationType(this Meta @this) => (IObjectType)@this.Get(CoreIds.UnitAssociationType);

    /// <summary>
    /// Unit role type.
    /// </summary>
    public static IObjectType UnitRoleType(this Meta @this) => (IObjectType)@this.Get(CoreIds.UnitRoleType);

    private static IMetaObject Get(this Meta @this, Guid id) => @this.Objects.First(v => ((Guid)v[@this.MetaMeta.MetaObjectId()]!) == id);
}
