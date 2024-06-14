namespace Allors.Core.Database.Methods;

using System;

/// <summary>
/// Object methods.
/// </summary>
public static class Object
{
    /// <summary>
    /// The OnBuild method gets called after the object has been created
    /// and the builders were executed.
    /// </summary>
    public static void OnBuild(this IObject @this, object method)
    {
        Console.WriteLine($"{@this.GetType().Name}.{nameof(OnBuild)}");
    }
}
