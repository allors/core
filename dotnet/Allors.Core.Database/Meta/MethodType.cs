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

        this.Add(m.MethodTypeMethodPart, methodPart);

        return methodPart;
    }

    /// <inheritdoc/>
    public override string ToString() => (string)this[this.MetaMeta.MethodTypeName]!;
}
