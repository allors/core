// <copyright file="Not.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

/// <summary>
/// A not predicate.
/// </summary>
public class Not : ICompositePredicate
{
    /// <summary>
    /// The operand.
    /// </summary>
    public required IPredicate Operand { get; init; }

    /// <inheritdoc />
    public void Accept(IVisitor visitor) => visitor.VisitNot(this);
}
