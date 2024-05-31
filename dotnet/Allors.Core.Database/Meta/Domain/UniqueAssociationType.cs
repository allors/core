﻿namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta.Domain;
using Allors.Core.Meta.Meta;

/// <summary>
/// An association type handle for a unit role.
/// </summary>
public sealed class UniqueAssociationType : MetaObject, IUnitAssociationType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UniqueAssociationType"/> class.
    /// </summary>
    public UniqueAssociationType(MetaPopulation population, MetaObjectType objectType)
        : base(population, objectType)
    {
    }
}