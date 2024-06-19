namespace Allors.Core.Database.Commands
{
    using System.CommandLine;
    using System.Linq;
    using Allors.Core.Database.Meta;
    using Allors.Core.Database.MetaMeta;
    using Allors.Core.MetaMeta;
    using Allors.Core.MetaMeta.Diagrams;

    internal static class GenerateUmlCommand
    {
        internal static void Configure(RootCommand rootCommand)
        {
            var generateUmlCommand = new Command("generate-uml", "Generate UML diagrams");
            rootCommand.Add(generateUmlCommand);

            var outputOption = new Option<DirectoryInfo?>(
                name: "--output",
                parseArgument: result =>
                {
                    if (!result.Tokens.Any())
                    {
                        return new DirectoryInfo(Directory.GetCurrentDirectory());
                    }

                    string? path = result.Tokens.Single().Value;
                    var directoryInfo = new DirectoryInfo(path);

                    if (!directoryInfo.Exists)
                    {
                        result.ErrorMessage = "Directory does not exist";
                        return null;
                    }

                    return directoryInfo;
                },
                isDefault: true,
                description: "A directory where to store the generated uml diagrams.");

            outputOption.AddAlias("-o");

            generateUmlCommand.Add(outputOption);

            generateUmlCommand.SetHandler(
                (output) =>
                {
                    var m = new MetaMeta();
                    CoreMetaMeta.Populate(m);

                    var meta = new Core.Meta.Meta(m);
                    CoreMeta.Populate(meta);

                    meta.Derive();

                    var directoryInfo = output!;

                    void WriteMetaClassDiagram(string name, IEnumerable<MetaObjectType>? metaComposites = null, IEnumerable<IMetaRoleType>? metaRoleTypes = null)
                    {
                        metaComposites ??= m.MetaComposites;
                        var diagram = new ClassDiagram().Render(metaComposites, metaRoleTypes);
                        var filePath = Path.Combine(directoryInfo.FullName, $"meta-{name}.class.mermaid");
                        File.WriteAllText(filePath, diagram);
                    }

                    WriteMetaClassDiagram("overview");

                    MetaObjectType[] primary = [m.MethodType(), m.ConcreteMethodType(), m.MethodPart()];
                    MetaObjectType[] secondary = [m.Composite(), m.Class()];
                    var metaComposites = primary.Union(secondary);
                    var metaRoleTypes = primary.SelectMany(v => v.RoleTypeByName.Values).Union([m.CompositeMethodTypes(), m.CompositeConcretes()]);
                    WriteMetaClassDiagram("method", metaComposites, metaRoleTypes);

                    return Task.FromResult(0);
                },
                outputOption);
        }
    }
}
