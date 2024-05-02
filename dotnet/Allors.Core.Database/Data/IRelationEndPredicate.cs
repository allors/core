// <copyright file="IPropertyPredicate.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

using Allors.Core.Database.Meta.Handles;

/// <summary>
/// A property predicate.
/// </summary>
public interface IRelationEndPredicate : IOperandPredicate
{
    /// <summary>
    /// The relation end type.
    /// </summary>
    RelationEndTypeHandle RelationEndType { get; }
}
