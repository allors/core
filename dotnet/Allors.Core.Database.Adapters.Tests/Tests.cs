namespace Allors.Core.Database.Adapters.Tests
{
    using Allors.Core.Database.Meta;

    public abstract class Tests
    {
        protected Tests()
        {
            var coreMeta = new CoreMeta();
            this.Meta = new AdaptersMeta(coreMeta);
        }

        public AdaptersMeta Meta { get; }
    }
}
