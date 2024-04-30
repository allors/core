// <copyright file="IPredicateExtent.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

/// <summary>
/// An extent with a predicate.
/// </summary>
public interface IPredicateExtent : IExtent
{
    /// <summary>
    /// The predicate.
    /// </summary>
    IPredicate? Predicate { get; }
}
