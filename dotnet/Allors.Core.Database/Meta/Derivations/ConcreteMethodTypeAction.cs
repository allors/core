namespace Allors.Core.Database.Meta.Derivations;

using System;
using System.Collections.Generic;
using System.Linq;
using Allors.Core.Database.MetaMeta;
using Allors.Core.Meta;

/// <summary>
/// Derive the action of a concrete method.
/// </summary>
public sealed class ConcreteMethodTypeAction(Meta meta) : IMetaDerivation
{
    /// <inheritdoc/>
    public void Derive(MetaChangeSet changeSet)
    {
        var m = meta.MetaMeta;

        var changedConcreteMethodTypeMethodParts = changeSet.ChangedRoles(m.ConcreteMethodTypeMethodParts());

        if (!changedConcreteMethodTypeMethodParts.Any())
        {
            return;
        }

        var sortedDomains = meta.Objects.Where(v => m.Domain().IsAssignableFrom(v.ObjectType)).ToList();
        sortedDomains.Sort((a, b) => a[m.DomainSuperdomains]!.Contains(b) ? -1 : 1);

        var sortedCompositesByConcrete = meta.Objects.Where(v => m.Class().IsAssignableFrom(v.ObjectType))
            .ToDictionary(v => v, v =>
            {
                var compositesWhereConcrete = v[m.CompositeConcretes().AssociationType]!.ToList();

                compositesWhereConcrete.Sort(
                    (a, b) =>
                    {
                        if (a[m.CompositeSupertypes].Contains(b))
                        {
                            return 1;
                        }

                        if (a[m.CompositeSupertypes().AssociationType].Contains(b))
                        {
                            return -1;
                        }

                        return string.CompareOrdinal((string)a[m.ObjectTypeSingularName]!, (string)b[m.ObjectTypeSingularName]!);
                    });

                return compositesWhereConcrete;
            });

        foreach (var concreteMethodType in meta.Objects.Where(v => m.ConcreteMethodType().IsAssignableFrom(v.ObjectType)))
        {
            var @class = concreteMethodType[m.ConcreteMethodTypeClass]!;
            var sortedComposites = sortedCompositesByConcrete[@class];

            var methodType = concreteMethodType[m.MethodTypeConcreteMethodTypes().AssociationType]!;
            var methodPartByDomainByComposite = methodType[m.MethodTypeMethodParts].GroupBy(v => v[m.MethodPartComposite]).ToDictionary(v => v.Key!, v => v.GroupBy(w => w[m.MethodPartDomain]).ToDictionary(v => v.Key!, v => v.First()));

            var actions = new List<Action>();

            foreach (var composite in sortedComposites)
            {
                foreach (var domain in sortedDomains)
                {
                    if (methodPartByDomainByComposite.TryGetValue(composite, out var methodPartByDomain))
                    {
                        if (methodPartByDomain.TryGetValue(domain, out var methodPart))
                        {
                            actions.Add((Action)methodPart[m.MethodPartAction]!);
                        }
                    }
                }
            }

            concreteMethodType[m.ConcreteMethodTypeActions] = actions;
        }
    }
}
