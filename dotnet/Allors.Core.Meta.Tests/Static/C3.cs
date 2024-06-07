namespace Allors.Core.Meta.Tests.Static;

using Allors.Core.Meta;
using Allors.Core.Meta.Meta;

public class C3(MetaPopulation population, MetaObjectType objectType)
    : MetaObject(population, objectType), I1;
