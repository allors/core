// <copyright file="TreeNode.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Data;

/// <summary>
/// A visitor.
/// </summary>
public interface IVisitor
{
    /// <summary>
    /// Visit and.
    /// </summary>
    void VisitAnd(And visited);

    /// <summary>
    /// Visit between.
    /// </summary>
    void VisitBetween(Between visited);

    /// <summary>
    /// Visit in.
    /// </summary>
    void VisitIn(In visited);

    /// <summary>
    /// Visit intersects.
    /// </summary>
    void VisitIntersects(Intersects visited);

    /// <summary>
    /// Visit has.
    /// </summary>
    void VisitHas(Has visited);

    /// <summary>
    /// Visit equals.
    /// </summary>
    void VisitEquals(Equal visited);

    /// <summary>
    /// Visit except.
    /// </summary>
    void VisitExcept(Except visited);

    /// <summary>
    /// Visit exists.
    /// </summary>
    void VisitExists(Exists visited);

    /// <summary>
    /// Visit the composite extent.
    /// </summary>
    void VisitExtent(CompositeExtent visited);

    /// <summary>
    /// Visit the association extent.
    /// </summary>
    void VisitExtent(AssociationExtent visited);

    /// <summary>
    /// Visit the role extent.
    /// </summary>
    void VisitExtent(RoleExtent visited);

    /// <summary>
    /// Visit select.
    /// </summary>
    void VisitSelect(Select visited);

    /// <summary>
    /// Visit greater than.
    /// </summary>
    void VisitGreaterThan(GreaterThan visited);

    /// <summary>
    /// Visit instance of.
    /// </summary>
    void VisitInstanceOf(Instanceof visited);

    /// <summary>
    /// Visit intersect.
    /// </summary>
    void VisitIntersect(Intersect visited);

    /// <summary>
    /// Visit less than.
    /// </summary>
    void VisitLessThan(LessThan visited);

    /// <summary>
    /// Visit like.
    /// </summary>
    void VisitLike(Like visited);

    /// <summary>
    /// Visit node.
    /// </summary>
    void VisitNode(Node visited);

    /// <summary>
    /// Visit not.
    /// </summary>
    void VisitNot(Not visited);

    /// <summary>
    /// Visit or.
    /// </summary>
    void VisitOr(Or visited);

    /// <summary>
    /// Visit pull.
    /// </summary>
    void VisitPull(Query visited);

    /// <summary>
    /// Visit result.
    /// </summary>
    void VisitResult(Result visited);

    /// <summary>
    /// Visit sort.
    /// </summary>
    void VisitSort(Sort visited);

    /// <summary>
    /// Visit union.
    /// </summary>
    void VisitUnion(Union visited);
}
