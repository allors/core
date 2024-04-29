// <copyright file="Like.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

using Allors.Core.Database.Meta.Handles;

/// <summary>
/// A like predicate.
/// </summary>
public class Like : IRolePredicate
{
    /// <summary>
    /// The role type.
    /// </summary>
    public required RoleTypeHandle RoleType { get; init; }

    /// <summary>
    /// The value.
    /// </summary>
    public string? Value { get; init; }

    /// <summary>
    /// The parameter.
    /// </summary>
    public string? Parameter { get; init; }

    /// <inheritdoc />
    public void Accept(IVisitor visitor) => visitor.VisitLike(this);
}
