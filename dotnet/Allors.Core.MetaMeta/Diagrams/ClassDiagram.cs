﻿namespace Allors.Core.MetaMeta.Diagrams;

using System.Linq;
using System.Text;
using Allors.Core.MetaMeta;

public sealed class ClassDiagram(MetaMeta metaMeta, ClassDiagram.Config? config = null)
{
    public string Render()
    {
        var diagram = new StringBuilder();

        if (config?.Title != null)
        {
            diagram.Append($"""
             ---
             title: {config.Title}
             ---

             """);
        }

        diagram.Append("""
                   classDiagram

                   """);

        var composites = metaMeta.ObjectTypeById.Values
            .Where(v => v.Kind != MetaObjectTypeKind.Unit)
            .OrderBy(v => v.Name);

        foreach (var composite in composites)
        {
            diagram.AppendLine($"    class {composite.Name}");

            var directSuperTypes = composite.DirectSupertypes;
            foreach (var directSuperType in directSuperTypes)
            {
                diagram.AppendLine($"    {directSuperType.Name} <|-- {composite.Name}");
            }

            var declaredRoleTypes = composite.DeclaredRoleTypeByName.Values.OrderBy(v => v.Name);
            foreach (var roleType in declaredRoleTypes)
            {
                if (roleType is MetaUnitRoleType)
                {
                    diagram.AppendLine($"    {composite.Name} : {roleType.ObjectType.Name} {roleType.Name}");
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

                    diagram.AppendLine($"    {composite.Name} {associationTypeMultiplicity}o--{roleTypeMultiplicity} {roleType.ObjectType.Name} : {roleType.Name}");
                }
            }
        }

        return diagram.ToString();
    }

    public sealed record Config
    {
        public string? Title { get; init; }

        public string? OneMultiplicity { get; init; }

        public string? ManyMultiplicity { get; init; }
    }
}