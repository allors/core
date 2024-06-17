namespace Allors.Core.Database.Meta;

using System;
using System.Linq;
using Allors.Core.Database.MetaMeta;
using Allors.Core.Meta;

/// <summary>
/// Meta Extensions.
/// </summary>
public static class MetaExtensions
{
    /// <summary>
    /// Adds a new concreteMethodType.
    /// </summary>
    public static Domain AddDomain(this Meta @this, Guid id, string name)
    {
        var m = @this.MetaMeta;

        var domain = @this.Build<Domain>(v =>
        {
            v[m.MetaObjectId] = id;
            v[m.DomainName] = name;
        });

        domain.Add(m.DomainTypes(), domain);

        return domain;
    }

    /// <summary>
    /// Adds a new concrete method type.
    /// </summary>
    public static ConcreteMethodType AddConcreteMethodType(this Meta @this, Class @class, MethodType methodType)
    {
        var m = @this.MetaMeta;

        var concreteMethodType = @this.Build<ConcreteMethodType>(v =>
        {
            v[m.ConcreteMethodTypeClass] = @class;
            v[m.ConcreteMethodTypeMethodType] = methodType;
        });

        concreteMethodType.Add(m.DomainTypes(), concreteMethodType);

        return concreteMethodType;
    }

    /// <summary>
    /// Binary.
    /// </summary>
    public static Unit Binary(this Meta @this) => (Unit)@this.Get(CoreMeta.Binary);

    /// <summary>
    /// Binary.
    /// </summary>
    public static Unit Boolean(this Meta @this) => (Unit)@this.Get(CoreMeta.Boolean);

    /// <summary>
    /// DateTime.
    /// </summary>
    public static Unit DateTime(this Meta @this) => (Unit)@this.Get(CoreMeta.DateTime);

    /// <summary>
    /// Decimal.
    /// </summary>
    public static Unit Decimal(this Meta @this) => (Unit)@this.Get(CoreMeta.Decimal);

    /// <summary>
    /// Float.
    /// </summary>
    public static Unit Float(this Meta @this) => (Unit)@this.Get(CoreMeta.Float);

    /// <summary>
    /// Integer.
    /// </summary>
    public static Unit Integer(this Meta @this) => (Unit)@this.Get(CoreMeta.Integer);

    /// <summary>
    /// String.
    /// </summary>
    public static Unit String(this Meta @this) => (Unit)@this.Get(CoreMeta.String);

    /// <summary>
    /// Unique.
    /// </summary>
    public static Unit Unique(this Meta @this) => (Unit)@this.Get(CoreMeta.Unique);

    /// <summary>
    /// Object.
    /// </summary>
    public static Interface Object(this Meta @this) => (Interface)@this.Get(CoreMeta.Object);

    private static IMetaObject Get(this Meta @this, Guid id) => @this.Objects.First(v => ((Guid)v[@this.MetaMeta.MetaObjectId]!) == id);
}
