namespace Allors.Core.Database.Meta;

using System;
using Allors.Core.Database.Meta.Derivations;
using Allors.Core.Database.Meta.Domain;
using Allors.Core.Meta.Domain;
using Allors.Core.Meta.Meta;

/// <summary>
/// Core Meta Meta.
/// </summary>
public sealed class CoreMetaMeta
{
    internal CoreMetaMeta()
    {
        this.MetaMeta = new MetaMeta();

        // ObjectTypes
        this.Boolean = this.AddMetaUnit(new Guid("a5e75543-08e0-4fdc-a804-e3681ba5eae5"), "Boolean");
        this.Integer = this.AddMetaUnit(new Guid("bb6c48ad-0ae3-4788-ba80-18d8ffdf7018"), "Integer");
        this.String = this.AddMetaUnit(new Guid("be72ed5d-508c-4b7c-b385-90b74700f1c8"), "String");
        this.Unique = this.AddMetaUnit(new Guid("f60a1552-58a0-40b0-87b5-be468e4a256d"), "Unique");
        this.AssociationType = this.AddMetaInterface(new Guid("e19973f8-ad9a-46f4-97e5-3e9640dee1b6"), "AssociationType");
        this.Class = this.AddMetaClass(new Guid("258ef66a-55d8-4aeb-96df-4651e8fc7d3a"), typeof(Class));
        this.Composite = this.AddMetaInterface(new Guid("b4b17450-9377-4b5c-9e34-9d27a35947c9"), "Composite");
        this.CompositeAssociationType = this.AddMetaInterface(new Guid("742e3899-a779-4c82-9166-9561ae15b734"), "CompositeRoleType");
        this.CompositeRoleType = this.AddMetaInterface(new Guid("eb7830e0-eead-4655-a76c-6b8b8724fa1c"), "CompositeAssociationType");
        this.Domain = this.AddMetaClass(new Guid("4e0633f5-c563-4034-be9c-ad8346a4a5c8"), typeof(Domain.Domain));
        this.Interface = this.AddMetaClass(new Guid("f22451ac-fafb-4465-9b32-0f6a3abe50d3"), typeof(Interface));
        this.ManyToAssociationType = this.AddMetaInterface(new Guid("bebcf6ef-ff0f-486a-a58d-135e60710bb5"), "ManyToAssociationType");
        this.ManyToManyAssociationType = this.AddMetaClass(new Guid("6caed1de-18ed-4ed8-85ff-92512affb077"), typeof(ManyToManyAssociationType));
        this.ManyToManyRoleType = this.AddMetaClass(new Guid("fc8945fc-9441-4fc8-a295-76a4af282e05"), typeof(ManyToManyRoleType));
        this.ManyToOneAssociationType = this.AddMetaClass(new Guid("b0c7762d-7638-4d18-87e4-48eb9053b156"), typeof(ManyToOneAssociationType));
        this.ManyToOneRoleType = this.AddMetaClass(new Guid("3900a114-55b6-42f8-aea9-b7937e38bc07"), typeof(ManyToOneRoleType));
        this.MetaObject = this.AddMetaInterface(new Guid("258b338e-a76f-4022-a431-6732ef7fc021"), "MetaObject");
        this.MethodType = this.AddMetaClass(new Guid("d21ffb87-afd8-4cbf-879a-c44d9decf01d"), typeof(MethodType));
        this.ObjectType = this.AddMetaInterface(new Guid("20088de8-27b2-4fe7-a016-502e7e096f4f"), "ObjectType");
        this.OneToAssociationType = this.AddMetaInterface(new Guid("0fd0f52e-9e79-440d-81a2-45513044aad4"), "OneToAssociationType");
        this.OneToManyAssociationType = this.AddMetaClass(new Guid("eb2e311d-b736-44e0-a283-05bc195a46be"), typeof(OneToManyAssociationType));
        this.OneToManyRoleType = this.AddMetaClass(new Guid("a4eb8af3-8d52-4e46-8b49-646a8a25e293"), typeof(OneToManyRoleType));
        this.OneToOneAssociationType = this.AddMetaClass(new Guid("8d081c3b-47bd-49aa-911d-3d537399383d"), typeof(OneToOneAssociationType));
        this.OneToOneRoleType = this.AddMetaClass(new Guid("fe392f1c-2674-4a1e-9e74-d0fb2afe0d95"), typeof(OneToOneRoleType));
        this.OperandType = this.AddMetaInterface(new Guid("ee1e80cf-7e36-46c4-9b8b-1fbcb2173299"), "OperandType");
        this.RelationEndType = this.AddMetaInterface(new Guid("a159dd87-bf9d-468d-9629-d345d8f59242"), "RelationEndType");
        this.RoleType = this.AddMetaInterface(new Guid("6b811301-cddc-456d-8614-ca9302f91e55"), "RoleType");
        this.BinaryAssociationType = this.AddMetaClass(new Guid("0ad616a3-aa2a-4851-9dd3-ebc58a11c0a1"), typeof(BinaryAssociationType));
        this.BinaryRoleType = this.AddMetaClass(new Guid("2a417adc-b471-408e-ab93-08f6a649a489"), typeof(BinaryRoleType));
        this.BooleanAssociationType = this.AddMetaClass(new Guid("e2ea3982-1548-4f0f-ae94-d555bf8f318e"), typeof(BooleanAssociationType));
        this.BooleanRoleType = this.AddMetaClass(new Guid("5c688a7a-0123-42ad-a642-45351382e213"), typeof(BooleanRoleType));
        this.DateTimeAssociationType = this.AddMetaClass(new Guid("03320d09-5062-403f-be64-1e09a856580c"), typeof(DateTimeAssociationType));
        this.DateTimeRoleType = this.AddMetaClass(new Guid("71533eaf-3894-45f9-b47d-1f6d8c8b61bc"), typeof(DateTimeRoleType));
        this.DecimalAssociationType = this.AddMetaClass(new Guid("3e50b873-4de6-46a3-9f9e-497285a77719"), typeof(DecimalAssociationType));
        this.DecimalRoleType = this.AddMetaClass(new Guid("8991761d-be85-4601-a67d-f85bcd5432e1"), typeof(DecimalRoleType));
        this.FloatAssociationType = this.AddMetaClass(new Guid("11c199c1-c2ba-4933-9455-643eebcb96f5"), typeof(FloatAssociationType));
        this.FloatRoleType = this.AddMetaClass(new Guid("2eada119-a35c-4076-b9ba-6b731684c2af"), typeof(FloatRoleType));
        this.IntegerAssociationType = this.AddMetaClass(new Guid("1e62c68e-d9ba-4169-add4-865b53136108"), typeof(IntegerAssociationType));
        this.IntegerRoleType = this.AddMetaClass(new Guid("65f77b86-5a2a-4961-b7e4-0eaa42b2a157"), typeof(IntegerRoleType));
        this.StringAssociationType = this.AddMetaClass(new Guid("0368b7c4-73f8-4f9b-8182-60e09771b62c"), typeof(StringAssociationType));
        this.StringRoleType = this.AddMetaClass(new Guid("b8d12288-b5aa-4b1a-9226-c8902cd684c1"), typeof(StringRoleType));
        this.UniqueAssociationType = this.AddMetaClass(new Guid("1e674346-ccff-43e5-a31d-842803e30957"), typeof(UniqueAssociationType));
        this.UniqueRoleType = this.AddMetaClass(new Guid("849fd4fa-951a-404e-b481-66db3dd43fd4"), typeof(UniqueRoleType));
        this.ToManyRoleType = this.AddMetaInterface(new Guid("7480bb35-30ad-47a6-8a70-e29d11b76539"), "ToManyRoleType ");
        this.ToOneRoleType = this.AddMetaInterface(new Guid("774a1bce-c414-423f-84a0-450be5eba46d"), "ToOneRoleType");
        this.Type = this.AddMetaInterface(new Guid("08768b55-1d35-427b-8209-9ad7c3c77d18"), "Type");
        this.Unit = this.AddMetaClass(new Guid("32c01fd7-9002-4a3a-b364-1b2ecae870a0"), typeof(Unit));
        this.UnitAssociationType = this.AddMetaInterface(new Guid("76e3b379-86c0-4015-b279-dfc8b272a128"), "UnitAssociationType");
        this.UnitRoleType = this.AddMetaInterface(new Guid("d295663a-1731-4c05-930b-a51002c25453"), "UnitRoleType");
        this.Workspace = this.AddMetaClass(new Guid("80473529-985e-4b70-a17f-9cc63276dca6"), typeof(Workspace));

        // Inheritance
        this.AssociationType.AddDirectSupertype(this.RelationEndType);
        this.Class.AddDirectSupertype(this.Composite);
        this.Composite.AddDirectSupertype(this.ObjectType);
        this.CompositeAssociationType.AddDirectSupertype(this.AssociationType);
        this.CompositeRoleType.AddDirectSupertype(this.RoleType);
        this.Domain.AddDirectSupertype(this.MetaObject);
        this.Interface.AddDirectSupertype(this.Composite);
        this.ManyToAssociationType.AddDirectSupertype(this.CompositeAssociationType);
        this.ManyToManyAssociationType.AddDirectSupertype(this.ManyToAssociationType);
        this.ManyToManyRoleType.AddDirectSupertype(this.ToManyRoleType);
        this.ManyToOneAssociationType.AddDirectSupertype(this.ManyToAssociationType);
        this.ManyToOneRoleType.AddDirectSupertype(this.ToOneRoleType);
        this.MethodType.AddDirectSupertype(this.OperandType);
        this.ObjectType.AddDirectSupertype(this.Type);
        this.OneToAssociationType.AddDirectSupertype(this.CompositeAssociationType);
        this.OneToManyAssociationType.AddDirectSupertype(this.OneToAssociationType);
        this.OneToManyRoleType.AddDirectSupertype(this.ToManyRoleType);
        this.OneToOneAssociationType.AddDirectSupertype(this.OneToAssociationType);
        this.OneToOneRoleType.AddDirectSupertype(this.ToOneRoleType);
        this.OperandType.AddDirectSupertype(this.Type);
        this.RelationEndType.AddDirectSupertype(this.OperandType);
        this.RoleType.AddDirectSupertype(this.RelationEndType);
        this.BinaryAssociationType.AddDirectSupertype(this.UnitAssociationType);
        this.BinaryRoleType.AddDirectSupertype(this.UnitRoleType);
        this.BooleanAssociationType.AddDirectSupertype(this.UnitAssociationType);
        this.BooleanRoleType.AddDirectSupertype(this.UnitRoleType);
        this.DateTimeAssociationType.AddDirectSupertype(this.UnitAssociationType);
        this.DateTimeRoleType.AddDirectSupertype(this.UnitRoleType);
        this.DecimalAssociationType.AddDirectSupertype(this.UnitAssociationType);
        this.DecimalRoleType.AddDirectSupertype(this.UnitRoleType);
        this.FloatAssociationType.AddDirectSupertype(this.UnitAssociationType);
        this.FloatRoleType.AddDirectSupertype(this.UnitRoleType);
        this.IntegerAssociationType.AddDirectSupertype(this.UnitAssociationType);
        this.IntegerRoleType.AddDirectSupertype(this.UnitRoleType);
        this.StringAssociationType.AddDirectSupertype(this.UnitAssociationType);
        this.StringRoleType.AddDirectSupertype(this.UnitRoleType);
        this.UniqueAssociationType.AddDirectSupertype(this.UnitAssociationType);
        this.UniqueRoleType.AddDirectSupertype(this.UnitRoleType);
        this.ToManyRoleType.AddDirectSupertype(this.CompositeRoleType);
        this.ToOneRoleType.AddDirectSupertype(this.CompositeRoleType);
        this.Type.AddDirectSupertype(this.MetaObject);
        this.Unit.AddDirectSupertype(this.ObjectType);
        this.UnitAssociationType.AddDirectSupertype(this.AssociationType);
        this.UnitRoleType.AddDirectSupertype(this.RoleType);
        this.Workspace.AddDirectSupertype(this.MetaObject);

        // Relations
        var metaMeta = this.MetaMeta;

        this.AssociationTypeComposite = metaMeta.AddManyToOne(this.AssociationType, this.Composite);

        this.CompositeDirectSupertypes = metaMeta.AddManyToMany(this.Composite, this.Interface, "DirectSupertype");
        this.CompositeSupertypes = metaMeta.AddManyToMany(this.Composite, this.Interface, "Supertype");

        this.DecimalRoleTypeAssignedPrecision = metaMeta.AddUnit(this.DecimalRoleType, this.Integer, "AssignedPrecision");
        this.DecimalRoleTypeDerivedPrecision = metaMeta.AddUnit(this.DecimalRoleType, this.Integer, "DerivedPrecision");
        this.DecimalRoleTypeAssignedScale = metaMeta.AddUnit(this.DecimalRoleType, this.Integer, "AssignedScale");
        this.DecimalRoleTypeDerivedScale = metaMeta.AddUnit(this.DecimalRoleType, this.Integer, "DerivedScale");

        this.DomainTypes = metaMeta.AddManyToMany(this.Domain, this.Type);

        this.ObjectTypeAssignedPluralName = metaMeta.AddUnit(this.ObjectType, this.String, "AssignedPluralName");
        this.ObjectTypeDerivedPluralName = metaMeta.AddUnit(this.ObjectType, this.String, "DerivedPluralName");
        this.ObjectTypeSingularName = metaMeta.AddUnit(this.ObjectType, this.String, "SingularName");

        this.MetaObjectId = metaMeta.AddUnit(this.MetaObject, this.Unique, "Id");

        this.RelationEndTypeIsMany = metaMeta.AddUnit(this.RelationEndType, this.Boolean, "IsMany");

        this.RoleTypeAssociationType = metaMeta.AddOneToOne(this.RoleType, this.AssociationType);
        this.RoleTypeAssignedPluralName = metaMeta.AddUnit(this.RoleType, this.String, "AssignedPluralName");
        this.RoleTypeDerivedPluralName = metaMeta.AddUnit(this.RoleType, this.String, "DerivedPluralName");
        this.RoleTypeObjectType = metaMeta.AddManyToOne(this.RoleType, this.ObjectType);
        this.RoleTypeName = metaMeta.AddUnit(this.RoleType, this.String, "Name");
        this.RoleTypeSingularName = metaMeta.AddUnit(this.RoleType, this.String, "SingularName");

        this.StringRoleTypeAssignedSize = metaMeta.AddUnit(this.StringRoleType, this.Integer, "AssignedSize");
        this.StringRoleTypeDerivedSize = metaMeta.AddUnit(this.StringRoleType, this.Integer, "DerivedSize");

        this.WorkspaceTypes = metaMeta.AddManyToMany(this.Workspace, this.Type);
    }

