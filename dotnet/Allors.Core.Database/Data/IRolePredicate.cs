﻿// <copyright file="IRolePredicate.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

using Allors.Core.Database.Meta.Handles;

/// <summary>
/// A role predicate.
/// </summary>
public interface IRolePredicate : IPredicate
{
    /// <summary>
    /// The role type.
    /// </summary>
    RoleTypeHandle RoleType { get; }
}
