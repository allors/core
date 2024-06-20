namespace Allors.Core.Database.Engines.Memory;

using System.Collections.Generic;

/// <inheritdoc />
public class MethodContext : IMethodContext
{
    private Dictionary<string, object>? context;

    /// <inheritdoc/>
    public object? this[string property]
    {
        get
        {
            if (this.context?.TryGetValue(property, out var value) == true)
            {
                return value;
            }

            return null;
        }

        set
        {
            if (value == null)
            {
                this.context?.Remove(property);
                return;
            }

            this.context ??= [];
            this.context[property] = value;
        }
    }
}
