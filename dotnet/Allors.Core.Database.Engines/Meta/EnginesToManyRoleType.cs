namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta;

/// <summary>
/// An engine role type handle with a multiplicity many.
/// </summary>
public abstract class EnginesToManyRoleType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesCompositeRoleType(enginesMeta, metaObject);
