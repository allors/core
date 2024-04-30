namespace Allors.Core.Database;

using System.Collections;
using System.Collections.Generic;

/// <summary>
/// An extent enumerable.
/// </summary>
public interface IExtentEnumerable
{
    /// <summary>
    /// Returns an enumerator that iterates through the extent set.
    /// </summary>
    IEnumerator GetEnumerator();
}

/// <summary>
/// An extent enumerable.
/// </summary>
public interface IExtentEnumerable<out T> : IExtentEnumerable
{
    /// <summary>
    /// Returns an enumerator that iterates through the extent set.
    /// </summary>
    new IEnumerator<T> GetEnumerator();
}
