namespace Allors.Core.Database.Tests.Meta;

using System;
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
}
