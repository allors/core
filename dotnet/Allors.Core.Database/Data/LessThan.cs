// <copyright file="LessThan.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

using Allors.Core.Database.Meta;

/// <summary>
/// A less than predicate.
/// </summary>
public record LessThan : IRolePredicate
{
    /// <summary>
    /// The role type.
    /// </summary>
    public required IRoleType RoleType { get; init; }

    /// <summary>
    /// The value.
    /// </summary>
    public object? Value { get; init; }

    /// <summary>
    /// The path.
    /// </summary>
    public IRoleType? Path { get; init; }

    /// <summary>
    /// The parameter.
    /// </summary>
    public string? Parameter { get; init; }

    /// <inheritdoc />
    public void Accept(IVisitor visitor) => visitor.VisitLessThan(this);
}
