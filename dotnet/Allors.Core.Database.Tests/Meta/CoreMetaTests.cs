﻿namespace Allors.Core.Database.Tests.Meta
{
    using System.Linq;
    using Allors.Core.Database.Meta;
    using Allors.Core.Database.Meta.Handles;
    using Allors.Core.Meta.Meta.Diagrams;
    using Xunit;

    public class CoreMetaTests
    {
        [Fact]
        public void Diagram()
        {
            var coreMeta = new CoreMeta();
            var diagram = new ClassDiagram(coreMeta.Meta.MetaMeta).Render();

            Assert.NotNull(diagram);
        }

        [Fact]
        public void Build()
        {
            var coreMeta = new CoreMeta();

            var units = coreMeta.MetaPopulation.Objects.OfType<Unit>().ToArray();
            var interfaces = coreMeta.MetaPopulation.Objects.OfType<Interface>().ToArray();
            var classes = coreMeta.MetaPopulation.Objects.OfType<Class>().ToArray();

            Assert.Equal(2, coreMeta.MetaPopulation.Objects.Count());
            Assert.Single(units);
            Assert.Single(interfaces);
            Assert.Empty(classes);
        }
    }
}
