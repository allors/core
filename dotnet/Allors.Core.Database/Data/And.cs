// <copyright file="And.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

/// <summary>
/// An and predicate.
/// </summary>
public record And : ICompositePredicate
{
    /// <summary>
    /// The operands.
    /// </summary>
    public required IPredicate[] Operands { get; init; }

    /// <inheritdoc/>
    public void Accept(IVisitor visitor) => visitor.VisitAnd(this);
}
