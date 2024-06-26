﻿// <copyright file="IOperatorExtent.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

/// <summary>
/// An operator extent.
/// </summary>
public interface IOperatorExtent : IExtent
{
    /// <summary>
    /// The operands.
    /// </summary>
    IExtent[] Operands { get; init; }
}
