﻿// <copyright file="Filter.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

using Allors.Core.Database.Meta.Handles;

/// <summary>
/// A filter.
/// </summary>
public class Filter() : IExtent, IPredicateContainer
{
    /// <summary>
    /// The object type.
    /// </summary>
    public required CompositeHandle ObjectType { get; init; }

    /// <summary>
    /// The predicate.
    /// </summary>
    public IPredicate? Predicate { get; init; }

    /// <summary>
    /// The sorting.
    /// </summary>
    public Sort[]? Sorting { get; init; }

    /// <inheritdoc />
    public void Accept(IVisitor visitor) => visitor.VisitExtent(this);
}
