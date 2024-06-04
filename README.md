## Allors CORE

[![Build Status](https://dev.azure.com/allors/Core/_apis/build/status%2Fallors.core?branchName=main)](https://dev.azure.com/allors/Core/_build/latest?definitionId=16&branchName=main)

## Meta
```mermaid
classDiagram
    class AssociationType
    RelationEndType <|-- AssociationType
    AssociationType o-- Composite : Composite
    class BinaryAssociationType
    UnitAssociationType <|-- BinaryAssociationType
    class BinaryRoleType
    UnitRoleType <|-- BinaryRoleType
    class BooleanAssociationType
    UnitAssociationType <|-- BooleanAssociationType
    class BooleanRoleType
    UnitRoleType <|-- BooleanRoleType
    class Class
    Composite <|-- Class
    class Composite
    ObjectType <|-- Composite
    Composite o-- Interface : DirectSupertypes
    Composite o-- Interface : Supertypes
    class CompositeAssociationType
    RoleType <|-- CompositeAssociationType
    class CompositeRoleType
    AssociationType <|-- CompositeRoleType
    class DateTimeAssociationType
    UnitAssociationType <|-- DateTimeAssociationType
    class DateTimeRoleType
    UnitRoleType <|-- DateTimeRoleType
    class DecimalAssociationType
    UnitAssociationType <|-- DecimalAssociationType
    class DecimalRoleType
    UnitRoleType <|-- DecimalRoleType
    DecimalRoleType : Integer AssignedPrecision
    DecimalRoleType : Integer AssignedScale
    DecimalRoleType : Integer DerivedPrecision
    DecimalRoleType : Integer DerivedScale
    class Domain
    MetaObject <|-- Domain
    Domain : String Name
    Domain o-- Type : Types
    class FloatAssociationType
    UnitAssociationType <|-- FloatAssociationType
    class FloatRoleType
    UnitRoleType <|-- FloatRoleType
    class Inheritance
    MetaObject <|-- Inheritance
    Inheritance o-- Composite : Subtype
    Inheritance o-- Interface : Supertype
    class IntegerAssociationType
    UnitAssociationType <|-- IntegerAssociationType
    class IntegerRoleType
    UnitRoleType <|-- IntegerRoleType
    class Interface
    Composite <|-- Interface
    class ManyToAssociationType
    CompositeRoleType <|-- ManyToAssociationType
    class ManyToManyAssociationType
    ManyToAssociationType <|-- ManyToManyAssociationType
    class ManyToManyRoleType
    ToManyRoleType  <|-- ManyToManyRoleType
    class ManyToOneAssociationType
    ManyToAssociationType <|-- ManyToOneAssociationType
    class ManyToOneRoleType
    ToOneRoleType <|-- ManyToOneRoleType
    class MetaObject
    MetaObject : Unique Id
    class ObjectType
    Type <|-- ObjectType
    ObjectType : String AssignedPluralName
    ObjectType : String DerivedPluralName
    ObjectType : String SingularName
    class OneToAssociationType
    CompositeRoleType <|-- OneToAssociationType
    class OneToManyAssociationType
    OneToAssociationType <|-- OneToManyAssociationType
    class OneToManyRoleType
    ToManyRoleType  <|-- OneToManyRoleType
    class OneToOneAssociationType
    OneToAssociationType <|-- OneToOneAssociationType
    class OneToOneRoleType
    ToOneRoleType <|-- OneToOneRoleType
    class OperandType
    Type <|-- OperandType
    class RelationEndType
    OperandType <|-- RelationEndType
    RelationEndType : Boolean IsMany
    class RoleType
    RelationEndType <|-- RoleType
    RoleType : String AssignedPluralName
    RoleType o-- AssociationType : AssociationType
    RoleType : String DerivedPluralName
    RoleType : String Name
    RoleType o-- ObjectType : ObjectType
    RoleType : String SingularName
    class StringAssociationType
    UnitAssociationType <|-- StringAssociationType
    class StringRoleType
    UnitRoleType <|-- StringRoleType
    StringRoleType : Integer AssignedSize
    StringRoleType : Integer DerivedSize
    class ToManyRoleType 
    CompositeAssociationType <|-- ToManyRoleType 
    class ToOneRoleType
    CompositeAssociationType <|-- ToOneRoleType
    class Type
    MetaObject <|-- Type
    class UniqueAssociationType
    UnitAssociationType <|-- UniqueAssociationType
    class UniqueRoleType
    UnitRoleType <|-- UniqueRoleType
    class Unit
    ObjectType <|-- Unit
    class UnitAssociationType
    AssociationType <|-- UnitAssociationType
    class UnitRoleType
    RoleType <|-- UnitRoleType

```
