﻿namespace Allors.Core.Meta.Meta.Diagrams;

using System.Linq;

public sealed class ClassDiagram(MetaMeta metaMeta, ClassDiagram.Config? config = null)
{
    public string Render()
    {
        var diagram = config?.Title != null ?
            $"""
             ---
             title: {config.Title}
             ---

             """ : string.Empty;

        diagram += """
                   classDiagram

                   """;

        var composites = metaMeta.ObjectTypeById.Values
            .Where(v => v.Kind != MetaObjectTypeKind.Unit)
            .OrderBy(v => v.Name);

        foreach (var composite in composites)
        {
            diagram += $"    class {composite.Name}\r\n";

            var directSuperTypes = composite.DirectSupertypes;
            foreach (var directSuperType in directSuperTypes)
            {
                diagram += $"    {directSuperType.Name} <|-- {composite.Name}\r\n";
            }

            var declaredRoleTypes = composite.DeclaredRoleTypeByName.Values.OrderBy(v => v.Name);
            foreach (var roleType in declaredRoleTypes)
            {
                if (roleType is MetaUnitRoleType)
                {
                    diagram += $"    {composite.Name} : {roleType.ObjectType.Name} {roleType.Name}\r\n";
                }
                else if (roleType is IMetaCompositeRoleType compositeRoleType && roleType.AssociationType is IMetaCompositeAssociationType compositeAssociationType)
                {
                    var oneMultiplicity = config?.OneMultiplicity;
                    var manyMultiplicity = config?.ManyMultiplicity;

                    var associationTypeMultiplicity = compositeAssociationType.IsOne ? oneMultiplicity : manyMultiplicity;
                    var roleTypeMultiplicity = compositeRoleType.IsOne ? oneMultiplicity : manyMultiplicity;

                    if (!string.IsNullOrWhiteSpace(associationTypeMultiplicity))
                    {
                        associationTypeMultiplicity = $"\"{associationTypeMultiplicity}\" ";
                    }

                    if (!string.IsNullOrWhiteSpace(roleTypeMultiplicity))
                    {
                        roleTypeMultiplicity = $" \"{roleTypeMultiplicity}\"";
                    }

                    diagram += $"    {composite.Name} {associationTypeMultiplicity}o--{roleTypeMultiplicity} {roleType.ObjectType.Name} : {roleType.Name}\r\n";
                }
            }
        }

        return diagram;
    }

    public sealed record Config
    {
        public string? Title { get; init; }

        public string? OneMultiplicity { get; init; }

        public string? ManyMultiplicity { get; init; }
    }
}
