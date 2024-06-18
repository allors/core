﻿namespace Allors.Core.Database.Commands
{
    using System.CommandLine;
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
                    var metaMeta = new MetaMeta();
                    CoreMetaMeta.Populate(metaMeta);

                    var meta = new Core.Meta.Meta(metaMeta);
                    CoreMeta.Populate(meta);

                    meta.Derive();

                    var directoryInfo = output!;

                    void WriteMetaClassDiagram(string name, IEnumerable<MetaObjectType>? metaComposites = null, IEnumerable<IMetaRoleType>? metaRoleTypes = null)
                    {
                        metaComposites ??= metaMeta.MetaComposites;
                        var diagram = new ClassDiagram().Render(metaComposites);
                        var filePath = Path.Combine(directoryInfo.FullName, $"meta-{name}.class.mermaid");
                        File.WriteAllText(filePath, diagram);
                    }

                    WriteMetaClassDiagram("overview");

                    WriteMetaClassDiagram("method", metaMeta.MetaComposites.Where(v => v.Name.Contains("Method")));

                    return Task.FromResult(0);
                },
                outputOption);
        }
    }
}