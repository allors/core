namespace Allors.Core.Database.Commands
{
    using System.CommandLine;

    internal static class Program
    {
        private static async Task<int> Main(string[] args)
        {
            var rootCommand = new RootCommand("cli for Allors Core Database");

            GenerateUmlCommand.Configure(rootCommand);

            return await rootCommand.InvokeAsync(args);
        }
    }
}
