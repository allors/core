namespace Allors.Core.Database.Meta.Derivations;

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

        foreach (var concreteMethodType in meta.Objects.Where(v => m.ConcreteMethodType().IsAssignableFrom(v.ObjectType)))
        {
            var @class = concreteMethodType[m.ConcreteMethodTypeClass];
            var methodType = concreteMethodType[m.MethodTypeConcreteMethodTypes().AssociationType];
        }
    }
}
