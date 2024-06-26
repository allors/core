﻿namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta;

/// <summary>
/// An engine association type.
/// </summary>
public abstract class EnginesCompositeAssociationType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesAssociationType(enginesMeta, metaObject);
