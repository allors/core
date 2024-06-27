namespace Allors.Core.Database.Engines.Tests;

using Allors.Core.Database.Engines.Path;
using FluentAssertions;
using Superpower;
using Xunit;

public abstract class PathTests : Tests
{
    [Fact]
    public void Tokenize()
    {
        const string input = "a/b/c";

        var tokenizerResult = Paths.Tokenizer.TryTokenize(input);

        tokenizerResult.HasValue.Should().BeTrue();
    }

    [Fact]
    public void Parse()
    {
        const string input = "a/b/c";
        var tokenizerResult = Paths.Tokenizer.TryTokenize(input);

        var parseResult = Paths.Parser.TryParse(tokenizerResult.Value);

        parseResult.HasValue.Should().BeTrue();
    }

    protected abstract IDatabase CreateDatabase();
}
