﻿namespace Allors.Core.Database.Adapters.Memory.Tests
{
    public class UnitTests : Adapters.Tests.UnitTests
    {
        protected override IDatabase CreateDatabase() => new Database();
    }
}
