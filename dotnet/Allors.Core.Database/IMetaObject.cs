namespace Allors.Core.Database
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// A meta object.
    /// </summary>
    public interface IMetaObject
    {
        /// <summary>
        /// The embedded object.
        /// </summary>
        EmbeddedObject EmbeddedObject { get; }
    }
}
