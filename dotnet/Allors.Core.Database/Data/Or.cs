// <copyright file="Or.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

/// <summary>
/// An or predicate.
/// </summary>
public record Or : IPredicate
{
    /// <summary>
    /// The operands.
    /// </summary>
    public required IPredicate[] Operands { get; init; }

    /// <inheritdoc />
    public void Accept(IVisitor visitor) => visitor.VisitOr(this);
}
