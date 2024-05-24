namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Database.Meta.Domain;

    /// <summary>
    /// An engine workspace.
    /// </summary>
    public sealed class EngineWorkspace(EngineMeta engineMeta, Workspace workspace) : EngineMetaObject(engineMeta, workspace)
    {
    }
}
