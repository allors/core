namespace Allors.Core.MetaMeta;

using System;

internal static class StringExtensions
{
    internal static string Pluralize(this string @this)
    {
        static bool EndsWith(string word, string ending) => word.EndsWith(ending, StringComparison.InvariantCultureIgnoreCase);

        if (EndsWith(@this, "y") &&
            !EndsWith(@this, "ay") &&
            !EndsWith(@this, "ey") &&
            !EndsWith(@this, "iy") &&
            !EndsWith(@this, "oy") &&
            !EndsWith(@this, "uy"))
        {
            return string.Concat(@this.AsSpan(0, @this.Length - 1), "ies");
        }

        if (EndsWith(@this, "us"))
        {
            return @this + "es";
        }

        if (EndsWith(@this, "ss"))
        {
            return @this + "es";
        }

        if (EndsWith(@this, "x") ||
            EndsWith(@this, "ch") ||
            EndsWith(@this, "sh"))
        {
            return @this + "es";
        }

        if (EndsWith(@this, "f") && @this.Length > 1)
        {
            return string.Concat(@this.AsSpan(0, @this.Length - 1), "ves");
        }

        if (EndsWith(@this, "fe") && @this.Length > 2)
        {
            return string.Concat(@this.AsSpan(0, @this.Length - 2), "ves");
        }

        return @this + "s";
    }
}
