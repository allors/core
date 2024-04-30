namespace Allors.Core.Database.Adapters.Memory;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Allors.Core.Database.Data;
using Allors.Core.Database.Meta.Handles;

/// <summary>
/// An extent set based on a role extent
/// </summary>
public class RoleExtentSet : IExtentSet
{
    private IExtent? extent;

    /// <summary>
    /// Initializes a new instance of the <see cref="RoleExtentSet"/> class.
    /// </summary>
    public RoleExtentSet(Object @object, ToManyRoleTypeHandle roleTypeHandle)
    {
        this.Object = @object;
        this.RoleTypeHandle = roleTypeHandle;
        this.extent = null;
    }

    ITransaction IExtentSet.Transaction => this.Transaction;

    /// <inheritdoc/>
    public IExtent Extent => this.extent ??= new RoleExtent
    {
        Object = this.Object.Id,
        RoleType = this.RoleTypeHandle,
    };

    private Object Object { get; }

    private ToManyRoleTypeHandle RoleTypeHandle { get; }

    private Transaction Transaction => this.Object.Transaction;

    /// <inheritdoc />
    public IObject[] ToArray()
    {
        var role = this.Object.ToManyRole(this.RoleTypeHandle);
        return role != null ? [.. role.Select(this.Transaction.Instantiate)] : [];
    }

    /// <inheritdoc/>
    public IEnumerator<IObject> GetEnumerator()
    {
        var materialized = this.ToArray();
        var enumerable = (IEnumerable<IObject>)materialized;
        return enumerable.GetEnumerator();
    }

    IEnumerator IExtentEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
