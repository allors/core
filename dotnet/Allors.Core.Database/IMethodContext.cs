namespace Allors.Core.Database;

/// <summary>
/// Describes the signature for a method.
/// </summary>
public delegate void Method(IObject obj, IMethodContext context);

/// <summary>
/// The database.
/// </summary>
public interface IMethodContext
{
    /// <summary>
    /// Gets or sets the property.
    /// </summary>
    object? this[string property] { get; set; }
}
