// <copyright file="IExtent.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

using Allors.Core.Database.Meta.Handles;

/// <summary>
/// An extent.
/// </summary>
public interface IExtent : IVisitable
{
    /// <summary>
    /// The object type.
    /// </summary>
    CompositeHandle ObjectType { get; }

    /// <summary>
    /// The sorting.
    /// </summary>
    Sort[]? Sorting { get; }
}
