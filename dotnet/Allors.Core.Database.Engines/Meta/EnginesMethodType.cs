namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Meta.Domain;

/// <summary>
/// An engine method type.
/// </summary>
public sealed class EnginesMethodType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesOperandType(enginesMeta, metaObject)
{
}
