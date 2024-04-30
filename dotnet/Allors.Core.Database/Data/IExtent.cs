﻿// <copyright file="IExtent.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

/// <summary>
/// An extent.
/// </summary>
public interface IExtent : IVisitable
{
    /// <summary>
    /// The sorting.
    /// </summary>
    Sort[]? Sorting { get; }
}