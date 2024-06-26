﻿namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta;

/// <summary>
/// An engine generic meta object.
/// </summary>
public sealed class EnginesGenericMetaObject(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesMetaObject(enginesMeta, metaObject);
