﻿namespace Allors.Core.Database.Meta
{
    using System;
    using Allors.Embedded.Domain;

    /// <summary>
    /// A role type with a multiplicity one.
    /// </summary>
    public abstract class ToOneRoleType : CompositeRoleType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeRoleType"/> class.
        /// </summary>
        protected ToOneRoleType(Guid id, EmbeddedObject embeddedObject)
            : base(id, embeddedObject)
        {
        }
    }
}
