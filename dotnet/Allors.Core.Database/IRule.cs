namespace Allors.Core.Database;

using System.Collections.Generic;

/// <summary>
/// A rule for a derivation.
/// </summary>
public interface IRule
{
    /// <summary>
    /// The paths.
    /// </summary>
    IEnumerable<string> Paths { get; }

    /// <summary>
    /// Derive the matched objects.
    /// </summary>
    void Derive(IContext context, IEnumerable<IObject> matches);
}
