namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine method type.
    /// </summary>
    public sealed class EngineMethodType(EngineMeta engineMeta, MetaObject metaObject) : EngineOperandType(engineMeta, metaObject)
    {
    }
}
