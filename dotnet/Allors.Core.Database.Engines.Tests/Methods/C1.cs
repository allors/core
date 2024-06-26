﻿namespace Allors.Core.Database.Engines.Tests.Methods;

using Allors.Core.Database.Engines.Tests.Meta;

/// <summary>
/// C1.
/// </summary>
public static class C1
{
    /// <summary>
    /// Do it.
    /// </summary>
    public static void DoIt(this IObject @this, IMethodContext ctx)
    {
        var m = @this.Transaction.Database.Meta;

        bool? shouldDoIt = (bool?)ctx["ShouldDoIt"];

        @this[m.C1DidIt] = shouldDoIt ?? true;

        ctx["Success"] = true;
    }
}
