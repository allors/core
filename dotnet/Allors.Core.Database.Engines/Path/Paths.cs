namespace Allors.Core.Database.Engines.Path;

using System.Linq;
using Superpower;
using Superpower.Parsers;
using Superpower.Tokenizers;

public static class Paths
{
    public static readonly Tokenizer<SimpleToken> Tokenizer = new TokenizerBuilder<SimpleToken>()
        .Match(Character.Letter.AtLeastOnce(), SimpleToken.Identifier)
        .Match(Character.EqualTo('/'), SimpleToken.Slash)
        .Build();

    public static readonly TokenListParser<SimpleToken, string[]> Parser =
        from lead in Token.EqualTo(SimpleToken.Identifier)
        from rest in Token.EqualTo(SimpleToken.Slash)
            .IgnoreThen(Token.EqualTo(SimpleToken.Identifier))
            .Many()
        select new[] { lead.ToString() }.Concat(rest.Select(t => t.ToString())).ToArray();
}
