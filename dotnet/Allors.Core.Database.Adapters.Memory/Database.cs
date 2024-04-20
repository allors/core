namespace Allors.Core.Database.Adapters.Memory
{
    /// <inheritdoc />
    public class Database : IDatabase
    {
        /// <inheritdoc />
        public ITransaction CreateTransaction()
        {
            return new Transaction(this);
        }
    }
}
