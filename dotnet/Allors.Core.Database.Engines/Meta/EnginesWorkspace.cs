namespace Allors.Core.Database.Engines.Meta
{
    using Allors.Core.Database.Meta.Domain;

    /// <summary>
    /// An engine workspace.
    /// </summary>
    public sealed class EnginesWorkspace(EnginesMeta enginesMeta, Workspace workspace) : EnginesMetaObject(enginesMeta, workspace)
    {
    }
}
