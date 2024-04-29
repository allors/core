// <copyright file="Result.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

/// <summary>
/// A result.
/// </summary>
public record Result : IVisitable
{
    /// <summary>
    /// The select.
    /// </summary>
    public Select? Select { get; init; }

    /// <summary>
    /// The nodes to include.
    /// </summary>
    public Node[]? Include { get; init; }

    /// <summary>
    /// The name.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// The objects to skip.
    /// </summary>
    public int? Skip { get; init; }

    /// <summary>
    /// The objects to take.
    /// </summary>
    public int? Take { get; init; }

    /// <inheritdoc />
    public void Accept(IVisitor visitor) => visitor.VisitResult(this);
}
