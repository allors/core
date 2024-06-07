namespace Allors.Core.Meta.Tests.Static;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

public class C2(MetaPopulation population, MetaObjectType objectType)
    : MetaObject(population, objectType), I2;
