// <copyright file="Except.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

/// <summary>
/// An except operator.
/// </summary>
public record Except : IExtentOperator
{
    /// <summary>
    /// The operands.
    /// </summary>
    public required IExtent[] Operands { get; init; }

    /// <summary>
    /// The sorting.
    /// </summary>
    public Sort[]? Sorting { get; init; }

    /// <inheritdoc />
    public void Accept(IVisitor visitor) => visitor.VisitExcept(this);
}
