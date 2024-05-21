// <copyright file="Filter.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

using Allors.Core.Database.Meta.Handles;

/// <summary>
/// An extent based of a composite.
/// </summary>
public record CompositeExtent : IPredicateExtent
{
    /// <summary>
    /// The composite.
    /// </summary>
    public required IComposite Composite { get; init; }

    /// <summary>
    /// The predicate.
    /// </summary>
    public IPredicate? Predicate { get; init; }

    /// <inheritdoc />
    public void Accept(IVisitor visitor) => visitor.VisitExtent(this);
}
