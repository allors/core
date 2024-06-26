﻿namespace Allors.Core.Database.Meta;

using Allors.Core.Meta;
using Allors.Core.MetaMeta;

/// <summary>
/// An association type handle with multiplicity many to one.
/// </summary>
public sealed class ManyToOneAssociationType : MetaObject, IManyToAssociationType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ManyToOneAssociationType"/> class.
    /// </summary>
    public ManyToOneAssociationType(Meta meta, MetaObjectType objectType)
        : base(meta, objectType)
    {
    }
}
