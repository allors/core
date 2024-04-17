namespace Allors.Core.Database
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// Meta Core.
    /// </summary>
    public sealed class CoreMeta
    {
        /// <summary>
        /// Creates a new Core Population.
        /// </summary>
        public CoreMeta()
        {
            this.CoreMetaMeta = new CoreMetaMeta();
            this.EmbeddedPopulation = new EmbeddedPopulation();

            this.Object = this.EmbeddedPopulation.Create(this.CoreMetaMeta.Interface);
        }

        /// <summary>
        /// The Meta Population.
        /// </summary>
        public CoreMetaMeta CoreMetaMeta { get; }

        /// <summary>
        /// The embedded population.
        /// </summary>
        public EmbeddedPopulation EmbeddedPopulation { get; set; }

        /// <summary>
        /// The Object.
        /// </summary>
        public EmbeddedObject Object { get; set; }
    }
}