    /// <summary>
    /// The meta meta.
    /// </summary>
    public MetaMeta MetaMeta { get; }

    /// <summary>
    /// An association type.
    /// </summary>
    public MetaObjectType AssociationType { get; init; }

    /// <summary>
    /// The composite of an association type.
    /// </summary>
    public MetaManyToOneRoleType AssociationTypeComposite { get; init; }

    /// <summary>
    /// A class.
    /// </summary>
    public MetaObjectType Class { get; init; }

    /// <summary>
    /// A composite.
    /// </summary>
    public MetaObjectType Composite { get; init; }

    /// <summary>
    /// Composite association type.
    /// </summary>
    public MetaObjectType CompositeAssociationType { get; set; }

    /// <summary>
    /// Composite role type.
    /// </summary>
    public MetaObjectType CompositeRoleType { get; set; }

    /// <summary>
    /// The supertypes of a composite.
    /// </summary>
    public MetaManyToManyRoleType CompositeSupertypes { get; init; }

    /// <summary>
    /// The direct supertypes of a composite.
    /// </summary>
    public MetaManyToManyRoleType CompositeDirectSupertypes { get; init; }

    /// <summary>
    /// A domain.
    /// </summary>
    public MetaObjectType Domain { get; init; }

    /// <summary>
    /// The types of a domain.
    /// </summary>
    public MetaManyToManyRoleType DomainTypes { get; init; }

