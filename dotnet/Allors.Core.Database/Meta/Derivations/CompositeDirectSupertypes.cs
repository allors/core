namespace Allors.Core.Database.Meta.Derivations;

using System.Linq;
using Allors.Core.Meta;

/// <summary>
/// Derive the supertypes of the composite.
/// </summary>
public sealed class CompositeDirectSupertypes(Meta meta) : IMetaDerivation
{
    /// <inheritdoc/>
    public void Derive(MetaChangeSet changeSet)
    {
        var m = meta.MetaMeta;

        var changedInheritanceSubtype = changeSet.ChangedRoles(m.InheritanceSubtype());
        var changedInheritanceSupertype = changeSet.ChangedRoles(m.InheritanceSupertype());

        if (!(changedInheritanceSubtype.Any() || changedInheritanceSupertype.Any()))
        {
            return;
        }

        foreach (var composite in meta.Objects.Where(v => m.Composite().IsAssignableFrom(v.ObjectType)))
        {
            composite[m.CompositeSupertypes()] = [];
        }

        foreach (var inheritance in meta.Objects.Where(v => m.Inheritance().IsAssignableFrom(v.ObjectType)))
        {
            var subtype = inheritance[m.InheritanceSubtype()];
            var supertype = inheritance[m.InheritanceSupertype()];

            if (subtype == null || supertype == null)
            {
                // TOOD: log error
                continue;
            }

            subtype.Add(m.CompositeDirectSupertypes(), supertype);
        }
    }
}
