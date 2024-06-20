namespace Allors.Core.Database.Meta.Derivations;

using System.Linq;
using Allors.Core.Database.MetaMeta;
using Allors.Core.Meta;

/// <summary>
/// Derive the method parts of a concrete method.
/// </summary>
public sealed class ConcreteMethodTypeMethodParts(Meta meta) : IMetaDerivation
{
    /// <inheritdoc/>
    public void Derive(MetaChangeSet changeSet)
    {
        var m = meta.MetaMeta;

        var changedMethodTypeConcreteMethodTypes = changeSet.ChangedRoles(m.MethodTypeConcreteMethodTypes());
        var changedMethodTypeMethodParts = changeSet.ChangedRoles(m.MethodTypeMethodParts());
        var changedMethodPartComposite = changeSet.ChangedRoles(m.MethodPartComposite());
        var changedMethodPartDomain = changeSet.ChangedRoles(m.MethodPartDomain());
        var changedMethodPartAction = changeSet.ChangedRoles(m.MethodPartAction());

        if (!(changedMethodTypeConcreteMethodTypes.Any() || changedMethodTypeMethodParts.Any() || changedMethodPartComposite.Any() || changedMethodPartDomain.Any() || changedMethodPartAction.Any()))
        {
            return;
        }

        foreach (var concreteMethodType in meta.Objects.Where(v => m.ConcreteMethodType().IsAssignableFrom(v.ObjectType)))
        {
            var @class = concreteMethodType[m.ConcreteMethodTypeClass]!;
            var compositesWhereConcrete = @class[m.CompositeConcretes().AssociationType]!.ToHashSet();
            var methodType = concreteMethodType[m.MethodTypeConcreteMethodTypes().AssociationType]!;
            var methodParts = methodType[m.MethodTypeMethodParts]!;
            var concreteMethodParts = methodParts.Where(v => compositesWhereConcrete.Contains(v[m.MethodPartComposite]!));
            concreteMethodType[m.ConcreteMethodTypeMethodParts] = concreteMethodParts;
        }
    }
}
