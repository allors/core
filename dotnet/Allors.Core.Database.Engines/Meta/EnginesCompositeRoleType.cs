﻿namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Database.Meta;
using Allors.Core.Database.MetaMeta;
using Allors.Core.Meta;

/// <summary>
/// An engine composite role type.
/// </summary>
public abstract class EnginesCompositeRoleType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesRoleType(enginesMeta, metaObject)
{
    private EnginesComposite? composite;

    /// <inheritdoc />
    public override EnginesObjectType ObjectType => this.Composite;

    /// <summary>
    /// The composite.
    /// </summary>
    public EnginesComposite Composite => this.composite ??= this.EnginesMeta[(IComposite)this.MetaObject[this.M.RoleTypeObjectType]!];
}
