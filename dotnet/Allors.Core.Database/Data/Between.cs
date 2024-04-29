// <copyright file="Between.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

using Allors.Core.Database.Meta.Handles;

/// <summary>
/// A between predicate.
/// </summary>
public record Between : IRolePredicate
{
    /// <summary>
    /// The role type.
    /// </summary>
    public required RoleTypeHandle RoleType { get; init; }

    /// <summary>
    /// The values.
    /// </summary>
    public object[]? Values { get; init; }

    /// <summary>
    /// The paths.
    /// </summary>
    public RoleTypeHandle[]? Paths { get; init; }

    /// <summary>
    /// The parameter.
    /// </summary>
    public string? Parameter { get; init; }

    /// <inheritdoc />
    public void Accept(IVisitor visitor) => visitor.VisitBetween(this);
}
