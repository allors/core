namespace Allors.Core.Database.Meta;

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
    public static readonly Guid Ide3a07ac80a864f7a901c3f630f1050a9 = new("e3a07ac8-0a86-4f7a-901c-3f630f1050a9");

    /// <summary>
    /// Binary.
    /// </summary>
    public static readonly Guid Idcc0e2b47d7d34a58b390e41f53b248a2 = new("cc0e2b47-d7d3-4a58-b390-e41f53b248a2");

    /// <summary>
    /// Boolean.
    /// </summary>
    public static readonly Guid Id6d2123913bc94707a4f0c4612088f9da = new("6d212391-3bc9-4707-a4f0-c4612088f9da");

    /// <summary>
    /// DateTime.
    /// </summary>
    public static readonly Guid Id7809b062412e42c58b5abaa1919ec156 = new("7809b062-412e-42c5-8b5a-baa1919ec156");

    /// <summary>
    /// Decimal.
    /// </summary>
    public static readonly Guid Id083579BF2E7248CDB49137D776C70F44 = new("083579BF-2E72-48CD-B491-37D776C70F44");

    /// <summary>
    /// Float.
    /// </summary>
    public static readonly Guid Idb2f5d674e8ce455c9799e2794d1ec78e = new("b2f5d674-e8ce-455c-9799-e2794d1ec78e");

    /// <summary>
    /// Integer.
    /// </summary>
    public static readonly Guid Idb6f39681cc744cdb9edd1f96aa07bdab = new("b6f39681-cc74-4cdb-9edd-1f96aa07bdab");

    /// <summary>
    /// String.
    /// </summary>
    public static readonly Guid Id9a6dc34f47804a35b7d647497a0dc3d1 = new("9a6dc34f-4780-4a35-b7d6-47497a0dc3d1");

    /// <summary>
    /// Unique.
    /// </summary>
    public static readonly Guid Ida45dde54f51f450b8cfa9bbb8090b936 = new("a45dde54-f51f-450b-8cfa-9bbb8090b936");

    /// <summary>
    /// Object.
    /// </summary>
    public static readonly Guid Idb9b23efefefb4867bce53fd78dd3617d = new("b9b23efe-fefb-4867-bce5-3fd78dd3617d");

    internal static Guid AllorsCore => Ide3a07ac80a864f7a901c3f630f1050a9;

    internal static Guid Binary => Idcc0e2b47d7d34a58b390e41f53b248a2;

    internal static Guid Boolean => Id6d2123913bc94707a4f0c4612088f9da;

    internal static Guid DateTime => Id7809b062412e42c58b5abaa1919ec156;

    internal static Guid Decimal => Id083579BF2E7248CDB49137D776C70F44;

    internal static Guid Float => Idb2f5d674e8ce455c9799e2794d1ec78e;

    internal static Guid Integer => Idb6f39681cc744cdb9edd1f96aa07bdab;

    internal static Guid String => Id9a6dc34f47804a35b7d647497a0dc3d1;

    internal static Guid Unique => Ida45dde54f51f450b8cfa9bbb8090b936;

    internal static Guid Object => Idb9b23efefefb4867bce53fd78dd3617d;

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
