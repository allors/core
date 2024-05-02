// <copyright file="And.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// An and predicate.
/// </summary>
public record Range : IExtent
{
    private int? hashCode;

    /// <summary>
    /// The objects.
    /// </summary>
    public required IReadOnlySet<long> Objects { get; init; }

    /// <inheritdoc/>
    public void Accept(IVisitor visitor) => visitor.VisitRange(this);

    /// <inheritdoc />
    public virtual bool Equals(Range? other)
    {
        return other != null && this.Objects.SetEquals(other.Objects);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return this.hashCode ??= this.Objects
            .OrderBy(v => v)
            .Aggregate(0, (acc, v) => HashCode.Combine(acc, v.GetHashCode()));
    }
}
