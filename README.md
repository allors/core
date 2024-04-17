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
    Domain o-- MetaIdentifiableObject : Members
    class Interface
    Composite <|-- Interface
    class MetaIdentifiableObject
    MetaIdentifiableObject : Guid Id
    class MethodType
    OperandType <|-- MethodType
    class ObjectType
    MetaIdentifiableObject <|-- ObjectType
    ObjectType : String AssignedPluralName
    ObjectType : String DerivedPluralName
    ObjectType : String SingularName
    class OperandType
    MetaIdentifiableObject <|-- OperandType
    class RelationEndType
    OperandType <|-- RelationEndType
    class RoleType
    RelationEndType <|-- RoleType
    RoleType o-- AssociationType : AssociationType
    RoleType o-- ObjectType : ObjectType
    RoleType : String RoleTypeAssignedPluralName
    RoleType : String RoleTypeDerivedPluralName
    RoleType : String RoleTypeSingularName
    class Unit
    ObjectType <|-- Unit
    class Workspace
    Workspace o-- MetaIdentifiableObject : Members
```
