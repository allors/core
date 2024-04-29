namespace Allors.Core.Database;

using Allors.Core.Database.Data;

/// <summary>
/// An extent set.
/// </summary>
public interface IExtentSet
{
    /// <summary>
    /// The transaction
    /// </summary>
    ITransaction Transaction { get; }

    /// <summary>
    /// The extent.
    /// </summary>
    IExtent Extent { get; }
}
