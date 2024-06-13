// <copyright file="Query.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>
namespace Allors.Core.Database.Data;

using Allors.Core.Database.Meta;

/// <summary>
/// A query.
/// </summary>
public record Query : IFilter
{
    /// <summary>
    /// The extent.
    /// </summary>
    public IExtent? Extent { get; init; }

    /// <summary>
    /// The select.
    /// </summary>
    public Select? Select { get; init; }

    /// <summary>
    /// The includes.
    /// </summary>
    public Include[]? Includes { get; init; }

    /// <inheritdoc />
    public IComposite? OfType { get; init; }

    /// <inheritdoc />
    public Sort[]? Sorting { get; init; }

    /// <inheritdoc />
    public int? Skip { get; init; }

    /// <inheritdoc />
    public int? Take { get; init; }

    /// <inheritdoc />
    public void Accept(IVisitor visitor) => visitor.VisitQuery(this);
}
