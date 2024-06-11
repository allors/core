namespace Allors.Core.Database.Meta;

using System;
using System.Linq;
using Allors.Core.Database.Meta.Derivations;
using Allors.Core.Database.Meta.Domain;
using Allors.Core.Meta;

/// <summary>
/// Core MetaMeta.
/// </summary>
public static class CoreMeta
{
    /// <summary>
    /// Populates meta with Core types.
    /// </summary>
    public static void Populate(Meta meta)
    {
        // Meta Derivations
        meta.DerivationById[nameof(CoreIds.CompositeDirectSupertypes)] = new CompositeDirectSupertypes(meta);
        meta.DerivationById[nameof(CoreIds.CompositeSupertypes)] = new CompositeSupertypes(meta);
        meta.DerivationById[nameof(CoreIds.DecimalRoleTypeDerivedPrecision)] = new DecimalRoleTypeDerivedPrecision(meta);
        meta.DerivationById[nameof(CoreIds.DecimalRoleTypeDerivedScale)] = new DecimalRoleTypeDerivedScale(meta);
        meta.DerivationById[nameof(CoreIds.RoleTypeName)] = new RoleTypeName(meta);
        meta.DerivationById[nameof(CoreIds.StringRoleTypeDerivedSize)] = new StringRoleTypeDerivedSize(meta);

        // Meta Domain
        var metaMeta = meta.MetaMeta;
        var byId = meta.Objects.ToDictionary(v => (Guid)v[metaMeta.MetaObjectId()]!, v => v);

        var d = (Domain.Domain)byId[CoreIds.AllorsCore];
        var @class = (Class)byId[CoreIds.Class];

        meta.AddUnit(d, CoreIds.Decimal, "Decimal");
        meta.AddUnit(d, CoreIds.Float, "Float");

        // Domain
        var @object = meta.AddInterface(d, CoreIds.Object, "Object");
        meta.AddManyToOneRelation(d, new Guid("9d2e16ae-8ce0-4b5b-9f39-284320de2452"), CoreIds.ObjectClass, @object, @class);
    }
}