    /// <summary>
    /// An interface.
    /// </summary>
    public MetaObjectType Interface { get; init; }

    /// <summary>
    /// An boolean.
    /// </summary>
    public MetaObjectType Boolean { get; init; }

    /// <summary>
    /// An string.
    /// </summary>
    public MetaObjectType Integer { get; init; }

    /// <summary>
    /// An string.
    /// </summary>
    public MetaObjectType String { get; init; }

    /// <summary>
    /// A unique.
    /// </summary>
    public MetaObjectType Unique { get; init; }

    /// <summary>
    /// Many to association type
    /// </summary>
    public MetaObjectType ManyToAssociationType { get; set; }

    /// <summary>
    /// Many to many association type.
    /// </summary>
    public MetaObjectType ManyToManyAssociationType { get; set; }

    /// <summary>
    /// Many to many role type.
    /// </summary>
    public MetaObjectType ManyToManyRoleType { get; set; }

    /// <summary>
    /// Many to one association type.
    /// </summary>
    public MetaObjectType ManyToOneAssociationType { get; set; }

    /// <summary>
    /// Many to one role type.
    /// </summary>
    public MetaObjectType ManyToOneRoleType { get; set; }

    /// <summary>
    /// A meta object.
    /// </summary>
    public MetaObjectType MetaObject { get; init; }

