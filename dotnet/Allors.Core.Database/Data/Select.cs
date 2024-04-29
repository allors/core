// <copyright file="Select.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

using Allors.Core.Database.Meta.Handles;

/// <summary>
/// A select.
/// </summary>
public record Select : IVisitable
{
    /// <summary>
    /// The relation end type.
    /// </summary>
    public required RelationEndTypeHandle RelationEndType { get; init; }

    /// <summary>
    /// Of type.
    /// </summary>
    public CompositeHandle? OfType { get; init; }

    /// <summary>
    /// The next select.
    /// </summary>
    public Select? Next { get; init; }

    /// <summary>
    /// The end select.
    /// </summary>
    public Select End => this.Next != null ? this.Next.End : this;

    /// <summary>
    /// The nodes to include.
    /// </summary>
    public Node[]? Include { get; init; }

    /// <inheritdoc/>
    public void Accept(IVisitor visitor) => visitor.VisitSelect(this);
}
