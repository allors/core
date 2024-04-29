// <copyright file="TreeNode.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

using Allors.Core.Database.Meta.Handles;

/// <summary>
/// A node.
/// </summary>
public record Node : IVisitable
{
    /// <summary>
    /// The relation end type.
    /// </summary>
    public required RelationEndTypeHandle RelationEndType { get; init; }

    /// <summary>
    /// The nodes.
    /// </summary>
    public Node[]? Nodes { get; private set; }

    /// <summary>
    /// Of type.
    /// </summary>
    public CompositeHandle? OfType { get; init; }

    /// <inheritdoc/>
    public void Accept(IVisitor visitor) => visitor.VisitNode(this);
}
