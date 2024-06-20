namespace Allors.Core.Database.Tests.Meta;

using System;
using System.Linq;
using Allors.Core.Database.Meta;
using Allors.Core.Database.MetaMeta;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;
using FluentAssertions;
using Xunit;

public class DerivationssTests
{
    [Fact]
    public void CompositeDirectSupertypes()
    {
        var metaMeta = new MetaMeta();
        var meta = new Meta(metaMeta);

        CoreMetaMeta.Populate(metaMeta);
        CoreMeta.Configure(meta);

        var m = metaMeta;

        var domain = meta.AddDomain(Guid.NewGuid(), "Domain");

        var s123 = domain.AddInterface(Guid.NewGuid(), "S123");
        var i12 = domain.AddInterface(Guid.NewGuid(), "I12");
        var i23 = domain.AddInterface(Guid.NewGuid(), "I23");
        var c1 = domain.AddClass(Guid.NewGuid(), "C1");
        var c2 = domain.AddClass(Guid.NewGuid(), "C2");
        var c3 = domain.AddClass(Guid.NewGuid(), "C3");

        domain.AddInheritance(i12, s123);
        domain.AddInheritance(i23, s123);
        domain.AddInheritance(c1, i12);
        domain.AddInheritance(c2, i12, i23);
        domain.AddInheritance(c3, i23);

        meta.Derive();

        s123[m.CompositeDirectSupertypes].Should().BeEmpty();
        i12[m.CompositeDirectSupertypes].Should().BeEquivalentTo([s123]);
        i23[m.CompositeDirectSupertypes].Should().BeEquivalentTo([s123]);
        c1[m.CompositeDirectSupertypes].Should().BeEquivalentTo([i12]);
        c2[m.CompositeDirectSupertypes].Should().BeEquivalentTo([i12, i23]);
        c3[m.CompositeDirectSupertypes].Should().BeEquivalentTo([i23]);
    }

    [Fact]
    public void CompositeSupertypes()
    {
        var metaMeta = new MetaMeta();
        var meta = new Meta(metaMeta);

        CoreMetaMeta.Populate(metaMeta);
        CoreMeta.Configure(meta);

        var m = metaMeta;

        var domain = meta.AddDomain(Guid.NewGuid(), "Domain");

        var s123 = domain.AddInterface(Guid.NewGuid(), "S123");
        var i12 = domain.AddInterface(Guid.NewGuid(), "I12");
        var i23 = domain.AddInterface(Guid.NewGuid(), "I23");
        var c1 = domain.AddClass(Guid.NewGuid(), "C1");
        var c2 = domain.AddClass(Guid.NewGuid(), "C2");
        var c3 = domain.AddClass(Guid.NewGuid(), "C3");

        domain.AddInheritance(i12, s123);
        domain.AddInheritance(i23, s123);
        domain.AddInheritance(c1, i12);
        domain.AddInheritance(c2, i12, i23);
        domain.AddInheritance(c3, i23);

        meta.Derive();

        s123[m.CompositeSupertypes].Should().BeEmpty();
        i12[m.CompositeSupertypes].Should().BeEquivalentTo([s123]);
        i23[m.CompositeSupertypes].Should().BeEquivalentTo([s123]);
        c1[m.CompositeSupertypes].Should().BeEquivalentTo([i12, s123]);
        c2[m.CompositeSupertypes].Should().BeEquivalentTo([i12, i23, s123]);
        c3[m.CompositeSupertypes].Should().BeEquivalentTo([i23, s123]);
    }

