// <copyright file="TreeNode.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

/// <summary>
/// A visitable object.
/// </summary>
public interface IVisitable
{
    /// <summary>
    /// Accept the visitor.
    /// </summary>
    void Accept(IVisitor visitor);
}
