namespace Allors.Core.Database.Adapters.Memory
{
    using System.Collections.Immutable;

    /// <inheritdoc />
    public class Database : IDatabase
    {
        private long nextObjectId;

        /// <summary>
        /// Initializes a new instance of the <see cref="Database"/> class.
        /// </summary>
        public Database()
        {
            this.State = ImmutableDictionary<long, State>.Empty;
            this.nextObjectId = 0;
        }

        internal ImmutableDictionary<long, State> State { get; set; }

        /// <inheritdoc />
        public ITransaction CreateTransaction()
        {
            return new Transaction(this);
        }

        internal long NextObjectId()
        {
            return this.nextObjectId++;
        }
    }
}
