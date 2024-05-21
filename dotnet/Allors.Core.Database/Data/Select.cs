// <copyright file="Path.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

using Allors.Core.Database.Meta.Handles;

/// <summary>
/// A path.
/// </summary>
public record Select : IFilter
{
    /// <summary>
    /// The relation end type.
    /// </summary>
    public required IRelationEndType RelationEndType { get; init; }

    /// <summary>
    /// The next select
    /// </summary>
    public Select? Next { get; init; }

    /// <inheritdoc />
    public IComposite? OfType { get; init; }

    /// <inheritdoc />
    public Sort[]? Sorting { get; }

    /// <inheritdoc />
    public int? Skip { get; init; }

    /// <inheritdoc />
    public int? Take { get; init; }

    /// <inheritdoc/>
    public void Accept(IVisitor visitor) => visitor.VisitSelect(this);
}
