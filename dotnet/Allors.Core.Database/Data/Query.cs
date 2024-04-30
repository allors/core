// <copyright file="Pull.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>
namespace Allors.Core.Database.Data;

using Allors.Core.Database.Meta.Handles;

/// <summary>
/// A query.
/// </summary>
public record Query : IVisitable
{
    /// <summary>
    /// The object type.
    /// </summary>
    public ObjectTypeHandle? ObjectType { get; init; }

    /// <summary>
    /// The extent.
    /// </summary>
    public IExtent? Extent { get; init; }

    /// <summary>
    /// The object.
    /// </summary>
    public long? Object { get; init; }

    /// <summary>
    /// The results.
    /// </summary>
    public Result[]? Results { get; init; }

    /// <inheritdoc />
    public void Accept(IVisitor visitor) => visitor.VisitPull(this);
}
