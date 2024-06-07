namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta;

/// <summary>
/// An engine role type handle with a multiplicity one.
/// </summary>
public abstract class EnginesToOneRoleType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesCompositeRoleType(enginesMeta, metaObject);
