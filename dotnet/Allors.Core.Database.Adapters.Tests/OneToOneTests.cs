﻿namespace Allors.Core.Database.Adapters.Tests
{
    using Xunit;

    public abstract class OneToOneTests
    {
        [Fact]
        public void ToDo()
        {
            Assert.True(true);
        }

        protected abstract IDatabase CreateDatabase();
    }
}