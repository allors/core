namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta.Domain;

/// <summary>
/// An engine operand type.
/// </summary>
public abstract class EnginesOperandType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesType(enginesMeta, metaObject);
