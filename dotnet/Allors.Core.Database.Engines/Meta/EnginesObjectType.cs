namespace Allors.Core.Database.Engines.Meta;

using Allors.Core.Database.Meta;
using Allors.Core.Meta;

/// <summary>
/// An engine object type.
/// </summary>
public abstract class EnginesObjectType(EnginesMeta enginesMeta, MetaObject metaObject) : EnginesType(enginesMeta, metaObject)
{
    private string? singularName;

    /// <summary>
    /// The name.
    /// </summary>
    public string SingularName => this.singularName ??= (string)this.MetaObject[this.M.ObjectTypeSingularName]!;
}
