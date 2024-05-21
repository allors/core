// <copyright file="In.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

using Allors.Core.Database.Meta.Handles;

/// <summary>
/// An in predicate.
/// </summary>
public record In : IRelationEndPredicate
{
    /// <summary>
    /// The relation end type.
    /// </summary>
    public required IRelationEndType RelationEndType { get; init; }

    /// <summary>
    /// The extent.
    /// </summary>
    public IExtent? Extent { get; init; }

    /// <summary>
    /// The parameter.
    /// </summary>
    public string? Parameter { get; init; }

    /// <inheritdoc/>
    public void Accept(IVisitor visitor) => visitor.VisitIn(this);
}