    [Fact]
    public void MethodTypeConcreteMethodTypes()
    {
        var metaMeta = new MetaMeta();
        var meta = new Meta(metaMeta);

        CoreMetaMeta.Populate(metaMeta);
        CoreMeta.Configure(meta);

        var m = metaMeta;

        var top = meta.AddDomain(Guid.NewGuid(), "Top");
        var middle = meta.AddDomain(Guid.NewGuid(), "Middle");
        var bottom = meta.AddDomain(Guid.NewGuid(), "Bottom");

        var s123 = top.AddInterface(Guid.NewGuid(), "S123");
        var i12 = middle.AddInterface(Guid.NewGuid(), "I12");
        var i23 = middle.AddInterface(Guid.NewGuid(), "I23");
        var c1 = bottom.AddClass(Guid.NewGuid(), "C1");
        var c2 = bottom.AddClass(Guid.NewGuid(), "C2");
        var c3 = bottom.AddClass(Guid.NewGuid(), "C3");

        top.AddInheritance(i12, s123);
        top.AddInheritance(i23, s123);
        top.AddInheritance(c1, i12);
        top.AddInheritance(c2, i12, i23);
        top.AddInheritance(c3, i23);

        var s123Method = top.AddMethodType(Guid.NewGuid(), s123, "S123Method");
        var i12Method = top.AddMethodType(Guid.NewGuid(), i12, "I12Method");
        var i23Method = top.AddMethodType(Guid.NewGuid(), i23, "I23Method");
        var c1Method = top.AddMethodType(Guid.NewGuid(), c1, "C1Method");
        var c2Method = top.AddMethodType(Guid.NewGuid(), c2, "C2Method");
        var c3Method = top.AddMethodType(Guid.NewGuid(), c3, "C3Method");

        meta.Derive();

        s123Method[m.MethodTypeConcreteMethodTypes].Should().HaveCount(3);
        i12Method[m.MethodTypeConcreteMethodTypes].Should().HaveCount(2);
        i23Method[m.MethodTypeConcreteMethodTypes].Should().HaveCount(2);
        c1Method[m.MethodTypeConcreteMethodTypes].Should().ContainSingle();
        c2Method[m.MethodTypeConcreteMethodTypes].Should().ContainSingle();
        c3Method[m.MethodTypeConcreteMethodTypes].Should().ContainSingle();
    }

