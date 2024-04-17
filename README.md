## Allors CORE

[![Build Status](https://dev.azure.com/allors/Core/_apis/build/status%2Fallors.core?branchName=main)](https://dev.azure.com/allors/Core/_build/latest?definitionId=16&branchName=main)

## Meta
```mermaid
classDiagram
    class AssociationType
    RelationEndType <|-- AssociationType
    AssociationType o-- Composite : Composite
    class Class
    Composite <|-- Class
    class Composite
    ObjectType <|-- Composite
    Composite o-- Interface : DirectSupertypes
    class Domain
    MetaObject <|-- Domain
    Domain o-- Type : Types
    class Interface
    Composite <|-- Interface
    class MetaObject
    MetaObject : Guid Id
    class MethodType
    OperandType <|-- MethodType
    class ObjectType
    Type <|-- ObjectType
    ObjectType : String AssignedPluralName
    ObjectType : String DerivedPluralName
    ObjectType : String SingularName
    class OperandType
    Type <|-- OperandType
    class RelationEndType
    OperandType <|-- RelationEndType
    class RoleType
    RelationEndType <|-- RoleType
    RoleType o-- AssociationType : AssociationType
    RoleType o-- ObjectType : ObjectType
    RoleType : String RoleTypeAssignedPluralName
    RoleType : String RoleTypeDerivedPluralName
    RoleType : String RoleTypeSingularName
    class Type
    MetaObject <|-- Type
    class Unit
    ObjectType <|-- Unit
    class Workspace
    MetaObject <|-- Workspace
    Workspace o-- Type : Types

```