    /// <summary>
    /// The id of a meta object.
    /// </summary>
    public MetaUnitRoleType MetaObjectId { get; init; }

    /// <summary>
    /// A method type.
    /// </summary>
    public MetaObjectType MethodType { get; init; }

    /// <summary>
    /// An object type.
    /// </summary>
    public MetaObjectType ObjectType { get; init; }

    /// <summary>
    /// The assigned plural name of an object type.
    /// </summary>
    public MetaUnitRoleType ObjectTypeAssignedPluralName { get; init; }

    /// <summary>
    /// The derived plural name of an object type.
    /// </summary>
    public MetaUnitRoleType ObjectTypeDerivedPluralName { get; init; }

    /// <summary>
    /// The singular name of an object type.
    /// </summary>
    public MetaUnitRoleType ObjectTypeSingularName { get; init; }

    /// <summary>
    /// One to association type.
    /// </summary>
    public MetaObjectType OneToAssociationType { get; set; }

    /// <summary>
    /// One to many association type.
    /// </summary>
    public MetaObjectType OneToManyAssociationType { get; set; }

    /// <summary>
    /// One to many role type.
    /// </summary>
    public MetaObjectType OneToManyRoleType { get; set; }

    /// <summary>
    /// One to one association type.
    /// </summary>
    public MetaObjectType OneToOneAssociationType { get; set; }

