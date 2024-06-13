﻿namespace Allors.Core.Database.Meta;

using System;
using Allors.Core.Database.Meta.Derivations;
using Allors.Core.Meta;

/// <summary>
/// Core MetaMeta.
/// </summary>
public static class CoreMeta
{
    /// <summary>
    /// Allors Core.
    /// </summary>
    public static readonly Guid AllorsCore = new("6f14a4e3-af2d-4f82-bd60-b06c7228cf4a");

    /// <summary>
    /// Binary.
    /// </summary>
    public static readonly Guid Binary = new("ebfaa622-3ba0-4957-bf08-0cb10d0a8041");

     /// <summary>
    /// Boolean.
    /// </summary>
    public static readonly Guid Boolean = new("e1d97d02-3ded-40a2-a5fa-ff9ece678ad6");

    /// <summary>
    /// DateTime.
    /// </summary>
    public static readonly Guid DateTime = new("c2549539-1a4f-4223-aedb-425b8ee1e8f2");

    /// <summary>
    /// Decimal.
    /// </summary>
    public static readonly Guid Decimal = new("59c9d0c0-d516-47a3-b3c4-cc3ff0d996b6");

    /// <summary>
    /// Float.
    /// </summary>
    public static readonly Guid Float = new("d840e9ce-ed4e-44ee-8e07-76edc9a1e2b1");

    /// <summary>
    /// Integer.
    /// </summary>
    public static readonly Guid Integer = new("f5e554ad-be4b-447f-aff7-526e58c7c651");

    /// <summary>
    /// String.
    /// </summary>
    public static readonly Guid String = new("162b6dc7-d43d-45e6-a1ee-6c31f6d3cc62");

    /// <summary>
    /// Unique.
    /// </summary>
    public static readonly Guid Unique = new("ffb37dfb-9e4e-4d25-b06c-83b261d87e0c");

    /// <summary>
    /// Object.
    /// </summary>
    public static readonly Guid Object = new("89612710-ce42-4507-b1cf-cf48d63739a2");

    /// <summary>
    /// Populates meta with Core types.
    /// </summary>
    public static void Populate(Meta meta)
    {
        // Derivations
        meta.DerivationById[nameof(CompositeDirectSupertypes)] = new CompositeDirectSupertypes(meta);
        meta.DerivationById[nameof(CompositeSupertypes)] = new CompositeSupertypes(meta);
        meta.DerivationById[nameof(DecimalRoleTypeDerivedPrecision)] = new DecimalRoleTypeDerivedPrecision(meta);
        meta.DerivationById[nameof(DecimalRoleTypeDerivedScale)] = new DecimalRoleTypeDerivedScale(meta);
        meta.DerivationById[nameof(RoleTypeName)] = new RoleTypeName(meta);
        meta.DerivationById[nameof(StringRoleTypeDerivedSize)] = new StringRoleTypeDerivedSize(meta);

        // Domain
        var d = meta.AddDomain(AllorsCore, nameof(AllorsCore));

        meta.AddUnit(d, Binary, nameof(Binary));
        meta.AddUnit(d, Boolean, nameof(Boolean));
        meta.AddUnit(d, DateTime, nameof(DateTime));
        meta.AddUnit(d, Decimal, nameof(Decimal));
        meta.AddUnit(d, Float, nameof(Float));
        meta.AddUnit(d, Integer, nameof(Integer));
        meta.AddUnit(d, String, nameof(String));
        meta.AddUnit(d, Unique, nameof(Unique));

        meta.AddInterface(d, Object, nameof(Object));
    }
}