    [Fact]
    public void ConcreteMethodTypeMethodParts()
    {
        var metaMeta = new MetaMeta();
        var meta = new Meta(metaMeta);

        CoreMetaMeta.Populate(metaMeta);
        CoreMeta.Configure(meta);

        var m = metaMeta;

        var top = meta.AddDomain(Guid.NewGuid(), "Top");
        var middle = meta.AddDomain(Guid.NewGuid(), "Middle");
        var bottom = meta.AddDomain(Guid.NewGuid(), "Bottom");

        var s123 = top.AddInterface(Guid.NewGuid(), "S123");
        var i12 = middle.AddInterface(Guid.NewGuid(), "I12");
        var i23 = middle.AddInterface(Guid.NewGuid(), "I23");
        var c1 = bottom.AddClass(Guid.NewGuid(), "C1");
        var c2 = bottom.AddClass(Guid.NewGuid(), "C2");
        var c3 = bottom.AddClass(Guid.NewGuid(), "C3");

        top.AddInheritance(i12, s123);
        top.AddInheritance(i23, s123);
        top.AddInheritance(c1, i12);
        top.AddInheritance(c2, i12, i23);
        top.AddInheritance(c3, i23);

        var methodType = top.AddMethodType(Guid.NewGuid(), s123, "S123Method");

        Method topS123Action = (_, _) => { };
        Method middleS123Action = (_, _) => { };
        Method bottomS123Action = (_, _) => { };

        Method topI12Action = (_, _) => { };
        Method middleI12Action = (_, _) => { };
        Method bottomI12Action = (_, _) => { };

        Method topI23Action = (_, _) => { };
        Method middleI23Action = (_, _) => { };
        Method bottomI23Action = (_, _) => { };

        Method topC1Action = (_, _) => { };
        Method middleC1Action = (_, _) => { };
        Method bottomC1Action = (_, _) => { };

        Method topC2Action = (_, _) => { };
        Method middleC2Action = (_, _) => { };
        Method bottomC2Action = (_, _) => { };

        Method topC3Action = (_, _) => { };
        Method middleC3Action = (_, _) => { };
        Method bottomC3Action = (_, _) => { };

        var topS123MethodPart = methodType.AddMethodPart(top, s123, topS123Action);
        var middleS123MethodPart = methodType.AddMethodPart(middle, s123, middleS123Action);
        var bottomS123MethodPart = methodType.AddMethodPart(bottom, s123, bottomS123Action);

        var topI12MethodPart = methodType.AddMethodPart(top, i12, topI12Action);
        var middleI12MethodPart = methodType.AddMethodPart(middle, i12, middleI12Action);
        var bottomI12MethodPart = methodType.AddMethodPart(bottom, i12, bottomI12Action);

        var topI23MethodPart = methodType.AddMethodPart(top, i23, topI23Action);
        var middleI23MethodPart = methodType.AddMethodPart(middle, i23, middleI23Action);
        var bottomI23MethodPart = methodType.AddMethodPart(bottom, i23, bottomI23Action);

        var topC1MethodPart = methodType.AddMethodPart(top, c1, topC1Action);
        var middleC1MethodPart = methodType.AddMethodPart(middle, c1, middleC1Action);
        var bottomC1MethodPart = methodType.AddMethodPart(bottom, c1, bottomC1Action);

        var topC2MethodPart = methodType.AddMethodPart(top, c2, topC2Action);
        var middleC2MethodPart = methodType.AddMethodPart(middle, c2, middleC2Action);
        var bottomC2MethodPart = methodType.AddMethodPart(bottom, c2, bottomC2Action);

        var topC3MethodPart = methodType.AddMethodPart(top, c3, topC3Action);
        var middleC3MethodPart = methodType.AddMethodPart(middle, c3, middleC3Action);
        var bottomC3MethodPart = methodType.AddMethodPart(bottom, c3, bottomC3Action);

        meta.Derive();

        var concreteMethodTypes = methodType[m.MethodTypeConcreteMethodTypes]!;

        var c1ConcreteMethodType = concreteMethodTypes.First(v => v[m.ConcreteMethodTypeClass] == c1);
        var c2ConcreteMethodType = concreteMethodTypes.First(v => v[m.ConcreteMethodTypeClass] == c2);
        var c3ConcreteMethodType = concreteMethodTypes.First(v => v[m.ConcreteMethodTypeClass] == c3);

        var c1ConcreteMethodTypeMethodParts = c1ConcreteMethodType[m.ConcreteMethodTypeMethodParts]!;
        var c2ConcreteMethodTypeMethodParts = c2ConcreteMethodType[m.ConcreteMethodTypeMethodParts]!;
        var c3ConcreteMethodTypeMethodParts = c3ConcreteMethodType[m.ConcreteMethodTypeMethodParts]!;

        c1ConcreteMethodTypeMethodParts.Should().HaveCount(9);
        c2ConcreteMethodTypeMethodParts.Should().HaveCount(12);
        c3ConcreteMethodTypeMethodParts.Should().HaveCount(9);
    }

