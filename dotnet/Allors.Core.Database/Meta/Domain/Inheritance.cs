﻿namespace Allors.Core.Database.Meta.Domain;

using Allors.Core.Meta.Domain;
using Allors.Core.Meta.Meta;

/// <summary>
/// An inheritance.
/// </summary>
public sealed class Inheritance : MetaObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Inheritance"/> class.
    /// </summary>
    public Inheritance(MetaPopulation population, MetaObjectType objectType)
        : base(population, objectType)
    {
    }
}