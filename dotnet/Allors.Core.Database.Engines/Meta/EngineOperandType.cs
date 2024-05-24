namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine operand type.
    /// </summary>
    public abstract class EngineOperandType(EngineMeta engineMeta, MetaObject metaObject) : EngineType(engineMeta, metaObject)
    {
    }
}
