namespace Allors.Core.Meta.Tests.Domain.Static
{
    using Allors.Core.Meta.Domain;
    using Allors.Core.Meta.Meta;

    public class C1(MetaPopulation population, MetaObjectType objectType)
        : MetaObject(population, objectType), I1;
}
