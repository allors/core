namespace Allors.Core.Database.Engines.Meta;

using System;
using Allors.Core.Database.Meta;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// An engine meta object.
/// </summary>
public abstract class EnginesMetaObject(EnginesMeta enginesMeta, MetaObject metaObject)
{
    private Guid? id;

    /// <summary>
    /// The engines meta.
    /// </summary>
    public EnginesMeta EnginesMeta { get; } = enginesMeta;

    /// <summary>
    /// The core meta meta.
    /// </summary>
    public MetaMeta M => this.EnginesMeta.Meta.MetaMeta;

    /// <summary>
    /// The meta object.
    /// </summary>
    public MetaObject MetaObject { get; } = metaObject;

    /// <summary>
    /// The id.
    /// </summary>
    public Guid Id => this.id ??= (Guid)this.MetaObject[this.M.MetaObjectId()]!;
}
