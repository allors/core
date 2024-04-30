namespace Allors.Core.Database.Adapters.Memory;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Allors.Core.Database.Data;
using Allors.Core.Database.Meta.Handles;

/// <summary>
/// An extent set based on an association extent
/// </summary>
public class AssociationExtentSet : IExtentSet
{
    private IExtent? extent;

    /// <summary>
    /// Initializes a new instance of the <see cref="AssociationExtentSet"/> class.
    /// </summary>
    public AssociationExtentSet(Object @object, ManyToAssociationTypeHandle associationTypeHandle)
    {
        this.Object = @object;
        this.AssociationTypeHandle = associationTypeHandle;
        this.extent = null;
    }

    ITransaction IExtentSet.Transaction => this.Transaction;

    /// <inheritdoc/>
    public IExtent Extent => this.extent ??= new AssociationExtent
    {
        Object = this.Object.Id,
        AssociationType = this.AssociationTypeHandle,
    };

    private Object Object { get; }

    private ManyToAssociationTypeHandle AssociationTypeHandle { get; }

    private Transaction Transaction => this.Object.Transaction;

    /// <inheritdoc />
    public IObject[] ToArray()
    {
        var association = this.Object.ManyToAssociation(this.AssociationTypeHandle);
        return association != null ? [.. association.Select(this.Transaction.Instantiate)] : [];
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
