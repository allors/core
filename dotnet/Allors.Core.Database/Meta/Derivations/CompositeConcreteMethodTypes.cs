namespace Allors.Core.Database.Meta.Derivations;

using System.Collections.Generic;
using System.Linq;
using Allors.Core.Database.MetaMeta;
using Allors.Core.Meta;

/// <summary>
/// Derive the supertypes of the composite.
/// </summary>
public sealed class CompositeConcreteMethodTypes(Meta meta) : IMetaDerivation
{
    /// <inheritdoc/>
    public void Derive(MetaChangeSet changeSet)
    {
        var m = meta.MetaMeta;

        var changedCompositeMethodTypes = changeSet.ChangedRoles(m.CompositeMethodTypes());
        var changedCompositeConcretes = changeSet.ChangedRoles(m.CompositeConcretes());

        if (!(changedCompositeMethodTypes.Any() || changedCompositeConcretes.Any()))
        {
            return;
        }

        var objects = meta.Objects;
        var composites = objects.Where(v => m.Composite().IsAssignableFrom(v.ObjectType));

        foreach (var composite in composites)
        {
            var classes = composite[m.CompositeConcretes];

            foreach (var @class in classes)
            {
                Dictionary<IMetaObject, IMetaObject> concreteMethodTypeByClass = composite[m.CompositeConcreteMethodTypes].ToDictionary(v => v[m.ConcreteMethodTypeClass]!, v => v);

                if (!concreteMethodTypeByClass.ContainsKey(@class))
                {
                }
            }
        }
    }
}
