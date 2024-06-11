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
        var m = this.MetaMeta;
        var d = this.MetaMeta.AddDomain(CoreIds.AllorsCore, "AllorsCore");

        // Units
        var boolean = m.AddUnit(d, CoreIds.Boolean, "Boolean");
        var integer = m.AddUnit(d, CoreIds.Integer, "Integer");
        var @string = m.AddUnit(d, CoreIds.String, "String");
        var unique = m.AddUnit(d, CoreIds.Unique, "Unique");

        // Composites
        var associationType = m.AddInterface(d, CoreIds.AssociationType, "AssociationType");
        var @class = m.AddClass(d, CoreIds.Class, typeof(Class));
        var composite = m.AddInterface(d, CoreIds.Composite, "Composite");
        var compositeAssociationType = m.AddInterface(d, CoreIds.CompositeAssociationType, "CompositeAssociationType");
        var compositeRoleType = m.AddInterface(d, CoreIds.CompositeRoleType, "CompositeRoleType");
        var domain = m.AddClass(d, CoreIds.Domain, typeof(Domain.Domain));
        var @interface = m.AddClass(d, CoreIds.Interface, typeof(Interface));
        var manyToAssociationType = m.AddInterface(d, CoreIds.ManyToAssociationType, "ManyToAssociationType");
        var manyToManyAssociationType = m.AddClass(d, CoreIds.ManyToManyAssociationType, typeof(ManyToManyAssociationType));
        var manyToManyRoleType = m.AddClass(d, CoreIds.ManyToManyRoleType, typeof(ManyToManyRoleType));
        var manyToOneAssociationType = m.AddClass(d, CoreIds.ManyToOneAssociationType, typeof(ManyToOneAssociationType));
        var manyToOneRoleType = m.AddClass(d, CoreIds.ManyToOneRoleType, typeof(ManyToOneRoleType));
        var metaObject = m.AddInterface(d, CoreIds.MetaObject, "MetaObject");
        var objectType = m.AddInterface(d, CoreIds.ObjectType, "ObjectType");
        var oneToAssociationType = m.AddInterface(d, CoreIds.OneToAssociationType, "OneToAssociationType");
        var oneToManyAssociationType = m.AddClass(d, CoreIds.OneToManyAssociationType, typeof(OneToManyAssociationType));
        var oneToManyRoleType = m.AddClass(d, CoreIds.OneToManyRoleType, typeof(OneToManyRoleType));
        var oneToOneAssociationType = m.AddClass(d, CoreIds.OneToOneAssociationType, typeof(OneToOneAssociationType));
        var oneToOneRoleType = m.AddClass(d, CoreIds.OneToOneRoleType, typeof(OneToOneRoleType));
        var operandType = m.AddInterface(d, CoreIds.OperandType, "OperandType");
        var relationEndType = m.AddInterface(d, CoreIds.RelationEndType, "RelationEndType");
        var roleType = m.AddInterface(d, CoreIds.RoleType, "RoleType");
        var binaryAssociationType = m.AddClass(d, CoreIds.BinaryAssociationType, typeof(BinaryAssociationType));
        var binaryRoleType = m.AddClass(d, CoreIds.BinaryRoleType, typeof(BinaryRoleType));
        var booleanAssociationType = m.AddClass(d, CoreIds.BooleanAssociationType, typeof(BooleanAssociationType));
        var booleanRoleType = m.AddClass(d, CoreIds.BooleanRoleType, typeof(BooleanRoleType));
        var dateTimeAssociationType = m.AddClass(d, CoreIds.DateTimeAssociationType, typeof(DateTimeAssociationType));
        var dateTimeRoleType = m.AddClass(d, CoreIds.DateTimeRoleType, typeof(DateTimeRoleType));
        var decimalAssociationType = m.AddClass(d, CoreIds.DecimalAssociationType, typeof(DecimalAssociationType));
        var decimalRoleType = m.AddClass(d, CoreIds.DecimalRoleType, typeof(DecimalRoleType));
        var floatAssociationType = m.AddClass(d, CoreIds.FloatAssociationType, typeof(FloatAssociationType));
        var floatRoleType = m.AddClass(d, CoreIds.FloatRoleType, typeof(FloatRoleType));
        var inheritance = m.AddClass(d, CoreIds.Inheritance, typeof(Inheritance));
        var integerAssociationType = m.AddClass(d, CoreIds.IntegerAssociationType, typeof(IntegerAssociationType));
        var integerRoleType = m.AddClass(d, CoreIds.IntegerRoleType, typeof(IntegerRoleType));
        var stringAssociationType = m.AddClass(d, CoreIds.StringAssociationType, typeof(StringAssociationType));
        var stringRoleType = m.AddClass(d, CoreIds.StringRoleType, typeof(StringRoleType));
        var uniqueAssociationType = m.AddClass(d, CoreIds.UniqueAssociationType, typeof(UniqueAssociationType));
        var uniqueRoleType = m.AddClass(d, CoreIds.UniqueRoleType, typeof(UniqueRoleType));
        var toManyRoleType = m.AddInterface(d, CoreIds.ToManyRoleType, "ToManyRoleType ");
        var toOneRoleType = m.AddInterface(d, CoreIds.ToOneRoleType, "ToOneRoleType");
        var type = m.AddInterface(d, CoreIds.Type, "Type");
        var unit = m.AddClass(d, CoreIds.Unit, typeof(Unit));
        var unitAssociationType = m.AddInterface(d, CoreIds.UnitAssociationType, "UnitAssociationType");
        var unitRoleType = m.AddInterface(d, CoreIds.UnitRoleType, "UnitRoleType");

        // Inheritance
        m.AddInheritance(d, new Guid("12ec1d97-6208-4cf5-866b-58675aa8a38e"), associationType, relationEndType);
        m.AddInheritance(d, new Guid("6625058e-9338-4d1e-a7ce-6f241bd9aae7"), @class, composite);
        m.AddInheritance(d, new Guid("fd3085a2-be84-44c0-941b-5cb3e161d4a1"), composite, objectType);
        m.AddInheritance(d, new Guid("f4e20681-b5a4-4489-8aee-3d87367d89f3"), compositeAssociationType, associationType);
        m.AddInheritance(d, new Guid("dba58e59-d8e6-4d48-8510-828bef2efd1c"), compositeRoleType, roleType);
        m.AddInheritance(d, new Guid("73ba5162-5de9-40bd-8e86-beba99ee6d7a"), domain, metaObject);
        m.AddInheritance(d, new Guid("954d70cd-86a2-45b6-8e6c-41302b07c946"), @interface, composite);
        m.AddInheritance(d, new Guid("976a8fd0-07fc-4e49-886f-22de54335ff6"), manyToAssociationType, compositeAssociationType);
        m.AddInheritance(d, new Guid("4a5f0a15-880d-4ce6-8802-f5b646f7d546"), manyToManyAssociationType, manyToAssociationType);
        m.AddInheritance(d, new Guid("7adb90ba-5646-4e5d-b6b2-6ab2a812be82"), manyToManyRoleType, toManyRoleType);
        m.AddInheritance(d, new Guid("ecda2eca-32d8-45f7-ab49-709357ff604f"), manyToOneAssociationType, manyToAssociationType);
        m.AddInheritance(d, new Guid("a7e687d0-033d-4f33-9fec-20b6b29fac4a"), manyToOneRoleType, toOneRoleType);
        m.AddInheritance(d, new Guid("ac7996e5-0480-4d63-82bd-5e0435f59a2d"), objectType, type);
        m.AddInheritance(d, new Guid("3fb261ac-3de2-4cfc-843a-770c579d1152"), oneToAssociationType, compositeAssociationType);
        m.AddInheritance(d, new Guid("8f893293-bb22-4c44-ac02-562263031c77"), oneToManyAssociationType, oneToAssociationType);
        m.AddInheritance(d, new Guid("30e042db-6458-48f6-bb95-9702959d782b"), oneToManyRoleType, toManyRoleType);
        m.AddInheritance(d, new Guid("d748f589-0bc7-4aa6-8ad7-7040c16eb60f"), oneToOneAssociationType, oneToAssociationType);
        m.AddInheritance(d, new Guid("3840768c-2827-48d4-ab25-e365a134e2d6"), oneToOneRoleType, toOneRoleType);
        m.AddInheritance(d, new Guid("04bf5359-45a3-4fee-9c4a-1701b706e90a"), operandType, type);
        m.AddInheritance(d, new Guid("e4296e62-702e-40ba-a397-2daabf905880"), relationEndType, operandType);
        m.AddInheritance(d, new Guid("054a729d-f4ff-47fc-bcec-7506116093a5"), roleType, relationEndType);
        m.AddInheritance(d, new Guid("baa9f110-3ef3-43d9-b0a8-507eb072f204"), binaryAssociationType, unitAssociationType);
        m.AddInheritance(d, new Guid("f8368952-f032-4310-b932-b4294b944e3b"), binaryRoleType, unitRoleType);
        m.AddInheritance(d, new Guid("cd773c76-a9b5-4ed9-8009-849ca947fe53"), booleanAssociationType, unitAssociationType);
        m.AddInheritance(d, new Guid("dc79c64b-1320-455d-b445-02cfd80a25dc"), booleanRoleType, unitRoleType);
        m.AddInheritance(d, new Guid("8dedd0ab-2d24-4716-8c9e-078d2102205b"), dateTimeAssociationType, unitAssociationType);
        m.AddInheritance(d, new Guid("3d08f874-93ad-4ccd-9d07-6b8fcaa56346"), dateTimeRoleType, unitRoleType);
        m.AddInheritance(d, new Guid("f21e0dff-6a4a-4c3e-8668-2b829fa9441f"), decimalAssociationType, unitAssociationType);
        m.AddInheritance(d, new Guid("7249f569-fc71-4383-b617-238f8f429f36"), decimalRoleType, unitRoleType);
        m.AddInheritance(d, new Guid("42e6e55d-3b61-4c14-b3d4-831c7e31b51b"), floatAssociationType, unitAssociationType);
        m.AddInheritance(d, new Guid("fb2e2ac7-327e-4dd6-a735-0c0656c2de10"), floatRoleType, unitRoleType);
        m.AddInheritance(d, new Guid("8ae84e0e-c8dd-4315-824c-5e022796b3ea"), inheritance, metaObject);
        m.AddInheritance(d, new Guid("0fbc3430-972d-4f0e-957c-64b35cc24df1"), integerAssociationType, unitAssociationType);
        m.AddInheritance(d, new Guid("7cca3842-c6a1-433a-9e65-34d2ef116673"), integerRoleType, unitRoleType);
        m.AddInheritance(d, new Guid("7df8b0b9-8bad-49f9-9300-a8c402a22f6d"), stringAssociationType, unitAssociationType);
        m.AddInheritance(d, new Guid("215dbfb0-ce0a-4a19-992f-1f7112c53454"), stringRoleType, unitRoleType);
        m.AddInheritance(d, new Guid("06bbf5f6-4859-4847-a90c-093fc9aee40a"), uniqueAssociationType, unitAssociationType);
        m.AddInheritance(d, new Guid("fec15108-1b04-415a-8f9b-135d2db267f5"), uniqueRoleType, unitRoleType);
        m.AddInheritance(d, new Guid("a68e3111-d057-4fed-956c-8a3612baeace"), toManyRoleType, compositeRoleType);
        m.AddInheritance(d, new Guid("4bc01b4b-1cb3-493c-9e9a-62b8d0df42ef"), toOneRoleType, compositeRoleType);
        m.AddInheritance(d, new Guid("9c5a71e4-be03-49f2-986f-1fff3078e18e"), type, metaObject);
        m.AddInheritance(d, new Guid("3bc240cd-a431-41d2-bae3-fe6ef3dfc1f2"), unit, objectType);
        m.AddInheritance(d, new Guid("75e71eea-aa9c-42ef-bd5b-9f61182bba29"), unitAssociationType, associationType);
        m.AddInheritance(d, new Guid("2f05d2cf-63b2-4f32-83c8-96403d934fef"), unitRoleType, roleType);

        // Relations
        m.AddManyToOneRelation(d, new Guid("cea0ed95-ae21-451d-bef3-2cd6ccb34c29"), CoreIds.AssociationTypeComposite, associationType, composite);

        m.AddManyToManyRelation(d, new Guid("960162a2-a902-4cd9-9576-ccb0e7c9e01c"), CoreIds.CompositeDirectSupertypes, composite, @interface, "DirectSupertype");
        m.AddManyToManyRelation(d, new Guid("b0129926-2eb2-4ac7-80e0-93a2329964cf"), CoreIds.CompositeSupertypes, composite, @interface, "Supertype");

        m.AddUnitRelation(d, new Guid("5b2ac86d-af09-46f6-a454-4d2a3bdeaed3"), CoreIds.DecimalRoleTypeAssignedPrecision, decimalRoleType, integer, "AssignedPrecision");
        m.AddUnitRelation(d, new Guid("3c25e09b-6b89-46e9-b1bb-f89c4d2fa4db"), CoreIds.DecimalRoleTypeDerivedPrecision, decimalRoleType, integer, "DerivedPrecision");
        m.AddUnitRelation(d, new Guid("ac79ed22-0a87-4370-9261-03b27ad8bbe6"), CoreIds.DecimalRoleTypeAssignedScale, decimalRoleType, integer, "AssignedScale");
        m.AddUnitRelation(d, new Guid("ebb42e88-b2c0-48ba-8865-67e86ef6d84e"), CoreIds.DecimalRoleTypeDerivedScale, decimalRoleType, integer, "DerivedScale");

        m.AddUnitRelation(d, new Guid("1dd008c4-755e-4f84-b52f-360afaf1a6cd"), CoreIds.DomainName, domain, @string, "Name");
        m.AddManyToManyRelation(d, new Guid("ae8311c8-75ea-426e-ad50-3f400accb264"), CoreIds.DomainSuperdomains, domain, domain, "Superdomain");
        m.AddManyToManyRelation(d, new Guid("93dcadad-f9e0-402a-bd4f-d150df5f5c26"), CoreIds.DomainTypes, domain, type);

        m.AddManyToOneRelation(d, new Guid("e2cd96af-a436-439c-b02b-ecf23f19a96f"), CoreIds.InheritanceSubtype, inheritance, composite, "Subtype");
        m.AddManyToOneRelation(d, new Guid("c9f8f651-5b6e-408d-b088-0ca925e8b2d5"), CoreIds.InheritanceSupertype, inheritance, @interface, "Supertype");

        m.AddUnitRelation(d, new Guid("c45fcfac-5614-4a03-b86a-df2f75cc5fcb"), CoreIds.ObjectTypeAssignedPluralName, objectType, @string, "AssignedPluralName");
        m.AddUnitRelation(d, new Guid("edb514d1-f0b6-44d0-a0ce-2ff9a132b4f7"), CoreIds.ObjectTypeDerivedPluralName, objectType, @string, "DerivedPluralName");
        m.AddUnitRelation(d, new Guid("4ae8c269-d329-4d01-b48f-3df905c0a9e0"), CoreIds.ObjectTypeSingularName, objectType, @string, "SingularName");

        m.AddUnitRelation(d, new Guid("3f24ceec-222a-4933-881c-3f85b6afebac"), CoreIds.MetaObjectId, metaObject, unique, "Id");

        m.AddUnitRelation(d, new Guid("1d3957c3-d1a7-45f4-b6a6-5554223d7326"), CoreIds.RelationEndTypeIsMany, relationEndType, boolean, "IsMany");

        m.AddOneToOneRelation(d, new Guid("ff05c66a-7a87-4b1e-8b77-c097285ec7ca"), CoreIds.RoleTypeAssociationType, roleType, associationType);
        m.AddUnitRelation(d, new Guid("39a5bdaf-3f44-436a-b9e8-a64a5bb770dd"), CoreIds.RoleTypeAssignedPluralName, roleType, @string, "AssignedPluralName");
        m.AddUnitRelation(d, new Guid("cd6a33fa-4537-4a83-a6b6-2d79725fb46e"), CoreIds.RoleTypeDerivedPluralName, roleType, @string, "DerivedPluralName");
        m.AddManyToOneRelation(d, new Guid("ff21b9ad-1a0b-40f5-a35f-5bfb9ed1ab02"), CoreIds.RoleTypeObjectType, roleType, objectType);
        m.AddUnitRelation(d, new Guid("15e2d931-89d1-480a-8bb2-42e7902c7ba7"), CoreIds.RoleTypeName, roleType, @string, "Name");
        m.AddUnitRelation(d, new Guid("13ba399e-bf05-48fc-8489-b70fb19fba15"), CoreIds.RoleTypeSingularName, roleType, @string, "SingularName");

        m.AddUnitRelation(d, new Guid("da78d554-bb1b-451c-ade1-4f6efe651d7c"), CoreIds.StringRoleTypeAssignedSize, stringRoleType, integer, "AssignedSize");
        m.AddUnitRelation(d, new Guid("46d33068-969c-4a88-a703-fb5f0d8e0cd8"), CoreIds.StringRoleTypeDerivedSize, stringRoleType, integer, "DerivedSize");
    }

    /// <summary>
    /// The meta meta.
    /// </summary>
    public MetaMeta MetaMeta { get; }

    /// <summary>
    /// Creates a new Meta
    /// </summary>
    public Meta CreateMetaPopulation()
    {
        var meta = new Meta(this.MetaMeta);

        meta.DerivationById[nameof(CoreIds.CompositeDirectSupertypes)] = new CompositeDirectSupertypes(meta);
        meta.DerivationById[nameof(CoreIds.CompositeSupertypes)] = new CompositeSupertypes(meta);
        meta.DerivationById[nameof(CoreIds.DecimalRoleTypeDerivedPrecision)] = new DecimalRoleTypeDerivedPrecision(meta);
        meta.DerivationById[nameof(CoreIds.DecimalRoleTypeDerivedScale)] = new DecimalRoleTypeDerivedScale(meta);
        meta.DerivationById[nameof(CoreIds.RoleTypeName)] = new RoleTypeName(meta);
        meta.DerivationById[nameof(CoreIds.StringRoleTypeDerivedSize)] = new StringRoleTypeDerivedSize(meta);

        return meta;
    }
}
