namespace Allors.Core.Database.Meta.Derivations
{
    using System.Collections.Generic;
    using System.Linq;
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// Derives the Composite Supertypes.
    /// </summary>
    public sealed class CompositeSupertypes(MetaPopulation population, CoreMetaMeta m) : IMetaDerivation
    {
        /// <inheritdoc/>
        public void Derive(MetaChangeSet changeSet)
        {
            var changedCompositeDirectSupertypes = changeSet.ChangedRoles(m.CompositeDirectSupertypes);

            if (!changedCompositeDirectSupertypes.Any())
            {
                return;
            }

            // TODO: Optimize
            foreach (var composite in population.Objects.Where(v => m.Composite.IsAssignableFrom(v.ObjectType)))
            {
                var supertypes = new HashSet<IMetaObject>();
                this.AccumulateSupertypes(composite, supertypes);
                composite[m.CompositeSupertypes] = supertypes;
            }
        }

        private void AccumulateSupertypes(IMetaObject composite, HashSet<IMetaObject> acc)
        {
            foreach (var directSupertype in composite[m.CompositeDirectSupertypes])
            {
                acc.Add(directSupertype);
                this.AccumulateSupertypes(directSupertype, acc);
            }
        }
    }
}
