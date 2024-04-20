﻿namespace Allors.Core.Database.Meta
{
    using System;
    using Allors.Embedded.Domain;

    /// <summary>
    /// An association type for a composite role.
    /// </summary>
    public abstract class CompositeAssociationType : AssociationType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeAssociationType"/> class.
        /// </summary>
        protected CompositeAssociationType(Guid id, EmbeddedObject embeddedObject)
            : base(id, embeddedObject)
        {
        }
    }
}