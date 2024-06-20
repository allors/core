namespace Allors.Core.Database.Meta;

using System;
using Allors.Core.Database.MetaMeta;
using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// A method type.
/// </summary>
public sealed class MethodType : MetaObject, IComposite
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MethodType"/> class.
    /// </summary>
    public MethodType(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }

    /// <summary>
    /// Adds a new concrete method type.
    /// </summary>
    public ConcreteMethodType AddConcreteMethodType(Class @class)
    {
        var m = this.MetaMeta;

        var concreteMethodType = this.Meta.Build<ConcreteMethodType>(v => v[m.ConcreteMethodTypeClass] = @class);

        this.Add(m.MethodTypeConcreteMethodTypes(), concreteMethodType);

        return concreteMethodType;
    }

    /// <summary>
    /// Adds a method definition.
    /// </summary>
    public MethodPart AddMethodPart(Domain domain, IComposite composite, Action<IObject, object> action)
    {
        var m = this.MetaMeta;

        var methodPart = this.Meta.Build<MethodPart>(v =>
        {
            v[m.MethodPartDomain] = domain;
            v[m.MethodPartComposite] = composite;
            v[m.MethodPartAction] = action;
        });

        this.Add(m.MethodTypeMethodParts, methodPart);

        return methodPart;
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        var m = this.MetaMeta;

        var composite = this[m.CompositeMethodTypes().AssociationType];
        var name = this[m.MethodTypeName];
        return $"{composite}.{name}";
    }
}
