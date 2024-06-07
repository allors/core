namespace Allors.Core.Database.Meta.Derivations;

using System.Linq;
using Allors.Core.Meta;

/// <summary>
/// Derive the supertypes of the composite.
/// </summary>
public sealed class CompositeDirectSupertypes(MetaPopulation population, CoreMetaMeta m) : IMetaDerivation
{
    /// <inheritdoc/>
    public void Derive(MetaChangeSet changeSet)
    {
        var changedInheritanceSubtype = changeSet.ChangedRoles(m.InheritanceSubtype);
        var changedInheritanceSupertype = changeSet.ChangedRoles(m.InheritanceSupertype);

        if (!(changedInheritanceSubtype.Any() || changedInheritanceSupertype.Any()))
        {
            return;
        }

        foreach (var composite in population.Objects.Where(v => m.Composite.IsAssignableFrom(v.ObjectType)))
        {
            composite[m.CompositeSupertypes] = [];
        }

        foreach (var inheritance in population.Objects.Where(v => m.Inheritance.IsAssignableFrom(v.ObjectType)))
        {
            var subtype = inheritance[m.InheritanceSubtype];
            var supertype = inheritance[m.InheritanceSupertype];

            if (subtype == null || supertype == null)
            {
                // TOOD: log error
                continue;
            }

            subtype.Add(m.CompositeDirectSupertypes, supertype);
        }
    }
}
