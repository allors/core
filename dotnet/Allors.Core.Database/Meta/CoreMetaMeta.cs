namespace Allors.Core.Database.Meta;

using System;
using Allors.Core.Database.Meta.Derivations;
using Allors.Core.Database.Meta.Domain;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// Core MetaMeta.
/// </summary>
public sealed class CoreMetaMeta
{
    internal CoreMetaMeta()
    {
        this.MetaMeta = new MetaMeta();
        this.AllorsCore = this.MetaMeta.AddDomain(CoreIds.AllorsCore, "AllorsCore");

        var m = this.MetaMeta;
        var d = this.AllorsCore;

        // Units
        this.Boolean = m.AddUnit(d, CoreIds.Boolean, "Boolean");
        this.Integer = m.AddUnit(d, CoreIds.Integer, "Integer");
        this.String = m.AddUnit(d, CoreIds.String, "String");
        this.Unique = m.AddUnit(d, CoreIds.Unique, "Unique");

        // Composites
        this.AssociationType = m.AddInterface(d, CoreIds.AssociationType, "AssociationType");
        this.Class = m.AddClass(d, CoreIds.Class, typeof(Class));
        this.Composite = m.AddInterface(d, CoreIds.Composite, "Composite");
        this.CompositeAssociationType = m.AddInterface(d, CoreIds.CompositeAssociationType, "CompositeAssociationType");
        this.CompositeRoleType = m.AddInterface(d, CoreIds.CompositeRoleType, "CompositeRoleType");
        this.Domain = m.AddClass(d, CoreIds.Domain, typeof(Domain.Domain));
        this.Interface = m.AddClass(d, CoreIds.Interface, typeof(Interface));
        this.ManyToAssociationType = m.AddInterface(d, CoreIds.ManyToAssociationType, "ManyToAssociationType");
        this.ManyToManyAssociationType = m.AddClass(d, CoreIds.ManyToManyAssociationType, typeof(ManyToManyAssociationType));
        this.ManyToManyRoleType = m.AddClass(d, CoreIds.ManyToManyRoleType, typeof(ManyToManyRoleType));
        this.ManyToOneAssociationType = m.AddClass(d, CoreIds.ManyToOneAssociationType, typeof(ManyToOneAssociationType));
        this.ManyToOneRoleType = m.AddClass(d, CoreIds.ManyToOneRoleType, typeof(ManyToOneRoleType));
        this.MetaObject = m.AddInterface(d, CoreIds.MetaObject, "MetaObject");
        this.ObjectType = m.AddInterface(d, CoreIds.ObjectType, "ObjectType");
        this.OneToAssociationType = m.AddInterface(d, CoreIds.OneToAssociationType, "OneToAssociationType");
        this.OneToManyAssociationType = m.AddClass(d, CoreIds.OneToManyAssociationType, typeof(OneToManyAssociationType));
        this.OneToManyRoleType = m.AddClass(d, CoreIds.OneToManyRoleType, typeof(OneToManyRoleType));
        this.OneToOneAssociationType = m.AddClass(d, CoreIds.OneToOneAssociationType, typeof(OneToOneAssociationType));
        this.OneToOneRoleType = m.AddClass(d, CoreIds.OneToOneRoleType, typeof(OneToOneRoleType));
        this.OperandType = m.AddInterface(d, CoreIds.OperandType, "OperandType");
        this.RelationEndType = m.AddInterface(d, CoreIds.RelationEndType, "RelationEndType");
        this.RoleType = m.AddInterface(d, CoreIds.RoleType, "RoleType");
        this.BinaryAssociationType = m.AddClass(d, CoreIds.BinaryAssociationType, typeof(BinaryAssociationType));
        this.BinaryRoleType = m.AddClass(d, CoreIds.BinaryRoleType, typeof(BinaryRoleType));
        this.BooleanAssociationType = m.AddClass(d, CoreIds.BooleanAssociationType, typeof(BooleanAssociationType));
        this.BooleanRoleType = m.AddClass(d, CoreIds.BooleanRoleType, typeof(BooleanRoleType));
        this.DateTimeAssociationType = m.AddClass(d, CoreIds.DateTimeAssociationType, typeof(DateTimeAssociationType));
        this.DateTimeRoleType = m.AddClass(d, CoreIds.DateTimeRoleType, typeof(DateTimeRoleType));
        this.DecimalAssociationType = m.AddClass(d, CoreIds.DecimalAssociationType, typeof(DecimalAssociationType));
        this.DecimalRoleType = m.AddClass(d, CoreIds.DecimalRoleType, typeof(DecimalRoleType));
        this.FloatAssociationType = m.AddClass(d, CoreIds.FloatAssociationType, typeof(FloatAssociationType));
        this.FloatRoleType = m.AddClass(d, CoreIds.FloatRoleType, typeof(FloatRoleType));
        this.Inheritance = m.AddClass(d, CoreIds.Inheritance, typeof(Inheritance));
        this.IntegerAssociationType = m.AddClass(d, CoreIds.IntegerAssociationType, typeof(IntegerAssociationType));
        this.IntegerRoleType = m.AddClass(d, CoreIds.IntegerRoleType, typeof(IntegerRoleType));
        this.StringAssociationType = m.AddClass(d, CoreIds.StringAssociationType, typeof(StringAssociationType));
        this.StringRoleType = m.AddClass(d, CoreIds.StringRoleType, typeof(StringRoleType));
        this.UniqueAssociationType = m.AddClass(d, CoreIds.UniqueAssociationType, typeof(UniqueAssociationType));
        this.UniqueRoleType = m.AddClass(d, CoreIds.UniqueRoleType, typeof(UniqueRoleType));
        this.ToManyRoleType = m.AddInterface(d, CoreIds.ToManyRoleType, "ToManyRoleType ");
        this.ToOneRoleType = m.AddInterface(d, CoreIds.ToOneRoleType, "ToOneRoleType");
        this.Type = m.AddInterface(d, CoreIds.Type, "Type");
        this.Unit = m.AddClass(d, CoreIds.Unit, typeof(Unit));
        this.UnitAssociationType = m.AddInterface(d, CoreIds.UnitAssociationType, "UnitAssociationType");
        this.UnitRoleType = m.AddInterface(d, CoreIds.UnitRoleType, "UnitRoleType");

        // Inheritance
        m.AddInheritance(d, new Guid("12ec1d97-6208-4cf5-866b-58675aa8a38e"), this.AssociationType, this.RelationEndType);
        m.AddInheritance(d, new Guid("6625058e-9338-4d1e-a7ce-6f241bd9aae7"), this.Class, this.Composite);
        m.AddInheritance(d, new Guid("fd3085a2-be84-44c0-941b-5cb3e161d4a1"), this.Composite, this.ObjectType);
        m.AddInheritance(d, new Guid("f4e20681-b5a4-4489-8aee-3d87367d89f3"), this.CompositeAssociationType, this.AssociationType);
        m.AddInheritance(d, new Guid("dba58e59-d8e6-4d48-8510-828bef2efd1c"), this.CompositeRoleType, this.RoleType);
        m.AddInheritance(d, new Guid("73ba5162-5de9-40bd-8e86-beba99ee6d7a"), this.Domain, this.MetaObject);
        m.AddInheritance(d, new Guid("954d70cd-86a2-45b6-8e6c-41302b07c946"), this.Interface, this.Composite);
        m.AddInheritance(d, new Guid("976a8fd0-07fc-4e49-886f-22de54335ff6"), this.ManyToAssociationType, this.CompositeAssociationType);
        m.AddInheritance(d, new Guid("4a5f0a15-880d-4ce6-8802-f5b646f7d546"), this.ManyToManyAssociationType, this.ManyToAssociationType);
        m.AddInheritance(d, new Guid("7adb90ba-5646-4e5d-b6b2-6ab2a812be82"), this.ManyToManyRoleType, this.ToManyRoleType);
        m.AddInheritance(d, new Guid("ecda2eca-32d8-45f7-ab49-709357ff604f"), this.ManyToOneAssociationType, this.ManyToAssociationType);
        m.AddInheritance(d, new Guid("a7e687d0-033d-4f33-9fec-20b6b29fac4a"), this.ManyToOneRoleType, this.ToOneRoleType);
        m.AddInheritance(d, new Guid("ac7996e5-0480-4d63-82bd-5e0435f59a2d"), this.ObjectType, this.Type);
        m.AddInheritance(d, new Guid("3fb261ac-3de2-4cfc-843a-770c579d1152"), this.OneToAssociationType, this.CompositeAssociationType);
        m.AddInheritance(d, new Guid("8f893293-bb22-4c44-ac02-562263031c77"), this.OneToManyAssociationType, this.OneToAssociationType);
        m.AddInheritance(d, new Guid("30e042db-6458-48f6-bb95-9702959d782b"), this.OneToManyRoleType, this.ToManyRoleType);
        m.AddInheritance(d, new Guid("d748f589-0bc7-4aa6-8ad7-7040c16eb60f"), this.OneToOneAssociationType, this.OneToAssociationType);
        m.AddInheritance(d, new Guid("3840768c-2827-48d4-ab25-e365a134e2d6"), this.OneToOneRoleType, this.ToOneRoleType);
        m.AddInheritance(d, new Guid("04bf5359-45a3-4fee-9c4a-1701b706e90a"), this.OperandType, this.Type);
        m.AddInheritance(d, new Guid("e4296e62-702e-40ba-a397-2daabf905880"), this.RelationEndType, this.OperandType);
        m.AddInheritance(d, new Guid("054a729d-f4ff-47fc-bcec-7506116093a5"), this.RoleType, this.RelationEndType);
        m.AddInheritance(d, new Guid("baa9f110-3ef3-43d9-b0a8-507eb072f204"), this.BinaryAssociationType, this.UnitAssociationType);
        m.AddInheritance(d, new Guid("f8368952-f032-4310-b932-b4294b944e3b"), this.BinaryRoleType, this.UnitRoleType);
        m.AddInheritance(d, new Guid("cd773c76-a9b5-4ed9-8009-849ca947fe53"), this.BooleanAssociationType, this.UnitAssociationType);
        m.AddInheritance(d, new Guid("dc79c64b-1320-455d-b445-02cfd80a25dc"), this.BooleanRoleType, this.UnitRoleType);
        m.AddInheritance(d, new Guid("8dedd0ab-2d24-4716-8c9e-078d2102205b"), this.DateTimeAssociationType, this.UnitAssociationType);
        m.AddInheritance(d, new Guid("3d08f874-93ad-4ccd-9d07-6b8fcaa56346"), this.DateTimeRoleType, this.UnitRoleType);
        m.AddInheritance(d, new Guid("f21e0dff-6a4a-4c3e-8668-2b829fa9441f"), this.DecimalAssociationType, this.UnitAssociationType);
        m.AddInheritance(d, new Guid("7249f569-fc71-4383-b617-238f8f429f36"), this.DecimalRoleType, this.UnitRoleType);
        m.AddInheritance(d, new Guid("42e6e55d-3b61-4c14-b3d4-831c7e31b51b"), this.FloatAssociationType, this.UnitAssociationType);
        m.AddInheritance(d, new Guid("fb2e2ac7-327e-4dd6-a735-0c0656c2de10"), this.FloatRoleType, this.UnitRoleType);
        m.AddInheritance(d, new Guid("8ae84e0e-c8dd-4315-824c-5e022796b3ea"), this.Inheritance, this.MetaObject);
        m.AddInheritance(d, new Guid("0fbc3430-972d-4f0e-957c-64b35cc24df1"), this.IntegerAssociationType, this.UnitAssociationType);
        m.AddInheritance(d, new Guid("7cca3842-c6a1-433a-9e65-34d2ef116673"), this.IntegerRoleType, this.UnitRoleType);
        m.AddInheritance(d, new Guid("7df8b0b9-8bad-49f9-9300-a8c402a22f6d"), this.StringAssociationType, this.UnitAssociationType);
        m.AddInheritance(d, new Guid("215dbfb0-ce0a-4a19-992f-1f7112c53454"), this.StringRoleType, this.UnitRoleType);
        m.AddInheritance(d, new Guid("06bbf5f6-4859-4847-a90c-093fc9aee40a"), this.UniqueAssociationType, this.UnitAssociationType);
        m.AddInheritance(d, new Guid("fec15108-1b04-415a-8f9b-135d2db267f5"), this.UniqueRoleType, this.UnitRoleType);
        m.AddInheritance(d, new Guid("a68e3111-d057-4fed-956c-8a3612baeace"), this.ToManyRoleType, this.CompositeRoleType);
        m.AddInheritance(d, new Guid("4bc01b4b-1cb3-493c-9e9a-62b8d0df42ef"), this.ToOneRoleType, this.CompositeRoleType);
        m.AddInheritance(d, new Guid("9c5a71e4-be03-49f2-986f-1fff3078e18e"), this.Type, this.MetaObject);
        m.AddInheritance(d, new Guid("3bc240cd-a431-41d2-bae3-fe6ef3dfc1f2"), this.Unit, this.ObjectType);
        m.AddInheritance(d, new Guid("75e71eea-aa9c-42ef-bd5b-9f61182bba29"), this.UnitAssociationType, this.AssociationType);
        m.AddInheritance(d, new Guid("2f05d2cf-63b2-4f32-83c8-96403d934fef"), this.UnitRoleType, this.RoleType);

        // Relations
        this.AssociationTypeComposite = m.AddManyToOneRelation(d, new Guid("cea0ed95-ae21-451d-bef3-2cd6ccb34c29"), CoreIds.AssociationTypeComposite, this.AssociationType, this.Composite);

        this.CompositeDirectSupertypes = m.AddManyToManyRelation(d, new Guid("960162a2-a902-4cd9-9576-ccb0e7c9e01c"), CoreIds.CompositeDirectSupertypes, this.Composite, this.Interface, "DirectSupertype");
        this.CompositeSupertypes = m.AddManyToManyRelation(d, new Guid("b0129926-2eb2-4ac7-80e0-93a2329964cf"), CoreIds.CompositeSupertypes, this.Composite, this.Interface, "Supertype");

        this.DecimalRoleTypeAssignedPrecision = m.AddUnitRelation(d, new Guid("5b2ac86d-af09-46f6-a454-4d2a3bdeaed3"), CoreIds.DecimalRoleTypeAssignedPrecision, this.DecimalRoleType, this.Integer, "AssignedPrecision");
        this.DecimalRoleTypeDerivedPrecision = m.AddUnitRelation(d, new Guid("3c25e09b-6b89-46e9-b1bb-f89c4d2fa4db"), CoreIds.DecimalRoleTypeDerivedPrecision, this.DecimalRoleType, this.Integer, "DerivedPrecision");
        this.DecimalRoleTypeAssignedScale = m.AddUnitRelation(d, new Guid("ac79ed22-0a87-4370-9261-03b27ad8bbe6"), CoreIds.DecimalRoleTypeAssignedScale, this.DecimalRoleType, this.Integer, "AssignedScale");
        this.DecimalRoleTypeDerivedScale = m.AddUnitRelation(d, new Guid("ebb42e88-b2c0-48ba-8865-67e86ef6d84e"), CoreIds.DecimalRoleTypeDerivedScale, this.DecimalRoleType, this.Integer, "DerivedScale");

        this.DomainName = m.AddUnitRelation(d, new Guid("1dd008c4-755e-4f84-b52f-360afaf1a6cd"), CoreIds.DomainName, this.Domain, this.String, "Name");
        this.DomainSuperdomains = m.AddManyToManyRelation(d, new Guid("ae8311c8-75ea-426e-ad50-3f400accb264"), CoreIds.DomainSuperdomains, this.Domain, this.Domain, "Superdomain");
        this.DomainTypes = m.AddManyToManyRelation(d, new Guid("93dcadad-f9e0-402a-bd4f-d150df5f5c26"), CoreIds.DomainTypes, this.Domain, this.Type);

        this.InheritanceSubtype = m.AddManyToOneRelation(d, new Guid("e2cd96af-a436-439c-b02b-ecf23f19a96f"), CoreIds.InheritanceSubtype, this.Inheritance, this.Composite, "Subtype");
        this.InheritanceSupertype = m.AddManyToOneRelation(d, new Guid("c9f8f651-5b6e-408d-b088-0ca925e8b2d5"), CoreIds.InheritanceSupertype, this.Inheritance, this.Interface, "Supertype");

        this.ObjectTypeAssignedPluralName = m.AddUnitRelation(d, new Guid("c45fcfac-5614-4a03-b86a-df2f75cc5fcb"), CoreIds.ObjectTypeAssignedPluralName, this.ObjectType, this.String, "AssignedPluralName");
        this.ObjectTypeDerivedPluralName = m.AddUnitRelation(d, new Guid("edb514d1-f0b6-44d0-a0ce-2ff9a132b4f7"), CoreIds.ObjectTypeDerivedPluralName, this.ObjectType, this.String, "DerivedPluralName");
        this.ObjectTypeSingularName = m.AddUnitRelation(d, new Guid("4ae8c269-d329-4d01-b48f-3df905c0a9e0"), CoreIds.ObjectTypeSingularName, this.ObjectType, this.String, "SingularName");

        this.MetaObjectId = m.AddUnitRelation(d, new Guid("3f24ceec-222a-4933-881c-3f85b6afebac"), CoreIds.MetaObjectId, this.MetaObject, this.Unique, "Id");

        this.RelationEndTypeIsMany = m.AddUnitRelation(d, new Guid("1d3957c3-d1a7-45f4-b6a6-5554223d7326"), CoreIds.RelationEndTypeIsMany, this.RelationEndType, this.Boolean, "IsMany");

        this.RoleTypeAssociationType = m.AddOneToOneRelation(d, new Guid("ff05c66a-7a87-4b1e-8b77-c097285ec7ca"), CoreIds.RoleTypeAssociationType, this.RoleType, this.AssociationType);
        this.RoleTypeAssignedPluralName = m.AddUnitRelation(d, new Guid("39a5bdaf-3f44-436a-b9e8-a64a5bb770dd"), CoreIds.RoleTypeAssignedPluralName, this.RoleType, this.String, "AssignedPluralName");
        this.RoleTypeDerivedPluralName = m.AddUnitRelation(d, new Guid("cd6a33fa-4537-4a83-a6b6-2d79725fb46e"), CoreIds.RoleTypeDerivedPluralName, this.RoleType, this.String, "DerivedPluralName");
        this.RoleTypeObjectType = m.AddManyToOneRelation(d, new Guid("ff21b9ad-1a0b-40f5-a35f-5bfb9ed1ab02"), CoreIds.RoleTypeObjectType, this.RoleType, this.ObjectType);
        this.RoleTypeName = m.AddUnitRelation(d, new Guid("15e2d931-89d1-480a-8bb2-42e7902c7ba7"), CoreIds.RoleTypeName, this.RoleType, this.String, "Name");
        this.RoleTypeSingularName = m.AddUnitRelation(d, new Guid("13ba399e-bf05-48fc-8489-b70fb19fba15"), CoreIds.RoleTypeSingularName, this.RoleType, this.String, "SingularName");

        this.StringRoleTypeAssignedSize = m.AddUnitRelation(d, new Guid("da78d554-bb1b-451c-ade1-4f6efe651d7c"), CoreIds.StringRoleTypeAssignedSize, this.StringRoleType, this.Integer, "AssignedSize");
        this.StringRoleTypeDerivedSize = m.AddUnitRelation(d, new Guid("46d33068-969c-4a88-a703-fb5f0d8e0cd8"), CoreIds.StringRoleTypeDerivedSize, this.StringRoleType, this.Integer, "DerivedSize");
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
    /// The name of a domain.
    /// </summary>
    public MetaUnitRoleType DomainName { get; init; }

    /// <summary>
    /// The super domains of a domain.
    /// </summary>
    public MetaManyToManyRoleType DomainSuperdomains { get; init; }

    /// <summary>
    /// The types of a domain.
    /// </summary>
    public MetaManyToManyRoleType DomainTypes { get; init; }

    /// <summary>
    /// An inheritance.
    /// </summary>
    public MetaObjectType Inheritance { get; init; }

    /// <summary>
    /// The subtype of an inheritance.
    /// </summary>
    public MetaManyToOneRoleType InheritanceSubtype { get; init; }

    /// <summary>
    /// The supertype of an inheritance.
    /// </summary>
    public MetaManyToOneRoleType InheritanceSupertype { get; init; }

    /// <summary>
    /// An interface.
    /// </summary>
    public MetaObjectType Interface { get; init; }

    /// <summary>
    /// The allors core meta domain.
    /// </summary>
    public MetaDomain AllorsCore { get; init; }

    /// <summary>
    /// A boolean.
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
    /// Creates a new MetaMeta
    /// </summary>
    public Meta CreateMetaPopulation()
    {
        var metaPopulation = new Meta(this.MetaMeta);

        metaPopulation.DerivationById[nameof(this.CompositeDirectSupertypes)] = new CompositeDirectSupertypes(metaPopulation, this);
        metaPopulation.DerivationById[nameof(this.CompositeSupertypes)] = new CompositeSupertypes(metaPopulation, this);
        metaPopulation.DerivationById[nameof(this.DecimalRoleTypeDerivedPrecision)] = new DecimalRoleTypeDerivedPrecision(this);
        metaPopulation.DerivationById[nameof(this.DecimalRoleTypeDerivedScale)] = new DecimalRoleTypeDerivedScale(this);
        metaPopulation.DerivationById[nameof(this.RoleTypeName)] = new RoleTypeName(this);
        metaPopulation.DerivationById[nameof(this.StringRoleTypeDerivedSize)] = new StringRoleTypeDerivedSize(this);

        return metaPopulation;
    }
}
