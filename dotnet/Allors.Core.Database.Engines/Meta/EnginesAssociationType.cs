namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Database.Meta.Domain;
using Allors.Core.Database.MetaMeta;
using Allors.Core.Meta;

/// <summary>
/// An engine association type.
/// </summary>
public abstract class EnginesAssociationType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesRelationEndType(enginesMeta, metaObject)
{
    private EnginesComposite? composite;

    /// <summary>
    /// The composite.
    /// </summary>
    public EnginesComposite Composite => this.composite ??= this.EnginesMeta[(IComposite)this.MetaObject[this.M.AssociationTypeComposite]!];
}
