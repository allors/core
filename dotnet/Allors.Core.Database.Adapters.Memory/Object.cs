namespace Allors.Core.Database.Adapters.Memory;

using System;
using System.Collections.Generic;
using Allors.Core.Database.Meta;

/// <inheritdoc />
public class Object : IObject
{
    private readonly State? state;
    private Dictionary<Guid, object?>? roleByRoleTypeId;

    /// <summary>
    /// Initializes a new instance of the <see cref="Object"/> class.
    /// </summary>
    public Object(Transaction transaction, Class @class, long id)
    {
        this.Transaction = transaction;
        this.Class = @class;
        this.Id = id;

        transaction.OriginalState.TryGetValue(this.Id, out this.state);
    }

    /// <inheritdoc />
    public Class Class { get; }

    /// <inheritdoc/>
    public long Id { get; }

    ITransaction IObject.Transaction => this.Transaction;

    internal Transaction Transaction { get; }

    /// <inheritdoc />
    public object? this[UnitRoleType unitRoleType]
    {
        get
        {
            if (this.roleByRoleTypeId != null && this.roleByRoleTypeId.TryGetValue(unitRoleType.Id, out var changedRole))
            {
                return changedRole;
            }

            if (this.state != null && this.state.RoleByRoleTypeId.TryGetValue(unitRoleType.Id, out var role))
            {
                return role;
            }

            return null;
        }

        set
        {
            var currentRole = this[unitRoleType];
            if (Equals(currentRole, value))
            {
                return;
            }

            this.roleByRoleTypeId ??= [];
            this.roleByRoleTypeId[unitRoleType.Id] = value;
        }
    }
}
