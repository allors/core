namespace Allors.Core.MetaMeta.Diagrams;

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Allors.Core.MetaMeta;

public sealed partial class ClassDiagram
{
    public string? Title { get; init; }

    public string? OneMultiplicity { get; init; }

    public string? ManyMultiplicity { get; init; }

    public string Render(IEnumerable<MetaObjectType> composites, IEnumerable<IMetaRoleType>? roleTypes = null)
    {
        var compositeSet = new HashSet<MetaObjectType>(composites);
        var roleTypeSet = roleTypes != null ? new HashSet<IMetaRoleType>(roleTypes) : null;

        var diagram = new StringBuilder();

        if (this.Title != null)
        {
            string title = $"""
             ---
             title: {this.Title}
             ---

             """;
            diagram.Append(title);
        }

        diagram.Append("""
                   classDiagram

                   """);

        foreach (var composite in compositeSet
            .OrderBy(v => v.Name))
        {
            diagram.AppendLine(CultureInfo.InvariantCulture, $"    class {composite.Name}");
            if (composite.Kind == MetaObjectTypeKind.Interface)
            {
                diagram.AppendLine(CultureInfo.InvariantCulture, $"    <<interface>> {composite.Name}");
            }

            foreach (var directSuperType in composite.DirectSupertypes
                .Where(compositeSet.Contains)
                .OrderBy(v => v.Name))
            {
                diagram.AppendLine(CultureInfo.InvariantCulture, $"    {directSuperType.Name} <|-- {composite.Name}");
            }

            foreach (var roleType in composite.DeclaredRoleTypeByName.Values
                .Where(v => roleTypeSet != null ? roleTypeSet.Contains(v) : true)
                .OrderBy(v => v.Name))
            {
                if (roleType is MetaUnitRoleType || !compositeSet.Contains(roleType.ObjectType))
                {
                    diagram.AppendLine(CultureInfo.InvariantCulture, $"    {composite.Name} : {roleType.ObjectType.Name} {roleType.Name}");
                }
                else if (roleType is IMetaCompositeRoleType compositeRoleType && roleType.AssociationType is IMetaCompositeAssociationType compositeAssociationType)
                {
                    var oneMultiplicity = this.OneMultiplicity;
                    var manyMultiplicity = this.ManyMultiplicity;

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

                    var assignedRoleTypeName = roleType.SingularName != roleType.ObjectType.Name ? $" : {roleType.Name}" : string.Empty;
                    diagram.AppendLine(CultureInfo.InvariantCulture, $"    {composite.Name} {associationTypeMultiplicity}o--{roleTypeMultiplicity} {roleType.ObjectType.Name}{assignedRoleTypeName}");
                }
            }
        }

        return diagram.ToString();
    }
}
