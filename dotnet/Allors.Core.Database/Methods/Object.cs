namespace Allors.Core.Database.Methods;

using System;

/// <summary>
/// Object methods.
/// </summary>
public static class Object
{
    /// <summary>
    /// The OnPostBuild method gets called after the object has been created
    /// and the builders (Build arguments and OnBuild methods) were executed.
    /// </summary>
    public static void OnPostBuild(this IObject @this, IMethodContext context)
    {
        Console.WriteLine($"{@this.GetType().Name}.{nameof(OnPostBuild)}");
    }
}
