namespace Allors.Core.Database.Meta.Derivations;

using System.Collections.Generic;
using System.Linq;
using Allors.Core.Meta;

/// <summary>
/// Derive the supertypes of the composite.
/// </summary>
public sealed class CompositeSupertypes(Meta meta) : IMetaDerivation
{
    /// <inheritdoc/>
    public void Derive(MetaChangeSet changeSet)
    {
        var m = meta.MetaMeta;

        var changedCompositeDirectSupertypes = changeSet.ChangedRoles(m.CompositeDirectSupertypes());

        if (!changedCompositeDirectSupertypes.Any())
        {
            return;
        }

        // TODO: Optimize
        foreach (var composite in meta.Objects.Where(v => m.Composite().IsAssignableFrom(v.ObjectType)))
        {
            var supertypes = new HashSet<IMetaObject>();
            AccumulateSupertypes(meta, composite, supertypes);
            composite[m.CompositeSupertypes()] = supertypes;
        }
    }

    private static void AccumulateSupertypes(Meta meta, IMetaObject composite, HashSet<IMetaObject> acc)
    {
        var m = meta.MetaMeta;

        foreach (var directSupertype in composite[m.CompositeDirectSupertypes()])
        {
            acc.Add(directSupertype);
            AccumulateSupertypes(meta, directSupertype, acc);
        }
    }
}