    [Fact]
    public void ConcreteMethodTypeActions()
    {
        var metaMeta = new MetaMeta();
        var meta = new Meta(metaMeta);

        CoreMetaMeta.Populate(metaMeta);
        CoreMeta.Configure(meta);

        var m = metaMeta;

        var top = meta.AddDomain(Guid.NewGuid(), "Top");
        var middle = meta.AddDomain(Guid.NewGuid(), "Middle");
        var bottom = meta.AddDomain(Guid.NewGuid(), "Bottom");

        var s123 = top.AddInterface(Guid.NewGuid(), "S123");
        var i12 = middle.AddInterface(Guid.NewGuid(), "I12");
        var i23 = middle.AddInterface(Guid.NewGuid(), "I23");
        var c1 = bottom.AddClass(Guid.NewGuid(), "C1");
        var c2 = bottom.AddClass(Guid.NewGuid(), "C2");
        var c3 = bottom.AddClass(Guid.NewGuid(), "C3");

        top.AddInheritance(i12, s123);
        top.AddInheritance(i23, s123);
        top.AddInheritance(c1, i12);
        top.AddInheritance(c2, i12, i23);
        top.AddInheritance(c3, i23);

        var methodType = top.AddMethodType(Guid.NewGuid(), s123, "S123Method");

        Method topS123Action = (_, _) => { };
        Method middleS123Action = (_, _) => { };
        Method bottomS123Action = (_, _) => { };

        Method topI12Action = (_, _) => { };
        Method middleI12Action = (_, _) => { };
        Method bottomI12Action = (_, _) => { };

        Method topI23Action = (_, _) => { };
        Method middleI23Action = (_, _) => { };
        Method bottomI23Action = (_, _) => { };

        Method topC1Action = (_, _) => { };
        Method middleC1Action = (_, _) => { };
        Method bottomC1Action = (_, _) => { };

        Method topC2Action = (_, _) => { };
        Method middleC2Action = (_, _) => { };
        Method bottomC2Action = (_, _) => { };

        Method topC3Action = (_, _) => { };
        Method middleC3Action = (_, _) => { };
        Method bottomC3Action = (_, _) => { };

        var topS123MethodPart = methodType.AddMethodPart(top, s123, topS123Action);
        var middleS123MethodPart = methodType.AddMethodPart(middle, s123, middleS123Action);
        var bottomS123MethodPart = methodType.AddMethodPart(bottom, s123, bottomS123Action);

        var topI12MethodPart = methodType.AddMethodPart(top, i12, topI12Action);
        var middleI12MethodPart = methodType.AddMethodPart(middle, i12, middleI12Action);
        var bottomI12MethodPart = methodType.AddMethodPart(bottom, i12, bottomI12Action);

        var topI23MethodPart = methodType.AddMethodPart(top, i23, topI23Action);
        var middleI23MethodPart = methodType.AddMethodPart(middle, i23, middleI23Action);
        var bottomI23MethodPart = methodType.AddMethodPart(bottom, i23, bottomI23Action);

        var topC1MethodPart = methodType.AddMethodPart(top, c1, topC1Action);
        var middleC1MethodPart = methodType.AddMethodPart(middle, c1, middleC1Action);
        var bottomC1MethodPart = methodType.AddMethodPart(bottom, c1, bottomC1Action);

        var topC2MethodPart = methodType.AddMethodPart(top, c2, topC2Action);
        var middleC2MethodPart = methodType.AddMethodPart(middle, c2, middleC2Action);
        var bottomC2MethodPart = methodType.AddMethodPart(bottom, c2, bottomC2Action);

        var topC3MethodPart = methodType.AddMethodPart(top, c3, topC3Action);
        var middleC3MethodPart = methodType.AddMethodPart(middle, c3, middleC3Action);
        var bottomC3MethodPart = methodType.AddMethodPart(bottom, c3, bottomC3Action);

        meta.Derive();

        var concreteMethodTypes = methodType[m.MethodTypeConcreteMethodTypes]!;

        var c1ConcreteMethodType = concreteMethodTypes.First(v => v[m.ConcreteMethodTypeClass] == c1);
        var c2ConcreteMethodType = concreteMethodTypes.First(v => v[m.ConcreteMethodTypeClass] == c2);
        var c3ConcreteMethodType = concreteMethodTypes.First(v => v[m.ConcreteMethodTypeClass] == c3);

        var c1ConcreteMethodTypeActions = (Method[])c1ConcreteMethodType[m.ConcreteMethodTypeActions]!;
        var c2ConcreteMethodTypeActions = (Method[])c2ConcreteMethodType[m.ConcreteMethodTypeActions]!;
        var c3ConcreteMethodTypeActions = (Method[])c3ConcreteMethodType[m.ConcreteMethodTypeActions]!;

        c1ConcreteMethodTypeActions.Should().HaveCount(9);
        c2ConcreteMethodTypeActions.Should().HaveCount(12);
        c3ConcreteMethodTypeActions.Should().HaveCount(9);

        c1ConcreteMethodTypeActions.Should().Equal([bottomS123Action, middleS123Action, topS123Action, bottomI12Action, middleI12Action, topI12Action, bottomC1Action, middleC1Action, topC1Action]);
        c2ConcreteMethodTypeActions.Should().Equal([bottomS123Action, middleS123Action, topS123Action, bottomI12Action, middleI12Action, topI12Action, bottomI23Action, middleI23Action, topI23Action, bottomC2Action, middleC2Action, topC2Action]);
        c3ConcreteMethodTypeActions.Should().Equal([bottomS123Action, middleS123Action, topS123Action, bottomI23Action, middleI23Action, topI23Action, bottomC3Action, middleC3Action, topC3Action]);
    }
}
