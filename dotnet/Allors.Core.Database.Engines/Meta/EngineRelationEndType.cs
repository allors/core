namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Meta.Domain;

    /// <summary>
    /// An engine relation type.
    /// </summary>
    public abstract class EngineRelationEndType(EngineMeta engineMeta, MetaObject metaObject) : EngineOperandType(engineMeta, metaObject)
    {
    }
}
