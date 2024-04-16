namespace Allors.Core.Database
{
    using Allors.Embedded.Domain;

    /// <summary>
    /// Meta Core.
    /// </summary>
    public sealed class Meta
    {
        /// <summary>
        /// Creates a new Core Population.
        /// </summary>
        public Meta()
        {
            this.MetaMeta = new MetaMeta();
            this.EmbeddedPopulation = new EmbeddedPopulation();

            this.Object = this.EmbeddedPopulation.Create(this.MetaMeta.Interface);
        }

        /// <summary>
        /// The Meta Population.
        /// </summary>
        public MetaMeta MetaMeta { get; }

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
