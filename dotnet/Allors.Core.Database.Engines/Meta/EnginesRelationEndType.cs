namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta;

/// <summary>
/// An engine relation type.
/// </summary>
public abstract class EnginesRelationEndType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesOperandType(enginesMeta, metaObject);