    /// <summary>
    /// One to one role type.
    /// </summary>
    public MetaObjectType OneToOneRoleType { get; set; }

    /// <summary>
    /// An operand type.
    /// </summary>public MetaObjectType OperandType { get; init; }
    public MetaObjectType OperandType { get; init; }

    /// <summary>
    /// A relation end type.
    /// </summary>public MetaObjectType OperandType { get; init; }
    public MetaObjectType RelationEndType { get; init; }

    /// <summary>
    /// The is many of a role type.
    /// </summary>
    public MetaUnitRoleType RelationEndTypeIsMany { get; }

    /// <summary>
    /// A role type.
    /// </summary>
    public MetaObjectType RoleType { get; init; }

    /// <summary>
    /// The association type a role type.
    /// </summary>
    public MetaOneToOneRoleType RoleTypeAssociationType { get; init; }

    /// <summary>
    /// The assigned plural name of a role type.
    /// </summary>
    public MetaUnitRoleType RoleTypeAssignedPluralName { get; init; }

    /// <summary>
    /// The derived plural name of a role type.
    /// </summary>
    public MetaUnitRoleType RoleTypeDerivedPluralName { get; init; }

    /// <summary>
    /// The role type of object type.
    /// </summary>
    public MetaManyToOneRoleType RoleTypeObjectType { get; init; }

    /// <summary>
    /// The name of role type.
    /// </summary>
    public MetaUnitRoleType RoleTypeName { get; init; }

    /// <summary>
    /// The singular name of role type.
    /// </summary>
    public MetaUnitRoleType RoleTypeSingularName { get; init; }

    /// <summary>
    /// The binary association type.
    /// </summary>
    public MetaObjectType BinaryAssociationType { get; set; }

    /// <summary>
    /// The binary role type.
    /// </summary>
    public MetaObjectType BinaryRoleType { get; set; }

    /// <summary>
    /// The boolean association type.
    /// </summary>
    public MetaObjectType BooleanAssociationType { get; set; }

    /// <summary>
    /// The boolean role type.
    /// </summary>
    public MetaObjectType BooleanRoleType { get; set; }

    /// <summary>
    /// The dateTime association type.
    /// </summary>
    public MetaObjectType DateTimeAssociationType { get; set; }

    /// <summary>
    /// The dateTime role type.
    /// </summary>
    public MetaObjectType DateTimeRoleType { get; set; }

    /// <summary>
    /// The decimal association type.
    /// </summary>
    public MetaObjectType DecimalAssociationType { get; set; }

    /// <summary>
    /// The decimal role type.
    /// </summary>
    public MetaObjectType DecimalRoleType { get; set; }

    /// <summary>
    /// The precision of the decimal role type.
    /// </summary>
    public MetaUnitRoleType DecimalRoleTypeAssignedPrecision { get; init; }

