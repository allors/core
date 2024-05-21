﻿// <copyright file="AssociationExtent.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

using Allors.Core.Database.Meta.Handles;

/// <summary>
/// An extent based on an association type.
/// </summary>
public record AssociationExtent : IPredicateExtent
{
    /// <summary>
    /// The object.
    /// </summary>
    public required long Object { get; init; }

    /// <summary>
    /// The role type.
    /// </summary>
    public required IAssociationType AssociationType { get; init; }

    /// <summary>
    /// The predicate.
    /// </summary>
    public IPredicate? Predicate { get; init; }

    /// <inheritdoc />
    public void Accept(IVisitor visitor) => visitor.VisitExtent(this);
}
