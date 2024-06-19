namespace Allors.Core.Database.Meta.Derivations;

using System.Linq;
using Allors.Core.Database.MetaMeta;
using Allors.Core.Meta;

/// <summary>
/// Derive the supertypes of the composite.
/// </summary>
public sealed class CompositeConcretes(Meta meta) : IMetaDerivation
{
    /// <inheritdoc/>
    public void Derive(MetaChangeSet changeSet)
    {
        var m = meta.MetaMeta;

        var changedCompositeSupertypes = changeSet.ChangedRoles(m.CompositeSupertypes());

        if (!changedCompositeSupertypes.Any())
        {
            return;
        }

        // TODO: Optimize
        foreach (var composite in meta.Objects.Where(v => m.Composite().IsAssignableFrom(v.ObjectType)))
        {
            if (composite.ObjectType == m.Class())
            {
                composite[m.CompositeConcretes] = [composite];
            }
            else
            {
                var subtypes = composite[m.CompositeSupertypes().AssociationType];
                var subclasses = subtypes.Where(v => v.ObjectType == m.Class());
                composite[m.CompositeConcretes] = subclasses;
            }
        }
    }
}
