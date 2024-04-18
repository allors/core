namespace Allors.Core.Database.Config
{
    using Allors.Core.Database.Meta;

    /// <summary>
    /// A meta config.
    /// </summary>
    public interface IMetaConfig
    {
        /// <summary>
        /// Builds the meta population.
        /// </summary>
        /// <returns></returns>
        MetaPopulation Build();
    }
}
