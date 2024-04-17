## Allors CORE

[![Build Status](https://dev.azure.com/allors/Core/_apis/build/status%2Fallors.core?branchName=main)](https://dev.azure.com/allors/Core/_build/latest?definitionId=16&branchName=main)

## Meta
```mermaid
classDiagram
    class AssociationType
    RelationEndType <|-- AssociationType
    class Class
    Composite <|-- Class
    class Composite
    ObjectType <|-- Composite
    Composite o-- AssociationType : AssociationTypes
    Composite o-- Class : Classes
    Composite o-- Composite : Composites
    class Domain
    class Interface
    Composite <|-- Interface
    class MetaIdentifiableObject
    MetaIdentifiableObject : Guid Id
    class MethodType
    MetaIdentifiableObject <|-- MethodType
    OperandType <|-- MethodType
    class ObjectType
    MetaIdentifiableObject <|-- ObjectType
    ObjectType : String AssignedPluralName
    ObjectType : String DerivedPluralName
    ObjectType : String SingularName
    class OperandType
    class RelationEndType
    OperandType <|-- RelationEndType
    class RelationType
    MetaIdentifiableObject <|-- RelationType
    class RoleType
    RelationEndType <|-- RoleType
    class Unit
    ObjectType <|-- Unit
    class Workspace
    Workspace o-- MetaIdentifiableObject : Members
```
