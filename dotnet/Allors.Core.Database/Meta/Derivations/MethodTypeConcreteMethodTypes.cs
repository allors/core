namespace Allors.Core.Database.Meta.Derivations;

using System.Linq;
using Allors.Core.Database.MetaMeta;
using Allors.Core.Meta;

/// <summary>
/// Derive the supertypes of the composite.
/// </summary>
public sealed class MethodTypeConcreteMethodTypes(Meta meta) : IMetaDerivation
{
    /// <inheritdoc/>
    public void Derive(MetaChangeSet changeSet)
    {
        var m = meta.MetaMeta;

        var changedCompositeConcretes = changeSet.ChangedRoles(m.CompositeConcretes());
        var changedCompositeMethodType = changeSet.ChangedRoles(m.CompositeMethodTypes());

        if (!(changedCompositeConcretes.Any() || changedCompositeMethodType.Any()))
        {
            return;
        }

        foreach (var composite in meta.Objects.Where(v => m.Composite().IsAssignableFrom(v.ObjectType)))
        {
            var concretes = composite[m.CompositeConcretes].ToHashSet();

            foreach (MethodType methodType in composite[m.CompositeMethodTypes])
            {
                var existingConcreteMethods = methodType[m.MethodTypeConcreteMethodTypes];
                var concretesToAdd = concretes.Except(existingConcreteMethods.Select(v => v[m.ConcreteMethodTypeClass]!));

                foreach (Class concrete in concretesToAdd)
                {
                    var concreteMethodType = methodType.AddConcreteMethodType(concrete);
                }
            }
        }
    }
}
