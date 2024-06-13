// <copyright file="TreeNode.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

using Allors.Core.Database.Meta;

/// <summary>
/// A node.
/// </summary>
public record Include : IFilter
{
    /// <summary>
    /// The relation end type.
    /// </summary>
    public required IRelationEndType RelationEndType { get; init; }

    /// <summary>
    /// The includes.
    /// </summary>
    public Include[]? Children { get; init; }

    /// <inheritdoc/>
    public IComposite? OfType { get; init; }

    /// <inheritdoc/>
    public Sort[]? Sorting { get; }

    /// <inheritdoc/>
    public int? Skip { get; init; }

    /// <inheritdoc/>
    public int? Take { get; init; }

    /// <inheritdoc/>
    public void Accept(IVisitor visitor) => visitor.VisitInclude(this);
}
