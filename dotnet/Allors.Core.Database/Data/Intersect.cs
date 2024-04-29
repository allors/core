﻿// <copyright file="Intersect.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

using Allors.Core.Database.Meta.Handles;

/// <summary>
/// An intersect operator.
/// </summary>
public class Intersect : IExtentOperator
{
    /// <summary>
    /// The operands.
    /// </summary>
    public required IExtent[] Operands { get; init; }

    /// <summary>
    /// The sorting.
    /// </summary>
    public Sort[]? Sorting { get; init; }

    /// <summary>
    /// The object type.
    /// </summary>
    public CompositeHandle ObjectType => this.Operands[0].ObjectType;

    /// <inheritdoc />
    public void Accept(IVisitor visitor) => visitor.VisitIntersect(this);
}
