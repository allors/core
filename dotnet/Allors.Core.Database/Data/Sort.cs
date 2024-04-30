﻿// <copyright file="Sort.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

using Allors.Core.Database.Meta.Handles;

/// <summary>
/// A sort.
/// </summary>
public record Sort : IVisitable
{
    /// <summary>
    /// The role type.
    /// </summary>
    public required RoleTypeHandle RoleType { get; init; }

    /// <summary>
    /// The descending.
    /// </summary>
    public bool Descending { get; init; } = false;

    /// <inheritdoc/>
    public void Accept(IVisitor visitor) => visitor.VisitSort(this);
}