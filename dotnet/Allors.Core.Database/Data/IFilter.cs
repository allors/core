// <copyright file="ISelection.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

using Allors.Core.Database.Meta.Handles;

/// <summary>
/// A selection.
/// </summary>
public interface IFilter : IVisitable
{
    /// <summary>
    /// Of type.
    /// </summary>
    public IComposite? OfType { get; init; }

    /// <summary>
    /// The sorting.
    /// </summary>
    Sort[]? Sorting { get; }

    /// <summary>
    /// The objects to skip.
    /// </summary>
    public int? Skip { get; init; }

    /// <summary>
    /// The objects to take.
    /// </summary>
    public int? Take { get; init; }
}