    /// <summary>
    /// The derived precision of the decimal role type.
    /// </summary>
    public MetaUnitRoleType DecimalRoleTypeDerivedPrecision { get; init; }

    /// <summary>
    /// The assigned scale of the decimal role type.
    /// </summary>
    public MetaUnitRoleType DecimalRoleTypeAssignedScale { get; init; }

    /// <summary>
    /// The derived scale of the decimal role type.
    /// </summary>
    public MetaUnitRoleType DecimalRoleTypeDerivedScale { get; init; }

    /// <summary>
    /// The float association type.
    /// </summary>
    public MetaObjectType FloatAssociationType { get; set; }

    /// <summary>
    /// The float role type.
    /// </summary>
    public MetaObjectType FloatRoleType { get; set; }

    /// <summary>
    /// The integer association type.
    /// </summary>
    public MetaObjectType IntegerAssociationType { get; set; }

    /// <summary>
    /// The integer role type.
    /// </summary>
    public MetaObjectType IntegerRoleType { get; set; }

    /// <summary>
    /// The string association type.
    /// </summary>
    public MetaObjectType StringAssociationType { get; set; }

    /// <summary>
    /// The string role type.
    /// </summary>
    public MetaObjectType StringRoleType { get; set; }

    /// <summary>
    /// The assigned size of the string role type.
    /// </summary>
    public MetaUnitRoleType StringRoleTypeAssignedSize { get; init; }

    /// <summary>
    /// The derived size of the string role type.
    /// </summary>
    public MetaUnitRoleType StringRoleTypeDerivedSize { get; init; }

    /// <summary>
    /// The unique association type.
    /// </summary>
    public MetaObjectType UniqueAssociationType { get; set; }

    /// <summary>
    /// The unique role type.
    /// </summary>
    public MetaObjectType UniqueRoleType { get; set; }

    /// <summary>
    /// To many role type.
    /// </summary>
    public MetaObjectType ToManyRoleType { get; set; }

    /// <summary>
    /// To one role type.
    /// </summary>
    public MetaObjectType ToOneRoleType { get; set; }

    /// <summary>
    /// A type.
    /// </summary>
    public MetaObjectType Type { get; init; }

    /// <summary>
    /// A unit.
    /// </summary>
    public MetaObjectType Unit { get; init; }

    /// <summary>
    /// Unit association type.
    /// </summary>
    public MetaObjectType UnitAssociationType { get; set; }

    /// <summary>
    /// Unit role type.
    /// </summary>
    public MetaObjectType UnitRoleType { get; set; }

    /// <summary>
    /// A workspace.
    /// </summary>
    public MetaObjectType Workspace { get; init; }

    /// <summary>
    /// The types of a workspace.
    /// </summary>
    public MetaManyToManyRoleType WorkspaceTypes { get; init; }

    /// <summary>
    /// Creates a new meta class.
    /// </summary>
    public MetaObjectType AddMetaClass(Guid id, Type type) => this.MetaMeta.AddClass(id, type);

    /// <summary>
    /// Creates a new meta interface.
    /// </summary>
    public MetaObjectType AddMetaInterface(Guid id, string name) => this.MetaMeta.AddInterface(id, name);

    /// <summary>
    /// Creates a new meta interface.
    /// </summary>
    public MetaObjectType AddMetaUnit(Guid id, string name) => this.MetaMeta.AddUnit(id, name);

    /// <summary>
    /// Creates a new MetaPopulation
    /// </summary>
    public MetaPopulation CreateMetaPopulation()
    {
        var metaPopulation = new MetaPopulation(this.MetaMeta);

        metaPopulation.DerivationById[nameof(this.CompositeSupertypes)] = new CompositeSupertypes(metaPopulation, this);
        metaPopulation.DerivationById[nameof(this.DecimalRoleTypeDerivedPrecision)] = new DecimalRoleTypeDerivedPrecision(this);
        metaPopulation.DerivationById[nameof(this.DecimalRoleTypeDerivedScale)] = new DecimalRoleTypeDerivedScale(this);
        metaPopulation.DerivationById[nameof(this.RoleTypeName)] = new RoleTypeName(this);
        metaPopulation.DerivationById[nameof(this.StringRoleTypeDerivedSize)] = new StringRoleTypeDerivedSize(this);

        return metaPopulation;
    }
}
